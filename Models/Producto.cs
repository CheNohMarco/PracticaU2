using System;
using System.Collections.Generic;
using System.Text;

namespace Tienda.Models
{
    public class Producto
    {
        public uint Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Tamano { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }

        public override string ToString()
        {
            return $"{Id}) {Nombre} precio {Precio:N2}MXN";

        }
    }
}
