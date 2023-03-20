
using Application.Common.Interfaces;
using MediatR;

namespace Application.Business.Category.Commands;

public class UpdateCategoryCommand :IRequest<Guid>
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public int UpdatedBy { get; set; } = 0;
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = _context.Categories.FirstOrDefault(x => x.Id == request.Id && x.DeletedTime == null);
        if (entity == null)
            return Guid.Empty;

        entity.Name = request.Name;
        entity.UpdatedBy = request.UpdatedBy;
        entity.UpdatedTime = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
