namespace Coal.Storing.Models
{
  public class LibraryDLC
  {
    public int LibraryId { get; set; }
    public Library Library { get; set; }
    public int ContentId { get; set; }
    public DownloadableContent DownloadableContent { get; set; }
  }
}