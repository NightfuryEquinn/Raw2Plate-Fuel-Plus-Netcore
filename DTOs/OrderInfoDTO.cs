using Raw2PlateFuelPlusNetcore.Models;

namespace Raw2PlateFuelPlusNetcore.DTOs
{
  public class OrderInfoDTO
  {
    // Order properties
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

    // Store properties
    public int? StoreId { get; set; }
    public required string StoreName { get; set; }
    public required string StoreImage { get; set; }
  }
}
