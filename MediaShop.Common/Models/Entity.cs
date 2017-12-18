using System;

namespace MediaShop.Common.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatorId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifierId { get; set; }
    }
}