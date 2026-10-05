# Entregable III - Repository Pattern

## Descripción

Este proyecto corresponde al **Entregable III de la capacitación de Principios SOLID y patrones de diseño en C# .NET**.

El objetivo es implementar el **Repository Pattern** utilizando un repositorio en memoria para la gestión de productos.

El proyecto separa la lógica de negocio del acceso a datos mediante una interfaz `IProductoRepository`, una implementación concreta `ProductoRepository` y una capa de servicio `ProductoService`.

Las operaciones implementadas son:

- `GetAll()`
- `GetById()`
- `Add()`
- `Update()`
- `Delete()`

---

## Objetivo

Aplicar el **Repository Pattern** para separar el acceso a datos de la lógica de negocio.

La aplicación utiliza un repositorio en memoria basado en:

```csharp
List<Producto>
```

De esta manera, la lógica de negocio no necesita conocer directamente cómo se almacenan los productos.

---

## Patrón utilizado

### Repository Pattern

El Repository Pattern crea una capa de abstracción entre la lógica de negocio y el mecanismo de almacenamiento de datos.

En este proyecto, la arquitectura es:

```text
Program.cs
     │
     ▼
ProductoService
     │
     ▼
IProductoRepository
     │
     ▼
ProductoRepository
     │
     ▼
List<Producto>
```

Cada componente tiene una responsabilidad específica:

- `Producto`: representa los datos de un producto.
- `IProductoRepository`: define las operaciones de acceso a datos.
- `ProductoRepository`: implementa las operaciones utilizando una lista en memoria.
- `ProductoService`: contiene lógica de negocio y utiliza la abstracción del repositorio.
- `Program.cs`: ejecuta las operaciones y muestra los resultados.

---

## Estructura del proyecto

```text
RepositoryPattern/
│
├── Evidencias/
│   ├── 01-estructura-repository.png
│   ├── 02-add-getall.png
│   ├── 03-getbyid.png
│   ├── 04-update.png
│   └── 05-delete.png
│
├── src/
│   └── RepositoryPattern/
│       │
│       ├── Models/
│       │   └── Producto.cs
│       │
│       ├── Repositories/
│       │   ├── IProductoRepository.cs
│       │   └── ProductoRepository.cs
│       │
│       ├── Services/
│       │   └── ProductoService.cs
│       │
│       ├── Program.cs
│       └── RepositoryPattern.csproj
│
├── .gitignore
├── README.md
└── RepositoryPattern.slnx
```

> Las carpetas `.vs`, `bin` y `obj` pueden existir localmente, pero no deben subirse al repositorio porque están excluidas mediante `.gitignore`.

---

## Modelo Producto

La clase `Producto` representa la entidad utilizada por la aplicación.

Contiene las propiedades:

- `Id`
- `Nombre`
- `Precio`

Ejemplo:

```csharp
public class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public decimal Precio { get; set; }
}
```

---

## IProductoRepository

La interfaz `IProductoRepository` define las operaciones que debe proporcionar cualquier repositorio de productos.

```csharp
public interface IProductoRepository
{
    IEnumerable<Producto> GetAll();

    Producto? GetById(int id);

    void Add(Producto producto);

    bool Update(Producto producto);

    bool Delete(int id);
}
```

La interfaz define qué operaciones están disponibles sin especificar cómo se almacenan los datos.

---

## ProductoRepository

`ProductoRepository` implementa `IProductoRepository`.

El almacenamiento se realiza en memoria mediante:

```csharp
private readonly List<Producto> _productos = new();
```

Por lo tanto, los datos existen únicamente mientras la aplicación está en ejecución.

Cuando se cierra la aplicación, los productos almacenados desaparecen.

Esto es intencional, ya que el requerimiento del entregable solicita un repositorio en memoria.

---

## Operaciones implementadas

### GetAll()

Devuelve todos los productos almacenados en el repositorio.

```csharp
public IEnumerable<Producto> GetAll()
{
    return _productos.ToList();
}
```

