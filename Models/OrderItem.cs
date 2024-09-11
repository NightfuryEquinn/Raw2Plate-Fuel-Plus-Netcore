namespace Raw2PlateFuelPlusNetcore.Models
{
  public partial class OrderItem
  {
    public required int OrderItemId { get; set; }
    public required int Quantity { get; set; }
    public int? ItemId { get; set; }
    public int? OrderId { get; set; }
  }
}
