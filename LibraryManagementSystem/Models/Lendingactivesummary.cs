using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class Lendingactivesummary
{
    public int? MemberId { get; set; }

    public string? Genre { get; set; }

    public long? GenreCount { get; set; }

    public decimal? Total { get; set; }
}
