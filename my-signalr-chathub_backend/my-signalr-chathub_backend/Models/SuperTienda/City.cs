using System;
using System.Collections.Generic;

namespace my_signalr_chathub_backend.Models.SuperTienda
{
    public partial class City
    {
        public City()
        {
            Orders = new HashSet<Order>();
        }

        public string IdCiudad { get; set; } = null!;
        public string IdPais { get; set; } = null!;
        public string? Ciudad { get; set; }

        public virtual Country IdPaisNavigation { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
