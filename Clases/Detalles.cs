using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tienda.Interfaces;
using Tienda.Models;

namespace Tienda.Clases
{
    class Detalles : Icrud, IDetalle
    {
        public void Menu()
        {
            Console.WriteLine("============================");
            Console.WriteLine("||           Menu         ||");
            Console.WriteLine("============================");
            Console.WriteLine("||   1) Buscar detalle    ||");
            Console.WriteLine("||   2) Crear detalle     ||");
            Console.WriteLine("||  3) Eliminar detalle   ||");
            Console.WriteLine("|| 4) Actualizar detalle  ||");
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
            Console.WriteLine("Buscar detalle");
            Console.Write("Buscar: ");
            string buscar = Console.ReadLine();

            using (TiendaContext tiendaContext = new TiendaContext())
            {
                IQueryable<Detalle> detalles = tiendaContext.Detalles.Where(d => d.Producto.Nombre.Contains(buscar));
                foreach (Detalle detalle in detalles)
                {
                    Console.WriteLine(detalles);
                }
            }
        }

        public void Crear()
        {
            Console.WriteLine("Crear detalle");
            Detalle detalle = new Detalle();
            detalle = LlenarDetalle(detalle);

            using (TiendaContext tiendaContext = new TiendaContext())
            {
                try
                {
                    tiendaContext.Add(detalle);
                    tiendaContext.SaveChanges();
                    Console.WriteLine("Detalle creado");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error al crear detalle: verifique la información introducida y vuelva a intenta");
                    Menu();
                }
            }
        }

        public void Editar()
        {
            Console.WriteLine("Actualizar detalle");
            Detalle detalle = SeleccionarDetalle();
            detalle = LlenarDetalle(detalle);

            using (TiendaContext tiendaContext = new TiendaContext())
            {
                tiendaContext.Update(detalle);
                tiendaContext.SaveChanges();
                Console.WriteLine("Detalle actualizado exitosamente");
            }
        }

        public void Eliminar()
        {
            Console.WriteLine("Eliminar detalle");
            Detalle detalle = SeleccionarDetalle();

            using (TiendaContext tiendaContext = new TiendaContext())
            {
                tiendaContext.Remove(detalle);
                tiendaContext.SaveChanges();
                Console.WriteLine("Detalle eliminado exitosamente :)");
            }
        }

        public Detalle LlenarDetalle(Detalle detalle)
        {
            try
            {
                Console.WriteLine("Producto id");
                detalle.ProductoId = uint.Parse(Console.ReadLine());
                Console.WriteLine("Venta id");
                detalle.VentaId = uint.Parse(Console.ReadLine());
                Console.WriteLine("Subtotal");
                detalle.Subtotal = decimal.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine("No es el formato correcto:( Intente crear el detalle nuevamente");
                Crear();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear el detalle nuevamente");
                Crear();
            }
            return detalle;
        }

        public Detalle SeleccionarDetalle()
        {
            Buscar();
            Console.Write("Selecciona el código del detalle: ");
            uint id = uint.Parse(Console.ReadLine());

            using (TiendaContext tiendaContext = new TiendaContext())
            {
                Detalle detalle = tiendaContext.Detalles.Find(id);
                if (detalle == null)
                {
                    SeleccionarDetalle();
                }
                return detalle;
            }
        }
    }
}
