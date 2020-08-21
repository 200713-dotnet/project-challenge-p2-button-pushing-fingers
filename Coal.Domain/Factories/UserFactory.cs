using Coal.Domain.Models;

namespace Coal.Domain.Factories
{
     public class UserFactory : IFactory<User>
     {
          public User Create()
          {
               return new User();
          }

          public User Create(int id, string name)
          {
               return new User()
               {
                    Id = id,
                    Name = name,
               };
          }
     }
}