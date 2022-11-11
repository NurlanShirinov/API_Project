
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboAz.Core.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string? Vendor { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public string? Color { get; set; }
    }
}
