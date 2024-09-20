namespace Raw2PlateFuelPlusNetcore.Models
{
  public partial class Meal
  {
    public required int MealId { get; set; }
    public required string MealType { get; set; }
    public required int RecipeId { get; set; }
    public string? Comment { get; set; }
    public int? PlannerId { get; set; }
    public int? TrackerId { get; set; }
  }
}
