using System;
using System.Collections.Generic;

namespace PaySpaceTaxCalculatorWebAPI.Data.Entities;

public partial class TaxCalculationType
{
    public Guid TaxCalculationTypeId { get; set; }

    public string TaxCalculationTypeValue { get; set; } = null!;

    public virtual ICollection<PostalCodeTaxCalculationType> PostalCodeTaxCalculationTypes { get; set; } = new List<PostalCodeTaxCalculationType>();

    public virtual ICollection<TaxCalculationResult> TaxCalculationResults { get; set; } = new List<TaxCalculationResult>();

    public virtual ICollection<TaxCalculationTypeIncomeRange> TaxCalculationTypeIncomeRanges { get; set; } = new List<TaxCalculationTypeIncomeRange>();
}
