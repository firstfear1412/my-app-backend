using MediatR;
using Microsoft.EntityFrameworkCore;
using my_app.Application.DTOs;
using my_app.Data.Contexts;

namespace my_app.Application.Features.Users.Queries;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ApiResponse<List<UserDto>>>
{
    private readonly AppDbContext _context;

    public GetAllUsersQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _context.Users
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var userDtos = users.Select(MapToDto).ToList();

            return new ApiResponse<List<UserDto>>
            {
                Data = userDtos,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<UserDto>>
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
