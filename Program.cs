using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Administracion
{
    class Program
    {
        static string GetName()
        {
            Console.WriteLine("Nombre del producto: ");
            Console.Write("- ");
            string nombre = Console.ReadLine();

            return nombre;
        }

        static bool GetAvailability()
        {
            bool disponible = false;
            
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

        static int ValidarEntero(int minValor = 0, int maxValor = 999999999, string Error = "", string NomVariable = "")
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

        static bool ValidarConfirmacion()
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
                        confirmacion= false;
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

        static Producto GuardarProducto(string nombre, int precio, bool disponible)
        {
            Producto newProducto = new Producto(nombre);

            newProducto.Nombre = nombre;
            newProducto.Precio = precio;
            newProducto.Disponible = disponible;
            newProducto.CantStock = 0;


            return newProducto;
        }

        static void Main(string[] strings)
        {
            List<Producto> ListaProductos = new List<Producto>();

            bool exitMainLoop = false;
            while (!exitMainLoop)
            {
                Console.WriteLine("Hello! Choose one of the following options:");
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. Add Stock");
                Console.WriteLine("3. Change Availability");
                Console.WriteLine("4. Check Item");

                int choice = ValidarEntero(0, 5, "Debe ser uno de los opciones disponibles", "");

                if (0 <= choice & choice < 5)
                {
                    Console.WriteLine(System.Environment.NewLine + "Elegiste: " + choice + System.Environment.NewLine);

                    switch (choice)
                    {
                        case 0:
                            exitMainLoop = true;

                            break;

                        case 1:
                            string nombre = GetName();

                            int precio = ValidarEntero(1000, 999999999, "Debe ser un entero mayor o igual a 1.000 Gs", "Precio");

                            bool disponible = GetAvailability();

                            bool exitSaveLoop = false;

                            while (!exitSaveLoop)
                            {
                                Console.WriteLine(System.Environment.NewLine + "Los datos ingresados son:");
                                Console.WriteLine(" Nombre: " + nombre);
                                Console.WriteLine(" Precio: " + precio);
                                Console.WriteLine(" Disponible: " + disponible);

                                Console.WriteLine(System.Environment.NewLine + "Por favor, confirmar para guardar: ");
                                bool confirmacionGuardar = ValidarConfirmacion();

                                if (confirmacionGuardar)
                                {
                                    Producto newProducto = GuardarProducto(nombre, precio, disponible);

                                    ListaProductos.Add(newProducto);

                                    Console.WriteLine(System.Environment.NewLine + "-Producto guardado con exito-" + System.Environment.NewLine);
                                    Console.WriteLine("Elements in the list:");
                                    foreach (var item in ListaProductos) Console.WriteLine(item);
                                    Console.WriteLine();

                                    exitSaveLoop = true;

                                }
                                else
                                {
                                    if (!confirmacionGuardar)
                                    {
                                        Console.WriteLine(System.Environment.NewLine + "Elegir una opcion:");
                                        Console.WriteLine(" 1. Cambiar valor");
                                        Console.WriteLine(" 2. Cancelar");

                                        int opcionNoGuardar = ValidarEntero(1, 2, "Debe ser uno de los opciones disponibles", "");

                                        switch (opcionNoGuardar)
                                        {
                                            case 1:
                                                Console.WriteLine(System.Environment.NewLine + "Elegir un valor a cambiar:");
                                                Console.WriteLine("1. Nombre");
                                                Console.WriteLine("2. Precio");
                                                Console.WriteLine("3. Disponibilidad");

                                                int opcionCambiar = ValidarEntero(1, 3, "Debe ser uno de los opciones disponibles", "");

                                                bool GuardarCambio;

                                                switch (opcionCambiar)
                                                {
                                                    case 1:
                                                        Console.WriteLine();
                                                        string nuevo_nombre = GetName();

                                                        Console.WriteLine(System.Environment.NewLine + "Nombre anterior: " + nombre);
                                                        Console.WriteLine("Nombre nuevo: " + nuevo_nombre);

                                                        Console.WriteLine(System.Environment.NewLine + "Quieres guardar este cambio?");

                                                        GuardarCambio = ValidarConfirmacion();

                                                        if (GuardarCambio)
                                                        {
                                                            nombre = nuevo_nombre;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine(System.Environment.NewLine + "-Cambio rechazado-");
                                                        }

                                                        break;

                                                    case 2:
                                                        int nuevo_precio = ValidarEntero(1000, 999999999, "Debe ser un entero mayor o igual a 1.000 Gs", "Precio");

                                                        Console.WriteLine(System.Environment.NewLine + "Precio anterior: " + precio);
                                                        Console.WriteLine("Precio nuevo: " + nuevo_precio);

                                                        Console.WriteLine(System.Environment.NewLine + "Quieres guardar este cambio?");

                                                        GuardarCambio = ValidarConfirmacion();

                                                        if (GuardarCambio)
                                                        {
                                                            precio = nuevo_precio;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Cambio rechazado");
                                                        }

                                                        break;

                                                    case 3:
                                                        bool nuevo_disponible = GetAvailability();

                                                        Console.WriteLine(System.Environment.NewLine + "Precio anterior: " + disponible);
                                                        Console.WriteLine("Precio nuevo: " + nuevo_disponible);

                                                        Console.WriteLine(System.Environment.NewLine + "Quieres guardar este cambio?");

                                                        GuardarCambio = ValidarConfirmacion();

                                                        if (GuardarCambio)
                                                        {
                                                            disponible = nuevo_disponible;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Cambio rechazado");
                                                        }

                                                        break;
                                                }

                                                break;

                                            case 2:
                                                Console.WriteLine(System.Environment.NewLine + "-Guardado cancelado-" + System.Environment.NewLine);

                                                exitSaveLoop = true;
                                                break;
                                        }
                                    }
                                }
                            }

                            break;

                        case 2:
                            Console.WriteLine("Ingresa el codigo o el nombre del producto:");
                            Console.Write("- ");
                            string search = Console.ReadLine();

                            int idSearch;

                            bool isNumber = int.TryParse(search, out idSearch);

                            if (isNumber)
                            {
                                var find = ListaProductos.FirstOrDefault(x => x.Id == idSearch);

                                if (find != null)
                                {
                                    Console.WriteLine(System.Environment.NewLine + "Producto encontrados:");
                                    Console.WriteLine(find);

                                    Console.WriteLine();
                                    int nuevoStock = ValidarEntero(1, 999999999, "Debe ser un entero mayor o igual a 1", "Cantidad a agregar");

                                    find.CantStock += nuevoStock;

                                    Console.WriteLine(System.Environment.NewLine + "-Stock actualizado con exito-");
                                    Console.WriteLine("Stock actual: " + find.CantStock + System.Environment.NewLine);
                                }
                            }
                            else
                            {
                                var find = ListaProductos.FirstOrDefault(x => x.Nombre == search);

                                if (find != null)
                                {
                                    Console.WriteLine(System.Environment.NewLine + "Productos encontrados:");
                                    Console.WriteLine(find);

                                    Console.WriteLine(System.Environment.NewLine + "Ingresa el ID del producto para anadir stock: ");

                                    idSearch = ValidarEntero(1, 999999999, "Debe ser un entero mayor o igual a 1", "ID");

                                    find = ListaProductos.FirstOrDefault(x => x.Id == idSearch);

                                    if (find != null)
                                    {
                                        Console.WriteLine();
                                        int nuevoStock = ValidarEntero(1, 999999999, "Debe ser un entero mayor o igual a 1", "Cantidad a agregar");

                                        find.CantStock += nuevoStock;

                                        Console.WriteLine(System.Environment.NewLine + "-Stock actualizado con exito-");
                                        Console.WriteLine("-Stock actual: " + find.CantStock + "-" + System.Environment.NewLine);
                                    }
                                }
                            }

                            break;
                    }
                }
            }
        }
    }
}