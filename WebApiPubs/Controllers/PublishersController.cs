using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {


        private readonly pubsContext context;

        public PublishersController(pubsContext context)
        {

            this.context = context;

        }

        //GET
        [HttpGet]

        public ActionResult<IEnumerable<Publishers>> Get()
        {
            return context.Publishers.ToList();
        }


        //GET BY ID
        [HttpGet("{id}")]
        public ActionResult<Publishers> GetById(string id)
        {
            Publishers publisher = (from p in context.Publishers
                                   where p.PubId == id
                                   select p).SingleOrDefault();
            return publisher;
        }


        //Post 
        [HttpPost]
        public ActionResult Post(Publishers publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(publisher);
            }
            context.Publishers.Add(publisher);
            context.SaveChanges();
            return Ok();
        }


        //Put

        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Publishers publisher)
        {
            if (id != publisher.PubId)
            {
                return BadRequest();
            }
            context.Entry(publisher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }



        //Delete
        [HttpDelete("{id}")]
        public ActionResult<Publishers> Delete(string id)
        {
            var publisher = (from p in context.Publishers
                             where p.PubId == id
                             select p).SingleOrDefault();
            if (publisher == null)
            {
                return NotFound();
            }
            context.Publishers.Remove(publisher);
            context.SaveChanges();
            return publisher;


        }


    }

}
