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

        public async Task Delete(int Id)
        {
            var entity = _easyLink.link!.Find(Id);

            if (entity == null)
                throw new LinkNotFoundException("Link não encontrado");

            _easyLink.link!.Remove(entity);

            await _easyLink.SaveChangesAsync();
        }
        public List<Link> GetAll(int userId)
        {
            var pageLinks = _easyLink.link!.Where(link => link.PageId == userId).ToList();
            return pageLinks;
        }       
        public async Task Register(Link link)
        {   
            try
            {
               await _easyLink.link!.AddAsync(link);
               await _easyLink.SaveChangesAsync();
            }
            catch (OperationCanceledException e)
            {
                throw new OperationCanceledException(e.Message);
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateException(e.Message);
            }
        }

        public async Task Update(Link link, int userId)
        {   
            var entity = await _easyLink.link!.Where(link => link.Id == link.Id && link.PageId == userId).FirstOrDefaultAsync();

            if (entity == null)
                throw new LinkNotFoundException("Link não encontrado");

            MapperToLinkDb(link,entity);

            try
            {
                _easyLink.link!.Update(entity);
                await _easyLink.SaveChangesAsync(); 
            }
            catch (OperationCanceledException e)
            {
                throw new OperationCanceledException(e.Message);
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateException(e.Message);
            }
                       
        }
        private void MapperToLinkDb(Link linkUpdate, Link linkDb)
        {   
            linkDb.LinkName = linkUpdate.LinkName;
            linkDb.UrlDirection = linkUpdate.UrlDirection;
        }
    }
}