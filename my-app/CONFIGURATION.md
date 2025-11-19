# My-App Backend Configuration Guide

## Project Structure

```
my-app/
├── API/
│   └── Controllers/
│       └── UsersController.cs          # REST API endpoints
├── Application/
│   ├── DTOs/                           # Data Transfer Objects
│   │   └── UserDto.cs
│   └── Features/
│       └── Users/
│           ├── Commands/               # CQRS Commands (Create, Update, Delete)
│           │   ├── CreateUserCommand.cs
│           │   ├── CreateUserCommandHandler.cs
│           │   ├── UpdateUserCommand.cs
│           │   ├── UpdateUserCommandHandler.cs
│           │   ├── DeleteUserCommand.cs
│           │   └── DeleteUserCommandHandler.cs
│           └── Queries/                # CQRS Queries (Read)
│               ├── GetUserByIdQuery.cs
│               ├── GetUserByIdQueryHandler.cs
│               ├── GetAllUsersQuery.cs
│               └── GetAllUsersQueryHandler.cs
├── Data/
│   └── Contexts/
│       └── AppDbContext.cs             # Entity Framework Core DbContext
├── Domain/
│   └── Entities/
│       └── User.cs                     # User entity model
└── Program.cs                          # Application startup configuration
```

## Architecture

### Design Pattern: CQRS (Command Query Responsibility Segregation)

- **Commands**: CreateUserCommand, UpdateUserCommand, DeleteUserCommand
- **Queries**: GetUserByIdQuery, GetAllUsersQuery
- **MediaTR**: Handles routing commands and queries to their handlers
- **Entity Framework Core**: Code First approach for database management

### Database

**Provider**: SQL Server (SQL Server 2016+)

**Table**: Users

| Column      | Type         | Constraints | Description |
|-------------|------------|-------------|-------------|
| Id          | UNIQUEIDENTIFIER | PK, Default=NEWID() | Primary Key |
| FirstName   | VARCHAR(100) | NOT NULL | User's first name |
| LastName    | VARCHAR(100) | NOT NULL | User's last name |
| Email       | VARCHAR(256) | NOT NULL, UNIQUE | User's email address |
| Phone       | VARCHAR(20) | NULL | User's phone number |
| BirthDay    | DATETIME2 | NULL | User's birth date |
| Occupation  | VARCHAR(100) | NULL | User's occupation |
| Sex         | VARCHAR(10) | NULL | User's gender |
| Profile     | VARBINARY(MAX) | NULL | User's profile picture (Base64) |
| CreatedAt   | DATETIME2 | NOT NULL, Default=GETUTCDATE() | Record creation timestamp |
| UpdatedAt   | DATETIME2 | NULL | Record last update timestamp |

## Prerequisites

- .NET 9 SDK
- SQL Server 2016 or later (or SQL Server Express)
- Visual Studio 2022 or VS Code

## Installation & Configuration

### 1. Install NuGet Packages

```bash
cd my-app
dotnet restore
```

This will install:
- Microsoft.EntityFrameworkCore 9.0.0
- Microsoft.EntityFrameworkCore.SqlServer 9.0.0
- Microsoft.EntityFrameworkCore.Tools 9.0.0
- MediatR 12.3.0
- MediatR.Extensions.Microsoft.DependencyInjection 11.1.0

### 2. Configure Connection String

Edit `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=my_app_db;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

**Options:**

- **SQL Server (Windows Authentication)**:
  ```
  Server=.;Database=my_app_db;Trusted_Connection=true;TrustServerCertificate=true;
  ```

- **SQL Server (SQL Authentication)**:
  ```
  Server=YOUR_SERVER;Database=my_app_db;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=true;
  ```

- **SQL Server Express**:
  ```
  Server=.\SQLEXPRESS;Database=my_app_db;Trusted_Connection=true;TrustServerCertificate=true;
  ```

### 3. Create & Apply Database Migration

```bash
# Create initial migration
dotnet ef migrations add InitialCreate

# Apply migration to database
dotnet ef database update
```

This will automatically create the `Users` table with proper indexes and constraints.

### 4. Build the Application

```bash
dotnet build
```

### 5. Run the Application

```bash
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `https://localhost:5001/swagger/index.html`

## API Endpoints

### Users Management

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/users` | Create new user |
| GET | `/api/users/:id` | Get user by ID |
| GET | `/api/users` | Get all users |
| PUT | `/api/users/:id` | Update user |
| DELETE | `/api/users/:id` | Delete user |

### Request/Response Examples

#### POST /api/users (Create User)

**Request Body**:
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "phone": "0812345678",
  "birthDay": "19/11/1990",
  "occupation": "engineer",
  "sex": "male",
  "profile": "base64_encoded_image_string"
}
```

