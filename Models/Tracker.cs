namespace Raw2PlateFuelPlusNetcore.Models
{
  public partial class Tracker
  {
    public required int TrackerId { get; set; }
    public required string Date { get; set; }
    public int? UserId { get; set; }
  }
}
