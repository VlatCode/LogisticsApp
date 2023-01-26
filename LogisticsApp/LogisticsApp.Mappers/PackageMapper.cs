using LogisticsApp.Domain.Models;
using LogisticsApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.Mappers
{
    public static class PackageMapper
    {
        public static PackageDto ToPackageDto(this Package package)
        {
            return new PackageDto
            {
                Id = package.Id,
                Weight = package.Weight,
                Dimensions = package.Dimensions,
                CalculationId = package.CalculationId
            };
        }

        public static Package ToPackage(this AddPackageDto addPackageDto)
        {
            return new Package()
            {
                Weight = addPackageDto.Weight,
                Dimensions = addPackageDto.Dimensions,
                CalculationId = addPackageDto.CalculationId,
            };
        }
    }
}
