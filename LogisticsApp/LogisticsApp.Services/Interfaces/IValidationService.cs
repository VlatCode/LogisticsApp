using LogisticsApp.Domain.Models;
using LogisticsApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.Services.Interfaces
{
    public interface IValidationService
    {
        List<ValidationDto> GetAllValidations();
        ValidationDto GetById(int id);
        void AddValidation(AddValidationDto validation);
        void DeleteValidation(int id);
        List<ValidationDto> GetValidationsByCourierId(int courierId);
    }
}
