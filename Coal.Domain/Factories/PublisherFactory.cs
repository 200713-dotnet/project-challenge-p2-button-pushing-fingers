using System.Collections.Generic;
using Coal.Domain.Models;

namespace Coal.Domain.Factories
{
     public class PublisherFactory : IFactory<Publisher>
     {
          public Publisher Create()
          {
               return new Publisher()
               {
                    Games = new List<Game>(),
               };
          }

          public Publisher Create(int id, string name)
          {
               return new Publisher()
               {
                    Id = id,
                    Name = name,
                    Games = new List<Game>(),
               };
          }
     }
}