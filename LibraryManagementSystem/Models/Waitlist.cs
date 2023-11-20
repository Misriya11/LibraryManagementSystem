using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class Waitlist
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public int BookId { get; set; }

    public DateTime RequestedTime { get; set; }
}
