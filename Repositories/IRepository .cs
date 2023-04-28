using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace easy_link.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task Register(T entity);
        public Task Update(T entity, int Id);        
    }
}