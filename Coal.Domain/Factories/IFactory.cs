namespace Coal.Domain.Factories
{
     public interface IFactory<T> where T : class, new()
     {
          T Create();
     }
}