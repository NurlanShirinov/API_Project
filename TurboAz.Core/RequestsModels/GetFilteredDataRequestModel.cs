using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboAz.Core.RequestsModels
{
    public class GetFilteredDataRequestModel
    {
        [Range(typeof(DateTime), "1/1/1900", "1/1/2023",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? StartDate { get; set; }
        [Range(typeof(DateTime), "1/2/1900", "1/2/2023",
         ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? EndDate { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string? Color { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public bool IsVip { get; set; }
        public bool IsActive { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Vendor { get; set; }
    }
}
