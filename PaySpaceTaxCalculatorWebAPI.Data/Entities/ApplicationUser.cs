using System;
using System.Collections.Generic;

namespace PaySpaceTaxCalculatorWebAPI.Data.Entities;

public partial class ApplicationUser
{
    public Guid ApplicationUserId { get; set; }

    public string Username { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string? AccessToken { get; set; }

    public DateTime? AccessTokenExpiryDate { get; set; }

    public virtual ICollection<TaxCalculationResult> TaxCalculationResults { get; set; } = new List<TaxCalculationResult>();
}
