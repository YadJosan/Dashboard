using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
  public  interface ISiteRepository : IRepository<Site>
    {
        IEnumerable<Site> GetAllSiteData();
        Site GetSiteById(int id);
        void AddSite(Site site);
        void UpdateSite(Site site);
        void DeleteSite(int id);
    }
}
