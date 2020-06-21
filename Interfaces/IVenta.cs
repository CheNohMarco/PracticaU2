using System;
using System.Collections.Generic;
using System.Text;
using Tienda.Models;

namespace Tienda.Interfaces
{
    public interface IVenta
    {
        Venta LlenarVenta(Venta venta);

        Venta SeleccionarVenta();
    }
}
