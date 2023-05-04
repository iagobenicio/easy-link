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
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace easy_link.Controllers
{   
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("/register")]
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
                return BadRequest(new {e.mensage,e.errors});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        } 
        [HttpPost("/login")]
        public async Task<IActionResult> SignIn(String email, String Password)
        {   
            try
            {
                await _userRepository.SignIn(email,Password);
                var user = await _userRepository.GetUser(email);
                var userLoggedDTO = BuildToken(user);

                return Ok(userLoggedDTO);
            }
            catch (UserNotFound e)
            {
                return NotFound(new {e.Menssage});
            }   
            catch (Exception e)
            {
                return BadRequest(new {e.Message});
            }    
        }
        private UserLoggedDTO BuildToken(User user)
        {   
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Key"]!));

            var userClaims = new[]
            {   
                new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.UserName!),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.Email!)
            };

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddHours(1);

            var jwtSecurityToken = new JwtSecurityToken(
                claims: userClaims,
                expires: expires,
                signingCredentials: credentials
            );

            var jwtHandler = new JwtSecurityTokenHandler();
            var tokenJWT = jwtHandler.WriteToken(jwtSecurityToken);

            return new UserLoggedDTO()
            {
                Token = tokenJWT,
                Expires = expires
            };

        } 
    }
}