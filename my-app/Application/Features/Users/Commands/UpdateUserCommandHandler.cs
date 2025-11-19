using MediatR;
using Microsoft.EntityFrameworkCore;
using my_app.Application.DTOs;
using my_app.Data.Contexts;

namespace my_app.Application.Features.Users.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse<UserDto>>
{
    private readonly AppDbContext _context;

    public UpdateUserCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user == null)
            {
                return new ApiResponse<UserDto>
                {
                    Success = false,
                    Message = "User not found",
                    Error = "USER_NOT_FOUND"
                };
            }

            // Check if email already exists (but not the same user)
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == request.User.Email.ToLower() && u.Id != request.Id, cancellationToken);

            if (existingUser != null)
            {
                return new ApiResponse<UserDto>
                {
                    Success = false,
                    Message = "Email already exists",
                    Error = "DUPLICATE_EMAIL"
                };
            }

            user.FirstName = request.User.FirstName.Trim();
            user.LastName = request.User.LastName.Trim();
            user.Email = request.User.Email.ToLower().Trim();
            user.Phone = request.User.Phone;
            user.BirthDay = ParseBirthDay(request.User.BirthDay);
            user.Occupation = request.User.Occupation.Trim();
            user.Sex = request.User.Sex.Trim();
            user.Profile = request.User.Profile != null ? Convert.FromBase64String(request.User.Profile) : null;
            user.UpdatedAt = DateTime.UtcNow;

            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            var userDto = MapToDto(user);

            return new ApiResponse<UserDto>
            {
                Id = user.Id,
                Message = "User updated successfully",
                Data = userDto,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<UserDto>
            {
                Success = false,
                Message = "Failed to update data. Please try again.",
                Error = ex.Message
            };
        }
    }

    private DateTime ParseBirthDay(string birthDay)
    {
        if (DateTime.TryParseExact(birthDay, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None, out var result))
        {
            return result;
        }

        return DateTime.MinValue;
    }

    private UserDto MapToDto(Domain.Entities.User user)
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
