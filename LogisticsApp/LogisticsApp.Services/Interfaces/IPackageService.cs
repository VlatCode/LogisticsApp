using LogisticsApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.Services.Interfaces
{
    public interface IPackageService
    {
        List<PackageDto> GetAllPackages();
        PackageDto GetById(int id);
        void AddPackage(AddPackageDto package);
        void DeletePackage (int id);
    }
}
