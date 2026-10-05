using RepositoryPattern.Models;
using RepositoryPattern.Repositories;
using RepositoryPattern.Services;

IProductoRepository productoRepository = new ProductoRepository();

var productoService = new ProductoService(productoRepository);

Console.WriteLine("=== REPOSITORY PATTERN - PRODUCTOS ===");
Console.WriteLine();


// ========================================
// ADD
// ========================================

Console.WriteLine("1. ADD - Agregar productos");
Console.WriteLine("--------------------------");

productoService.Agregar(new Producto
{
    Id = 1,
    Nombre = "Laptop",
    Precio = 3500m
});

productoService.Agregar(new Producto
{
    Id = 2,
    Nombre = "Monitor",
    Precio = 900m
});

productoService.Agregar(new Producto
{
    Id = 3,
    Nombre = "Teclado",
    Precio = 150m
});

Console.WriteLine("Productos agregados correctamente.");
Console.WriteLine();


// ========================================
// GET ALL
// ========================================

Console.WriteLine("2. GET ALL - Lista de productos");
Console.WriteLine("--------------------------------");

foreach (Producto producto in productoService.ObtenerTodos())
{
    Console.WriteLine($"Id: {producto.Id} | " + $"Nombre: {producto.Nombre} | " +  $"Precio: S/ {producto.Precio:N2}");
}

Console.WriteLine();


// ========================================
// GET BY ID
// ========================================

Console.WriteLine("3. GET BY ID - Buscar producto");
Console.WriteLine("------------------------------");

Producto? productoEncontrado = productoService.ObtenerPorId(2);

if (productoEncontrado is not null)
{
    Console.WriteLine($"Producto encontrado: " + $"{productoEncontrado.Nombre} - " + $"S/ {productoEncontrado.Precio:N2}");
}
else
{
    Console.WriteLine("Producto no encontrado.");
}

Console.WriteLine();


// ========================================
// UPDATE
// ========================================

Console.WriteLine("4. UPDATE - Actualizar producto");
Console.WriteLine("-------------------------------");

bool actualizado = productoService.Actualizar(
    new Producto
    {
        Id = 2,
        Nombre = "Monitor",
        Precio = 850m
    });

Console.WriteLine(actualizado? "Producto actualizado correctamente." : "No se pudo actualizar el producto.");

Producto? productoActualizado = productoService.ObtenerPorId(2);

if (productoActualizado is not null)
{
    Console.WriteLine($"Nuevo precio de {productoActualizado.Nombre}: " + $"S/ {productoActualizado.Precio:N2}");
}

Console.WriteLine();


// ========================================
// DELETE
// ========================================

Console.WriteLine("5. DELETE - Eliminar producto");
Console.WriteLine("------------------------------");

bool eliminado = productoService.Eliminar(1);

Console.WriteLine(eliminado? "Producto eliminado correctamente." : "No se pudo eliminar el producto.");

Console.WriteLine();


// ========================================
// LISTA FINAL
// ========================================

Console.WriteLine("=== LISTA FINAL DE PRODUCTOS ===");

foreach (Producto producto in productoService.ObtenerTodos())
{
    Console.WriteLine($"Id: {producto.Id} | " + $"Nombre: {producto.Nombre} | " + $"Precio: S/ {producto.Precio:N2}");
}

Console.WriteLine();
Console.WriteLine("Proceso finalizado.");