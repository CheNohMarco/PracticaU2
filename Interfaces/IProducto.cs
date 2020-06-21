using System;
using System.Collections.Generic;
using System.Text;
using Tienda.Models;

namespace Tienda.Interfaces
{
    public interface IProducto
    {
        Producto SeleccionarProducto();

        Producto LlenarProducto(Producto producto);
    }
}
