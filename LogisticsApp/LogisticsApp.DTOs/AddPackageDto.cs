using LogisticsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.DTOs
{
    public class AddPackageDto
    {
        public int Weight { get; set; }
        public int Dimensions { get; set; }
        public int CalculationId { get; set; }
    }
}
