# KFC.Backend

## Justificación del modelo de dominio

El modelo de dominio está diseñado para reflejar la necesidad de la marca KFC de gestionar productos, canales de venta y precios diferenciados por canal. Se identificaron las siguientes entidades principales:
- **Producto**: Representa los productos ofrecidos.
- **Canal de venta**: Representa los distintos canales (RAPPI, DIDI, PEYA, WEB, etc.).
- **Precio por canal**: Relaciona un producto con un canal y su precio específico, permitiendo estrategias comerciales y promociones diferenciadas.

Esta estructura permite una gestión flexible y escalable de los precios según el canal, cumpliendo con los requisitos del negocio.

## Arquitectura aplicada

Se aplicó una arquitectura por capas, separando claramente:
- **Entidades de dominio** (Entities)
- **DTOs** (Data Transfer Objects)
- **Repositorios** (Gateways)
- **Interactors** (Casos de uso)
- **Validadores** (Validators)
- **Presentadores y Controladores** (Presenters/Controllers)

Esto facilita el mantenimiento, la escalabilidad y la posibilidad de realizar pruebas unitarias y de integración.

## Qué principios SOLID se aplicaron
- **S (Single Responsibility)**: Cada clase tiene una única responsabilidad (por ejemplo, los repositorios solo acceden a datos, los interactors solo orquestan lógica de negocio).
- **O (Open/Closed)**: El sistema es fácilmente extensible (por ejemplo, se pueden agregar nuevos validadores o canales sin modificar el core).
- **L (Liskov Substitution)**: Las interfaces permiten intercambiar implementaciones sin romper el sistema.
- **I (Interface Segregation)**: Las interfaces son específicas y no fuerzan a implementar métodos innecesarios.
- **D (Dependency Inversion)**: Se utiliza inyección de dependencias para desacoplar las capas y facilitar pruebas.

## Patrones utilizados
- **Repository**: Para el acceso a datos y persistencia.
- **Unit of Work**: Para manejar transacciones y consistencia de datos.
- **DTO**: Para transportar datos entre capas.
- **Dependency Injection**: Para desacoplar componentes y facilitar pruebas.
- **Command/Query**: Separación de comandos (acciones que modifican estado) y queries (consultas).
- **Validator**: Para validar reglas de negocio antes de ejecutar casos de uso.

## Cómo correr el proyecto (SQLite/InMemory)

1. **Clona el repositorio**
2. Configura la cadena de conexión en `appsettings.json`:
   - Para SQLite:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=KFC.db"
     }
     ```
   - Para InMemory (solo pruebas):
     Cambia la configuración en el `DbContext` para usar `UseInMemoryDatabase("KFC")`.
3. Ejecuta las migraciones (si usas SQLite):
   ```sh
   dotnet ef database update
   ```
4. Levanta la API:
   ```sh
   dotnet run --project KFC.WebApi
   ```
5. Accede a Swagger en `https://localhost:{puerto}/swagger` para probar los endpoints.

## Cómo ejecutar pruebas unitarias

1. Ve al proyecto de tests (si existe, por ejemplo `KFC.Tests`).
2. Ejecuta:
   ```sh
   dotnet test
   ```
   Las pruebas usan una base de datos InMemory para aislar el entorno y garantizar repetibilidad.

## Qué se podría mejorar si tuviera más tiempo

- Implementar pruebas de integración y de extremo a extremo (E2E).
- Mejorar la documentación de los endpoints y agregar ejemplos en Swagger.
- Agregar autenticación y autorización más granular por roles.
- Optimizar consultas y agregar paginación avanzada.
- Implementar cacheo para consultas frecuentes.
- Desplegar en un entorno cloud y configurar CI/CD.
- Internacionalización y soporte multi-moneda.
- Mejorar la gestión de errores y logs avanzados.
