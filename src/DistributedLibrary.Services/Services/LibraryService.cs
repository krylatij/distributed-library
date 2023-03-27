using AutoMapper;
using DistributedLibrary.Data.Entities;
using DistributedLibrary.Data.Interfaces;
using DistributedLibrary.Data.Repositories;
using DistributedLibrary.Services.Dto;
using DistributedLibrary.Shared.Configuration;
using DistributedLibrary.Shared.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DistributedLibrary.Services.Services;

public class LibraryService
{
    private readonly ILibraryRepository _libraryRepository;
    private readonly NotificationService _notificationService;
    private readonly IOptions<ApplicationConfiguration> _configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LibraryService(ILogger<LibraryService> logger,
        ILibraryRepository libraryRepository,
        NotificationService notificationService,
        IOptions<ApplicationConfiguration> configuration,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _libraryRepository = libraryRepository;
        _notificationService = notificationService;
        _configuration = configuration;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseDto<BookDto?>> UpsertBookAsync(BookDto bookDto, string userId)
    {
        var entity = _mapper.Map<BookEntity>(bookDto);
        entity.ContributorId = userId;

        if (!string.IsNullOrEmpty(bookDto.Isbn))
        {
            var oldBook = await _libraryRepository.GetAsync<BookEntity>(x => x.Isbn == bookDto.Isbn && x.BookId != bookDto.BookId);

            if (oldBook != null)
            {
                return ResponseDto.ValidationFail<BookDto>($"Book with ISBN {bookDto.Isbn} already exists.");
            }
        }

        if (bookDto.BookId != default)
        {
            entity = _libraryRepository.Update(entity);
        }
        else
        {
            entity = _libraryRepository.Add(entity);
        }

        await _unitOfWork.CommitAsync(userId);

        var dto = _mapper.Map<BookDto>(entity);

        return ResponseDto.Ok(dto);
    }

    public async Task<ResponseDto<BookDto?>> GetBookAsync(int bookId)
    {
        var entity = await _libraryRepository.GetAsync<BookEntity>(x => x.BookId == bookId);

        var dto = _mapper.Map<BookDto>(entity);

        return ResponseDto.Ok(dto);
    }

    public async Task<BookDto[]> GetBooksAsync(string? userId)
    {
        var entities = await _libraryRepository.GetMany<BookEntity>(x => userId == null || x.HolderId == userId).ToArrayAsync();
        
        var dtos = _mapper.Map<BookDto[]>(entities);

        return dtos;
    }

    public async Task<ResponseDto> DeleteBookAsync(int bookId, string userId)
    {
        var book = await _libraryRepository.GetAsync<BookEntity, ICollection<LoanEntity>, ICollection<ReservationEntity>>(
            x => x.BookId == bookId, x => x.Loans, x => x.Reservations, true);

        if (book == null)
        {
            return ResponseDto.ValidationFail("Book is not found.");
        }

        _libraryRepository.DeleteMany(book.Loans);
        _libraryRepository.DeleteMany(book.Reservations);

        _libraryRepository.Delete(book);

        await _unitOfWork.CommitAsync(userId);

        return ResponseDto.Ok();
    }

    public async Task<UserDto?> GetUserAsync(string userId)
    {
        var entity = await _libraryRepository.GetAsync<User>(x => x.Id == userId);

        var dto = _mapper.Map<UserDto>(entity);

        return dto;
    }

    public async Task<UserDto[]> GetUsersAsync()
    {
        var entities = await _libraryRepository.GetMany<User>().ToArrayAsync();

        var dtos = _mapper.Map<UserDto[]>(entities);

        return dtos;
    }

    public async Task<ResponseDto<LoanDto?>> LoanBookAsync(int bookId, string userId)
    {
        var existingLoan = await _libraryRepository.GetMany<LoanEntity>(x => x.BookId == bookId && x.DateTo == null).FirstOrDefaultAsync();

        if (existingLoan != null)
        {
            return ResponseDto.ValidationFail<LoanDto>($"This book is already assigned to {existingLoan.User.UserName}. Ask user to return it first.");
        }

        var entity = new LoanEntity
        {
            BookId = bookId,
            UserId = userId,
            DateFrom = DateTime.Now
        };

        var book = await _libraryRepository.GetAsync<BookEntity>(x => x.BookId == bookId);
        if (book == null)
        {
            return ResponseDto.ValidationFail<LoanDto>($"There is no book with id {bookId}");
        }

        book.HolderId = userId;

        _libraryRepository.Update(book);
        _libraryRepository.Update(entity);

        await _unitOfWork.CommitAsync(userId);

        var dto = _mapper.Map<LoanDto>(entity);

        return ResponseDto.Ok(dto);
    }
    public async Task<ResponseDto<LoanDto?>> ReturnBookAsync(int bookId, string userId)
    {
        var existingLoan = await _libraryRepository.GetMany<LoanEntity>(x => x.BookId == bookId && x.DateTo == null).Include(x => x.Book).FirstOrDefaultAsync();
        
        if (existingLoan == null)
        {
            return ResponseDto.Ok<LoanDto>(default);
        }

        if (existingLoan.UserId != userId)
        {
            return ResponseDto.ValidationFail<LoanDto>($"This book is already assigned to {existingLoan.User.UserName}. Ask user to return it first.");
        }

        var book = existingLoan.Book;
        book.HolderId = null;

        existingLoan.DateTo = DateTime.Now;

        _libraryRepository.Update(book);
        _libraryRepository.Update(existingLoan);

        await _unitOfWork.CommitAsync(userId);

        var dto = _mapper.Map<LoanDto>(existingLoan);

        return ResponseDto.Ok(dto);
    }

    public async Task<ResponseDto<ReservationDto?>> AddReservationAsync(ReservationDto reservation, string userId, string reservationsUrl)
    {
        if (reservation.ReservationDate < DateTime.Now)
        {
            return ResponseDto.ValidationFail<ReservationDto>("Reservation data must be greater than today.");
        }

        var book = await _libraryRepository.GetAsync<BookEntity>(x =>
            x.HolderId == userId && x.BookId == reservation.BookId);
          
        if (book == null)
        {
            return ResponseDto.ValidationFail<ReservationDto>("Book should be assigned to a reservation initiator.");
        }

        var entity = _mapper.Map<ReservationEntity>(reservation);
        
        entity = _libraryRepository.Add(entity);

        await _unitOfWork.CommitAsync(userId);

        var user = await _libraryRepository.GetMany<User>(x => x.Id == userId).SingleAsync();

        var url = $"{_configuration.Value.Host}{reservationsUrl}";
        await _notificationService.SendReservationMailAsync(user.Email, url);

        var dto = _mapper.Map<ReservationDto>(entity);

        return ResponseDto.Ok(dto);
    }

    public async Task<ResponseDto> AcceptReservationAsync(int reservationId, string userId)
    {
        var reservation = await _libraryRepository.GetAsync<ReservationEntity, BookEntity>(
            x => x.ReservationId == reservationId && x.ReservationDate > DateTime.Now, x => x.Book);

        if (reservation == null)
        {
            return ResponseDto.ValidationFail("Reservation doesn't exist or is outdated.");
        }

        if (reservation.UserId != userId)
        {
            return ResponseDto.ValidationFail("Reservation can be accepted only by assigned user.");
        }

        var book = reservation.Book;

        if (book.HolderId != reservation.CreatedBy)
        {
            return ResponseDto.ValidationFail("Reservation was already accepted by another user.");
        }

        book.HolderId = userId;

        var otherReservations = _libraryRepository.GetMany<ReservationEntity>(
            x => x.ReservationId != reservationId && x.BookId == reservation.BookId);

        var oldLoan = await _libraryRepository
            .GetMany<LoanEntity>(x =>
                x.BookId == reservation.BookId && x.UserId == reservation.UserId && x.DateTo == null)
            .SingleOrDefaultAsync();

        if (oldLoan == null)
        {
            throw new InvalidOperationException($"There is no loan for user '{userId}', reservation {reservationId} and book {book}.");
        }
        
        oldLoan.DateTo = DateTime.Now;

        var newLoan = new LoanEntity {BookId = book.BookId, UserId = userId, DateFrom = DateTime.Now};

        _libraryRepository.Add(newLoan);
        _libraryRepository.Update(book);
        _libraryRepository.Delete(reservation);
        _libraryRepository.DeleteMany(otherReservations);

        await _unitOfWork.CommitAsync(userId);

        return ResponseDto.Ok();
    }

    public async Task<ResponseDto> DeclineReservationAsync(int reservationId, string userId)
    {
        var reservation = await _libraryRepository.GetAsync<ReservationEntity>(x => x.ReservationId == reservationId && x.ReservationDate > DateTime.Now);
       
        if (reservation == null)
        {
            return ResponseDto.ValidationFail("Reservation doesn't exist or is outdated.");
        }

        if (reservation.UserId != userId)
        {
            return ResponseDto.ValidationFail("Reservation can be declined only by assigned user.");
        }

        _libraryRepository.Delete(reservation);

        await _unitOfWork.CommitAsync(userId);

        return ResponseDto.Ok();
    }

    public async Task<ResponseDto> DeleteReservationAsync(int reservationId, string userId)
    {
        var reservation = await _libraryRepository.GetAsync<ReservationEntity>(x =>
            x.ReservationId == reservationId && x.ReservationDate > DateTime.Now);
            
        if (reservation == null)
        {
            return ResponseDto.ValidationFail("Reservation doesn't exist or is outdated.");
        }

        if (reservation.CreatedBy != userId)
        {
            return ResponseDto.ValidationFail("Reservation can be deleted only by author.");
        }

        _libraryRepository.Delete(reservation);

        await _unitOfWork.CommitAsync(userId);

        return ResponseDto.Ok();
    }
}