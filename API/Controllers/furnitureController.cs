using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database;
using API.Models;
using API.Models.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class furnitureController : ControllerBase
    {
        // GET: api/furniture
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<Furniture> Get()
        {
            IGetAllFurniture readObject = new ReadFurnitureData();
            return readObject.GetAllFurniture();
        }

        // GET: api/furniture/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetFurniture")]
        public Furniture Get(int id)
        {
            IGetFurniture readObject = new ReadFurnitureData();
            return readObject.GetFurniture(id);
        }

        // POST: api/furniture
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] Furniture value)
        {
            IPostFurniture postObject = new PostFurniture();
            postObject.PostFurniture(value);
        }

        // PUT: api/furniture/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Furniture value)
        {
            ISellFurniture sellObject = new SellFurniture();
            sellObject.ChangeSoldStatus(value);
        }

        // DELETE: api/furniture/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            IDeleteData deleteObject = new DeleteData();
            deleteObject.DeleteData(id);
        }
    }
}
