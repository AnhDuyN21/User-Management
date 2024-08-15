using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Account : BaseEntity
{

    public string? Username { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Address { get; set; }

    public string? Phonenumber { get; set; }

    public int RoleId { get; set; }

    public string? ConfirmationToken { get; set; }

    public bool IsConfirmed { get; set; }


    public virtual Role Role { get; set; } = null!;
}
