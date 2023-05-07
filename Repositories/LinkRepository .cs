using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using easy_link.Context;
using easy_link.Entities;
using easy_link.Failures;
using Microsoft.EntityFrameworkCore;

namespace easy_link.Repositories
{
    public class LinkRepository : ILinkRepository
    {   
        private readonly EasylinkContext _easyLink;

        public LinkRepository(EasylinkContext easylinkContext)
        {
            _easyLink = easylinkContext;
        }

        public async Task Delete(int Id, int PageId)
        {
            var entity = _easyLink.link!.Where(l => l.Id == Id && l.PageId == PageId).FirstOrDefault();

            if (entity == null)
                throw new LinkNotFoundException("Link não encontrado");

            _easyLink.link!.Remove(entity);

            await _easyLink.SaveChangesAsync();
        }
        
        public async Task Register(Link link)
        {   
            await _easyLink.link!.AddAsync(link);
            await _easyLink.SaveChangesAsync(); 
        }

        public async Task Update(Link link, int userId)
        {   
            var entity = await _easyLink.link!.Where(link => link.Id == link.Id && link.PageId == userId).FirstOrDefaultAsync();

            if (entity == null)
                throw new LinkNotFoundException("Link não encontrado");

            MapperToLinkDb(link,entity);

            _easyLink.link!.Update(entity);
            await _easyLink.SaveChangesAsync(); 
                           
        }

        private void MapperToLinkDb(Link linkUpdate, Link linkDb)
        {   
            linkDb.LinkName = linkUpdate.LinkName;
            linkDb.UrlDirection = linkUpdate.UrlDirection;
        }
    }
}