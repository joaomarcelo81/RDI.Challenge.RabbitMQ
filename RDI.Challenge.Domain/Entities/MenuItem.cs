using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RDI.Challenge.Domain.Entities
{
    public class MenuItem
    {
        [Key]
        public long MenuItemId { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public string Description { get; set; }

    }
}
