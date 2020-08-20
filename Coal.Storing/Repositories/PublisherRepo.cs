namespace Coal.Storing.Repositories
{
  public class PublisherRepo
  {
    private CoalDbContext _db;

    public PublisherRepo(CoalDbContext dbContext)
    {
      _db = dbContext;
    }
  }
}