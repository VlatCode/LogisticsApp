using LogisticsApp.Domain.Models;
using LogisticsApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.Mappers
{
    public static class CalculationMapper
    {
        public static CalculationDto ToCalculationDto(this Calculation calculation)
        {
            return new CalculationDto
            {
                Id = calculation.Id,
                CourierId = calculation.CourierId,
                CalculationType = calculation.CalculationType,
                From = calculation.From,
                To = calculation.To,
                Cost = calculation.Cost
            };
        }

        public static Calculation ToCalculation(this AddCalculationDto addCalculationDto)
        {
            return new Calculation()
            {
                CourierId = addCalculationDto.CourierId,
                CalculationType = addCalculationDto.CalculationType,
                From = addCalculationDto.From,
                To = addCalculationDto.To,
                Cost = addCalculationDto.Cost
            };
        }
    }
}
