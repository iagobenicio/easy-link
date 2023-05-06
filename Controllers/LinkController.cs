using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using easy_link.DTOs;
using easy_link.Entities;
using easy_link.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                var pageId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                var linkEntity = _mapper.Map<Link>(linkDTO);
    
                if (pageId == null || linkEntity == null)
                    return BadRequest("Não foi possivel cadastrar esse link para sua página");
    
                linkEntity.PageId = int.Parse(pageId);
                
                await _linkRepository.Register(linkEntity);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}