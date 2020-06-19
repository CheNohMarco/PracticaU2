using System;
using System.Collections.Generic;
using System.Text;

namespace Tienda.Models
{
    class Venta
    {
        public uint Id { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public virtual ICollection<Detalle> Detalles { get; set; }

        public override string ToString()
        {
            return $"{Id}) {Cliente} total  {Total:N2}MXN";
        }
    }
}
