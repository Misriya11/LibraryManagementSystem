using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models;

public partial class Member
{
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }
}
