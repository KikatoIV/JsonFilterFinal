using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JsonFilter.Models
{
    public class SalesHistory
    {
        public int year { get; set; }
        public int vehiclesSold { get; set; }
    }
    public class CarModel
    {
        public int id { get; set; }
        public string model { get; set; }
        public string colour { get; set; }
        public string manufacturer { get; set; }
       
        public List<SalesHistory> salesHistory { get; set; }
    }

}
