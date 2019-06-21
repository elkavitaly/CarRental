using System;

namespace BusinessLayer.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public bool Driver { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double Total { get; set; }
        public Guid CarId { get; set; }
    }
}