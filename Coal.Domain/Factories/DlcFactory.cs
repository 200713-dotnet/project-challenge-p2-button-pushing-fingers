using Coal.Domain.Models;

namespace Coal.Domain.Factories
{
  public class DlcFactory : IFactory<Dlc>
  {
    public Dlc Create()
    {
      return new Dlc();
    }

    public Dlc Create(int id, string name)
    {
      return new Dlc()
      {
        Id = id,
        Name = name,
      };
    }
  }
}