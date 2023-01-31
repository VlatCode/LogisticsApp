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
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
                throw new InvalidEntryException("All fields are required!");
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
                throw new NotFoundException($"Calculation with ID {id} was not found.");
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
                throw new NotFoundException($"Calculation with ID {id} was not found.");
            }
            if (id == null)
            {
                throw new InvalidEntryException("Calculation ID is required");
            }

            CalculationDto calculationDto = calculationDb.ToCalculationDto();
            return calculationDto;
        }

        public List<CalculationDto> GetCalculationsByCourierId(int courierId)
        {
            var calculationsDb = _calculationRepository.GetAll();

            if (calculationsDb == null)
            {
                throw new NotFoundException($"Courier with ID {courierId} was not found.");
            }
            if (courierId == null)
            {
                throw new InvalidEntryException("Calculation ID is required");
            }

            return calculationsDb.Where(x => x.CourierId == courierId)
                .Select(x => x.ToCalculationDto())
                .ToList();
        }

        public List<CalculationDto> GetCalculationsByType(int calculationType)
        {
            var calculationsDb = _calculationRepository.GetAll();

            if (calculationsDb == null)
            {
                throw new NotFoundException($"Calculation type does not exist! Try again.");
            }
            if (calculationType == null)
            {
                throw new InvalidEntryException("Calculation type is required!");
            }

            return calculationsDb.Where(x => x.CalculationType == calculationType).
                Select(x => x.ToCalculationDto())
                .ToList();
        }

        public CalculationDto GetCostByInputs(int weight, int height, int width, int depth)
        {
            var calculationsDb = _calculationRepository.GetAll();

            if (weight == null || height == null || width == null || depth == null)
            {
                throw new InvalidEntryException("All fields are required!");
            }
            int dimensions = height * width * depth;

            var costsByWeight = calculationsDb
                .Where(x => x.From <= weight)
                .Where(x => x.To >= weight)
                .Where(x => x.CalculationType == 0)
                .Select(x => x.ToCalculationDto())
                .ToList();

            var costsByDimensions = calculationsDb
                .Where(x => x.From <= dimensions)
                .Where(x => x.To >= dimensions)
                .Where(x => x.CalculationType == 1)
                .Select(x => x.ToCalculationDto())
                .ToList();

            var calculations = new List<CalculationDto>();
            calculations.AddRange(costsByWeight);
            calculations.AddRange(costsByDimensions);

            var grouped = calculations.GroupBy(x => x.CourierId)
                .Where(x => x.Count() > 1)
                .OrderBy(x => x.Key);

            var calculationForEachCourier = new List<CalculationDto>();

            foreach (var item in grouped)
            {
                var tempList = item.ToList();

                foreach (var calc in tempList)
                {
                    if (calc.CourierId == 2 && calc.CalculationType == 0 && weight > 25)
                    {
                        calc.Cost = (weight - 25) * 0.417 + calc.Cost;
                    }
                    if (calc.CourierId == 3 && calc.CalculationType == 0 && weight > 30)
                    {
                        calc.Cost = (weight - 30) * 0.41 + calc.Cost;
                    }
                }

                calculationForEachCourier.Add(tempList.OrderByDescending(x => x.Cost).FirstOrDefault());
            }

            var finalCalculation = calculationForEachCourier.OrderBy(x => x.Cost).FirstOrDefault();

            return finalCalculation;
        }
    }
}
