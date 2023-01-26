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
    public class PackageService : IPackageService
    {
        private readonly IRepository<Package> _packageRepository;

        public PackageService(IRepository<Package> packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public void AddPackage(AddPackageDto addPackageDto)
        {
            // 1. Validation
            if (addPackageDto.Weight == null || addPackageDto.Dimensions == null)
            {
                throw new InvalidEntryException("All fields are required! Try again.");
            }
            // 2. Map to domain model
            Package newPackage = addPackageDto.ToPackage();
            // 3. Add to db
            _packageRepository.Add(newPackage);
        }

        public void DeletePackage(int id)
        {
            Package packageDb = _packageRepository.GetById(id);

            if (packageDb == null)
            {
                throw new NotFoundException($"Package with ID {id} was not found.");
            }

            _packageRepository.Delete(packageDb);
        }

        public List<PackageDto> GetAllPackages()
        {
            var packagesDb = _packageRepository.GetAll();
            return packagesDb.Select(x => x.ToPackageDto()).ToList();
        }

        public PackageDto GetById(int id)
        {
            Package packageDb = _packageRepository.GetById(id);

            if (packageDb == null)
            {
                throw new NotFoundException($"Package with ID {id} was not found.");
            }
            if (id == null)
            {
                throw new InvalidEntryException("Package ID is required");
            }

            PackageDto packageDto = packageDb.ToPackageDto();
            return packageDto;
        }
    }
}
