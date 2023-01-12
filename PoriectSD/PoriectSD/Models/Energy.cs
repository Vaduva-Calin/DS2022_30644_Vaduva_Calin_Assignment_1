using System;
using System.Collections.Generic;

namespace PoriectSD.Models
{
    public partial class Energy
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double EnergyConsumption { get; set; } = 0;
        public int DeviceId { get; set; }

    }
}
