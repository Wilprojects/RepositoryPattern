using RepositoryPattern.Models;
using RepositoryPattern.Repositories;

namespace RepositoryPattern.Services;

public class ProductoService
{
    private readonly IProductoRepository _productoRepository;

    public ProductoService(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public IEnumerable<Producto> ObtenerTodos()
    {
        return _productoRepository.GetAll();
    }

    public Producto? ObtenerPorId(int id)
    {
        return _productoRepository.GetById(id);
    }

    public bool Agregar(Producto producto)
    {
        if (producto.Id <= 0 || string.IsNullOrWhiteSpace(producto.Nombre) || producto.Precio < 0)
        {
            return false;
        }

        if (_productoRepository.GetById(producto.Id) is not null)
        {
            return false;
        }

        _productoRepository.Add(producto);

        return true;
    }

    public bool Actualizar(Producto producto)
    {
        if (producto.Id <= 0 || string.IsNullOrWhiteSpace(producto.Nombre) || producto.Precio < 0)
        {
            return false;
        }

        return _productoRepository.Update(producto);
    }

    public bool Eliminar(int id)
    {
        return _productoRepository.Delete(id);
    }
}