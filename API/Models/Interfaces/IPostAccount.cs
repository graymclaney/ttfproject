using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database;

namespace API.Models.Interfaces
{
    public interface IPostAccount
    {
        public void PostAccount(Account value);
    }
}