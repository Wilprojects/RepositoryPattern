using RepositoryPattern.Models;

namespace RepositoryPattern.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly List<Producto> _productos = new();

    public IEnumerable<Producto> GetAll()
    {
        return _productos.ToList();
    }

    public Producto? GetById(int id)
    {
        return _productos.FirstOrDefault(p => p.Id == id);
    }

    public void Add(Producto producto)
    {
        _productos.Add(producto);
    }

    public bool Update(Producto producto)
    {
        Producto? productoExistente = GetById(producto.Id);

        if (productoExistente is null)
        {
            return false;
        }

        productoExistente.Nombre = producto.Nombre;
        productoExistente.Precio = producto.Precio;

        return true;
    }

    public bool Delete(int id)
    {
        Producto? producto = GetById(id);

        if (producto is null)
        {
            return false;
        }

        _productos.Remove(producto);

        return true;
    }
}