**Success Response** (200 OK):
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "message": "User saved successfully",
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "firstName": "John",
    "lastName": "Doe",
    "email": "john@example.com",
    "phone": "0812345678",
    "birthDay": "19/11/1990",
    "occupation": "engineer",
    "sex": "male",
    "profile": "base64_encoded_image_string"
  },
  "success": true
}
```

**Error Response** (400 Bad Request):
```json
{
  "success": false,
  "message": "Email already exists",
  "error": "DUPLICATE_EMAIL"
}
```

#### GET /api/users/:id (Get User by ID)

**Success Response** (200 OK):
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "firstName": "John",
    "lastName": "Doe",
    "email": "john@example.com",
    "phone": "0812345678",
    "birthDay": "19/11/1990",
    "occupation": "engineer",
    "sex": "male",
    "profile": "base64_encoded_image_string"
  },
  "success": true
}
```

**Error Response** (404 Not Found):
```json
{
  "success": false,
  "message": "User not found",
  "error": "USER_NOT_FOUND"
}
```

#### GET /api/users (Get All Users)

**Response** (200 OK):
```json
{
  "data": [
    {
      "id": "550e8400-e29b-41d4-a716-446655440000",
      "firstName": "John",
      "lastName": "Doe",
      "email": "john@example.com",
      "phone": "0812345678",
      "birthDay": "19/11/1990",
      "occupation": "engineer",
      "sex": "male",
      "profile": "base64_encoded_image_string"
    }
  ],
  "success": true
}
```

#### PUT /api/users/:id (Update User)

**Request Body**: Same as Create User

**Response**: Same as Create User with 200 OK status

#### DELETE /api/users/:id (Delete User)

**Success Response** (200 OK):
```json
{
  "message": "User deleted successfully",
  "success": true
}
```

**Error Response** (404 Not Found):
```json
{
  "success": false,
  "message": "User not found",
  "error": "USER_NOT_FOUND"
}
```

## Data Validation & Processing

The application automatically handles:

1. **Email**: Converted to lowercase and trimmed
2. **Text Fields**: Trimmed before saving
3. **Phone Number**: Stored as provided (formatted by client if needed)
4. **BirthDay**: Parsed from `DD/MM/YYYY` format to DateTime
5. **Profile**: Base64 string converted to binary data
6. **Timestamps**: Auto-populated (CreatedAt, UpdatedAt)
7. **Duplicate Email**: Prevents duplicate email addresses

## Development Notes

### Using CQRS Pattern

The application separates reads (Queries) and writes (Commands):

- **Queries** (read-only):
  ```csharp
  var query = new GetUserByIdQuery { Id = userId };
  var result = await _mediator.Send(query);
  ```

- **Commands** (state-changing):
  ```csharp
  var command = new CreateUserCommand { User = userDto };
  var result = await _mediator.Send(command);
  ```

### Adding New Features

To add a new feature (e.g., Search Users):

1. Create Query in `Application/Features/Users/Queries/SearchUsersQuery.cs`
2. Create Handler: `SearchUsersQueryHandler.cs`
3. Add endpoint in `UsersController.cs`
4. Register in MediatR (auto-registered via assembly scanning)

### Entity Framework Migrations

```bash
# Create migration
dotnet ef migrations add MigrationName

# Apply migration
dotnet ef database update

# Revert to previous migration
dotnet ef database update PreviousMigrationName

# Remove last migration
dotnet ef migrations remove
```

## Troubleshooting

### Database Connection Error

1. Verify SQL Server is running
2. Check connection string in `appsettings.json`
3. Ensure database has proper permissions

### Migration Fails

1. Check for syntax errors
2. Ensure all entities are properly configured
3. Run: `dotnet ef database update --verbose`

### Swagger UI Not Loading

1. Ensure app is running in Development mode
2. Check HTTPS certificate is valid
3. Try accessing via HTTP instead

## Performance Considerations

1. **AsNoTracking()**: Used in read-only queries
2. **Unique Index on Email**: Prevents duplicate queries
3. **Guid Primary Key**: Allows distributed database design
4. **Base64 Profile**: Consider chunking large images in production

## Security Notes

1. Add authentication/authorization in production
2. Implement rate limiting
3. Add input validation attributes
4. Use HTTPS in production
5. Implement proper error logging
