using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.DTOs
{
    public class PackageDto
    {
        public int Id { get; set; }
        // weight - kg
        public int Weight { get; set; }
        // dimensions - cm3
        public int Dimensions { get; set; }
        public int CalculationId { get; set; }
    }
}
