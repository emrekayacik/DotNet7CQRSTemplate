using Application.Common.Interfaces;
using MediatR;

namespace Application.Business.Category.Commands;

public class DeleteCategoryCommand : IRequest<Guid>
{
    public Guid Id { get; set; } = Guid.Empty;
    public int DeletedBy { get; set; } = 0;
}
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public DeleteCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = _context.Categories.FirstOrDefault(a => a.Id == request.Id && a.DeletedTime == null);

        if (entity == null)
            return Guid.Empty;

        entity.DeletedBy = request.DeletedBy;
        entity.DeletedTime = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
