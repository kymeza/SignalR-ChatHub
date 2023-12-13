using System;
using System.Collections.Generic;

namespace my_signalr_chathub_backend.Models.SuperTienda
{
    public partial class CourierPriority
    {
        public CourierPriority()
        {
            Orders = new HashSet<Order>();
        }

        public string IdModoEnvio { get; set; } = null!;
        public string? ModoEnvío { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
