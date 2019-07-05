using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models
{
    public class Car
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)] public string Name { get; set; }

        [Required(AllowEmptyStrings = false)] public string Company { get; set; }

        [Required(AllowEmptyStrings = false)] public string Class { get; set; }

        [DataType(DataType.Currency)] public double Price { get; set; }

        [Required(AllowEmptyStrings = false)] public string Image { get; set; }

        public string Options { get; set; }

        [Required(AllowEmptyStrings = false)] public string Transmission { get; set; }
    }
}