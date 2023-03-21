using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Blog> Blogs { get; }
    DbSet<Category> Categories { get; }
    DbSet<User> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}