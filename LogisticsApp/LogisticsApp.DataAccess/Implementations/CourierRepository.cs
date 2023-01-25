using LogisticsApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.DataAccess.Implementations
{
    public class CourierRepository : IRepository<Courier>
    {
        private LogisticsAppDbContext _logisticsAppDbContext;

        public CourierRepository(LogisticsAppDbContext logisticsAppDbContext)
        {
            _logisticsAppDbContext = logisticsAppDbContext;
        }

        public void Add(Courier entity)
        {
            _logisticsAppDbContext.Couriers.Add(entity);
            _logisticsAppDbContext.SaveChanges();
        }

        public void Delete(Courier entity)
        {
            _logisticsAppDbContext.Couriers.Remove(entity);
            _logisticsAppDbContext.SaveChanges();
        }

        public List<Courier> GetAll()
        {
            return _logisticsAppDbContext.Couriers
                .ToList();
        }

        public Courier GetById(int id)
        {
            return _logisticsAppDbContext.Couriers
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Courier entity)
        {
            throw new NotImplementedException();
        }
    }
}
