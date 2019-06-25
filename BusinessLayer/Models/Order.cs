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
        public Guid CarEntityId { get; set; }
        public Car Car { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Passport { get; set; }
    }
}