using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models;

//[Table("books")]s
public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Author { get; set; }

    public string? Publisher { get; set; }

    public DateOnly? PublicationDate { get; set; }

    public string? Genre { get; set; }

    public decimal Price { get; set; }

    public string Barcode { get; set; } = null!;

    public short? Rating { get; set; }

    public int? ReviewCount { get; set; }

    public int TotalCopies { get; set; }

    public int AvailableCopies { get; set; }
}
