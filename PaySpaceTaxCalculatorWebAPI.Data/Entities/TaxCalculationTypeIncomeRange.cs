using System;
using System.Collections.Generic;

namespace PaySpaceTaxCalculatorWebAPI.Data.Entities;

public partial class TaxCalculationTypeIncomeRange
{
    public Guid TaxCalculationTypeIncomeRangeId { get; set; }

    public Guid TaxCalculationTypeId { get; set; }

    public Guid IncomeRangeId { get; set; }

    public virtual IncomeRange IncomeRange { get; set; } = null!;

    public virtual TaxCalculationType TaxCalculationType { get; set; } = null!;
}
