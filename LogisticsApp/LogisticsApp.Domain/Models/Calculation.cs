using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.Domain.Models
{
    public class Calculation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CourierId { get; set; }
        // 0 - weight; 1 - dimensions
        public int CalculationType { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public double Cost { get; set; }
        public Courier Courier { get; set; }
    }
}