### GetById()

Busca un producto mediante su identificador.

```csharp
public Producto? GetById(int id)
{
    return _productos.FirstOrDefault(p => p.Id == id);
}
```

Si el producto no existe, el método devuelve `null`.

### Add()

Agrega un producto al repositorio.

```csharp
public void Add(Producto producto)
{
    _productos.Add(producto);
}
```

### Update()

Busca un producto existente mediante su identificador y actualiza sus datos.

Si el producto no existe, devuelve `false`. Si la actualización se realiza correctamente, devuelve `true`.

### Delete()

Busca el producto mediante su identificador y lo elimina del repositorio.

Si el producto no existe, devuelve `false`. Si la eliminación se realiza correctamente, devuelve `true`.

---

## ProductoService

La clase `ProductoService` representa la capa de lógica de negocio.

En lugar de depender directamente de `ProductoRepository`, utiliza:

```csharp
IProductoRepository
```

Ejemplo:

```csharp
private readonly IProductoRepository _productoRepository;
```

Esto permite separar claramente la lógica de negocio del mecanismo utilizado para almacenar la información.

La clase contiene operaciones como:

```text
ObtenerTodos()
ObtenerPorId()
Agregar()
Actualizar()
Eliminar()
```

También puede realizar validaciones antes de enviar una operación al repositorio, por ejemplo:

- Validar que el `Id` sea mayor a cero.
- Validar que el nombre no esté vacío.
- Validar que el precio no sea negativo.
- Evitar identificadores duplicados.

---

## Diferencia entre Service y Repository

### ProductoService

Se encarga de la lógica de negocio.

Ejemplo:

```text
¿El producto tiene datos válidos?
¿El identificador ya existe?
¿La operación puede realizarse?
```

### ProductoRepository

Se encarga del acceso a datos.

Ejemplo:

```text
Agregar un producto a la lista.
Buscar un producto.
Actualizar sus datos.
Eliminarlo.
```

La separación puede representarse así:

```text
ProductoService
→ decide si la operación debe realizarse

ProductoRepository
→ realiza la operación sobre los datos
```

---

## Funcionamiento

Al ejecutar la aplicación se demuestran las cinco operaciones solicitadas.

### 1. Add

Se agregan productos de ejemplo:

```text
Laptop
Monitor
Teclado
```

### 2. GetAll

Se muestran todos los productos almacenados.

Ejemplo:

```text
Id: 1 | Nombre: Laptop | Precio: S/ 3,500.00
Id: 2 | Nombre: Monitor | Precio: S/ 900.00
Id: 3 | Nombre: Teclado | Precio: S/ 150.00
```

### 3. GetById

Se busca un producto por identificador.

Ejemplo:

```text
Producto encontrado: Monitor - S/ 900.00
```

### 4. Update

Se actualiza un producto existente.

Ejemplo:

```text
Producto actualizado correctamente.
Nuevo precio de Monitor: S/ 850.00
```

### 5. Delete

Se elimina un producto del repositorio.

Ejemplo:

```text
Producto eliminado correctamente.
```

Después se muestra nuevamente la lista para verificar que el producto ya no existe.

---

## Ejemplo de ejecución

```text
=== REPOSITORY PATTERN - PRODUCTOS ===

1. ADD - Agregar productos
--------------------------
Productos agregados correctamente.

2. GET ALL - Lista de productos
--------------------------------
Id: 1 | Nombre: Laptop | Precio: S/ 3,500.00
Id: 2 | Nombre: Monitor | Precio: S/ 900.00
Id: 3 | Nombre: Teclado | Precio: S/ 150.00

3. GET BY ID - Buscar producto
------------------------------
Producto encontrado: Monitor - S/ 900.00

4. UPDATE - Actualizar producto
-------------------------------
Producto actualizado correctamente.
Nuevo precio de Monitor: S/ 850.00

5. DELETE - Eliminar producto
------------------------------
Producto eliminado correctamente.

=== LISTA FINAL DE PRODUCTOS ===
Id: 2 | Nombre: Monitor | Precio: S/ 850.00
Id: 3 | Nombre: Teclado | Precio: S/ 150.00

Proceso finalizado.
```

