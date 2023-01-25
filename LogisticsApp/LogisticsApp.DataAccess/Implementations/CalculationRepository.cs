using LogisticsApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.DataAccess.Implementations
{
    public class CalculationRepository : IRepository<Calculation>
    {
        private LogisticsAppDbContext _logisticsAppDbContext;

        public CalculationRepository(LogisticsAppDbContext logisticsAppDbContext)
        {
            _logisticsAppDbContext = logisticsAppDbContext;
        }
        public void Add(Calculation entity)
        {
            _logisticsAppDbContext.Calculations.Add(entity);
            _logisticsAppDbContext.SaveChanges();
        }

        public void Delete(Calculation entity)
        {
            _logisticsAppDbContext.Calculations.Remove(entity);
            _logisticsAppDbContext.SaveChanges();
        }

        public List<Calculation> GetAll()
        {
            return _logisticsAppDbContext.Calculations
                .ToList();
        }

        public Calculation GetById(int id)
        {
            return _logisticsAppDbContext.Calculations
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Calculation entity)
        {
            throw new NotImplementedException();
        }
    }
}
