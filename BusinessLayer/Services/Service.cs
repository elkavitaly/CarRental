using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BusinessLayer.Factory;
using BusinessLayer.Infrastructure;
using BusinessLayer.Models;

namespace BusinessLayer.Services
{
    public class Service : IService
    {
        private readonly IUnitOfWork _unitOfWork;

        public Service() => _unitOfWork = RepositoryFactory.Instance.Initialize;

        public Car GetById(string id) => _unitOfWork.Cars.GetById(id);

        public IEnumerable<Car> Filter(Dictionary<string, IEnumerable<string>> dict)
        {
            var list = _unitOfWork.Cars.GetAll().ToList();
//            var dict = Util.Deserialize<Dictionary<string, IEnumerable<string>>>(parameters);

            if (dict == null || dict.Keys.Count == 0)
            {
                return list;
            }

            var result = new List<Car>();
            foreach (var element in dict.Keys)
            {
                foreach (var val in dict[element])
                {
                    var l = list.Where(e => e.GetType().GetProperty(element)?.GetValue(e).ToString() == val);
                    foreach (var car in l)
                    {
                        if (!result.Contains(car))
                        {
                            result.Add(car);
                        }
                    }
                }
            }

            return result;
        }

        public Dictionary<string, List<string>> FilterParameters()
        {
            var dict = new Dictionary<string, List<string>>();
            var cars = _unitOfWork.Cars.GetAll().ToList();

            var classes = cars.GroupBy(e => e.Class);
            dict.Add("Class", new List<string>());
            foreach (var element in classes)
            {
                dict["Class"].Add(element.First().Class);
            }

            var companies = cars.GroupBy(e => e.Company).ToList();
            dict.Add("Company", new List<string>());
            foreach (var company in companies)
            {
                dict["Company"].Add(company.First().Company);
            }

            return dict;
        }

        public IEnumerable<T> Search<T>(IEnumerable<T> items, string pattern)
        {
            var result = new List<T>();
            var properties = typeof(T).GetProperties();

            foreach (var item in items)
            {
                foreach (var property in properties)
                {
                    var value = item.GetType().GetProperty(property.Name)?.GetValue(item).ToString();
                    if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    {
                        if (!result.Contains(item))
                        {
                            result.Add(item);
                        }
                    }
                }
            }

            return result.Distinct();
        }

        public IEnumerable<Car> Sort(IEnumerable<Car> cars, string parameter)
        {
            switch (parameter)
            {
                case "price_down":
                    return cars.OrderBy(c => c.Price);
                case "name":
                    return cars.OrderBy(c => c.Name);
                default:
                    return cars.OrderByDescending(c => c.Price);
            }
        }
    }
}