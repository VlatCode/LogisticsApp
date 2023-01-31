using LogisticsApp.Domain.Models;
using LogisticsApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.Services.Interfaces
{
    public interface ICalculationService
    {
        List<CalculationDto> GetAllCalculations();
        CalculationDto GetById(int id);
        void AddCalculation(AddCalculationDto calculation);
        void DeleteCalculation(int id);
        List<CalculationDto> GetCalculationsByCourierId(int courierId);
        List<CalculationDto> GetCalculationsByType(int calculationType);
        CalculationDto GetCostByInputs(int weight, int height, int width, int depth);
    }
}
