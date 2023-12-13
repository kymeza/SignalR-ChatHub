using System;
using System.Collections.Generic;

namespace my_signalr_chathub_backend.Models.SuperTienda
{
    public partial class OrderDetail
    {
        public double LineaDetalle { get; set; }
        public string IdPedido { get; set; } = null!;
        public string ArticuloId { get; set; } = null!;
        public double? Cantidad { get; set; }
        public double? Descuento { get; set; }
        public double? CosteEnvío { get; set; }

        public virtual Order IdPedido1 { get; set; } = null!;
        public virtual Product IdPedidoNavigation { get; set; } = null!;
    }
}
