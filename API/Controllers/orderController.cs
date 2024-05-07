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
using Mysqlx.Crud;
using NuGet.Protocol.Core.Types;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class orderController : ControllerBase
    {
        // GET: api/order
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<OrderForm> Get()
        {
            IGetAllOrders readObject = new ReadOrderData();
            return readObject.GetAllOrders();
        }

        // GET: api/order/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetOrderForms")]
        public OrderForm Get(int id)
        {
            IGetOrder readObject = new ReadOrderData();
            return readObject.GetOrder(id);
        }

        // POST: api/order
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] OrderForm value)
        {
            IPostOrder insertObject = new PostOrder();
            insertObject.PostOrder(value);
        }

        // PUT: api/order/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/order/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            IDeleteData deleteObject = new DeleteData();
            deleteObject.DeleteData(id);
        }
    }
}
