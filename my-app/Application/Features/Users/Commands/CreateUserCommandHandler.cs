using MediatR;
using Microsoft.EntityFrameworkCore;
using my_app.Application.DTOs;
using my_app.Data.Contexts;
using my_app.Domain.Entities;

namespace my_app.Application.Features.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<UserDto>>
{
    private readonly AppDbContext _context;

    public CreateUserCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Check if email already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == request.User.Email.ToLower(), cancellationToken);

            if (existingUser != null)
            {
                return new ApiResponse<UserDto>
                {
                    Success = false,
                    Message = "Email already exists",
                    Error = "DUPLICATE_EMAIL"
                };
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.User.FirstName.Trim(),
                LastName = request.User.LastName.Trim(),
                Email = request.User.Email.ToLower().Trim(),
                Phone = request.User.Phone,
                BirthDay = ParseBirthDay(request.User.BirthDay),
                Occupation = request.User.Occupation.Trim(),
                Sex = request.User.Sex.Trim(),
                Profile = request.User.Profile != null ? Convert.FromBase64String(request.User.Profile) : null,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            var userDto = MapToDto(user);

            return new ApiResponse<UserDto>
            {
                Id = user.Id,
                Message = "User saved successfully",
                Data = userDto,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<UserDto>
            {
                Success = false,
                Message = "Failed to save data. Please try again.",
                Error = ex.Message
            };
        }
    }

    private DateTime ParseBirthDay(string birthDay)
    {
        // Expected format: DD/MM/YYYY
        if (DateTime.TryParseExact(birthDay, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, 
            System.Globalization.DateTimeStyles.None, out var result))
        {
            return result;
        }

        return DateTime.MinValue;
    }

    private UserDto MapToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            BirthDay = user.BirthDay.ToString("dd/MM/yyyy"),
            Occupation = user.Occupation,
            Sex = user.Sex,
            Profile = user.Profile != null ? Convert.ToBase64String(user.Profile) : null
        };
    }
}
