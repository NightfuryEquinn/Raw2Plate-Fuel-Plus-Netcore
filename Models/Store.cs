namespace Raw2PlateFuelPlusNetcore.Models
{
  public partial class Store
  {
    public required int StoreId { get; set; }
    public required string Name { get; set; }
    public required string Image { get; set; }
    public required double Distance { get; set; }
  }
}
