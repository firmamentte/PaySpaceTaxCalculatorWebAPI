using System;
using System.Collections.Generic;

namespace PaySpaceTaxCalculatorWebAPI.Data.Entities;

public partial class PostalCode
{
    public Guid PostalCodeId { get; set; }

    public string PostalCodeValue { get; set; } = null!;

    public virtual ICollection<PostalCodeTaxCalculationType> PostalCodeTaxCalculationTypes { get; set; } = new List<PostalCodeTaxCalculationType>();

    public virtual ICollection<TaxCalculationResult> TaxCalculationResults { get; set; } = new List<TaxCalculationResult>();
}
