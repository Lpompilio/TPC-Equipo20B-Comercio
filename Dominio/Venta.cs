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
        public string NumeroNC { get; set; }         // NUEVO
        public string MetodoPago { get; set; }

        public List<VentaLinea> Lineas { get; set; } = new List<VentaLinea>();

        public decimal Total
        {
            get
            {
                if (Lineas == null || Lineas.Count == 0)
                    return 0;
                return Lineas.Sum(l => l.Subtotal);
            }
        }

        // Total que viene de la base de datos
        public decimal TotalBD { get; set; }

        public bool Cancelada { get; set; }
        public string MotivoCancelacion { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public Usuario UsuarioCancelacion { get; set; }

    }
}
