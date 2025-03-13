﻿using System;
using System.Collections.Generic;

namespace BankingSystem.Models;

public partial class Transaction
{
    public long Id { get; set; }

    public long? Accountid { get; set; }

    public string? Transactiontype { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? Timestamp { get; set; }

    public string? Status { get; set; }

    public string? Details { get; set; }

    public virtual Account IdNavigation { get; set; } = null!;
}
