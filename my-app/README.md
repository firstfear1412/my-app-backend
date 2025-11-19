# My-App Backend API

A modern .NET 9 Web API application built with CQRS pattern, Entity Framework Core, and SQL Server.

## Overview

`my-app` is a RESTful API for managing users with the following features:

✅ **CQRS Pattern** - Separated read (Queries) and write (Commands) operations  
✅ **MediatR** - Central request/response handling  
✅ **Entity Framework Core** - Code-first database design  
✅ **SQL Server** - Robust relational database  
✅ **Swagger/OpenAPI** - Interactive API documentation  
✅ **Clean Architecture** - Organized layer separation  

---

## Key Features

### User Management API
- Create new users with validation
- Retrieve user by ID
- Fetch all users
- Update user information
- Delete users
- Automatic email uniqueness validation
- Base64 profile image support

### Architecture Highlights
- **Domain Layer**: Pure business entities (User)
- **Data Layer**: Entity Framework Core with SQL Server
- **Application Layer**: CQRS commands and queries
- **API Layer**: RESTful endpoints with Swagger UI

### Database Design
- GUID primary keys for distributed systems
- Unique email constraint
- Automatic timestamps (CreatedAt, UpdatedAt)
- Base64 support for profile images
- Efficient indexing on Email field

---

## Tech Stack

| Technology | Version | Purpose |
|-----------|---------|---------|
| .NET | 9.0 | Framework |
| C# | 12.0 | Language |
| Entity Framework Core | 9.0.0 | ORM |
| SQL Server | 2016+ | Database |
| MediatR | 12.1.1 | CQRS |
| Swashbuckle | 6.4.0 | Swagger |

---

## Quick Start

### Prerequisites
```bash
# Check .NET installation
dotnet --version  # Should be 9.x or later
```

### Installation
```bash
# 1. Restore dependencies
dotnet restore

# 2. Update appsettings.json with your SQL Server connection string

# 3. Create and apply database migrations
dotnet ef migrations add InitialCreate
dotnet ef database update

# 4. Run the application
dotnet run
```

### Access API
```
Swagger UI: https://localhost:5001/swagger/index.html
API Base URL: https://localhost:5001/api
```

---

## API Endpoints

### POST /api/users
Create a new user
```json
Request:
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "phone": "0812345678",
  "birthDay": "19/11/1990",
  "occupation": "engineer",
  "sex": "male",
  "profile": "base64_string_or_null"
}

Response (200 OK):
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "message": "User saved successfully",
  "data": { ... },
  "success": true
}
```

### GET /api/users/:id
Get user by ID
```json
Response (200 OK):
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "firstName": "John",
    "lastName": "Doe",
    "email": "john@example.com",
    ...
  },
  "success": true
}
```

### GET /api/users
Get all users
```json
Response (200 OK):
{
  "data": [ ... ],
  "success": true
}
```

### PUT /api/users/:id
Update user
```json
Request: (Same as POST)

Response (200 OK): (Same as POST)
```

### DELETE /api/users/:id
Delete user
```json
Response (200 OK):
{
  "message": "User deleted successfully",
  "success": true
}
```

---

## Project Structure

```
my-app/
├── API/
│   └── Controllers/
│       └── UsersController.cs          # REST endpoints
├── Application/
│   ├── DTOs/                           # Request/Response models
│   │   └── UserDto.cs
│   └── Features/
│       └── Users/
│           ├── Commands/               # Write operations (CQRS)
│           │   ├── CreateUserCommand.cs
│           │   ├── CreateUserCommandHandler.cs
│           │   ├── UpdateUserCommand.cs
│           │   ├── UpdateUserCommandHandler.cs
│           │   ├── DeleteUserCommand.cs
│           │   └── DeleteUserCommandHandler.cs
│           └── Queries/                # Read operations (CQRS)
│               ├── GetUserByIdQuery.cs
│               ├── GetUserByIdQueryHandler.cs
│               ├── GetAllUsersQuery.cs
│               └── GetAllUsersQueryHandler.cs
├── Data/
│   └── Contexts/
│       └── AppDbContext.cs             # EF Core DbContext
├── Domain/
│   └── Entities/
│       └── User.cs                     # User entity
├── Program.cs                          # Startup configuration
├── appsettings.json                    # Configuration
├── CONFIGURATION.md                    # Detailed configuration guide
├── SETUP.md                            # Setup instructions
└── README.md                           # This file
```

---

## Database Schema

