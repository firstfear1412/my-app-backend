using MediatR;
using Microsoft.EntityFrameworkCore;
using my_app.Application.DTOs;
using my_app.Data.Contexts;

namespace my_app.Application.Features.Users.Queries;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<UserDto>>
{
    private readonly AppDbContext _context;

    public GetUserByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user == null)
            {
                return new ApiResponse<UserDto>
                {
                    Success = false,
                    Message = "User not found",
                    Error = "USER_NOT_FOUND"
                };
            }

            var userDto = MapToDto(user);

            return new ApiResponse<UserDto>
            {
                Id = user.Id,
                Data = userDto,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<UserDto>
            {
                Success = false,
                Message = "Failed to fetch data. Please try again.",
                Error = ex.Message
            };
        }
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
