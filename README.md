# 👥 Sistema de Gestión de Usuarios

Sistema web desarrollado con arquitectura por capas para la gestión de usuarios.

---

## 📋 Tabla de Contenidos

- [Descripción](#-descripción)
- [Arquitectura](#-arquitectura)
- [Tecnologías](#-tecnologías)
- [Requisitos Previos](#-requisitos-previos)
- [Instalación](#-instalación)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Características](#-características)
- [Ejecución](#-ejecución)
- [Endpoints del Servicio](#-endpoints-del-servicio)

---

## 📖 Descripción

Sistema de gestión de usuarios con arquitectura por capas que permite realizar operaciones CRUD completas. Incluye una interfaz web intuitiva con funcionalidades avanzadas de búsqueda, filtrado y paginación.

### Funcionalidades Principales:

- ✅ Registro de usuarios con validaciones
- ✅ Consulta de usuarios con búsqueda en tiempo real
- ✅ Modificación de datos de usuarios
- ✅ Eliminación lógica (Soft Delete)
- ✅ Filtrado por sexo
- ✅ Ordenamiento por ID
- ✅ Paginación avanzada

---

## 🏗️ Arquitectura

El proyecto implementa una **arquitectura por capas** con separación de responsabilidades:

```
┌─────────────────────────────────────────────┐
│         Presentation Layer (ASP.NET)        │
│  - Usuario.aspx (Registro)                  │
│  - UsuarioConsulta.aspx (Consulta/Grid)     │
└─────────────────┬───────────────────────────┘
                  │ WCF Service Reference
┌─────────────────▼───────────────────────────┐
│       Business Layer (WCF Service)          │
│  - UserService (SOAP/WCF)                   │
│  - UserLogic (Business Rules)               │
│  - AutoMapper (DTO Mapping)                 │
│  - Autofac (Dependency Injection)           │
└─────────────────┬───────────────────────────┘
                  │ Repository Pattern
┌─────────────────▼───────────────────────────┐
│     Persistence Layer (Data Access)         │
│  - UserRepository                           │
│  - SQL Server Connection                    │
└─────────────────┬───────────────────────────┘
                  │ ADO.NET
┌─────────────────▼───────────────────────────┐
│          Database (SQL Server)              │
│  - Table: Users                             │
│  - SP: sp_ManageUser (CRUD)                 │
└─────────────────────────────────────────────┘
```

### Proyectos:

1. **BDO (Business Domain Objects)**
   - Entidades de dominio
   - Interfaces de repositorios y casos de uso
   - Excepciones personalizadas
   - Enumeraciones

2. **Persistence (DataAccess)**
   - Implementación de repositorios
   - Acceso a base de datos con ADO.NET
   - Ejecución de Stored Procedures

3. **Business (WCF Service)**
   - Servicio WCF con endpoints SOAP
   - Lógica de negocio
   - Manejo centralizado de errores
   - Inyección de dependencias
   - AutoMapper para DTOs

4. **Presentation (ASP.NET Web Forms)**
   - Interfaz de usuario web
   - Páginas de registro y consulta
   - Bootstrap 5 para diseño responsive

---

## 🛠️ Tecnologías

### Backend:
- **.NET Framework 4.8**
- **C# 7.3**
- **WCF (Windows Communication Foundation)** - Servicio SOAP
- **ADO.NET** - Acceso a datos
- **SQL Server** - Base de datos
- **Autofac 6.5.0** - Inyección de dependencias
- **AutoMapper 12.0.1** - Mapeo de objetos

### Frontend:
- **ASP.NET Web Forms**
- **Bootstrap 5.3.0** - Framework CSS
- **Bootstrap Icons 1.11.3**
- **jQuery 3.7.0**

### Patrones y Principios:
- Repository Pattern
- Dependency Injection
- DTO (Data Transfer Objects)
- Envelope Pattern (Response wrapper)
- SOLID Principles
- Separation of Concerns

---

## ✅ Requisitos Previos

- **Visual Studio 2019 o superior**
- **SQL Server 2016 o superior** (Express, Developer o Enterprise)
- **.NET Framework 4.8** instalado
- **IIS Express** (incluido con Visual Studio)

---

## 📥 Instalación

### 1. Clonar el Repositorio
```bash
git clone [URL_DEL_REPOSITORIO]
cd PruebaTecnica
```

### 2. Restaurar Base de Datos
Ejecutar el script SQL en SQL Server Management Studio:
```sql
-- Ubicación: CreateDatabase.sql
-- Crea la base de datos DigitalBankDB
-- Crea la tabla Users
-- Crea el procedimiento sp_ManageUser
```

### 3. Configurar Cadena de Conexión
Actualizar la cadena de conexión en:
- `Business/Web.config`
- `Persistence/app.config`

```xml
<connectionStrings>
  <add name="DigitalBankDB" 
       connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DigitalBankDB;Integrated Security=True" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### 4. Restaurar Paquetes NuGet
```bash
# Visual Studio restaurará automáticamente
# O manualmente:
nuget restore PruebaTecnica.slnx
```

### 5. Compilar Solución
```bash
# En Visual Studio: Ctrl+Shift+B
# O con MSBuild:
msbuild PruebaTecnica.slnx /t:Build /p:Configuration=Release
```

---

## 📁 Estructura del Proyecto

```
PruebaTecnica/
├── BDO/                          # Business Domain Objects
│   ├── Entities/                 # User, PaginatedResult
│   ├── UseCases/                 # IUserUseCases
│   ├── Repositories/             # IUserRepository
│   ├── Exceptions/               # Custom exceptions
│   └── Enums/                    # StatusCode
│
├── Persistence/                  # Data Access Layer
│   └── Repositories/             
│       └── UserRepository.cs     # SQL Server data access
│
├── Business/                     # WCF Service Layer
│   ├── Services/                 
│   │   ├── UserService.svc       # WCF Service endpoint
│   │   └── ServiceHandler.cs     # Error handling
│   ├── Logic/                    
│   │   └── UserLogic.cs          # Business rules
│   ├── DataContracts/            # Request/Response DTOs
│   ├── Mappings/                 # AutoMapper profiles
│   └── App_Data/                 # Database files (.mdf, .ldf)
│
├── Presentation/                 # Web UI Layer
│   ├── Usuario.aspx              # User registration page
│   ├── UsuarioConsulta.aspx      # User list with CRUD
│   ├── Site.Master               # Master page layout
│   ├── Content/                  # CSS (Bootstrap)
│   ├── Scripts/                  # JavaScript (jQuery, Bootstrap)
│   └── Connected Services/       # WCF Service Reference
│
└── CreateDatabase.sql            # Database setup script
```

---

## ✨ Características

### Página de Registro (Usuario.aspx)
- 📝 Formulario de registro con validaciones
- 📅 Selector de fecha con restricción de fechas futuras
- ✅ Validaciones en cliente y servidor
- 🎉 Modal de confirmación al registrar
- 🔄 Limpieza de formulario automática

### Página de Consulta (UsuarioConsulta.aspx)
- 📊 Grilla interactiva con todos los usuarios
- 🔍 Búsqueda en tiempo real por nombre
- 🚻 Filtro cíclico por sexo (Todos/Masculino/Femenino)
- ⬆️⬇️ Ordenamiento por ID (Ascendente/Descendente)
- 📄 Paginación avanzada con navegación directa
- ✏️ Edición inline de registros
- 🗑️ Eliminación con confirmación
- 📈 Contador de usuarios registrados
- 💬 Mensajes de éxito/error

### Servicio WCF
- 🔌 5 endpoints SOAP disponibles
- 📦 DTOs para Request/Response
- ⚠️ Manejo centralizado de errores
- 📊 Paginación en todos los endpoints de consulta
- 🔒 Validaciones en capa de negocio

### Base de Datos
- 🗄️ Tabla Users con Soft Delete
- 📜 Stored Procedure único para todas las operaciones
- 🔄 Soft Delete con campo DeletedAt
- 📊 Índice filtrado para consultas optimizadas

---

## 🚀 Ejecución

### Opción 1: Ejecutar desde Visual Studio

#### Paso 1: Iniciar el Servicio WCF
1. En **Solution Explorer**, click derecho en **Business**
2. Seleccionar **"Set as StartUp Project"**
3. Presionar **F5**
4. El servicio estará disponible en: `http://localhost:57179/Services/UserService.svc`

#### Paso 2: Iniciar la Aplicación Web
1. **Mantener Business ejecutándose**
2. Click derecho en **Presentation** → **"Set as StartUp Project"**
3. Presionar **F5**
4. Se abrirá en: `http://localhost:50239/`

### Opción 2: Múltiples Proyectos de Inicio
1. Click derecho en la **Solución**
2. **Properties** → **Multiple startup projects**
3. Configurar **Business** y **Presentation** como **Start**
4. Presionar **F5**

---

## 🌐 Endpoints del Servicio

Base URL: `http://localhost:57179/Services/UserService.svc`

### Métodos Disponibles:

| Método | Descripción | Request | Response |
|--------|-------------|---------|----------|
| `AddUser` | Registra un nuevo usuario | AddUserContract | AddUserResponse |
| `GetUserById` | Obtiene un usuario por ID | GetUserByIdContract | UserDto |
| `GetUsersPaginated` | Lista paginada de usuarios | GetUsersPaginatedContract | GetUsersPaginatedResponse |
| `GetUsersByNamePaginated` | Búsqueda paginada por nombre | GetUsersByNamePaginatedContract | GetUsersPaginatedResponse |
| `UpdateUser` | Actualiza datos de usuario | UpdateUserContract | UpdateUserResponse |
| `DeleteUser` | Elimina usuario (soft delete) | DeleteUserContract | DeleteUserResponse |

### Ejemplo de Consumo (SOAP):
```xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" 
                   xmlns:tem="http://tempuri.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:AddUser>
         <tem:contract>
            <tem:name>Juan Pérez</tem:name>
            <tem:birth_date>1990-05-15</tem:birth_date>
            <tem:gender>M</tem:gender>
         </tem:contract>
      </tem:AddUser>
   </soapenv:Body>
</soapenv:Envelope>
```

---

## 🗃️ Esquema de Base de Datos

### Tabla: Users
```sql
UserId       INT IDENTITY(1,1) PRIMARY KEY
Name         VARCHAR(100) NOT NULL
BirthDate    DATE NOT NULL
Gender       CHAR(1) NOT NULL CHECK (Gender IN ('M', 'F'))
DeletedAt    DATETIME NULL
```

**Notas:**
- `DeletedAt` se usa para Soft Delete (borrado lógico)
- Los registros activos tienen `DeletedAt IS NULL`
- Los registros eliminados tienen una fecha en `DeletedAt`

### Stored Procedure: sp_ManageUser
Acciones soportadas:
- `CREATE` - Crear usuario
- `GET_BY_ID` - Consultar por ID
- `GET_PAGINATED` - Listado paginado
- `GET_BY_NAME_PAGINATED` - Búsqueda paginada
- `GET_BY_GENDER_PAGINATED` - Filtrado por sexo
- `UPDATE` - Actualizar usuario
- `REMOVE` - Borrado lógico

---

## 📸 Capturas de Pantalla

### Página de Registro
- Formulario con validaciones en tiempo real
- Selector de fecha HTML5 con restricción de futuras
- Dropdown de sexo (Masculino/Femenino)
- Modal de confirmación al registrar exitosamente

### Página de Consulta
- GridView con paginación (10 registros por página)
- Búsqueda dinámica por nombre
- Filtro cíclico por sexo con iconos
- Ordenamiento ascendente/descendente por ID
- Edición inline de registros
- Eliminación con mensajes de confirmación

---

## 🔧 Configuración Adicional

### Puerto del Servicio WCF
Configurado en `Business/Properties/launchSettings.json` o proyecto:
```
http://localhost:57179
```

### Puerto de la Aplicación Web
Configurado en `Presentation/Properties/launchSettings.json`:
```
http://localhost:50239
```

### Service Reference
La referencia al servicio WCF está configurada en:
```
Presentation/Connected Services/UserServiceReference/
```

Si necesitas regenerarla:
1. Ejecutar **Business** (F5)
2. Click derecho en **Presentation** → **Add** → **Service Reference**
3. Address: `http://localhost:57179/Services/UserService.svc`
4. Namespace: `UserServiceReference`

---

## 📦 Paquetes NuGet Utilizados

### Business Layer:
- Autofac 6.5.0
- Autofac.Wcf 6.1.0
- AutoMapper 12.0.1
- System.ServiceModel.Http 4.10.3

### Presentation Layer:
- Bootstrap 5.3.0
- jQuery 3.7.0
- Modernizr 2.8.3
- Microsoft.CodeDom.Providers.DotNetCompilerPlatform 4.1.0

---

## 🧪 Validaciones Implementadas

### En Entidad User (BDO):
- Nombre no puede estar vacío
- Fecha de nacimiento no puede ser futura
- Sexo debe ser 'M' o 'F'

### En Presentación:
- RequiredFieldValidator para campos obligatorios
- CustomValidator para fecha no futura
- Validaciones de formato en servidor

### En Base de Datos:
- CHECK constraint en Gender
- NOT NULL en campos requeridos

---

## 📝 Notas Técnicas

### Patrón Repository
Todas las operaciones de datos pasan por `IUserRepository`, facilitando:
- Testeo con mocks
- Cambio de proveedor de datos
- Separación de responsabilidades

### DTO Pattern
Los DTOs (Data Transfer Objects) se utilizan para:
- Desacoplar el modelo de dominio del servicio
- Controlar qué datos se exponen
- Versionado de contratos

### Dependency Injection
Autofac gestiona las dependencias:
```csharp
builder.RegisterType<UserLogic>().As<IUserUseCases>();
builder.RegisterType<UserRepository>().As<IUserRepository>();
```

### Error Handling
Manejo centralizado en `ServiceHandler.cs`:
- ValidationErrorException → 400
- NotFoundException → 404
- Unhandled → 500

---

## 🎯 Cumplimiento de Requisitos

| Requisito | Implementación | Estado |
|-----------|----------------|--------|
| Arquitectura por capas | 4 capas (BDO, Persistence, Business, Presentation) | ✅ |
| Página Usuario | Usuario.aspx con 3 campos | ✅ |
| Página Consulta | UsuarioConsulta.aspx con GridView editable | ✅ |
| Servicio WCF | 6 métodos implementados | ✅ |
| Métodos CRUD | Agregar, Modificar, Consultar, Eliminar | ✅ |
| Conexión desde Business | UserLogic gestiona conexiones | ✅ |
| Tabla en BD | Users con campos especificados | ✅ |
| Stored Procedure | sp_ManageUser con CRUD completo | ✅ |

---

## 👨‍💻 Autor

**Desarrollado por:** JorgeGorrito  
**Fecha:** Febrero 2026

---

## 📧 Contacto

- **LinkedIn:** [linkedin.com/in/jorgegorrito](https://www.linkedin.com/in/jorgegorrito/)
- **Email:** j0rg3.4b3ll4@gmail.com

---

## 📄 Licencia

Este proyecto fue desarrollado como prueba técnica para proceso de selección.
