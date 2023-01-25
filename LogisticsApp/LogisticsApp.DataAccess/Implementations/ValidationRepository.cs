using LogisticsApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.DataAccess.Implementations
{
    public class ValidationRepository : IRepository<Validation>
    {
        private LogisticsAppDbContext _logisticsAppDbContext;

        public ValidationRepository(LogisticsAppDbContext logisticsAppDbContext)
        {
            _logisticsAppDbContext = logisticsAppDbContext;
        }
        public void Add(Validation entity)
        {
            _logisticsAppDbContext.Validations.Add(entity);
            _logisticsAppDbContext.SaveChanges();
        }

        public void Delete(Validation entity)
        {
            _logisticsAppDbContext.Validations.Remove(entity);
            _logisticsAppDbContext.SaveChanges();
        }

        public List<Validation> GetAll()
        {
            return _logisticsAppDbContext.Validations
                .ToList();
        }

        public Validation GetById(int id)
        {
            return _logisticsAppDbContext.Validations
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Validation entity)
        {
            throw new NotImplementedException();
        }
    }
}

