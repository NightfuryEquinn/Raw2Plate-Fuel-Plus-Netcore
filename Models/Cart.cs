namespace Raw2PlateFuelPlusNetcore.Models
{
  public partial class Cart
  {
    public required int CartId { get; set; }
    public required int Quantity { get; set; }
    public int? UserId { get; set; }
    public int? ItemId { get; set; }
  }
}
