using MediatR;
using my_app.Application.DTOs;

namespace my_app.Application.Features.Users.Queries;

public class GetAllUsersQuery : IRequest<ApiResponse<List<UserDto>>>
{
}
