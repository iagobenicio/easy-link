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
                throw new PageNotFoundException("Você não tem uma página");

            _easyLink.Remove(entity);
            await _easyLink.SaveChangesAsync();

        }

        public Page GetByPageName(string pageName)
        {
            var page = _easyLink.page!.Where(p => p.PageName == pageName).FirstOrDefault();

            if (page == null)
                throw new PageNotFoundException("Página não encontrada");

            return page;
        }

        public async Task Register(Page entity)
        {   
            try
            {   var pageExist = _easyLink.page!.Any(p => p.PageName!.ToLower() == entity.PageName!.ToLower());
            
                if (pageExist)
                    throw new RegisterPageException("Este nome de página já existe");

                await _easyLink.page!.AddAsync(entity);
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

        public async Task Update(Page entity, int Id)
        {
            var page = _easyLink.page!.Where(p => p.Id == Id).FirstOrDefault();

            if (page == null)
                throw new PageNotFoundException("Você não tem uma página");
            
            try
            {
                MapperToPageDb(entity,page);
                _easyLink.page!.Update(page);
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
        private void MapperToPageDb(Page entity, Page pageDb)
        {
            pageDb.PageName = entity.PageName;
            pageDb.PageDescription = entity.PageDescription;
        }
    }
}