using System.Collections.Generic;
using Coal.Domain.Models;

namespace Coal.Domain.Factories
{
     public class LibraryFactory : IFactory<Library>
     {
          public Library Create()
          {
               return new Library()
               {
                    LibraryGames = new List<Game>(),
               };
          }

          public Library Create(int id, string name)
          {
               return new Library()
               {
                    Id = id,
                    Name = name, 
                    LibraryGames = new List<Game>(),
               };
          }
     }
}