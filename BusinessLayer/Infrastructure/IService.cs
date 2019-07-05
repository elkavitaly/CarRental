using System.Collections.Generic;
using BusinessLayer.Models;

namespace BusinessLayer.Infrastructure
{
    public interface IService
    {
        Car GetById(string id);

        IEnumerable<T> Filter<T>(IEnumerable<T> list, Dictionary<string, IEnumerable<string>> dictionary);

        IEnumerable<Car> Sort(IEnumerable<Car> cars, string parameter);

        Dictionary<string, List<string>> FilterParameters();

        IEnumerable<T> Search<T>(IEnumerable<T> cars, string pattern);

        double CalculateTotalPrice(double price, int days, bool driver);
    }
}