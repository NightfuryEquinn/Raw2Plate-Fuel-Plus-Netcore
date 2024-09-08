namespace Raw2PlateFuelPlusNetcore.Models
{
  public partial class ManualMeal
  {
    public required int ManualMealId { get; set; }
    public required string Name { get; set; }
    public required double Calories { get; set; }
    public int? TrackerId { get; set; }
  }
}
