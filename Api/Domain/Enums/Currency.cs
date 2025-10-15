using System.Text.Json.Serialization;

namespace Api.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Currency
{
    PLN,
    USD,
    EUR,
    GBP,
    CHF
}
