using MediatR;
using my_app.Application.DTOs;

namespace my_app.Application.Features.Users.Queries;

public class GetUserByIdQuery : IRequest<ApiResponse<UserDto>>
{
    public Guid Id { get; set; }
}
