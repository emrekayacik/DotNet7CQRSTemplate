using BC = BCrypt.Net.BCrypt;
using Application.Common.Interfaces;
using MediatR;
using BCrypt.Net;

namespace Application.Business.User.Commands;

public class CreateUserCommand : IRequest<Guid>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.User
        {
            Username = request.Username,
            Email = request.Email,
            Name = request.Name,
            Surname = request.Surname,
            CreatedTime = DateTime.Now,
            CreatedBy = 1,
            Password = BC.EnhancedHashPassword(request.Password, HashType.SHA512)
        };

        _context.Users.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
