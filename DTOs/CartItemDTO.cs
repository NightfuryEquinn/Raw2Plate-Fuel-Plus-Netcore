namespace Raw2PlateFuelPlusNetcore.DTOs
{
  public class CartItemDTO
  {
    // Cart properties
    public required int CartId { get; set; }
    public required int Quantity { get; set; }
    public int? UserId { get; set; }
    public int? ItemId { get; set; }

    // Item properties
    public required string Name { get; set; }
    public required string Category { get; set; }
    public required string Image { get; set; }
    public required double Price { get; set; }
    public required string Description { get; set; }
    public int? StoreId { get; set; }
  }
}
