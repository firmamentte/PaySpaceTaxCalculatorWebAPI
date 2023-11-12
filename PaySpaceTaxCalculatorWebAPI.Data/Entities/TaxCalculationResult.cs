using System;
using System.Collections.Generic;

namespace PaySpaceTaxCalculatorWebAPI.Data.Entities;

public partial class TaxCalculationResult
{
    public Guid TaxCalculationResultId { get; set; }

    public Guid ApplicationUserId { get; set; }

    public Guid PostalCodeId { get; set; }

    public Guid TaxCalculationTypeId { get; set; }

    public Guid TaxRateId { get; set; }

    public Guid IncomeRangeId { get; set; }

    public decimal AnnualIncome { get; set; }

    public string PostalCodeValue { get; set; } = null!;

    public string TaxCalculationTypeValue { get; set; } = null!;

    public decimal TaxRateValue { get; set; }

    public bool IsPercentage { get; set; }

    public decimal IncomeRangeFrom { get; set; }

    public decimal IncomeRangeTo { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual ApplicationUser ApplicationUser { get; set; } = null!;

    public virtual IncomeRange IncomeRange { get; set; } = null!;

    public virtual PostalCode PostalCode { get; set; } = null!;

    public virtual TaxCalculationType TaxCalculationType { get; set; } = null!;

    public virtual TaxRate TaxRate { get; set; } = null!;
}
