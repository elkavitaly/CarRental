using System;
using System.Collections.Generic;
using BusinessLayer.Models;

namespace BusinessLayer.Infrastructure
{
    public interface IService
    {
        Car GetById(Guid id);

        IEnumerable<Car> Filter(Dictionary<string, IEnumerable<string>> dictionary);

        IEnumerable<Car> Sort(IEnumerable<Car> cars, string parameter);

        Dictionary<string, List<string>> FilterParameters();
    }
}