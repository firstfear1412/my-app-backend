# Project Summary: my-app

## ✅ Project Created Successfully!

**Location**: `d:\ProjectDemo\DemoTest\Backend\my-app`

---

## 📋 What Has Been Created

### 1. **Complete .NET 9 Web API Project**
- Framework: .NET 9 (ASP.NET Core)
- Language: C# 12
- Architecture: Clean Architecture with CQRS Pattern

### 2. **Database Layer**
- **DbContext**: `Data/Contexts/AppDbContext.cs`
- **Entity**: `Domain/Entities/User.cs`
- **Provider**: SQL Server with Entity Framework Core
- **Configuration**: Code-First approach

### 3. **Application Layer - CQRS Pattern**

#### Commands (Write Operations)
- `CreateUserCommand` + `CreateUserCommandHandler`
- `UpdateUserCommand` + `UpdateUserCommandHandler`
- `DeleteUserCommand` + `DeleteUserCommandHandler`

#### Queries (Read Operations)
- `GetUserByIdQuery` + `GetUserByIdQueryHandler`
- `GetAllUsersQuery` + `GetAllUsersQueryHandler`

### 4. **API Layer**
- `API/Controllers/UsersController.cs` - 5 REST endpoints
  - POST /api/users - Create
  - GET /api/users/:id - Read by ID
  - GET /api/users - Read all
  - PUT /api/users/:id - Update
  - DELETE /api/users/:id - Delete

### 5. **Data Transfer Objects (DTOs)**
- `UserDto` - For API responses
- `CreateUserDto` - For create requests
- `UpdateUserDto` - For update requests
- `ApiResponse<T>` - Standardized response wrapper

---

## 📊 Database Schema

### Users Table

| Column | Type | Constraint |
|--------|------|-----------|
| **Id** | UNIQUEIDENTIFIER | PK, DEFAULT=NEWID() |
| **FirstName** | VARCHAR(100) | NOT NULL |
| **LastName** | VARCHAR(100) | NOT NULL |
| **Email** | VARCHAR(256) | NOT NULL, UNIQUE |
| **Phone** | VARCHAR(20) | NULL |
| **BirthDay** | DATETIME2 | NULL |
| **Occupation** | VARCHAR(100) | NULL |
| **Sex** | VARCHAR(10) | NULL |
| **Profile** | VARBINARY(MAX) | NULL (Base64 image) |
| **CreatedAt** | DATETIME2 | NOT NULL, DEFAULT=GETUTCDATE() |
| **UpdatedAt** | DATETIME2 | NULL |

---

## 🚀 Quick Start Commands

### 1. **Install Dependencies**
```bash
cd my-app
dotnet restore
```

### 2. **Configure Database**
Edit `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=my_app_db;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### 3. **Create Database & Migration**
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. **Run Application**
```bash
dotnet run
```

### 5. **Access API**
- **Swagger UI**: https://localhost:5001/swagger/index.html
- **API Base**: https://localhost:5001/api/users

---

## 📁 Project Structure

```
my-app/
├── Domain/
│   └── Entities/
│       └── User.cs                          # User entity
├── Data/
│   └── Contexts/
│       └── AppDbContext.cs                  # EF Core DbContext
├── Application/
│   ├── DTOs/
│   │   └── UserDto.cs                       # Request/Response DTOs
│   └── Features/Users/
│       ├── Commands/
│       │   ├── CreateUserCommand.cs
│       │   ├── CreateUserCommandHandler.cs
│       │   ├── UpdateUserCommand.cs
│       │   ├── UpdateUserCommandHandler.cs
│       │   ├── DeleteUserCommand.cs
│       │   └── DeleteUserCommandHandler.cs
│       └── Queries/
│           ├── GetUserByIdQuery.cs
│           ├── GetUserByIdQueryHandler.cs
│           ├── GetAllUsersQuery.cs
│           └── GetAllUsersQueryHandler.cs
├── API/
│   └── Controllers/
│       └── UsersController.cs                # REST API endpoints
├── Program.cs                               # Application configuration
├── appsettings.json                         # Connection string
├── appsettings.Development.json
├── my-app.csproj                           # Project file
├── README.md                                # Project documentation
├── CONFIGURATION.md                         # Detailed config guide
└── SETUP.md                                 # Setup instructions
```

---

## 🔧 NuGet Packages Installed

| Package | Version | Purpose |
|---------|---------|---------|
| Microsoft.EntityFrameworkCore | 9.0.0 | ORM |
| Microsoft.EntityFrameworkCore.SqlServer | 9.0.0 | SQL Server provider |
| Microsoft.EntityFrameworkCore.Tools | 9.0.0 | Migrations tools |
| MediatR | 12.1.1 | CQRS pattern |
| MediatR.Extensions.Microsoft.DependencyInjection | 11.1.0 | DI integration |
| Swashbuckle.AspNetCore | 6.4.0 | Swagger UI |

---

## 🔌 API Endpoints

### Create User
```bash
POST /api/users
Content-Type: application/json

