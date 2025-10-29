using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Venta
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public Usuario Usuario { get; set; }
        public string NumeroFactura { get; set; }
        public string MetodoPago { get; set; }

        public List<VentaLinea> Lineas { get; set; } = new List<VentaLinea>();
        public decimal Total => Lineas?.Sum(l => l.Subtotal) ?? 0;


    }
}
