using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models
{
    public class Order
    {
        [Required] public Guid Id { get; set; }

        public bool Driver { get; set; }

        public DateTime DateTime { get; set; }

        public string Status { get; set; }

        [Required] [DataType(DataType.Date)] public DateTime Start { get; set; }

        [Required] [DataType(DataType.Date)] public DateTime End { get; set; }

        [Required] public double Total { get; set; }

        [Required] public Guid CarEntityId { get; set; }

        public Car Car { get; set; }

        [Required] public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 3)]
        public string FullName { get; set; }

        [Required]
        [RegularExpression("^([A-Z]{2}[0-9]{5})|([0-9]{9})$", ErrorMessage =
            "Значение {0} должно соответствовать шаблону AA11111 или 000000000.")]
        [StringLength(256, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 7)]
        public string Passport { get; set; }
    }
}