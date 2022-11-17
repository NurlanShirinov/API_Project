﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;

namespace TurboAz.Core.ResponseModels
{
    public class AnnoncementResponseModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Number { get; set; }
        public int CarId { get; set; }
        public Car? Car { get; set; }
        public int CityId { get; set; }
        public City? City { get; set; }
        public int CategoryId { get; set; }
        public Category? CarCategory { get; set; }
        public decimal Price { get; set; }
        public int ViewCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsVip { get; set; }
        public DateTime? Expired { get; set; }
        public string CarName { get; set; }
        public string CategoryName { get; set; }
        public string  CityName { get; set; }
    }
}
