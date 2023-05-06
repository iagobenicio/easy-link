using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using easy_link.DTOs;
using easy_link.Entities;
using easy_link.Failures;
using easy_link.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace easy_link.Controllers
{   
    [ApiController]
    [Route("/page")]
    public class PageController : ControllerBase
    {
        private readonly IPageRepository _pageRepository;
        private readonly IMapper _mapper;

        public PageController(IPageRepository pageRepository, IMapper mapper)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
        }
        
        [HttpGet("{pagename}")]
        public IActionResult GetPage(String pagename)
        {
            try
            {
                var page = _pageRepository.GetByPageName(pagename);
                var pageDto = _mapper.Map<PageDto>(page);
                return Ok(pageDto);
            }
            catch (PageNotFoundException e)
            {
                return NotFound(new {Erro = $"{e.Menssage}"});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetDashboard()
        {   

            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null)
                    return BadRequest();

                var userPage = _pageRepository.GetByUserId(int.Parse(userId));

                var userPageDTO = _mapper.Map<DashboardDTO>(userPage);

                return Ok(userPageDTO);

            }
            catch (PageNotFoundException e)
            {
                return NotFound(new {Erro = $"{e.Menssage}"});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterPageDTO pageDTO)
        {   

            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var pageEntity = _mapper.Map<Page>(pageDTO);

                if (userId == null || pageEntity == null)
                    return BadRequest("Não foi possível cadastrar uma página");

                pageEntity.Id = int.Parse(userId);

                await _pageRepository.Register(pageEntity);

                return StatusCode(StatusCodes.Status201Created);

            }
            catch (RegisterPageException e)
            {
                return BadRequest(e.Menssage);
            }
            catch (OperationCanceledException)
            {
                return BadRequest(new {Erro = "Operação cancelada"});
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new {Erro = "Erro ao salvar as alterações"});
            }
            catch (Exception e)
            {
                return BadRequest(new {Erro = $"{e.Message}"});
            }
            
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(RegisterPageDTO pageDTO)
        {   

            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
                var pageEntity = _mapper.Map<Page>(pageDTO);
    
                if (userId == null || pageEntity == null)
                    return BadRequest(new {Erro = "Não foi possível fazer as alterações"});
    
                await _pageRepository.Update(pageEntity,int.Parse(userId));
                return Ok();
            }
            catch (PageNotFoundException e)
            {
                return BadRequest(new {Erro = $"{e.Menssage}"});
            }
            catch (UpdatePageException e)
            {
                return BadRequest(new {Erro = $"{e.Menssage}"});
            }
            catch (OperationCanceledException)
            {
                return BadRequest(new {Erro = "Operação cancelada"});
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new {Erro = "Erro ao salvar as alterações"});
            }
            catch (Exception e)
            {
                return BadRequest(new {Erro = $"{e.Message}"});
            }

        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
                if (userId == null)
                    return BadRequest("Não foi possivel deletar sua página");
    
                await _pageRepository.Delete(int.Parse(userId));

                return NoContent();

            }   
            catch (PageNotFoundException e)
            {
                return BadRequest(new {Erro = $"{e.Menssage}"});
            }
            catch (OperationCanceledException)
            {
                return BadRequest(new {Erro = "Operação cancelada"});
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new {Erro = "Erro ao salvar as alterações"});
            }
            catch (Exception e)
            {
                return BadRequest(new {Erro = $"{e.Message}"});
            }
        } 
    }
}