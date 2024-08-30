namespace Raw2PlateFuelPlusNetcore.Models
{
  public partial class Planner
  {
    public required int PlannerId { get; set; }
    public required string Date { get; set; }
    public int? UserId { get; set; }
  }
}
