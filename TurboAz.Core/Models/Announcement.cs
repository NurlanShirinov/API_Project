using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboAz.Core.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AnnouncemenNumber { get; set; }
        public int AnnouncedCarId { get; set; }
        public Car? AnnouncedCar { get; set; }
        public int AnnouncedCityId { get; set; }
        public City? AnnouncedCity { get; set; }
        public int AnnouncedCarCategoryId { get; set; }
        public Category? AnnouncedCarCategory { get; set; }
        public decimal Price { get; set; }
        public int ViewCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsVip { get; set; }
        public DateTime? AnnouncementDeadline { get; set; }
    }
}
