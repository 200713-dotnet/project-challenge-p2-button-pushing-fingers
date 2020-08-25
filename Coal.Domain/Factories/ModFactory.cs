using Coal.Domain.Models;

namespace Coal.Domain.Factories
{
  public class ModFactory : IFactory<Mod>
  {
    public Mod Create()
    {
      return new Mod();
    }

    public Mod Create(int id, string name)
    {
      return new Mod()
      {
        Id = id,
        Name = name,
      };
    }
  }
}