using System;
using Microsoft.AspNetCore.Mvc;
//using domain = Coal.Domain.Models;
//using Coal.Domain.Factories;
using storing = Coal.Storing.Models;
using Coal.Storing.Repositories;
using Coal.Storing;
using Microsoft.AspNetCore.Cors;

namespace Coal.Domain.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     [EnableCors]
     public class PublisherController : ControllerBase
     {
          private static storing.Publisher _pub;
          private PublisherRepo pr;

          private readonly CoalDbContext _db;

          public PublisherController(CoalDbContext dbContext)
          {
               _db = dbContext;
               pr = new PublisherRepo(_db);
          }

          [HttpGet("{name}")]
          public ObjectResult Get(string name)
          {
               /*_pub = */pr.Read();
               
               if(_pub == null)
               {
                    pr.Create();
               }
               
               return Ok(_pub);
          }    
     }
}