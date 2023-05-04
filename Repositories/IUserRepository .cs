using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using easy_link.Entities;

namespace easy_link.Repositories
{
    public interface IUserRepository : IRepository<User>
    {   
        public Task RegisterWithPassWord(User user, string passWord);
        public Task SignIn(String email, String passWord);
        public Task<User> GetUser(String email);
    }
}