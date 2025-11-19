using MediatR;
using my_app.Application.DTOs;

namespace my_app.Application.Features.Users.Commands;

public class DeleteUserCommand : IRequest<ApiResponse<string>>
{
    public Guid Id { get; set; }
}
