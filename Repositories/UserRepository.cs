using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using easy_link.Context;
using easy_link.Entities;
using easy_link.Failures;
using Microsoft.AspNetCore.Identity;

namespace easy_link.Repositories
{
    public class UserRepository : IUserRepository
    {   

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly EasylinkContext _easyLinkContext; 
        public UserRepository(UserManager<User> user, SignInManager<User> signInManager, EasylinkContext easylinkContext)
        {
            _userManager = user;
            _signInManager = signInManager;
            _easyLinkContext = easylinkContext;
        }

        public Task Register(User entity)
        {
            throw new NotImplementedException();
        }
        public async Task<User> GetUser(String email)
        {   
            var user = await _userManager.FindByNameAsync(email);
            if (user == null)
            {
                throw new UserNotFound("Não foi possivel encontrar seus dados");
            }
            return user;
        }
        public async Task RegisterWithPassWord(User user, String passWord)
        {  
            try
            {   
                
                user.UserName = user.Email;
                user.Email = user.UserName;

                var result = await _userManager.CreateAsync(user,passWord);

                if (!result.Succeeded)
                    throw new UserFailure(result.Errors,"Não foi possivel se registrar");

            }
            catch (Exception e)
            {    
                throw new Exception($"Erro: {e.Message}");
            } 
           
           
        }
        public async Task SignIn(string email, string passWord)
        {   
            try
            {
                var result = await _signInManager.PasswordSignInAsync(email,passWord,false,false);
            
                if (!result.Succeeded)
                    throw new Exception("Não foi possível fazer a autenticação, verifique suas credenciaias");
            }
            catch (Exception e)
            {    
                throw new Exception($"Erro: {e.Message}");
            } 

        }

        public async Task Update(User user, int Id)
        {
            try
            {

                var result = await _userManager.UpdateAsync(user);
            
                if (!result.Succeeded)
                    throw new UserFailure(result.Errors,"Não foi possivel alterar os dados");

            }
            catch (Exception e)
            {
                throw new Exception($"Erro: {e.Message}");
            }

        }
    }
}