using System;
using System.Linq;
using Tienda.Models;
using System.Security.Cryptography;
using System.Text;

namespace Tienda
{
    class Program
    {
        static void Main(string[] args)
        {
            Bienvenido();
        }

        //Login
        public static void Bienvenido()
        {
            Console.WriteLine("============================");
            Console.WriteLine("||        Bienvenido      ||");
            Console.WriteLine("============================");
            Console.WriteLine("||                        ||");
            Console.WriteLine("||        1) Entrar       ||");
            Console.WriteLine("||    2) Crear usuario    ||");
            Console.WriteLine("||                        ||");
            Console.WriteLine("============================");

            string opcion = Console.ReadLine();
            switch(opcion)
            {
                case "1":
                    Login();
                    break;
                case "2":
                    CrearUsuario();
                    break;
                case "0": return;
                default:
                    Console.WriteLine("Introduzca una opción valida");
                    Bienvenido();
                    break;
            }
        }

        public static void Login()
        {
            Console.WriteLine("============================");
            Console.WriteLine("||          Entrar        ||");
            Console.WriteLine("============================");
            Console.WriteLine("============================");
            Console.WriteLine("                            ");

            try
            {
                Console.WriteLine("Nombre");
                string Usuario = Console.ReadLine();
                Console.WriteLine("Contraseña");
                string password = Console.ReadLine();
                string contrasena = GetSHA1(password);

                using (TiendaContext context = new TiendaContext())
                {
                    IQueryable<Usuario> usuario = context.Usuarios.Where(u => u.nombre.Contains(Usuario) && u.contrasena.Contains(contrasena));

                    if (usuario.Count() == 0)
                    {
                        Console.WriteLine("**No se encontro el usuario verifica la información :(**");
                        Bienvenido();
                    }
                    else
                    {
                        Console.WriteLine("Bienvenido");
                        Menu();
                    }
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Ocurrio un error de conexión");
                BuscarProductos();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear el producto nuevamente");
                BuscarProductos();
            }
        }

        public static void CrearUsuario()
        {
            Console.WriteLine("============================");
            Console.WriteLine("||      Crear usuario     ||");
            Console.WriteLine("============================");
            Console.WriteLine("============================");
            Console.WriteLine("                            ");

            Usuario usuario = new Usuario();
            usuario = LlenarUsuario(usuario);

            using (TiendaContext tiendaContext = new TiendaContext())
            {
                tiendaContext.Add(usuario);
                tiendaContext.SaveChanges();
                Console.WriteLine("Usuario creado exitosamente");
                Login();
            }

        }

        public static Usuario LlenarUsuario(Usuario usuario)
        {
            try
            {
                Console.WriteLine("Nombre");
                usuario.nombre = Console.ReadLine();
                Console.WriteLine("Contraseña");
                string passnocrypt = Console.ReadLine();
                usuario.contrasena = GetSHA1(passnocrypt);

            }
            catch (FormatException e)
            {
                Console.WriteLine("Hubo un error, vuelva a intentarlo");
                CrearUsuario();

            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente nuevamente");
                CrearUsuario();
            }
            return usuario;
        }

        public static string GetSHA1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        //Menu
        public static void Menu()
        {
            Console.WriteLine("============================");
            Console.WriteLine("||           Menu         ||");
            Console.WriteLine("============================");
            Console.WriteLine("||   1) Buscar producto   ||");
            Console.WriteLine("||   2) Crear producto    ||");
            Console.WriteLine("||  3) Eliminar producto  ||");
            Console.WriteLine("|| 4) Actualizar producto ||");
            Console.WriteLine("||       0) Salir         ||");
            Console.WriteLine("============================");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    BuscarProductos();
                    break;
                case "2":
                    CrearDetalle();
                    break;
                case "3":
                    ActualizarProducto();
                    break;
                case "4":
                    EliminarProducto();
                    break;
                case "5":
                    CrearVenta();
                    break;
                case "0": return;
            }
            Menu();
        }
        //venta
        public static void BuscarVenta()
        {
            Console.WriteLine("Buscar venta");
            Console.Write("Buscar: ");
            string buscar = Console.ReadLine();

            using (TiendaContext tiendaContect = new TiendaContext())
            {
                IQueryable<Venta> ventas = tiendaContect.Ventas.Where(p => p.Cliente.Contains(buscar));
                foreach(Venta venta in ventas)
                {
                    Console.WriteLine(venta);
                }
            }
        }

        public static void CrearVenta()
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

        public static Venta LlenarVenta(Venta venta)
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
            catch(FormatException e)
            {
                Console.WriteLine("No es el formato correcto:( Intente crear la venta  nuevamente");
                CrearVenta();

            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear la venta nuevamente");
                CrearVenta();
            }

            return venta;
        }