### Users Table

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| Id | UNIQUEIDENTIFIER | PK, DEFAULT=NEWID() | Primary Key |
| FirstName | VARCHAR(100) | NOT NULL | User's first name |
| LastName | VARCHAR(100) | NOT NULL | User's last name |
| Email | VARCHAR(256) | NOT NULL, UNIQUE | Email address |
| Phone | VARCHAR(20) | NULL | Phone number |
| BirthDay | DATETIME2 | NULL | Date of birth |
| Occupation | VARCHAR(100) | NULL | Occupation |
| Sex | VARCHAR(10) | NULL | Gender |
| Profile | VARBINARY(MAX) | NULL | Profile image (Base64) |
| CreatedAt | DATETIME2 | NOT NULL, DEFAULT=GETUTCDATE() | Creation timestamp |
| UpdatedAt | DATETIME2 | NULL | Last update timestamp |

---

## CQRS Pattern Implementation

### Commands (Write Operations)
```csharp
// Example: Creating a user
var command = new CreateUserCommand { User = userDto };
var result = await _mediator.Send(command);
```

- `CreateUserCommand`: Create new user
- `UpdateUserCommand`: Update existing user
- `DeleteUserCommand`: Delete user

### Queries (Read Operations)
```csharp
// Example: Getting a user
var query = new GetUserByIdQuery { Id = userId };
var result = await _mediator.Send(query);
```

- `GetUserByIdQuery`: Fetch single user
- `GetAllUsersQuery`: Fetch all users

---

## Configuration

### Connection String Options

**Windows Authentication (Local)**
```
Server=.;Database=my_app_db;Trusted_Connection=true;TrustServerCertificate=true;
```

**SQL Server Express**
```
Server=.\SQLEXPRESS;Database=my_app_db;Trusted_Connection=true;TrustServerCertificate=true;
```

**SQL Authentication**
```
Server=YOUR_SERVER;Database=my_app_db;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=true;
```

Set in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONNECTION_STRING"
  }
}
```

---

## Development

### Creating New Features

To add a new feature (e.g., search users):

1. Create Query in `Application/Features/Users/Queries/`
```csharp
public class SearchUsersQuery : IRequest<ApiResponse<List<UserDto>>>
{
    public string SearchTerm { get; set; }
}
```

2. Create Handler
```csharp
public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, ApiResponse<List<UserDto>>>
{
    // Implementation
}
```

3. Add Endpoint in `UsersController`
```csharp
[HttpGet("search")]
public async Task<ActionResult<ApiResponse<List<UserDto>>>> SearchUsers(string term)
{
    // Call handler via mediator
}
```

### Database Migrations

```bash
# Create new migration
dotnet ef migrations add MigrationName

# Apply migrations
dotnet ef database update

# Revert to previous migration
dotnet ef database update PreviousMigrationName

# View migration history
dotnet ef migrations list
```

---

## Best Practices Implemented

✅ **Separation of Concerns** - Clear layer organization  
✅ **CQRS Pattern** - Separated reads and writes  
✅ **Repository Pattern** - Data access abstraction  
✅ **DTOs** - Request/response contracts  
✅ **Validation** - Input validation and unique constraints  
✅ **Error Handling** - Structured error responses  
✅ **Async/Await** - Non-blocking operations  
✅ **Dependency Injection** - Loosely coupled services  

---

## Security Notes

⚠️ **Development Only** - Current implementation is for development  

For production, add:
- Authentication (JWT, OAuth)
- Authorization (Role-based access)
- Input validation attributes
- Rate limiting
- HTTPS enforced
- Proper error logging
- SQL injection prevention (already handled by EF Core)

---

## Troubleshooting

### Database Connection Error
```bash
# Verify SQL Server is running
# Check connection string in appsettings.json
# Ensure proper permissions
```

### Certificate Error
```bash
dotnet dev-certs https --trust
```

### Port Already in Use
```bash
dotnet run --urls="https://localhost:5002"
```

### Migration Issues
```bash
dotnet ef database update --verbose
```

---

## Documentation

- [CONFIGURATION.md](./CONFIGURATION.md) - Detailed configuration guide
- [SETUP.md](./SETUP.md) - Step-by-step setup instructions

---

## Testing

Access Swagger UI to test endpoints:
```
https://localhost:5001/swagger/index.html
```

Or use curl:
```bash
curl -X POST https://localhost:5001/api/users \
  -H "Content-Type: application/json" \
  -d '{"firstName":"John","lastName":"Doe","email":"john@example.com"}'
```

---

## Performance Considerations

- GUID primary keys allow distributed databases
- Email indexed for fast lookups
- AsNoTracking() used in read-only queries
- Profile images stored as Base64 (consider chunking for large files)

---

## License

This project is part of the DemoTest Backend solution.

---

## Support

For issues or questions, refer to:
- CONFIGURATION.md - Detailed configuration
- SETUP.md - Setup instructions
- Swagger UI - Interactive API documentation

---

**Built with .NET 9 and C# 12**
