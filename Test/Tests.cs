using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Infrastructure;
using BusinessLayer.Models;
using BusinessLayer.Services;
using DataLayer.Infrastructure;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class Tests
    {
        private readonly IUnitOfWork _unitOfWork = new UnitOfWork();

        [Test]
        public void CalculatePrice([Range(100, 250, 10)] double price, [Range(1, 20)] int days,
            [Values(true, false)] bool driver)
        {
            var result = driver ? (price + 50) * days : price * days;
            Assert.AreEqual(result, new Service(_unitOfWork).CalculateTotalPrice(price, days, driver));
        }

        [Test]
        public void Search_ClassNameFull_size()
        {
            var cars = new List<Car>()
            {
                new Car()
                {
                    Class = "Full-size", Company = string.Empty, Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 0, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Standard", Company = string.Empty, Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 0, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Economic", Company = string.Empty, Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 0, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Full-size", Company = string.Empty, Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 0, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Premium", Company = string.Empty, Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 0, Transmission = string.Empty
                }
            };

            var result = new Service(_unitOfWork).Search(cars, "Full-size");
            var pattern = cars.Where(car => car.Class == "Full-size");

            Assert.IsTrue(CompareCollections(result, pattern));
        }

        [Test]
        public void Search_NameToyotaIgnoreCase()
        {
            var cars = new List<Car>()
            {
                new Car()
                {
                    Class = "Full-size", Company = "Toyota", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 0, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Standard", Company = "Renault", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 0, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Economic", Company = "Mazda", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 0, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Full-size", Company = "Toyota", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 0, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Premium", Company = "Toyota", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 0, Transmission = string.Empty
                }
            };

            var result = new Service(_unitOfWork).Search(cars, "toyota");
            var pattern = cars.Where(car => car.Company == "Toyota");

            Assert.IsTrue(CompareCollections(result, pattern));
        }

        [Test]
        public void Sort_PriceUp()
        {
            var cars = new List<Car>()
            {
                new Car()
                {
                    Class = "Full-size", Company = "Toyota", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 10, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Standard", Company = "Renault", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 20, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Economic", Company = "Mazda", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 15, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Full-size", Company = "Porshe", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 10, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Premium", Company = "Toyota", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 25, Transmission = string.Empty
                }
            };

            var result = new Service(_unitOfWork).Sort(cars, "price_down");
            var pattern = cars.OrderBy(c => c.Price);

            Assert.IsTrue(CompareCollectionsСonsistently(result, pattern));
        }

        [Test]
        public void Sort_PriceDown()
        {
            var cars = new List<Car>()
            {
                new Car()
                {
                    Class = "Full-size", Company = "Toyota", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 15, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Standard", Company = "Renault", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 20, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Economic", Company = "Mazda", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 15, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Full-size", Company = "Toyota", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 50, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Premium", Company = "Toyota", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 40, Transmission = string.Empty
                }
            };

            var result = new Service(_unitOfWork).Sort(cars, "price_up");
            var pattern = cars.OrderByDescending(c => c.Price);

            Assert.IsTrue(CompareCollectionsСonsistently(result, pattern));
        }

        [Test]
        public void Sort_Name()
        {
            var cars = new List<Car>()
            {
                new Car()
                {
                    Class = "Full-size", Company = "Toyota", Image = string.Empty, Name = "Mazda 3",
                    Options = string.Empty, Price = 10, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Standard", Company = "Renault", Image = string.Empty, Name = "Porshe Careera",
                    Options = string.Empty, Price = 20, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Economic", Company = "Mazda", Image = string.Empty, Name = "Mazda 6",
                    Options = string.Empty, Price = 15, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Full-size", Company = "Toyota", Image = string.Empty, Name = "Toyota Land Cruiser",
                    Options = string.Empty, Price = 5, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Premium", Company = "Toyota", Image = string.Empty, Name = "Toyota Camry",
                    Options = string.Empty, Price = 25, Transmission = string.Empty
                }
            };

            var result = new Service(_unitOfWork).Sort(cars, "name");
            var pattern = cars.OrderBy(c => c.Name);

            Assert.IsTrue(CompareCollectionsСonsistently(result, pattern));
        }

        [Test]
        public void Filter_ClassFullSize()
        {
            var cars = new List<Car>()
            {
                new Car()
                {
                    Class = "Full-size", Company = "Toyota", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 10, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Standard", Company = "Renault", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 20, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Economic", Company = "Mazda", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 15, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Full-size", Company = "Toyota", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 5, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Premium", Company = "Toyota", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 25, Transmission = string.Empty
                }
            };

            var parameters = new Dictionary<string, IEnumerable<string>> {{"Class", new[] {"Full-size"}}};
            var result = new Service(_unitOfWork).Filter(cars, parameters);
            var pattern = cars.Where(car => car.Class.Equals("Full-size"));

            Assert.IsTrue(CompareCollections(result, pattern));
        }

        [Test]
        public void Filter_ClassPremiumOrPrice10()
        {
            var cars = new List<Car>()
            {
                new Car()
                {
                    Class = "Full-size", Company = "Toyota", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 10, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Premium", Company = "Renault", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 10, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Economic", Company = "Mazda", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 15, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Full-size", Company = "Toyota", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 5, Transmission = string.Empty
                },
                new Car()
                {
                    Class = "Premium", Company = "Toyota", Image = string.Empty, Name = string.Empty,
                    Options = string.Empty, Price = 25, Transmission = string.Empty
                }
            };

            var parameters = new Dictionary<string, IEnumerable<string>>
                {{"Class", new[] {"Premium"}}, {"Price", new[] {"10"}}};
            var result = new Service(_unitOfWork).Filter(cars, parameters);
            var pattern = cars.Where(car => car.Class.Equals("Premium")).ToList();
            pattern.AddRange(cars.Where(car => car.Price.Equals(10)));


            Assert.IsTrue(CompareCollections(result, pattern.Distinct()));
        }

        private static bool CompareCollections<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first.ToList().Count != second.ToList().Count)
            {
                return false;
            }

            foreach (var element in first)
            {
                if (!second.Contains(element))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CompareCollectionsСonsistently<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstList = first.ToList();
            var secondList = second.ToList();

            if (firstList.Count != secondList.Count)
            {
                return false;
            }

            for (var i = 0; i < firstList.Count; i++)
            {
                if (!firstList[i].Equals(secondList[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}