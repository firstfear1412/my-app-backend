# My App Backend

A .NET 8 backend application with Entity Framework Core, built with Clean Architecture principles using CQRS pattern.

## Quick Start

### Prerequisites
- .NET 8 SDK or higher
- SQL Server (or any EF Core supported database)

### Setup & Run

1. **Clone the repository**
   ```bash
   git clone https://github.com/firstfear1412/my-app-backend.git
   cd my-app-backend
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure database connection**
   - Edit `my-app/appsettings.json` with your database connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=YourDB;Integrated Security=true;"
     }
   }
   ```

4. **Apply migrations**
   ```bash
   dotnet ef database update --project my-app/my-app.csproj
   ```

5. **Run the application**
   ```bash
   dotnet run --project my-app/my-app.csproj
   ```

The API will be available at:
- **HTTP**: `http://localhost:5089`
- **HTTPS**: `https://localhost:7027`

Access Swagger UI at `/swagger` endpoint (e.g., `http://localhost:5089/swagger`)

## Configuration

### appsettings.json

Create and configure `appsettings.json` in the `my-app` directory:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MyAppDb;Integrated Security=true;TrustServerCertificate=true;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

### appsettings.Development.json

For development environment configuration (not tracked by git):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MyAppDb;Integrated Security=true;TrustServerCertificate=true;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Warning"
    }
  }
}
```

> **Note:** `appsettings.json` is ignored by git for security. Create your own copy with your database credentials.

## Project Structure

```
my-app/
├── API/
│   └── Controllers/          # API endpoints
├── Application/
│   ├── DTOs/                 # Data Transfer Objects
│   └── Features/
│       └── Users/
│           ├── Commands/     # Create, Update, Delete operations
│           └── Queries/      # Get operations
├── Domain/
│   └── Entities/             # Core business entities
├── Data/
│   └── Contexts/             # DbContext configuration
├── Migrations/               # EF Core migrations
└── Program.cs                # Application entry point
```

## API Endpoints

### Users

- **GET** `/api/users` - Get all users
- **GET** `/api/users/{id}` - Get user by ID
- **POST** `/api/users` - Create new user
- **PUT** `/api/users/{id}` - Update user
- **DELETE** `/api/users/{id}` - Delete user

### Request/Response Example

**Create User (POST /api/users)**

Request:
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "phone": "0812345678",
  "birthDay": "19/11/1990",
  "occupation": "engineer",
  "sex": "male"
}
```

Success Response:
```json
{
  "id": "uuid",
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "phone": "0812345678",
  "birthDay": "19/11/1990",
  "occupation": "engineer",
  "sex": "male"
}
```

## Development

### Build
```bash
dotnet build
```

### Run Tests
```bash
dotnet test
```

### Watch Mode (Auto-reload on file changes)
```bash
dotnet watch run --project my-app/my-app.csproj
```

## Database Migrations

### Create a new migration
```bash
dotnet ef migrations add <MigrationName> --project my-app/my-app.csproj
```

### Update database
```bash
dotnet ef database update --project my-app/my-app.csproj
```

### Remove last migration
```bash
dotnet ef migrations remove --project my-app/my-app.csproj
```

## Architecture

This project follows **Clean Architecture** with **CQRS (Command Query Responsibility Segregation)** pattern:

- **Commands**: Handle write operations (Create, Update, Delete)
- **Queries**: Handle read operations (Get)
- **DTOs**: Transfer data between layers
- **Entities**: Core domain models
- **DbContext**: Database access layer

## Git Ignore

The following are automatically ignored:

- `bin/`, `obj/` - Build artifacts
- `appsettings.json`, `appsettings.*.json` - Configuration files with sensitive data
- `.vs/`, `.vscode/` - IDE files
- `.github/instructions/` - GitHub instructions
- `*.db`, `*.log` - Runtime files

## Troubleshooting

### Database connection fails
- Verify your connection string in `appsettings.json`
- Check SQL Server is running
- Ensure database user has proper permissions

### Migrations fail
```bash
# Drop database (development only!)
dotnet ef database drop --project my-app/my-app.csproj

# Recreate from scratch
dotnet ef database update --project my-app/my-app.csproj
```

### Port already in use
Modify `launchSettings.json` to use different ports

## Contributing

1. Create a feature branch
2. Commit changes
3. Push to origin
4. Create a Pull Request

## License

This project is licensed under the MIT License.

## Contact

For issues or questions, please open an issue on GitHub.
