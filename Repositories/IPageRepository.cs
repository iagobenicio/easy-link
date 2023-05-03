using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using easy_link.Entities;

namespace easy_link.Repositories
{
    public interface IPageRepository : IRepository<Page>
    {   
        public Page GetByPageName(string pageName);
        public Task Delete(int Id);
    }
}