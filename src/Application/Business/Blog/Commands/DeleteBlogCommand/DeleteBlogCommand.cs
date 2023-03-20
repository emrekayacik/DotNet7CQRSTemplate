
using Application.Common.Interfaces;
using MediatR;

namespace Application.Business.Blog.Commands;

public class DeleteBlogCommand : IRequest<Guid>
{
    public Guid Id { get; set; } = Guid.Empty;
    public int DeletedBy { get; set; } = 0;
}
public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public DeleteBlogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
    {
        var entity = _context.Blogs.FirstOrDefault(a => a.Id == request.Id && a.DeletedTime == null);

        if (entity == null)
            return Guid.Empty;

        entity.DeletedBy = request.DeletedBy;
        entity.DeletedTime = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
