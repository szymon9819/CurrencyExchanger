using Api.Domain.Entities;
using Api.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Rate> Rates => Set<Rate>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var currencyConverter = new EnumToStringConverter<Currency>();
        var providerConverter = new EnumToNumberConverter<CurrencyProvider, byte>();

        modelBuilder.Entity<Rate>(b =>
        {
            b.ToTable("Rates");

            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();

            b.Property(x => x.Currency)
                .HasConversion(currencyConverter)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsRequired();

            b.Property(x => x.CurrencyProvider)
                .HasConversion(providerConverter)
                .HasColumnType("tinyint")
                .IsRequired();

            b.Property(x => x.Date).IsRequired();

            b.Property(x => x.Mid)
                .HasColumnType("decimal(18,6)")
                .IsRequired();

            b.Property(x => x.Bid)
                .HasColumnType("decimal(18,6)")
                .IsRequired(false);

            b.Property(x => x.Ask)
                .HasColumnType("decimal(18,6)")
                .IsRequired(false);
            
            b.Property(x => x.CreatedAt)
                .HasColumnType("datetime(6)")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .ValueGeneratedOnAdd();

            b.Property(x => x.UpdatedAt)
                .HasColumnType("datetime(6)")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .ValueGeneratedOnAddOrUpdate();

            b.HasIndex(x => new { x.Currency, x.Date, x.CurrencyProvider }).IsUnique();
            b.HasIndex(x => x.Currency);
            b.HasIndex(x => x.Date);
            b.HasIndex(x => x.CurrencyProvider);
        });
    }
}
