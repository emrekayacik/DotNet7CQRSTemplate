using Application.Common.Interfaces;
using MediatR;

namespace Application.Business.Category.Commands.CreateCategoryCommand;
public class CreateCategoryCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public int? CreatedBy { get; set; } = 0;
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Category
        {
            Name = request.Name,
            CreatedBy = request.CreatedBy,
            CreatedTime = DateTime.Now
        };

        _context.Categories.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
