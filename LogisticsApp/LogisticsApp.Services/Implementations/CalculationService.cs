using LogisticsApp.DataAccess;
using LogisticsApp.DataAccess.Implementations;
using LogisticsApp.Domain.Models;
using LogisticsApp.DTOs;
using LogisticsApp.Mappers;
using LogisticsApp.Services.Interfaces;
using LogisticsApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.Services.Implementations
{
    public class CalculationService : ICalculationService
    {
        private readonly IRepository<Calculation> _calculationRepository;

        public CalculationService(IRepository<Calculation> calculationRepository)
        {
            _calculationRepository = calculationRepository;
        }

        public void AddCalculation(AddCalculationDto addCalculationDto)
        {
            // 1. Validation
            if (addCalculationDto.CourierId == null || addCalculationDto.CalculationType == null || addCalculationDto.From == null || addCalculationDto.To == null || addCalculationDto.Cost == null)
            {
                throw new InvalidEntryException("All fields are required! Try again.");
            }
            // 2. Map to domain model
            Calculation newCalculation = addCalculationDto.ToCalculation();
            // 3. Add to db
            _calculationRepository.Add(newCalculation);
        }

        public void DeleteCalculation(int id)
        {
            Calculation calculationDb = _calculationRepository.GetById(id);
            if (calculationDb == null)
            {
                throw new NotFoundException($"Calculation with id {id} was not found.");
            }

            _calculationRepository.Delete(calculationDb);
        }

        public List<CalculationDto> GetAllCalculations()
        {
            var calculationsDb = _calculationRepository.GetAll();
            return calculationsDb.Select(x => x.ToCalculationDto()).ToList();
        }

        public CalculationDto GetById(int id)
        {
            Calculation calculationDb = _calculationRepository.GetById(id);
            if (calculationDb == null)
            {
                throw new NotFoundException($"Calculation with id {id} was not found.");
            }
            if (id == null)
            {
                throw new InvalidEntryException("Calculation id is required");
            }

            CalculationDto calculationDto = calculationDb.ToCalculationDto();
            return calculationDto;
        }

        public List<CalculationDto> GetCalculationsByCourierId(int courierId)
        {
            var calculationsDb = _calculationRepository.GetAll();
            return calculationsDb.Where(x => x.CourierId == courierId)
                .Select(x => x.ToCalculationDto())
                .ToList();
        }

        public List<CalculationDto> GetCalculationsByType(int calculationType)
        {
            var calculationsDb = _calculationRepository.GetAll();
            return calculationsDb.Where(x => x.CalculationType == calculationType).
                Select(x => x.ToCalculationDto())
                .ToList();
        }

        public List<CalculationDto> GetCalculationsByInputs(int calculationType, int value)
        {
            var calculationsDb = _calculationRepository.GetAll();

            return calculationsDb
                .Where(x => x.CalculationType == calculationType)
                .Where(x => x.From <= value)
                .Where(x => x.To >= value)
                .Select(x => x.ToCalculationDto())
                .ToList();
        }
    }
}
