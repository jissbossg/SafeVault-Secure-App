# SafeVault - Aplicación Segura con Mitigación de Vulnerabilidades

Este proyecto es una API construida en ASP.NET Core que implementa las mejores prácticas de seguridad, mitigando vulnerabilidades críticas como Inyección SQL, Cross-Site Scripting (XSS) y controlando el acceso mediante roles.

## Resumen de Vulnerabilidades y Correcciones (Uso de Microsoft Copilot)

### 1. Inyección de SQL
**Vulnerabilidad Identificada:** En versiones iniciales del proyecto, las consultas se realizaban mediante concatenación de cadenas, lo cual permitía ataques de inyección SQL (por ejemplo, ingresando `' OR 1=1 --` en el buscador).
**Corrección Aplicada:** Se implementó Entity Framework Core (EF Core). Copilot asistió generando el `AppDbContext` y reescribiendo las consultas como expresiones LINQ parametrizadas (`_context.Users.FirstOrDefault(...)`), lo que desinfecta inherentemente las entradas y neutraliza el SQLi.

### 2. Cross-Site Scripting (XSS)
**Vulnerabilidad Identificada:** La aplicación devolvía texto introducido por el usuario sin escapar, lo que permitía inyectar etiquetas `<script>` maliciosas que se ejecutaban en el navegador de otro usuario.
**Corrección Aplicada:** Copilot sugirió la utilización de `System.Text.Encodings.Web.HtmlEncoder`. Se creó el servicio `InputSanitizer.cs` que codifica los caracteres peligrosos antes de enviarlos al cliente.

### 3. Control de Acceso (Auth/RBAC)
**Vulnerabilidad Identificada:** Cualquier usuario autenticado podía acceder a rutas administrativas (por ejemplo, listar todos los usuarios).
**Corrección Aplicada:** Copilot generó la configuración del middleware JWT y los atributos `[Authorize(Roles = "Admin")]` en el controlador. Se implementó un sistema de roles donde las restricciones se validan a nivel de token.

### 4. Validación de Entrada
**Corrección Aplicada:** Se utilizaron Data Annotations en el modelo `User.cs` (`[Required]`, `[StringLength]`), generados con la ayuda de Copilot para garantizar que los datos que ingresan al sistema cumplan con los formatos esperados antes de llegar a la base de datos.

## Pruebas de Seguridad
Se ha implementado un proyecto de pruebas unitarias (`SafeVault.Tests`) utilizando xUnit. Copilot ayudó a generar los *test cases* que simulan ataques XSS enviando payloads maliciosos para asegurar que el sistema de sanitización responda correctamente.
Para ejecutar las pruebas, utilice el comando:
`dotnet test`
