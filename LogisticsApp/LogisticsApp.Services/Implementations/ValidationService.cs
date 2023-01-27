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
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.Services.Implementations
{
    public class ValidationService : IValidationService
    {
        private readonly IRepository<Validation> _validationRepository;

        public ValidationService(IRepository<Validation> validationRepository)
        {
            _validationRepository = validationRepository;
        }

        public void AddValidation(AddValidationDto addValidationDto)
        {
            // 1. Validation
            if (addValidationDto.CourierId == null || addValidationDto.ValidationType == null || addValidationDto.From == null || addValidationDto.To == null)
            {
                throw new InvalidEntryException("All fields are required! Try again.");
            }
            // 2. Map to domain model
            Validation newValidation = addValidationDto.ToValidation();
            // 3. Add to db
            _validationRepository.Add(newValidation);
        }

        public void DeleteValidation(int id)
        {
            Validation validationDb = _validationRepository.GetById(id);
            if (validationDb == null)
            {
                throw new NotFoundException($"Validation with ID {id} was not found.");
            }

            _validationRepository.Delete(validationDb);
        }

        public List<ValidationDto> GetAllValidations()
        {
            var validationsDb = _validationRepository.GetAll();
            return validationsDb.Select(x => x.ToValidationDto()).ToList();
        }

        public ValidationDto GetById(int id)
        {
            Validation validationDb = _validationRepository.GetById(id);
            if (validationDb == null)
            {
                throw new NotFoundException($"Validation with ID {id} was not found.");
            }
            if (id == null)
            {
                throw new InvalidEntryException("Validation ID is required");
            }

            ValidationDto validationDto = validationDb.ToValidationDto();
            return validationDto;
        }

        public List<ValidationDto> GetValidationsByCourierId(int courierId)
        {
            var validationsDb = _validationRepository.GetAll();

            if (validationsDb == null)
            {
                throw new NotFoundException($"Courier with ID {courierId} was not found.");
            }
            if (courierId == null)
            {
                throw new InvalidEntryException("Courier ID is required");
            }

            return validationsDb.Where(x => x.CourierId == courierId)
                .Select(x => x.ToValidationDto())
                .ToList();
        }
    }
}
