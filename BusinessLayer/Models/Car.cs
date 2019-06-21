using System;

namespace BusinessLayer.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Class { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string Options { get; set; }
        public string Transmission { get; set; }
    }
}