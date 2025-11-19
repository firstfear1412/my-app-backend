namespace my_app.Application.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string BirthDay { get; set; } = string.Empty;
    public string Occupation { get; set; } = string.Empty;
    public string Sex { get; set; } = string.Empty;
    public string? Profile { get; set; }
}

public class CreateUserDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string BirthDay { get; set; } = string.Empty;
    public string Occupation { get; set; } = string.Empty;
    public string Sex { get; set; } = string.Empty;
    public string? Profile { get; set; }
}

public class UpdateUserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string BirthDay { get; set; } = string.Empty;
    public string Occupation { get; set; } = string.Empty;
    public string Sex { get; set; } = string.Empty;
    public string? Profile { get; set; }
}

public class ApiResponse<T>
{
    public Guid? Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public bool Success { get; set; } = true;
    public string? Error { get; set; }
}
