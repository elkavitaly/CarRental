using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    /// <summary>
    /// Represent car model in database
    /// </summary>
    public class CarEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Class { get; set; }
        [Range(0, double.MaxValue)] public double Price { get; set; }
        public string Image { get; set; }
        public string Options { get; set; }
        public string Transmission { get; set; }
    }
}