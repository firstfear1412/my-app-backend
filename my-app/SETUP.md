# My-App Backend Setup Instructions

## Quick Start Guide

### Prerequisites
- .NET 9 SDK
- SQL Server 2016+ (or SQL Server Express)
- Visual Studio 2022 / VS Code

---

## Step 1: Restore Dependencies

```bash
cd my-app
dotnet restore
```

---

## Step 2: Configure Database Connection

Edit `appsettings.json` and set your SQL Server connection string:

### Option 1: SQL Server with Windows Authentication (Local)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=my_app_db;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### Option 2: SQL Server Express
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=my_app_db;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### Option 3: SQL Server with SQL Authentication
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=my_app_db;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=true;"
  }
}
```

---

## Step 3: Create Database & Apply Migrations

### Create Migration
```bash
dotnet ef migrations add InitialCreate
```

### Apply Migration
```bash
dotnet ef database update
```

This will automatically create:
- Database: `my_app_db`
- Table: `Users` with all required columns and indexes

---

## Step 4: Build Project

```bash
dotnet build
```

Expected output: `Build succeeded.`

---

## Step 5: Run Application

```bash
dotnet run
```

Application will start at:
- **HTTP**: http://localhost:5000
- **HTTPS**: https://localhost:5001
- **Swagger UI**: https://localhost:5001/swagger/index.html

---

## Testing the API

### Using curl or Postman

#### 1. Create a User (POST)
```bash
curl -X POST https://localhost:5001/api/users \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "John",
    "lastName": "Doe",
    "email": "john@example.com",
    "phone": "0812345678",
    "birthDay": "19/11/1990",
    "occupation": "engineer",
    "sex": "male",
    "profile": null
  }'
```

#### 2. Get All Users (GET)
```bash
curl -X GET https://localhost:5001/api/users
```

#### 3. Get User by ID (GET)
```bash
curl -X GET https://localhost:5001/api/users/{userId}
```

#### 4. Update User (PUT)
```bash
curl -X PUT https://localhost:5001/api/users/{userId} \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "Jane",
    "lastName": "Doe",
    "email": "jane@example.com",
    "phone": "0812345678",
    "birthDay": "19/11/1990",
    "occupation": "doctor",
    "sex": "female",
    "profile": null
  }'
```

#### 5. Delete User (DELETE)
```bash
curl -X DELETE https://localhost:5001/api/users/{userId}
```

---

## Project Structure Explanation

### Domain Layer (`Domain/`)
- **Entities**: Core business models (User.cs)
- Contains no dependencies on other layers

### Data Layer (`Data/`)
- **DbContext**: AppDbContext for Entity Framework Core
- Database mapping and configuration
- SQL Server provider configuration

### Application Layer (`Application/`)
- **DTOs**: Data Transfer Objects for API contracts
- **Features/Users/Commands**: Write operations using CQRS
  - CreateUserCommand, UpdateUserCommand, DeleteUserCommand
  - Their respective handlers that execute business logic
- **Features/Users/Queries**: Read operations using CQRS
  - GetUserByIdQuery, GetAllUsersQuery
  - Their respective handlers for data retrieval

### API Layer (`API/`)
- **Controllers**: REST API endpoints
- UsersController exposes CRUD operations

---

## Database Schema

### Users Table

```sql
CREATE TABLE [Users] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [FirstName] VARCHAR(100) NOT NULL,
    [LastName] VARCHAR(100) NOT NULL,
    [Email] VARCHAR(256) NOT NULL UNIQUE,
    [Phone] VARCHAR(20),
    [BirthDay] DATETIME2,
    [Occupation] VARCHAR(100),
    [Sex] VARCHAR(10),
    [Profile] VARBINARY(MAX),
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedAt] DATETIME2
)
```

---

## Common Issues & Solutions

### Issue: Database Connection Failed
**Solution**: 
1. Verify SQL Server is running
2. Check connection string in appsettings.json
3. Ensure user has proper permissions

### Issue: Migration Failed
**Solution**:
```bash
# Run with verbose output
dotnet ef database update --verbose

# Check existing migrations
dotnet ef migrations list
```

### Issue: Port Already in Use
**Solution**: 
```bash
# Run on specific port
dotnet run --urls="https://localhost:5002"
```

### Issue: Certificate Error
**Solution**:
```bash
# Trust HTTPS certificate for development
dotnet dev-certs https --trust
```

---

## Environment Configuration

### Development (default)
- ASPNETCORE_ENVIRONMENT = Development
- Swagger UI enabled
- Detailed error logging

### Production
Edit `appsettings.Production.json` and set `ASPNETCORE_ENVIRONMENT=Production`

---

## NuGet Packages Used

| Package | Version | Purpose |
|---------|---------|---------|
| Microsoft.EntityFrameworkCore | 9.0.0 | ORM Framework |
| Microsoft.EntityFrameworkCore.SqlServer | 9.0.0 | SQL Server Provider |
| Microsoft.EntityFrameworkCore.Tools | 9.0.0 | Migration Tools |
| MediatR | 12.1.1 | CQRS Pattern Implementation |
| MediatR.Extensions.Microsoft.DependencyInjection | 11.1.0 | DI Integration |
| Swashbuckle.AspNetCore | 6.4.0 | Swagger/OpenAPI |

---

## Next Steps

1. Implement authentication/authorization
2. Add input validation attributes
3. Implement error handling middleware
4. Add logging
5. Write unit tests
6. Deploy to cloud platform

---

## References

- [.NET 9 Documentation](https://learn.microsoft.com/dotnet/)
- [Entity Framework Core](https://learn.microsoft.com/ef/)
- [MediatR Documentation](https://github.com/jbogard/MediatR)
- [CQRS Pattern](https://docs.microsoft.com/azure/architecture/patterns/cqrs)
