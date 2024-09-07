namespace Raw2PlateFuelPlusNetcore.Models
{
  public partial class Order
  {
    public required int OrderId { get; set; }
    public required string Receiver { get; set; }
    public required string Contact { get; set; }
    public required string Address { get; set; }
    public required double TotalPrice { get; set; }
    public required string PaidWith { get; set; }
    public required string Status { get; set; }
    public required string Date { get; set; }
    public required string OrderTime { get; set; }
    public required string DeliveredTime { get; set; }
    public required string Driver { get; set; }
    public int? UserId { get; set; }
  }
}
