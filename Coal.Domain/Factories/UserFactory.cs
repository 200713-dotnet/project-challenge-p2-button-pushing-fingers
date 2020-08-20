using Coal.Domain.Models;

namespace Coal.Domain.Factories
{
     public class UserFactory : IFactory<User>
     {
          public User Create()
          {
               return new User();
          }
     }
}