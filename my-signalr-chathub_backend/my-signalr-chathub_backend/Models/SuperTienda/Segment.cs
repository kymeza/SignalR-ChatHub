using System;
using System.Collections.Generic;

namespace my_signalr_chathub_backend.Models.SuperTienda
{
    public partial class Segment
    {
        public Segment()
        {
            Clients = new HashSet<Client>();
        }

        public string IdSegmento { get; set; } = null!;
        public string? Segmento { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
