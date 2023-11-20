using System;
using System.Collections.Generic;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Models;

public partial class LmsContext : DbContext
{
    

    public LmsContext(DbContextOptions<LmsContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<BorrowedBook> BorrowedBooks { get; set; }
    public DbSet<Waitlist> Waitlists { get; set; }
    public DbSet<Lendingactivesummary> Lendingactivesummarys { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity
               // .HasNoKey()
                .ToTable("books");

            entity.HasIndex(e => e.Id, "books_id_key").IsUnique();

            entity.Property(e => e.Author)
                .HasMaxLength(50)
                .HasColumnName("author");
            entity.Property(e => e.AvailableCopies).HasColumnName("available_copies");
            entity.Property(e => e.Barcode)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("barcode");
            entity.Property(e => e.Genre)
                .HasMaxLength(30)
                .HasColumnName("genre");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Price)
                .HasPrecision(5, 2)
                .HasColumnName("price");
            entity.Property(e => e.PublicationDate).HasColumnName("publication_date");
            entity.Property(e => e.Publisher)
                .HasMaxLength(50)
                .HasColumnName("publisher");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ReviewCount).HasColumnName("review_count");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");
            entity.Property(e => e.TotalCopies).HasColumnName("total_copies");
        });
        modelBuilder.Entity<Member>(entity =>
        {
            entity
                //.HasNoKey()
                .ToTable("members");

            entity.HasIndex(e => e.Email, "members_email_key").IsUnique();

            entity.HasIndex(e => e.Id, "members_id_key").IsUnique();

            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .HasColumnName("first_name");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .HasColumnName("last_name");
        });
        modelBuilder.Entity<BorrowedBook>(entity =>
        {
            entity
               // .HasNoKey()
                .ToTable("borrowed_books");

            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.BorrowDate).HasColumnName("borrow_date");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.ReturnDate).HasColumnName("return_date");
        });
         modelBuilder.Entity<Waitlist>(entity =>
        {
            entity
               // .HasNoKey()
                .ToTable("waitlists");

            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.RequestedTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("requested_time");
        });
        modelBuilder.Entity<Lendingactivesummary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("lendingactivesummary");

            entity.Property(e => e.Genre)
                .HasMaxLength(30)
                .HasColumnName("genre");
            entity.Property(e => e.GenreCount).HasColumnName("genre_count");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Total).HasColumnName("total");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
