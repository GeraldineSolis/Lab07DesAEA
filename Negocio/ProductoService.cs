using System;
using System.Collections.Generic;
using Entidades;
using Datos;

namespace Negocio
{
    public class ProductoService
    {
        private readonly ProductoRepository _repo;

        public ProductoService()
        {
            _repo = new ProductoRepository();
        }

        public List<Producto> Listar() => _repo.Listar();

        public Producto ObtenerPorId(int id)
        {
            var p = _repo.ObtenerPorId(id);
            return p;
        }

        public int Insertar(Producto p)
        {
            if (string.IsNullOrWhiteSpace(p.NombreProducto)) throw new ArgumentException("El nombre no puede estar vacío.");
            if (p.PrecioUnidad < 0) throw new ArgumentException("El precio debe ser >= 0.");
            if (p.UnidadesEnExistencia < 0) throw new ArgumentException("El stock debe ser >= 0.");
            return _repo.Insertar(p);
        }

        public void Actualizar(Producto p)
        {
            if (p.IdProducto <= 0) throw new ArgumentException("Id inválido.");
            if (string.IsNullOrWhiteSpace(p.NombreProducto)) throw new ArgumentException("El nombre no puede estar vacío.");
            if (p.PrecioUnidad < 0) throw new ArgumentException("El precio debe ser >= 0.");
            if (p.UnidadesEnExistencia < 0) throw new ArgumentException("El stock debe ser >= 0.");

            var existente = _repo.ObtenerPorId(p.IdProducto);
            if (existente == null || !existente.Estado) throw new InvalidOperationException("Producto no existe o está eliminado.");

            var ok = _repo.Actualizar(p);
            if (!ok) throw new InvalidOperationException("No se pudo actualizar el producto.");
        }

        public void Eliminar(int id)
        {
            if (id <= 0) throw new ArgumentException("Id inválido.");
            var existente = _repo.ObtenerPorId(id);
            if (existente == null || !existente.Estado) throw new InvalidOperationException("Producto no existe o ya está eliminado.");
            var ok = _repo.Eliminar(id);
            if (!ok) throw new InvalidOperationException("No se pudo eliminar el producto.");
        }

        public List<Producto> Buscar(string nombre) => _repo.Buscar(nombre);
    }
}
