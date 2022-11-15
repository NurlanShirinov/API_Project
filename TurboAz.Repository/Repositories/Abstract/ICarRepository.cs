﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Core.Models;

namespace TurboAz.Repository.Repositories.Abstract
{
    public interface ICarRepository
    {
        Task<int> AddCar(Car car);
        Task<bool> DeleteCar(int id);
        Task<Car> UpdateCar(Car car);
        Task<Car> GetById(int id);
        Task<IEnumerable<Car>> GetAll();
    }
}
