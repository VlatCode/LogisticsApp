using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.Domain.Models
{
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        // weight - kg
        public int Weight { get; set; }
        // dimensions - cm3
        public int Dimensions { get; set; }
        public int CalculationId { get; set; }
        public Calculation Calculation { get; set; }
    }
}
