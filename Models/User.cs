namespace Raw2PlateFuelPlusNetcore.Models
{
  public partial class User
  {
    public required int UserId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public string? Image { get; set; }
    public required string Email { get; set; }
    public string? Contact { get; set; }
    public string? DateOfBirth { get; set; }
    public int? Height { get; set; }
    public int? Weight { get; set; }
    public int? Age { get; set; }
    public required string RegisteredDate { get; set; }
    public required bool IsDarkMode { get; set; }
    public required bool IsAppleAuth { get; set; }
    public required bool IsGoogleAuth { get; set; }
  }
}
