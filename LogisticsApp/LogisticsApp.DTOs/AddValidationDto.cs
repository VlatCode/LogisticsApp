using LogisticsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.DTOs
{
    public class AddValidationDto
    {
        public int CourierId { get; set; }
        // 0 - weight; 1 - dimensions
        public int ValidationType { get; set; }
        public int From { get; set; }
        public int To { get; set; }
    }
}
