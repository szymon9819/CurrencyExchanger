using Api.Domain.Enums;

namespace Api.Domain.Entities;

public class Rate
{
    public int Id { get; init; }
    
    public required Currency Currency { get; set; }

    public required CurrencyProvider CurrencyProvider { get; set; }

    public required DateOnly Date { get; set; }

    public required decimal Mid { get; set; }

    public decimal? Bid { get; set; }

    public decimal? Ask { get; set; }
    
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
