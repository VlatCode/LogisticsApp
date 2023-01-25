using LogisticsApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.Services.Interfaces
{
    public interface ICourierService
    {
        List<CourierDto> GetAllCouriers();
        CourierDto GetById(int id);
        void AddCourier(AddCourierDto courier);
        void DeleteCourier(int id);
    }
}
