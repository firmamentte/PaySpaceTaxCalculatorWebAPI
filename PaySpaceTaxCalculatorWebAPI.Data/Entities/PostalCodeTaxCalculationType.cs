using System;
using System.Collections.Generic;

namespace PaySpaceTaxCalculatorWebAPI.Data.Entities;

public partial class PostalCodeTaxCalculationType
{
    public Guid PostalCodeTaxCalculationTypeId { get; set; }

    public Guid PostalCodeId { get; set; }

    public Guid TaxCalculationTypeId { get; set; }

    public virtual PostalCode PostalCode { get; set; } = null!;

    public virtual TaxCalculationType TaxCalculationType { get; set; } = null!;
}
