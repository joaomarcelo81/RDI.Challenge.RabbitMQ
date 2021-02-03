using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RDI.Challenge.Domain.Entities
{
    public class Order
    {
        [Key]
        public long OrderId { get; set; }
        public int TableNumber { get; set; }
        public virtual IList<MenuItem> MenuItems { get; set; }

    }
}
