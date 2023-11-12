using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using static PaySpaceTaxCalculatorWebAPI.Data.StaticClass;

namespace PaySpaceTaxCalculatorWebAPI.Data.Entities;

public partial class PaySpaceTaxCalculatorContext : DbContext
{
    public PaySpaceTaxCalculatorContext()
    {
    }

    public PaySpaceTaxCalculatorContext(DbContextOptions<PaySpaceTaxCalculatorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public virtual DbSet<IncomeRange> IncomeRanges { get; set; }

    public virtual DbSet<IncomeRangeTaxRate> IncomeRangeTaxRates { get; set; }

    public virtual DbSet<PostalCode> PostalCodes { get; set; }

    public virtual DbSet<PostalCodeTaxCalculationType> PostalCodeTaxCalculationTypes { get; set; }

    public virtual DbSet<TaxCalculationResult> TaxCalculationResults { get; set; }

    public virtual DbSet<TaxCalculationType> TaxCalculationTypes { get; set; }

    public virtual DbSet<TaxCalculationTypeIncomeRange> TaxCalculationTypeIncomeRanges { get; set; }

    public virtual DbSet<TaxRate> TaxRates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(DatabaseHelper.ConnectionString);
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("ApplicationUser");

            entity.Property(e => e.ApplicationUserId).ValueGeneratedNever();
            entity.Property(e => e.AccessToken)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.AccessTokenExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<IncomeRange>(entity =>
        {
            entity.ToTable("IncomeRange");

            entity.Property(e => e.IncomeRangeId).ValueGeneratedNever();
            entity.Property(e => e.IncomeRangeFrom).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IncomeRangeTo).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<IncomeRangeTaxRate>(entity =>
        {
            entity.ToTable("IncomeRangeTaxRate");

            entity.Property(e => e.IncomeRangeTaxRateId).ValueGeneratedNever();

            entity.HasOne(d => d.IncomeRange).WithMany(p => p.IncomeRangeTaxRates)
                .HasForeignKey(d => d.IncomeRangeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IncomeRangeTaxRate_IncomeRange");

            entity.HasOne(d => d.TaxRate).WithMany(p => p.IncomeRangeTaxRates)
                .HasForeignKey(d => d.TaxRateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IncomeRangeTaxRate_TaxRate");
        });

        modelBuilder.Entity<PostalCode>(entity =>
        {
            entity.ToTable("PostalCode");

            entity.Property(e => e.PostalCodeId).ValueGeneratedNever();
            entity.Property(e => e.PostalCodeValue)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PostalCodeTaxCalculationType>(entity =>
        {
            entity.ToTable("PostalCodeTaxCalculationType");

            entity.Property(e => e.PostalCodeTaxCalculationTypeId).ValueGeneratedNever();

            entity.HasOne(d => d.PostalCode).WithMany(p => p.PostalCodeTaxCalculationTypes)
                .HasForeignKey(d => d.PostalCodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostalCodeTaxCalculationType_PostalCode");

            entity.HasOne(d => d.TaxCalculationType).WithMany(p => p.PostalCodeTaxCalculationTypes)
                .HasForeignKey(d => d.TaxCalculationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostalCodeTaxCalculationType_TaxCalculationType");
        });

        modelBuilder.Entity<TaxCalculationResult>(entity =>
        {
            entity.ToTable("TaxCalculationResult");

            entity.Property(e => e.TaxCalculationResultId).ValueGeneratedNever();
            entity.Property(e => e.AnnualIncome).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.IncomeRangeFrom).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IncomeRangeTo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PostalCodeValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TaxCalculationTypeValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TaxRateValue).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ApplicationUser).WithMany(p => p.TaxCalculationResults)
                .HasForeignKey(d => d.ApplicationUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaxCalculationResult_ApplicationUser");

            entity.HasOne(d => d.IncomeRange).WithMany(p => p.TaxCalculationResults)
                .HasForeignKey(d => d.IncomeRangeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaxCalculationResult_IncomeRange");

            entity.HasOne(d => d.PostalCode).WithMany(p => p.TaxCalculationResults)
                .HasForeignKey(d => d.PostalCodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaxCalculationResult_PostalCode");

            entity.HasOne(d => d.TaxCalculationType).WithMany(p => p.TaxCalculationResults)
                .HasForeignKey(d => d.TaxCalculationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaxCalculationResult_TaxCalculationType");

            entity.HasOne(d => d.TaxRate).WithMany(p => p.TaxCalculationResults)
                .HasForeignKey(d => d.TaxRateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaxCalculationResult_TaxRate");
        });

        modelBuilder.Entity<TaxCalculationType>(entity =>
        {
            entity.ToTable("TaxCalculationType");

            entity.Property(e => e.TaxCalculationTypeId).ValueGeneratedNever();
            entity.Property(e => e.TaxCalculationTypeValue)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TaxCalculationTypeIncomeRange>(entity =>
        {
            entity.ToTable("TaxCalculationTypeIncomeRange");

            entity.Property(e => e.TaxCalculationTypeIncomeRangeId).ValueGeneratedNever();

            entity.HasOne(d => d.IncomeRange).WithMany(p => p.TaxCalculationTypeIncomeRanges)
                .HasForeignKey(d => d.IncomeRangeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaxCalculationTypeIncomeRange_IncomeRange");

            entity.HasOne(d => d.TaxCalculationType).WithMany(p => p.TaxCalculationTypeIncomeRanges)
                .HasForeignKey(d => d.TaxCalculationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaxCalculationTypeIncomeRange_TaxCalculationType");
        });

        modelBuilder.Entity<TaxRate>(entity =>
        {
            entity.ToTable("TaxRate");

            entity.Property(e => e.TaxRateId).ValueGeneratedNever();
            entity.Property(e => e.TaxRateValue).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
