using MediatR;
using my_app.Application.DTOs;

namespace my_app.Application.Features.Users.Commands;

public class CreateUserCommand : IRequest<ApiResponse<UserDto>>
{
    public CreateUserDto User { get; set; } = null!;
}
