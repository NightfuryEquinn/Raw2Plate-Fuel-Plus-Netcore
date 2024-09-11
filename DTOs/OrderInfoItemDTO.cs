namespace Raw2PlateFuelPlusNetcore.DTOs
{
  public class OrderInfoItemDTO
  {
    public required int ItemId;
    public required string ItemName;
    public required int Quantity;
    public required double Price;
    public int? OrderId;
  }
}
