using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Producto
    {
        public int Id { get; set; }
        public string CodigoSKU { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        public Proveedor Proveedor { get; set; }
        public decimal StockMinimo { get; set; }
        public decimal StockActual { get; set; }
        public decimal PorcentajeGanancia { get; set; }
        public string UrlImagen { get; set; }
        public bool Activo { get; set; } = true;

        public List<PrecioCompra> PreciosCompra { get; set; } = new List<PrecioCompra>();

    }
}
