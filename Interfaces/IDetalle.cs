using System;
using System.Collections.Generic;
using System.Text;
using Tienda.Models;

namespace Tienda.Interfaces
{
    public interface IDetalle
    {
        Detalle LlenarDetalle(Detalle detalle);

        Detalle SeleccionarDetalle();
    }
}
