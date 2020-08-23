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
               _pub = pr.Read(name);
               
               if(_pub == null)
               {
                    pr.Create(name);
               }
               
               return Ok(_pub);
          } 

          [HttpPost("{name}/{des}/{price}")]
          public ObjectResult NewGame(string name, string des, decimal price) 
          {
               //int gameId = pr.CreateGame(_pub.Id, name, des, price);
               return Ok(pr.CreateGame(_pub.Id, name, des, price)); 
                
          } 

          [HttpPost("{gid}/{name}/{des}")]
          public ObjectResult NewMod(int gid, string name, string des) 
          {
               return Ok(pr.CreateMod(_pub.Id, gid, name, des)); 
          } 

          [HttpPost("{gid}/{name}/{des}/{price}")]
          public ObjectResult NewDlc(int gid, string name, string des, decimal price) 
          {
               return Ok(pr.CreateDLC(_pub.Id, gid, name, des, price));
          } 
     }
}