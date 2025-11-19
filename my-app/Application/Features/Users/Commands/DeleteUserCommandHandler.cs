using MediatR;
using Microsoft.EntityFrameworkCore;
using my_app.Application.DTOs;
using my_app.Data.Contexts;

namespace my_app.Application.Features.Users.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse<string>>
{
    private readonly AppDbContext _context;

    public DeleteUserCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "User not found",
                    Error = "USER_NOT_FOUND"
                };
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new ApiResponse<string>
            {
                Message = "User deleted successfully",
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<string>
            {
                Success = false,
                Message = "Failed to delete data. Please try again.",
                Error = ex.Message
            };
        }
    }
}
