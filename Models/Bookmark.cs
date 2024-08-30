namespace Raw2PlateFuelPlusNetcore.Models
{
  public partial class Bookmark
  {
    public required int BookmarkId { get; set; }
    public required string MealType { get; set; }
    public required int RecipeId { get; set; }
    public required string DateAdded { get; set; }
    public int? UserId { get; set; }
  }
}
