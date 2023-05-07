using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    [Route("[controller]")]
    public class LinkController : ControllerBase
    {
        private readonly ILinkRepository _linkRepository;
        private readonly IMapper _mapper;

        public LinkController(ILinkRepository linkRepository, IMapper mapper)
        {
            _linkRepository = linkRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterLinkDTO linkDTO)
        {   
            try
            {
                var userIdAndpageId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                var linkEntity = _mapper.Map<Link>(linkDTO);
    
                if (userIdAndpageId == null || linkEntity == null)
                    return BadRequest("Não foi possivel cadastrar esse link para sua página");
    
                linkEntity.PageId = int.Parse(userIdAndpageId);
                
                await _linkRepository.Register(linkEntity);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (OperationCanceledException)
            {
                return BadRequest(new {Erro = "Operação cancelada"});
            }
            catch (DbUpdateException)
            {
                return BadRequest(new {Erro = "Não foi possivel salvar os dados"});
            }
            catch (Exception e)
            {
                return BadRequest(new {Erro = e.Message});
            }

        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(RegisterLinkDTO linkDTO)
        {
            try
            {
                var userIdAndpageId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
                var linkEntity = _mapper.Map<Link>(linkDTO);
    
                if (userIdAndpageId == null || linkEntity == null)
                    return BadRequest("Não foi possivel atualizar o link");
    
                await _linkRepository.Update(linkEntity,int.Parse(userIdAndpageId.ToString()));
                return Ok();
            }
            catch (LinkNotFoundException e)
            {
                return BadRequest(new {Erro = e.Menssage});
            }
            catch (OperationCanceledException)
            {
                return BadRequest(new {Erro = "Operação cancelada"});
            }
            catch (DbUpdateException)
            {
                return BadRequest(new {Erro = "Não foi possivel salvar as alterações"});
            }
            catch (Exception e)
            {
                return BadRequest(new {Erro = e.Message});
            }

        }
        [Authorize]
        [HttpDelete("{linkId}")]
        public async Task<IActionResult> Delete(int linkId)
        {
            try
            {
                var userIdAndpageId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userIdAndpageId == null)
                    return BadRequest("Não foi possivel deletar este link");

                await _linkRepository.Delete(linkId,int.Parse(userIdAndpageId));

                return Ok();

            }
            catch (LinkNotFoundException e)
            {
                return BadRequest(new {Erro = e.Menssage});
            }
            catch (OperationCanceledException)
            {
                return BadRequest(new {Erro = "Operação cancelada"});
            }
            catch (DbUpdateException)
            {
                return BadRequest(new {Erro = "Não foi possivel salvar as alterações"});
            }
            catch (Exception e)
            {
                return BadRequest(new {Erro = e.Message});
            }
        }
    }
}