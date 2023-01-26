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
    public class CourierService : ICourierService
    {
        private readonly IRepository<Courier> _courierRepository;

        public CourierService(IRepository<Courier> courierRepository)
        {
            _courierRepository = courierRepository;
        }

        public void AddCourier(AddCourierDto addCourierDto)
        {
            // 1. Validation
            if (string.IsNullOrEmpty(addCourierDto.CourierName))
            {
                throw new NotFoundException("Please enter courier name!");
            }
            if (addCourierDto.CourierName.Length > 100)
            {
                throw new InvalidEntryException("Courier name is too long! Try again.");
            }
            // 2. Map to domain model
            Courier newCourier = addCourierDto.ToCourier();
            // 3. Add to db
            _courierRepository.Add(newCourier);
        }

        public void DeleteCourier(int id)
        {
            Courier courierDb = _courierRepository.GetById(id);
            if (courierDb == null)
            {
                throw new NotFoundException($"Courier with ID {id} was not found.");
            }

            _courierRepository.Delete(courierDb);
        }

        public List<CourierDto> GetAllCouriers()
        {
            var couriersDb = _courierRepository.GetAll();
            return couriersDb.Select(x => x.ToCourierDto()).ToList();
        }

        public CourierDto GetById(int id)
        {
            Courier courierDb = _courierRepository.GetById(id);
            if (courierDb == null)
            {
                throw new NotFoundException($"Courier with ID {id} was not found.");
            }
            if (id == null)
            {
                throw new InvalidEntryException("Courier ID is required");
            }

            CourierDto courierDto = courierDb.ToCourierDto();
            return courierDto;
        }
    }
}