        public static Venta  SeleccionarVenta()
        {

            BuscarVenta();
            Console.Write("Selecciona el código de la venta: ");
            uint id = uint.Parse(Console.ReadLine());

            using (TiendaContext tiendaContext = new TiendaContext())
            {
                Venta venta = tiendaContext.Ventas.Find(id);
                if (venta == null)
                {
                    SeleccionarProducto();
                }
                return venta;
            }
        }

        public static void ActualizarVenta()
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

        public static void EliminarVenta()
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


        //productos
        public static void BuscarProductos()
        {
            try
            {
                Console.WriteLine("Buscar prodcutos");
                Console.Write("Buscar: ");
                string buscar = Console.ReadLine();

                using (TiendaContext context = new TiendaContext())
                {
                    IQueryable<Producto> productos = context.Productos.Where(p => p.Nombre.Contains(buscar));

                    if (productos.Count() == 0)
                    {
                        Console.WriteLine("No se obtuvieron resultado en la busqueda");
                    }
                    else
                    {
                        foreach (Producto producto in productos)
                        {
                            Console.WriteLine(producto);
                        }
                    }
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Ocurrio un error de conexión");
                BuscarProductos();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear el producto nuevamente");
                BuscarProductos();
            }
        }

        public static void CrearProductos()
        {
            try
            {
                Console.WriteLine("Crear producto");
                Producto producto = new Producto();
                producto = LlenarProducto(producto);

                using (TiendaContext context = new TiendaContext())
                {
                    context.Add(producto);
                    context.SaveChanges();
                    Console.WriteLine("Producto creado exitosamente");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Ocurrio un error de conexión");
                CrearProductos();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear el producto nuevamente");
                CrearProductos();
            }
        }

        public static Producto LlenarProducto(Producto producto)
        {
            try
            {
                Console.Write("Nombre: ");
                producto.Nombre = Console.ReadLine();
                Console.Write("Descripcion: ");
                producto.Descripcion = Console.ReadLine();
                Console.Write("Precio: ");
                producto.Precio = decimal.Parse(Console.ReadLine());
                Console.Write("Costo: ");
                producto.Costo = decimal.Parse(Console.ReadLine());
                Console.Write("Cantidad: ");
                producto.Cantidad = decimal.Parse(Console.ReadLine());
                Console.Write("Tamaño: ");
                producto.Tamano = Console.ReadLine();
            }
            catch (FormatException e)
            {
                Console.WriteLine("No es el formato correcto:( Intente crear el producto nuevamente");
                CrearProductos();
            } catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear el producto nuevamente");
                CrearProductos();
            }
            return producto;
        }

        public static Producto SeleccionarProducto()
        {
            BuscarProductos();
            Console.Write("Selecciona el código de producto: ");
            uint id = uint.Parse(Console.ReadLine());

            using (TiendaContext context = new TiendaContext())
            {
                Producto producto = context.Productos.Find(id);
                if (producto == null)
                {
                    SeleccionarProducto();
                }
                return producto;
            }
        }

        public static void ActualizarProducto()
        {
            try
            {
                Console.WriteLine("Actualizar producto");
                Producto producto = SeleccionarProducto();
                producto = LlenarProducto(producto);

                using (TiendaContext context = new TiendaContext())
                {
                    context.Update(producto);
                    context.SaveChanges();
                    Console.WriteLine("Producto actualizado exitosamente");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Ocurrio un error de conexión");
                BuscarProductos();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente actualizar el producto nuevamente");
                ActualizarProducto();
            }

        }

        public static void EliminarProducto()
        {
            try
            {
                Console.WriteLine("Eliminar producto");
                Producto producto = SeleccionarProducto();

                using (TiendaContext context = new TiendaContext())
                {
                    context.Remove(producto);
                    context.SaveChanges();
                    Console.WriteLine("Producto eliminado exitosamente");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Ocurrio un error de conexión");
                EliminarProducto();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente eliminar el producto nuevamente");
                EliminarProducto();
            }

        }

        //Detalle
        public static void BuscarDetalle()
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

        public static void CrearDetalle()
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

        public static Detalle LlenarDetalle(Detalle detalle)
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
                CrearDetalle();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear el detalle nuevamente");
                CrearDetalle();
            }
            return detalle;
        }

        public static Detalle SeleccionarDetalle()
        {
            BuscarDetalle();
            Console.Write("Selecciona el código del detalle: ");
            uint id = uint.Parse(Console.ReadLine());

            using (TiendaContext tiendaContext = new TiendaContext())
            {
                Detalle detalle = tiendaContext.Detalles.Find(id);
                if (detalle == null)
                {
                    SeleccionarProducto();
                }
                return detalle;
            }
        }

        public static void ActualizarDetalle()
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

        public static void EliminarDetalle()
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

    }
}
