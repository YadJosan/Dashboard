using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
   public class SiteRepository : Repository<Site>, ISiteRepository
    {
        public SiteRepository(DbContext context) : base(context)
        { }




        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public void AddSite(Site site)
        {
             _appContext.Add(site);
        }

        public IEnumerable<Site> GetAllSiteData()
        {
           return _appContext.Sites.ToList();
        }

        public Site GetSiteById(int id)
        {
            return _appContext.Sites.Where(x=>x.Id== id).FirstOrDefault();
        }


        public void UpdateSite(Site site)
        {
            _appContext.Update(site);
        }
        public void DeleteSite(int id)
        {
           var site =_appContext.Sites.Where(x => x.Id == id).FirstOrDefault();
            _appContext.Sites.Remove(site);
        }
    }
}