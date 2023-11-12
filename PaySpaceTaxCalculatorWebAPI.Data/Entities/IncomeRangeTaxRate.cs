using System;
using System.Collections.Generic;

namespace PaySpaceTaxCalculatorWebAPI.Data.Entities;

public partial class IncomeRangeTaxRate
{
    public Guid IncomeRangeTaxRateId { get; set; }

    public Guid IncomeRangeId { get; set; }

    public Guid TaxRateId { get; set; }

    public virtual IncomeRange IncomeRange { get; set; } = null!;

    public virtual TaxRate TaxRate { get; set; } = null!;
}
