using System;
using System.Collections.Generic;

namespace PaySpaceTaxCalculatorWebAPI.Data.Entities;

public partial class TaxRate
{
    public Guid TaxRateId { get; set; }

    public decimal TaxRateValue { get; set; }

    public bool IsPercentage { get; set; }

    public virtual ICollection<IncomeRangeTaxRate> IncomeRangeTaxRates { get; set; } = new List<IncomeRangeTaxRate>();

    public virtual ICollection<TaxCalculationResult> TaxCalculationResults { get; set; } = new List<TaxCalculationResult>();
}
