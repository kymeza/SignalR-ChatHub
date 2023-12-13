using System;
using System.Collections.Generic;

namespace my_signalr_chathub_backend.Models.SuperTienda
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string IdArtículo { get; set; } = null!;
        public string IdSubCategoria { get; set; } = null!;
        public string? Producto { get; set; }
        public double? PrecioUnitario { get; set; }
        public double? CostoUnitario { get; set; }

        public virtual SubCategory IdSubCategoriaNavigation { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
