using System;
using System.Collections.Generic;

namespace my_signalr_chathub_backend.Models.SuperTienda
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public string IdPais { get; set; } = null!;
        public string? Pais { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
