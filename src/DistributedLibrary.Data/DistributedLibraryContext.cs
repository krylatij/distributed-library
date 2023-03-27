using System.Diagnostics.CodeAnalysis;
using DistributedLibrary.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DistributedLibrary.Data;

[ExcludeFromCodeCoverage]
public partial class DistributedLibraryContext : IdentityDbContext<User>
{
    public DistributedLibraryContext()
    {
    }

    public DistributedLibraryContext(DbContextOptions<DistributedLibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BookEntity> Books { get; set; }

    public virtual DbSet<LoanEntity> Loans { get; set; }
    public virtual DbSet<ReservationEntity> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BookEntity>(entity =>
        {
            entity.ToTable("Book");

            entity.Property(e => e.BookId);
            entity.HasKey(x => x.BookId);

            entity.HasIndex(e => e.Isbn, "IX_Book")
                .IsUnique()
                .HasFilter("([ISBN] IS NOT NULL)");

            entity.HasIndex(e => e.ContributorId, "IX_Book_ContributorId");

            entity.HasIndex(e => e.HolderId, "IX_Book_HolderId");

            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.Genres).HasMaxLength(500);
            entity.Property(e => e.Isbn)
                .HasMaxLength(100)
                .HasColumnName("ISBN");
            entity.Property(e => e.PublicationDate).HasColumnType("date");
            entity.Property(e => e.Publisher).HasMaxLength(100);
            entity.Property(e => e.Tags).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(300);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);

            entity.HasOne(d => d.Contributor).WithMany(p => p.BookContributors)
                .HasForeignKey(d => d.ContributorId)
                .HasConstraintName("FK_Book_User");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BookCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Book_AspNetUsers");

            entity.HasOne(d => d.Holder).WithMany(p => p.BookHolders)
                .HasForeignKey(d => d.HolderId)
                .HasConstraintName("FK_Book_User1");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.BookUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Book_AspNetUsers1");

        });

        modelBuilder.Entity<LoanEntity>(entity =>
        {
            entity.ToTable("Loan");

            entity.Property(e => e.LoanId);
            entity.HasKey(x => x.LoanId);

            entity.HasIndex(e => e.BookId, "IX_Loan_BookId");

            entity.HasIndex(e => e.UserId, "IX_Loan_UserId");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateTo).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);

            entity.HasOne(d => d.Book).WithMany(p => p.Loans)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Loan_Book");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.LoanCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Loan_AspNetUsers");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.LoanUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Loan_AspNetUsers1");

            entity.HasOne(d => d.User).WithMany(p => p.LoanUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Loan_User");

        });

        modelBuilder.Entity<ReservationEntity>(entity =>
        {
            entity.ToTable("Reservation");

            entity.Property(e => e.ReservationId);
            entity.HasKey(x => x.ReservationId);

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.ReservationDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.Book).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_Book");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ReservationCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Reservation_AspNetUsers1");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ReservationUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Reservation_AspNetUsers2");

            entity.HasOne(d => d.User).WithMany(p => p.ReservationUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_AspNetUsers");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
