using System;

namespace DataLayer.Models
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public bool Driver { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double Total { get; set; }
        public virtual Guid CarEntityId { get; set; }
        public Guid UserId { get; set; }
    }
}