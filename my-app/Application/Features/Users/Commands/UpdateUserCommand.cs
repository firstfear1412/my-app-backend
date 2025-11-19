using MediatR;
using my_app.Application.DTOs;

namespace my_app.Application.Features.Users.Commands;

public class UpdateUserCommand : IRequest<ApiResponse<UserDto>>
{
    public Guid Id { get; set; }
    public UpdateUserDto User { get; set; } = null!;
}
