using LogisticsApp.Domain.Models;
using LogisticsApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.Mappers
{
    public static class CourierMapper
    {
        public static CourierDto ToCourierDto(this Courier courier)
        {
            return new CourierDto
            {
                Id = courier.Id,
                CourierName = courier.CourierName
            };
        }

        public static Courier ToCourier(this AddCourierDto addCourierDto)
        {
            return new Courier()
            {
                CourierName = addCourierDto.CourierName,
            };
        }
    }
}