{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "phone": "0812345678",
  "birthDay": "19/11/1990",
  "occupation": "engineer",
  "sex": "male",
  "profile": null
}
```

### Get All Users
```bash
GET /api/users
```

### Get User by ID
```bash
GET /api/users/{userId}
```

### Update User
```bash
PUT /api/users/{userId}
Content-Type: application/json

{ ... same as create ... }
```

### Delete User
```bash
DELETE /api/users/{userId}
```

---

## 🎯 Key Features

✅ **Clean Architecture** - Clear separation of concerns  
✅ **CQRS Pattern** - Separated reads and writes  
✅ **Entity Framework Core** - Code-first with migrations  
✅ **MediatR** - Centralized request handling  
✅ **SQL Server** - Robust relational database  
✅ **Swagger/OpenAPI** - Interactive API documentation  
✅ **Async/Await** - Non-blocking operations  
✅ **Dependency Injection** - Loose coupling  
✅ **Error Handling** - Structured responses  
✅ **Email Validation** - Unique constraint on email  

---

## ⚙️ Configuration Details

### Connection String Formats

**Windows Auth (Local)**:
```
Server=.;Database=my_app_db;Trusted_Connection=true;TrustServerCertificate=true;
```

**SQL Server Express**:
```
Server=.\SQLEXPRESS;Database=my_app_db;Trusted_Connection=true;TrustServerCertificate=true;
```

**SQL Auth**:
```
Server=YOUR_SERVER;Database=my_app_db;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=true;
```

### Environment Variables
- `ASPNETCORE_ENVIRONMENT` = `Development` (default)
- Set to `Production` for production deployment

---

## 🧪 Testing

### Using Swagger UI
1. Run application: `dotnet run`
2. Open: https://localhost:5001/swagger/index.html
3. Try all endpoints interactively

### Using curl
```bash
# Create user
curl -X POST https://localhost:5001/api/users \
  -H "Content-Type: application/json" \
  -d '{"firstName":"John","lastName":"Doe","email":"john@example.com"}'

# Get all users
curl https://localhost:5001/api/users
```

### Using Postman
Import endpoints from Swagger or manually create:
- POST: https://localhost:5001/api/users
- GET: https://localhost:5001/api/users
- GET: https://localhost:5001/api/users/{id}
- PUT: https://localhost:5001/api/users/{id}
- DELETE: https://localhost:5001/api/users/{id}

---

## 📚 Documentation Files

1. **README.md** - Project overview and quick start
2. **SETUP.md** - Step-by-step setup instructions
3. **CONFIGURATION.md** - Detailed configuration guide with examples

---

## ✨ Next Steps

1. ✅ Build project: `dotnet build`
2. ✅ Create migration: `dotnet ef migrations add InitialCreate`
3. ✅ Update database: `dotnet ef database update`
4. ✅ Run application: `dotnet run`
5. ✅ Test with Swagger: https://localhost:5001/swagger/index.html

---

## 🔒 Security Notes

For production deployment, add:
- ✓ Authentication (JWT/OAuth)
- ✓ Authorization (Role-based access)
- ✓ Input validation attributes
- ✓ Rate limiting
- ✓ HTTPS enforcement
- ✓ Proper logging
- ✓ CORS configuration

---

## 🆘 Troubleshooting

### Database Connection Failed
1. Verify SQL Server is running
2. Check connection string format
3. Ensure proper permissions on database

### Migration Issues
```bash
# View detailed error
dotnet ef database update --verbose

# Revert migration
dotnet ef database update PreviousMigrationName
```

### Certificate Error
```bash
# Trust development certificate
dotnet dev-certs https --trust
```

### Port Already in Use
```bash
# Run on different port
dotnet run --urls="https://localhost:5002"
```

---

## 📞 Support

Refer to:
- `/README.md` - Overview and examples
- `/SETUP.md` - Installation and configuration
- `/CONFIGURATION.md` - Detailed configuration guide
- Swagger UI - Interactive API documentation

---

## 🎓 Architecture Highlights

### CQRS Pattern (Command Query Responsibility Segregation)
Separates:
- **Commands**: CreateUserCommand, UpdateUserCommand, DeleteUserCommand
- **Queries**: GetUserByIdQuery, GetAllUsersQuery

Benefits:
- Single Responsibility Principle
- Better performance optimization
- Improved testability
- Clear separation of concerns

### Clean Architecture
Layers:
1. **API Layer**: Controllers (REST endpoints)
2. **Application Layer**: Commands, Queries, DTOs
3. **Domain Layer**: Entities (business logic)
4. **Data Layer**: DbContext, Migrations

---

**Project Status**: ✅ Ready for Development

**Last Updated**: November 19, 2025
