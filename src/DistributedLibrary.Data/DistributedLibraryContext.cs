using System;
using System.Collections.Generic;
using DistributedLibrary.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DistributedLibrary.Data;

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

    public virtual DbSet<BookTagEntity> BookTags { get; set; }

    public virtual DbSet<LoanEntity> Loans { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BookEntity>(entity =>
        {
            entity.ToTable("Book");

            entity.HasIndex(e => e.Isbn, "IX_Book").IsUnique();

            entity.Property(e => e.BookId);
            entity.HasKey(x => x.BookId);

            entity.Property(e => e.Author)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Isbn)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("ISBN");
            entity.Property(e => e.PublicationDate).HasColumnType("date");
            entity.Property(e => e.Publisher)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsFixedLength();

            entity.HasOne(d => d.Contributor).WithMany(p => p.BookContributors)
                .HasForeignKey(d => d.ContributorId)
                .HasConstraintName("FK_Book_User");

            entity.HasOne(d => d.Holder).WithMany(p => p.BookHolders)
                .HasForeignKey(d => d.HolderId)
                .HasConstraintName("FK_Book_User1");
        });

        modelBuilder.Entity<BookTagEntity>(entity =>
        {
            entity.ToTable("BookTag");

            entity.Property(e => e.BookTagId);
            entity.HasKey(x => x.BookTagId);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsFixedLength();

            entity.HasOne(d => d.Book).WithMany(p => p.BookTags)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK_BookTag_Book");
        });

        modelBuilder.Entity<LoanEntity>(entity =>
        {
            entity.ToTable("Loan");

            entity.Property(e => e.LoanId);
            entity.HasKey(x => x.LoanId);

            entity.Property(e => e.DateFrom).HasColumnType("date");
            entity.Property(e => e.DateTo)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.BookEntity).WithMany(p => p.Loans)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Loan_Book");

            entity.HasOne(d => d.User).WithMany(p => p.Loans)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Loan_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
