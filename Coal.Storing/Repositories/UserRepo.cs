namespace Coal.Storing.Repositories
{
  public class UserRepo
  {
    private CoalDbContext _db;

    public UserRepo(CoalDbContext dbContext)
    {
      _db = dbContext;
    }
  }
}