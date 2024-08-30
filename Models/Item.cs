namespace Raw2PlateFuelPlusNetcore.Models
{
  public partial class Item
  {
    public required int ItemId { get; set; }
    public required string Name { get; set; }
    public required string Category { get; set; }
    public required string Image { get; set; }
    public required int Price { get; set; }
    public required string Description { get; set; }
    public int? StoreId { get; set; }
  }
}
