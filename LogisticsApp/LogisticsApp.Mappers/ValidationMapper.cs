using LogisticsApp.Domain.Models;
using LogisticsApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.Mappers
{
    public static class ValidationMapper
    {
        public static ValidationDto ToValidationDto(this Validation validation)
        {
            return new ValidationDto
            {
                Id = validation.Id,
                CourierId = validation.CourierId,
                ValidationType = validation.ValidationType,
                From = validation.From,
                To = validation.To
            };
        }

        public static Validation ToValidation(this AddValidationDto addValidationDto)
        {
            return new Validation()
            {
                CourierId = addValidationDto.CourierId,
                ValidationType = addValidationDto.ValidationType,
                From = addValidationDto.From,
                To = addValidationDto.To
            };
        }

    }
}
