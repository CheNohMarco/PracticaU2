using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tienda.Interfaces;
using Tienda.Models;

namespace Tienda.Clases
{
     class Ventas : Icrud, IVenta
    {
        public void Menu()
        {
            Console.WriteLine("============================");
            Console.WriteLine("||           Menu         ||");
            Console.WriteLine("============================");
            Console.WriteLine("||   1) Buscar venta      ||");
            Console.WriteLine("||   2) Crear venta       ||");
            Console.WriteLine("||  3) Eliminar venta     ||");
            Console.WriteLine("|| 4) Actualizar venta    ||");
            Console.WriteLine("||       0) Salir         ||");
            Console.WriteLine("============================");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    Buscar();
                    break;
                case "2":
                    Crear();
                    break;
                case "3":
                    Editar();
                    break;
                case "4":
                    Eliminar();
                    break;
                case "0": return;
            }
            Menu();
        }

        public void Buscar()
        {
            Console.WriteLine("Buscar venta");
            Console.Write("Buscar: ");
            string buscar = Console.ReadLine();

            using (TiendaContext tiendaContect = new TiendaContext())
            {
                IQueryable<Venta> ventas = tiendaContect.Ventas.Where(p => p.Cliente.Contains(buscar));
                foreach (Venta venta in ventas)
                {
                    Console.WriteLine(venta);
                }
            }
        }

        public void Crear()
        {
            Console.WriteLine("Crear venta");
            Venta venta = new Venta();
            venta = LlenarVenta(venta);


            using (TiendaContext tiendaContext = new TiendaContext())
            {
                tiendaContext.Add(venta);
                tiendaContext.SaveChanges();
                Console.WriteLine("Venta creada");
            }
        }

        public void Editar()
        {
            Console.WriteLine("Actualizar venta");
            Venta venta = SeleccionarVenta();
            venta = LlenarVenta(venta);

            using (TiendaContext tiendaContext = new TiendaContext())
            {
                tiendaContext.Update(venta);
                tiendaContext.SaveChanges();
                Console.WriteLine("Venta actualizada exitosamente");
            }
        }

        public void Eliminar()
        {
            Console.WriteLine("Eliminar venta");
            Venta venta = SeleccionarVenta();

            using (TiendaContext tiendaContext = new TiendaContext())
            {
                tiendaContext.Remove(venta);
                tiendaContext.SaveChanges();
                Console.WriteLine("Venta eliminada exitosamente :)");
            }
        }

        public Venta LlenarVenta(Venta venta)
        {
            try
            {
                Console.WriteLine("Total");
                venta.Total = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Fecha");
                venta.Fecha = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Cliente");
                venta.Cliente = Console.ReadLine();

            }
            catch (FormatException e)
            {
                Console.WriteLine("No es el formato correcto:( Intente crear la venta  nuevamente");
               Crear();

            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear la venta nuevamente");
                Crear();
            }

            return venta;
        }

        public Venta SeleccionarVenta()
        {
            Buscar();
            Console.Write("Selecciona el código de la venta: ");
            uint id = uint.Parse(Console.ReadLine());

            using (TiendaContext tiendaContext = new TiendaContext())
            {
                Venta venta = tiendaContext.Ventas.Find(id);
                if (venta == null)
                {
                    SeleccionarVenta();
                }
                return venta;
            }
        }
    }
}
