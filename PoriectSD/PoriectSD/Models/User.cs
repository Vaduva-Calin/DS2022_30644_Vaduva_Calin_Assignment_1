using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoriectSD.Models
{
    public partial class User
    {
        public User()
        {
            Devices = new HashSet<Device>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserName { get; set; } = null!;

        public virtual ICollection<Device> Devices { get; set; }
    }
}
