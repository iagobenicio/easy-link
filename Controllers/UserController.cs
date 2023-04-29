using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using easy_link.DTOs;
using easy_link.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using easy_link.Entities;
using AutoMapper;
using easy_link.Failures;

namespace easy_link.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterWithPassword(UserDTO userDTO)
        {
            try
            {

               var userEntity = _mapper.Map<User>(userDTO);
               await _userRepository.RegisterWithPassWord(userEntity,userDTO.Password!);

               return StatusCode(StatusCodes.Status201Created);
            }
            catch (UserFailure e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }  
    }
}