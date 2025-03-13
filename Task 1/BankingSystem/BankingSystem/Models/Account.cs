using System;
using System.Collections.Generic;

namespace BankingSystem.Models;

public partial class Account
{
    public long Id { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Email { get; set; }

    public virtual Transaction? Transaction { get; set; }
}
