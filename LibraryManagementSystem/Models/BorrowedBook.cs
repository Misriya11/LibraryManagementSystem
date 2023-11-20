using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class BorrowedBook
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public int BookId { get; set; }

    public DateOnly BorrowDate { get; set; }

    public DateOnly DueDate { get; set; }

    public DateOnly? ReturnDate { get; set; }
}
