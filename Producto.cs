using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion
{
    public class Producto
    {
        private static int _nextId = 1;
        public int Id { get; private set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public bool Disponible { get; set; }
        public int CantStock { get; set; }

        public Producto(string nombre)
        {
            Id = _nextId++;
            Nombre = nombre;
        }

        public override string ToString()
        {
            return string.Format
            (
                "------------------------------" + System.Environment.NewLine +
                "  ID: {0}" + System.Environment.NewLine + 
                "   Nombre: {1}" + System.Environment.NewLine +
                "   Precio: {2}" + System.Environment.NewLine + 
                "   Disponible: {3}" + System.Environment.NewLine + 
                "   Stock: {4}", 
                Id, Nombre, Precio, Disponible ? "Si" : "No", CantStock
            );
        }

        public string ToShortString()
        {
            return string.Format(
            "{0,-4} | {1,-16} | {2,8} Gs | {3,-4} | {4,5}",
            Id, Nombre, Precio, Disponible ? "Si" : "No", CantStock
        );
        }
    }
}
