using System;
using System.Collections.Generic;

namespace my_signalr_chathub_backend.Models.SuperTienda
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public string IdCategoria { get; set; } = null!;
        public string? Categoría { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
