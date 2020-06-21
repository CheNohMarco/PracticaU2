using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Tienda.Models;

namespace Tienda.Clases
{
    public class Usuarios
    {

        public  void Login()
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
                    Program p = new Program();
                    if (usuario.Count() == 0)
                    {
                        Console.WriteLine("**No se encontro el usuario verifica la información :(**");
                        
                        p.Bienvenido();
                    }
                    else
                    {
                        Console.WriteLine("Bienvenido");
                        p.menu();
                    }
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Ocurrio un error de conexión");
                Login();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear el producto nuevamente");
                Login();
            }
        }

        public void CrearUsuario()
        {
            try
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
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Ocurrio un error de conexión");
                CrearUsuario();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el proceso. Intente crear el usuario nuevamente");
                CrearUsuario();
            }


        }

        public  Usuario LlenarUsuario(Usuario usuario)
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
    }
}
