using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Compra
    {
        public int Id { get; set; }
        public Proveedor Proveedor { get; set; }
        public DateTime Fecha { get; set; }
        public Usuario Usuario { get; set; }
        public string Observaciones { get; set; }

        public List<CompraLinea> Lineas { get; set; } = new List<CompraLinea>();
    }
}
