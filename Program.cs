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
        static string GetName(bool newLine = false)
        {
            if (newLine) {Console.WriteLine();}

            Console.WriteLine("Nombre del producto: ");
            Console.Write("- ");
            string nombre = Console.ReadLine();

            return nombre;
        }

        static bool GetAvailability(bool newLine = false)
        {
            bool disponible = false;
            
            if (newLine) {Console.WriteLine();}

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

        static void ImprimirProductos(List<Producto> ListaProductos)
        {
            foreach (Producto producto in ListaProductos)
            {
                Console.WriteLine(producto);
            }
            Console.WriteLine();
        }

        static Producto ModificarProducto(List<Producto> ListaProductos, Producto findModificar, int opcionCambiar, string nuevoValor) // Not yet done
        {
            switch (opcionCambiar)
            {
                case 1:
                    findModificar.Nombre = nuevoValor;
                    break;
                case 2:
                    int nuevoPrecio;
                    if (int.TryParse(nuevoValor, out nuevoPrecio))
                    {
                        findModificar.Precio = nuevoPrecio;
                    }
                    break;
                case 3:
                    bool nuevoDisponible;
                    if (bool.TryParse(nuevoValor, out nuevoDisponible))
                    {
                        findModificar.Disponible = nuevoDisponible;
                    }
                    break;
                case 4:
                    int nuevaCantidad;
                    if (int.TryParse(nuevoValor, out nuevaCantidad))
                    {
                        findModificar.CantStock = nuevaCantidad;
                    }
                    break;
            }
            return findModificar;
        }

        static object ModificarAtributo(string nombre, int precio, bool disponible, int stock, int opcionCambiar = 1)
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

        static Producto BuscarProducto(List<Producto> ListaProductos, bool Espeficifo = false)
        {
            if (ListaProductos.Count != 0)
            {                
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
                        Console.WriteLine(System.Environment.NewLine + "Producto encontrado:");
                        Console.WriteLine(find);

                        return find;
                    }
                }
                else
                {
                    var resultados = ListaProductos.Where(x => x.Nombre.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

                    if (resultados.Count > 0)
                    {
                        Console.WriteLine(System.Environment.NewLine + "Productos encontrados:");
                        foreach (var producto in resultados)
                        {
                            Console.WriteLine(producto);
                        }

                        if (Espeficifo)
                        {
                            Console.WriteLine(System.Environment.NewLine + "Ingresa el ID del producto para anadir stock: ");

                            idSearch = ValidarEntero(1, 999999999, "Debe ser un entero mayor o igual a 1", "");

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
            else
            {
                Console.WriteLine("-No hay productos guardados-" + System.Environment.NewLine);
                return null;
            }
        }

        static void Main(string[] strings)
        {
            List<Producto> ListaProductos = new List<Producto>();

            bool exitMainLoop = false;
            while (!exitMainLoop)
            {
                Console.WriteLine("Hello! Choose one of the following options:");
                Console.WriteLine("1. Anadir producto");
                Console.WriteLine("2. Argegar Stock");
                Console.WriteLine("3. Cambiar atributos");
                Console.WriteLine("4. Buscar producto");
                Console.WriteLine("5. Listar productos");

                int choice = ValidarEntero(0, 6, "Debe ser uno de los opciones disponibles", "");

                Console.WriteLine(System.Environment.NewLine + "Elegiste: " + choice + System.Environment.NewLine);

                switch (choice)
                {
                    case 0: // Salir del programa
                        exitMainLoop = true;

                        break; // Salir del programa

                    case 1: // Anadir un producto
                        string nombre = GetName(false);

                        int precio = ValidarEntero(1000, 999999999, "Debe ser un entero mayor o igual a 1.000 Gs", "Precio");

                        bool disponible = GetAvailability(false);

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

                                            switch (opcionCambiar)
                                            {
                                                case 1:
                                                    nombre = (string)ModificarAtributo(nombre, precio, disponible, 0, opcionCambiar);
                                                    break;
                                                case 2:
                                                    precio = (int)ModificarAtributo(nombre, precio, disponible, 0, opcionCambiar);
                                                    break;
                                                case 3:
                                                    disponible = (bool)ModificarAtributo(nombre, precio, disponible, 0, opcionCambiar);
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

                        break; // Anadir un producto

                    case 2: // Agregar stock
                        var find = BuscarProducto(ListaProductos, true);

                        if (find != null)
                        {
                            Console.WriteLine();
                            int nuevoStock = ValidarEntero(0, 999999999, "Debe ser un entero positivo", "Cantidad a agregar");

                            find.CantStock += nuevoStock;

                            Console.WriteLine(System.Environment.NewLine + "-Stock actualizado con exito-");
                            Console.WriteLine($"-Stock actual: {find.CantStock}-" + System.Environment.NewLine);

                            if (find.CantStock > 0 & !find.Disponible)
                            {
                                Console.WriteLine("El producto no estaba disponible, desea cambiar su estado a disponible?");
                                bool cambiarEstado = ValidarConfirmacion();

                                if (cambiarEstado)
                                {
                                    find.Disponible = true;
                                    Console.WriteLine(System.Environment.NewLine + "-El producto ahora esta disponible-" + System.Environment.NewLine);
                                }
                                else
                                {
                                    Console.WriteLine(System.Environment.NewLine + "-El producto se queda indisponible-" + System.Environment.NewLine);
                                }
                            }
                        }

                        break;// Agregar stock

                    case 3: // Cambiar atributos
                        var findModificar = BuscarProducto(ListaProductos, true);

                        if (findModificar != null)
                        {
                            Console.WriteLine(System.Environment.NewLine + "Elegir un atributo a cambiar:");
                            Console.WriteLine(" 1. Nombre");
                            Console.WriteLine(" 2. Precio");
                            Console.WriteLine(" 3. Disponibilidad");
                            Console.WriteLine(" 4. Stock");

                            int opcionCambiarAtributo = ValidarEntero(1, 4, "Debe ser uno de los opciones disponibles", "");
                            string nuevoValor = "";

                            switch (opcionCambiarAtributo)
                            {
                                case 1: // Cambiar nombre
                                    nuevoValor = (string)ModificarAtributo(findModificar.Nombre, findModificar.Precio, findModificar.Disponible, findModificar.CantStock, opcionCambiarAtributo);
                                    break;
                                case 2: // Cambiar precio
                                    nuevoValor = ModificarAtributo(findModificar.Nombre, findModificar.Precio, findModificar.Disponible, findModificar.CantStock, opcionCambiarAtributo).ToString();
                                    break;
                                case 3: // Cambiar disponibilidad
                                    nuevoValor = ModificarAtributo(findModificar.Nombre, findModificar.Precio, findModificar.Disponible, findModificar.CantStock, opcionCambiarAtributo).ToString();
                                    break;
                                case 4: // Cambiar stock
                                    nuevoValor = ModificarAtributo(findModificar.Nombre, findModificar.Precio, findModificar.Disponible, findModificar.CantStock, opcionCambiarAtributo).ToString();
                                    break;
                            }

                            ModificarProducto(ListaProductos, findModificar, opcionCambiarAtributo, nuevoValor);
                            Console.WriteLine(System.Environment.NewLine + "-Atributo actualizado con exito-" + System.Environment.NewLine);
                        }
                        break;// Cambiar atributos

                    case 4: // Buscar producto
                        BuscarProducto(ListaProductos, false);
                        break; // Buscar producto

                    case 5: // Listar productos
                        ImprimirProductos(ListaProductos);
                        break; // Listar productos
                }
            }
        }
    }
}