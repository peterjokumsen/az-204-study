namespace SimpleApi.Models;

public record ManufacturingStockItem
{
    public Guid Id { get; init; }
    public string PartNumber { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public int QuantityAvailable { get; init; }
}
