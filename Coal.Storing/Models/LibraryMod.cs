namespace Coal.Storing.Models
{
  public class LibraryMod
  {
    public int LibraryId { get; set; }
    public Library Library { get; set; }
    public int ModId { get; set; }
    public Mod Mod { get; set; }
  }
}