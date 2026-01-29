# API Facturación

Documentación oficial de la API para el Sistema de Facturación.

## Estándares de Petición (Request)

### Paginación
Todos los endpoints de listado (`GET`) soportan paginación mediante los siguientes parámetros en el Query String (URL):

| Parámetro | Tipo | Descripción | Valor por defecto |
| :--- | :--- | :--- | :--- |
| `PageNumber` | `int` | Número de página a recuperar. | 1 |
| `PageSize` | `int` | Cantidad de registros por página (Máximo 100). | 10 |
| `TraerTodo` | `bool` | Si se envía como `true`, ignora la paginación y retorna todos los registros disponibles. | `false` |

## Estándares de Respuesta (Response)

Todas las respuestas de la API están envueltas en una estructura estándar JSON.

### Respuesta Estándar (Single Object / Command)
Utilizada para creación, actualización, eliminación u obtención por ID.

```json
{
  "succeeded": true,     // bool: Indica si la operación fue exitosa
  "mensaje": "Texto",    // string: Mensaje informativo o descripción del error
  "errores": null,       // list<string>: Lista de errores de validación (si los hay)
  "datos": { ... }       // object: El recurso solicitado o ID generado
}
```

### Respuesta Paginada (List)
Utilizada para todos los listados.

```json
{
  "pageNumber": 1,       // int: Página actual
  "pageSize": 10,        // int: Registros por págia
  "totalRecords": 150,   // int: Total de registros en BD
  "totalPages": 15,      // int: Total de páginas calculadas
  "datos": [ ... ],      // array: Lista de objetos
  "succeeded": true,
  "mensaje": null,
  "errores": null
}
```

## Endpoints Disponibles

### Autenticación
**Base URL:** `/api/v1/Login`

| Método | Endpoint | Descripción | Body (Payload) |
| :--- | :--- | :--- | :--- |
| `POST` | `/` | Iniciar sesión y obtener token JWT | `LoginQuery` (Usuario, Password) |

### Clientes
**Base URL:** `/api/v1/Clientes`

| Método | Endpoint | Descripción | Parámetros / Body |
| :--- | :--- | :--- | :--- |
| `GET` | `/` | Listado paginado de clientes | QueryString: `PageNumber`, `PageSize`, etc. |
| `POST` | `/` | Crear un nuevo cliente | Body: `CrearClienteComando` |
| `PUT` | `/{id}` | Actualizar datos de un cliente | Body: `ActualizarClienteCommand` |

### Facturas
**Base URL:** `/api/v1/Facturas`

| Método | Endpoint | Descripción | Parámetros / Body |
| :--- | :--- | :--- | :--- |
| `GET` | `/` | Listado paginado de facturas | QueryString: `PageNumber`, `PageSize` |
| `GET` | `/{id}` | Obtener detalle completo de una factura | Path: `id` |

### Productos
**Base URL:** `/api/v1/Productos`

| Método | Endpoint | Descripción | Parámetros / Body |
| :--- | :--- | :--- | :--- |
| `GET` | `/` | Listado paginado de productos | QueryString: `PageNumber`, `PageSize` |
| `POST` | `/` | Crear un nuevo producto | Body: `CrearProductoCommand` |
| `PUT` | `/{id}` | Actualizar datos de un producto | Body: `ActualizarProductoCommand` |

### Usuarios
**Base URL:** `/api/v1/Usuarios`

| Método | Endpoint | Descripción | Parámetros / Body |
| :--- | :--- | :--- | :--- |
| `GET` | `/` | Listado paginado de usuarios | QueryString: `PageNumber`, `PageSize` |
| `POST` | `/` | Crear un nuevo usuario | Body: `CrearUsuarioCommand` |
| `PUT` | `/{id}` | Actualizar datos de un usuario | Body: `ActualizarUsuarioCommand` |

### Métodos de Pago
**Base URL:** `/api/v1/MetodosPago`

| Método | Endpoint | Descripción | Parámetros / Body |
| :--- | :--- | :--- | :--- |
| `GET` | `/` | Obtener todos los métodos de pago | Sin parámetros |
