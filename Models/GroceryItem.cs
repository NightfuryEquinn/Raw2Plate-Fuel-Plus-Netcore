namespace Raw2PlateFuelPlusNetcore.Models
{
  public partial class GroceryItem
  {
    public required int GroceryItemId { get; set; }
    public required string Name { get; set; }
    public required bool IsCompleted { get; set; }
    public int? GroceryListId { get; set; }
  }
}
