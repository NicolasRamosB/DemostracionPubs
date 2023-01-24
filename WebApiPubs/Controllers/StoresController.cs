using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController:ControllerBase
    {
        private readonly pubsContext context;

        public StoresController(pubsContext context)
        {

            this.context = context;

        }

        //GET
        [HttpGet]
        public ActionResult<IEnumerable<Stores>> Get()
        {
            return context.Stores.ToList();
        }


        //GET BY ID
        [HttpGet("{id}")]
        public ActionResult<Stores> GetById(string id)
        {
            Stores store = (from p in context.Stores
                            where p.StorId == id
                            select p).SingleOrDefault();
            return store;
        }


        [HttpGet("name/{storeName}")]
        public ActionResult<IEnumerable<Stores>> GetByName(string storeName)
        {
            List<Stores> store = (from p in context.Stores
                            where p.StorName == storeName
                            select p).ToList();
            return store;
        }


        [HttpGet("listado/{zip}")]
        public ActionResult<IEnumerable<Stores>> GetByZip(string zip)
        {
            List<Stores> store = (from p in context.Stores
                            where p.Zip == zip
                            select p).ToList();
            return store;
        }


        [HttpGet("listado/{city}/{state}")]
        public ActionResult<IEnumerable<Stores>> GetByCityState(string city, string state)
        {
            List<Stores> store = (from p in context.Stores
                            where p.City == city && p.State == state   
                            select p).ToList();
            return store;
        }


        //Post 
        [HttpPost]
        public ActionResult Post(Stores stores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Stores.Add(stores);
            context.SaveChanges();
            return Ok();
        }


        //Put
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Stores store)
        {
            if (id != store.StorId)
            {
                return BadRequest();
            }
            context.Entry(store).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();

            return Ok();
        }



        //Delete
        [HttpDelete("{id}")]
        public ActionResult<Stores> Delete(string id)
        {
            Stores stores = (from s in context.Stores
                            where s.StorId == id
                            select s).SingleOrDefault();

            if (stores == null)
            {
                return NotFound();
            }
            context.Stores.Remove(stores);
            context.SaveChanges();
            return stores;
        }




    }
}