> El formato utilizado para números y decimales puede variar ligeramente según la configuración regional del sistema operativo.

---

## Tecnologías utilizadas

- C#
- .NET
- Aplicación de consola
- Visual Studio
- Git
- GitHub

---

## Requisitos

Para ejecutar el proyecto se necesita:

- Visual Studio con soporte para desarrollo .NET, o
- .NET SDK instalado.

Git es necesario únicamente para trabajar con control de versiones y subir el proyecto a GitHub.

---

## Cómo ejecutar desde Visual Studio

1. Abrir el archivo:

```text
RepositoryPattern.slnx
```

2. Verificar que `RepositoryPattern` sea el proyecto de inicio.

3. Ejecutar con:

```text
Ctrl + F5
```

4. Comprobar en consola el resultado de las operaciones:

```text
Add
GetAll
GetById
Update
Delete
```

---

## Cómo ejecutar desde terminal

Desde la raíz del repositorio ejecutar:

```bash
dotnet run --project src/RepositoryPattern/RepositoryPattern.csproj
```

La aplicación ejecutará automáticamente la demostración de todas las operaciones del repositorio.

---

## Evidencias

Las evidencias del desarrollo y funcionamiento se encuentran en:

```text
./Evidencias/
```

### Evidencia 1 - Estructura Repository Pattern

```text
./Evidencias/01-estructura-repository.png
```

Muestra la estructura del proyecto y la separación entre modelo, repositorio, interfaz y servicio.

### Evidencia 2 - Add y GetAll

```text
./Evidencias/02-add-getall.png
```

Muestra la creación de productos y la consulta de todos los elementos almacenados.

### Evidencia 3 - GetById

```text
./Evidencias/03-getbyid.png
```

Muestra la búsqueda de un producto mediante su identificador.

### Evidencia 4 - Update

```text
./Evidencias/04-update.png
```

Muestra la actualización de los datos de un producto existente.

### Evidencia 5 - Delete

```text
./Evidencias/05-delete.png
```

Muestra la eliminación de un producto y la lista resultante después de la operación.

---

## Beneficios del Repository Pattern

La implementación del Repository Pattern permite:

- Separar la lógica de negocio del acceso a datos.
- Centralizar las operaciones de almacenamiento.
- Trabajar mediante una abstracción (`IProductoRepository`).
- Reducir el acoplamiento entre componentes.
- Facilitar el mantenimiento del código.
- Permitir cambiar la fuente de datos en el futuro con menor impacto.

Actualmente:

```text
IProductoRepository
        ↑
ProductoRepository
        ↓
List<Producto>
```

En un proyecto real podría reemplazarse por otra implementación:

```text
IProductoRepository
        ↑
SqlProductoRepository
        ↓
SQL Server
```

La lógica de negocio podría continuar trabajando con la misma interfaz.

---

## Control de versiones

Ejemplos de commits utilizados durante el desarrollo:

```text
chore: initialize repository pattern project
feat: add product model and repository interface
feat: implement in-memory product repository
feat: add product service and repository operations demo
docs: add README and execution evidence
```

Esto permite mantener un historial organizado de los cambios realizados durante el proyecto.

---

## Conclusión

Este entregable demuestra la implementación del **Repository Pattern** mediante un repositorio en memoria para productos.

La interfaz `IProductoRepository` define las operaciones disponibles, mientras que `ProductoRepository` se encarga de implementarlas utilizando una colección `List<Producto>`.

La clase `ProductoService` mantiene separada la lógica de negocio del acceso a datos y depende de la abstracción del repositorio en lugar de una implementación concreta.

La aplicación demuestra correctamente las operaciones:

```text
GetAll()
GetById()
Add()
Update()
Delete()
```

cumpliendo con el objetivo de **separar el acceso a datos de la lógica de negocio**.
