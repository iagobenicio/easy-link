using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using easy_link.Entities;

namespace easy_link.Repositories
{
    public interface ILinkRepository : IRepository<Link>
    {
        public void Delete(int Id);
    }
}