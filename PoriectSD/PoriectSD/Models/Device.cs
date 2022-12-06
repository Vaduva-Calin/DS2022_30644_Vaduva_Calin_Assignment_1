using System;
using System.Collections.Generic;

namespace PoriectSD.Models
{
    public partial class Device
    {
        public Device()
        {
            Energies = new HashSet<Energy>();
        }

        public int Id { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; } = null!;
        public double MaxConsumption { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<Energy> Energies { get; set; }
    }
}
