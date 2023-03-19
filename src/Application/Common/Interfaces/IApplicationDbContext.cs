using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Blog> Blogs { get; }
    DbSet<Category> Categories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}