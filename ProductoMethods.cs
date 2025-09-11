using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Administracion
{
    class ProductoMethods
    {
        public static string GetName(bool newLine = false)
        {
            if (newLine) { Console.WriteLine(); }

            Console.WriteLine("Nombre del producto: ");
            Console.Write("- ");
            string nombre = Console.ReadLine();

            return nombre;
        }

        public static bool GetAvailability(bool newLine = false)
        {
            bool disponible = false;

            if (newLine) { Console.WriteLine(); }

            Console.WriteLine("Esta disponibile?");

            bool confirmacion = ValidarConfirmacion();

            if (confirmacion)
            {
                disponible = true;
            }
            else
            {
                if (!confirmacion)
                {
                    disponible = false;
                }
            }

            return disponible;
        }

        public static int ValidarEntero(int minValor = 0, int maxValor = 999999999, string Error = "", string NomVariable = "")
        {
            if (NomVariable != "")
            {
                Console.WriteLine(NomVariable + " del producto:");
            }
            Console.Write("- ");
            string ingreso = Console.ReadLine();
            int resultado;

            while (true)
            {
                while (!int.TryParse(ingreso, out resultado))
                {
                    Console.WriteLine("Ingreso invalido! " + Error);
                    Console.Write("- ");
                    ingreso = Console.ReadLine();
                }

                if (minValor <= resultado & resultado <= maxValor)
                {
                    break;
                }
                else
                {
                    ingreso = "";
                }
            }

            return resultado;
        }

        public static bool ValidarConfirmacion()
        {
            bool confirmacion;

            Console.WriteLine("Si/No");

            while (true)
            {
                Console.Write("- ");
                string respuesta = Console.ReadLine();

                if (respuesta == "Si" || respuesta == "si" || respuesta == "SI")
                {
                    confirmacion = true;
                    break;
                }
                else
                {
                    if (respuesta == "No" || respuesta == "no" || respuesta == "NO")
                    {
                        confirmacion = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ingreso invalido! Por favor ingesar 'Si' o 'No'");
                    }
                }
            }

            return confirmacion;
        }

        public static Producto GuardarProducto(string nombre, int precio, bool disponible)
        {
            Producto newProducto = new Producto(nombre);

            newProducto.Nombre = nombre;
            newProducto.Precio = precio;
            newProducto.Disponible = disponible;
            newProducto.CantStock = 0;


            return newProducto;
        }

        public static void ImprimirProductos(List<Producto> ListaProductos)
        {
            if (ListaProductos.Count == 0)
            {
                Console.WriteLine("-No hay productos guardados-" + System.Environment.NewLine);
                return;
            }

            Console.WriteLine(" ID  | Nombre           |    Precio   | Disp | Stock");
            Console.WriteLine("-----------------------------------------------------");

            foreach (Producto producto in ListaProductos)
            {
                Console.WriteLine(producto.ToShortString());
            }
            Console.WriteLine();
        }

        public static Producto ModificarProducto(Producto findModificar, int opcionCambiar, string nuevoValor) // Not yet done
        {
            using (var db = new ProductoContext())
            {
                var producto = db.Productos.FirstOrDefault(x => x.Id == findModificar.Id);
                if (producto == null)
                {
                    return null;
                }

                switch (opcionCambiar)
                {
                    case 1:
                        producto.Nombre = nuevoValor;
                        db.SaveChanges();
                        break;

                    case 2:
                        int nuevoPrecio;
                        if (int.TryParse(nuevoValor, out nuevoPrecio))
                        {
                            producto.Precio = nuevoPrecio;
                        }
                        break;

                    case 3:
                        bool nuevoDisponible;
                        if (bool.TryParse(nuevoValor, out nuevoDisponible))
                        {
                            producto.Disponible = nuevoDisponible;
                        }
                        break;

                    case 4:
                        int nuevaCantidad;
                        if (int.TryParse(nuevoValor, out nuevaCantidad))
                        {
                            producto.CantStock = nuevaCantidad;
                        }
                        break;
                }

                db.SaveChanges();
                return findModificar;
            }
        }

        public static object ModificarAtributo(string nombre, int precio, bool disponible, int stock, int opcionCambiar = 1)
        {
            string nomVariable = "";
            object anteriorVariable = null;
            object nuevoVariable = null;

            switch (opcionCambiar)
            {
                case 1:
                    nuevoVariable = GetName(true);
                    anteriorVariable = nombre;
                    nomVariable = "Nombre";
                    break;

                case 2:
                    nuevoVariable = ValidarEntero(1000, 999999999, "Debe ser un entero mayor o igual a 1.000 Gs", $"{System.Environment.NewLine}Precio");
                    anteriorVariable = precio;
                    nomVariable = "Precio";
                    break;

                case 3:
                    nuevoVariable = GetAvailability(true);
                    anteriorVariable = disponible;
                    nomVariable = "Disponible";
                    break;

                case 4:
                    nuevoVariable = ValidarEntero(0, 999999999, "Debe ser un entero mayor o igual a 0", $"{System.Environment.NewLine}Cantidad en stock");
                    anteriorVariable = stock;
                    nomVariable = "Stock";
                    break;
            }


            Console.WriteLine(System.Environment.NewLine + $"{nomVariable} anterior: {anteriorVariable}");
            Console.WriteLine($"{nomVariable} nuevo: {nuevoVariable}");

            Console.WriteLine(System.Environment.NewLine + "Quieres guardar este cambio?");

            bool GuardarCambio = ValidarConfirmacion();

            if (GuardarCambio)
            {
                return nuevoVariable;
            }
            else
            {
                Console.WriteLine(System.Environment.NewLine + "-Cambio rechazado-");
                return anteriorVariable;
            }
        }

        public static Producto BuscarProducto(bool Especifico = false, bool newLine = false)
        {
            using (var db = new ProductoContext())
            {
                var listaProductos = db.Productos.ToList();

                if (listaProductos.Count == 0)
                {
                    Console.WriteLine("-No hay productos guardados-" + Environment.NewLine);
                    return null;
                }
                else
                {

                    if (newLine) { Console.WriteLine(); }

                    Console.WriteLine("Ingresa el codigo o el nombre del producto:");
                    Console.Write("- ");
                    string search = Console.ReadLine();

                    int idSearch;

                    bool isNumber = int.TryParse(search, out idSearch);

                    if (isNumber)
                    {
                        var findDb = db.Productos.FirstOrDefault(x => x.Id == idSearch);
                        if (findDb != null)
                        {
                            Console.WriteLine(System.Environment.NewLine + "Producto encontrado:");
                            Console.WriteLine(findDb);
                            return findDb;
                        }
                    }
                    else
                    {
                        var resultados = db.Productos.Where(x => x.Nombre.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

                        if (resultados.Count > 0)
                        {
                            Console.WriteLine(System.Environment.NewLine + "Productos encontrados:");
                            ProductoMethods.ImprimirProductos(resultados);

                            if (Especifico)
                            {
                                Console.WriteLine(System.Environment.NewLine + "Ingresa el ID del producto para anadir stock: ");

                                idSearch = ProductoMethods.ValidarEntero(1, 999999999, "Debe ser un entero mayor o igual a 1", "");

                                var elegido = resultados.FirstOrDefault(x => x.Id == idSearch);

                                return elegido;
                            }
                            else
                            {
                                Console.WriteLine();
                                return resultados.First();
                            }
                        }
                    }
                    Console.WriteLine(System.Environment.NewLine + "-No se encontraron productos con esos datos-" + System.Environment.NewLine);
                    return null;
                }
            }
        }
    }
}