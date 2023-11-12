using System;
using System.Collections.Generic;

namespace PaySpaceTaxCalculatorWebAPI.Data.Entities;

public partial class IncomeRange
{
    public Guid IncomeRangeId { get; set; }

    public decimal IncomeRangeFrom { get; set; }

    public decimal IncomeRangeTo { get; set; }

    public virtual ICollection<IncomeRangeTaxRate> IncomeRangeTaxRates { get; set; } = new List<IncomeRangeTaxRate>();

    public virtual ICollection<TaxCalculationResult> TaxCalculationResults { get; set; } = new List<TaxCalculationResult>();

    public virtual ICollection<TaxCalculationTypeIncomeRange> TaxCalculationTypeIncomeRanges { get; set; } = new List<TaxCalculationTypeIncomeRange>();
}
