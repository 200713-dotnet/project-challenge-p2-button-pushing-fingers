namespace Coal.Storing.Models
{
  public class LibraryGame
  {
    public int LibraryId { get; set; }
    public Library Library { get; set; }
    public int GameId { get; set; }
    public Game Game { get; set; }
  }
}