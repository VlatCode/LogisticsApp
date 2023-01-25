using LogisticsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.DataAccess.Implementations
{
    public class PackageRepository : IRepository<Package>
    {
        private LogisticsAppDbContext _logisticsAppDbContext;

        public PackageRepository(LogisticsAppDbContext logisticsAppDbContext)
        {
            _logisticsAppDbContext = logisticsAppDbContext;
        }

        public void Add(Package entity)
        {
            _logisticsAppDbContext.Packages.Add(entity);
            _logisticsAppDbContext.SaveChanges();
        }

        public void Delete(Package entity)
        {
            _logisticsAppDbContext.Packages.Remove(entity);
            _logisticsAppDbContext.SaveChanges();
        }

        public List<Package> GetAll()
        {
            return _logisticsAppDbContext.Packages
                            .ToList();
        }

        public Package GetById(int id)
        {
            return _logisticsAppDbContext.Packages
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Package entity)
        {
            _logisticsAppDbContext.Packages.Update(entity);
            _logisticsAppDbContext.SaveChanges();
        }
    }
}
