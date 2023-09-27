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
    public class PageRepository : IPageRepository
    {   
        private readonly EasylinkContext _easyLink;

        public PageRepository(EasylinkContext easylinkContext)
        {
            _easyLink = easylinkContext;
        }
        
        public async Task Delete(int Id)
        {
            var entity = _easyLink.page!.Find(Id);
            if (entity == null)
                throw new PageNotFoundException("Você não tem uma página para deletar");

            _easyLink.Remove(entity);
            await _easyLink.SaveChangesAsync();

        }
        public Page GetByUserId(int userId)
        {
            var page = _easyLink.page!.Where(p => p.Id == userId)
                .Select(p => new Page(){
                    Id = p.Id,
                    PageName = p.PageName,
                    PageDescription = p.PageDescription,
                    Links = p.Links!.Select(l => new Link(){
                        Id = l.Id,
                        LinkName = l.LinkName,
                        UrlDirection = l.UrlDirection,
                        PageId = l.PageId
                    }).ToList()
                }).FirstOrDefault();

            if (page == null)
                throw new PageNotFoundException("Página não encontrada");

            return page;
      
        }
        public Page GetByPageName(string pageName)
        {
            var page = _easyLink.page!.Where(p => p.PageName!.ToLower() == pageName.ToLower())
                .Select(p => new Page(){
                    PageName = p.PageName,
                    PageDescription = p.PageDescription,
                    Links = p.Links!.Select(l => new Link(){
                        LinkName = l.LinkName,
                        UrlDirection = l.UrlDirection
                    }).ToList()
                }).FirstOrDefault();

            if (page == null)
                throw new PageNotFoundException("Página não encontrada");

            return page;
        }

        public async Task Register(Page entity)
        {   
            var pageExist = _easyLink.page!
                .Any(p => p.PageName!.ToLower() == entity.PageName!.ToLower() || p.Id == entity.Id);
        
            if (pageExist)
                throw new RegisterPageException("Este nome de página já existe ou você já tem uma página");

            await _easyLink.page!.AddAsync(entity);
            await _easyLink.SaveChangesAsync();
        }

        public async Task Update(Page entity, int Id)
        {
            var page = _easyLink.page!.Where(p => p.Id == Id).FirstOrDefault();

            if (page == null)
                throw new PageNotFoundException("Você não tem uma página");

            var pageNameExist = _easyLink.page!.Any(p => p.PageName!.ToUpper() == entity.PageName!.ToUpper() && p.Id != Id); 

            if (pageNameExist)
                throw new UpdatePageException("Este nome de página já existe");

            MapperToPageDb(entity,page);
            _easyLink.page!.Update(page);
            await _easyLink.SaveChangesAsync();
            
        }

        public byte[] GetImage(string pageName)
        {   
            var image = _easyLink.page!.Where(p => p.PageName!.ToLower() == pageName.ToLower()).Select(p => p.ImageProfile).FirstOrDefault();
            
            if(image == null)
                throw new PageNotFoundException("Não foi encontrado uma página com este nome");

            return image;
        }

        public async Task UdateImage(int pageId, byte[] image)
        {
            var entity = _easyLink.page!.FirstOrDefault(p => p.Id == pageId);

            if (entity == null)
                throw new PageNotFoundException("Você não tem uma página");

            entity.ImageProfile = image;

            _easyLink.page!.Attach(entity);

            _easyLink.Entry(entity).Property(p => p.ImageProfile).IsModified = true;

            await _easyLink.SaveChangesAsync();
        }

        private void MapperToPageDb(Page entity, Page pageDb)
        {
            pageDb.PageName = entity.PageName;
            pageDb.PageDescription = entity.PageDescription;
            pageDb.ImageProfile = pageDb.ImageProfile;
        }
    }
}