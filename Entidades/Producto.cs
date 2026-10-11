using System;

namespace Entidades
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal PrecioUnidad { get; set; }
        public int UnidadesEnExistencia { get; set; }
        public bool Estado { get; set; }
    }
}
