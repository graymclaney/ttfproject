using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mysqlx.Crud;

namespace API.Models.Interfaces
{
    public interface IPostOrder
    {
        public void PostOrder(OrderForm value);
    }
}