using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

// dotnet ef migrations add "InitialMigration" --project src\Infrastructure --startup-project src\WebAPI --output-dir Persistance\Migrations
// dotnet ef database update --project src\Infrastructure --startup-project src\WebAPI
// dotnet publish -c release -f net7.0 --runtime win-x86 -o C:\Projects\Personal\WebAPI\publish

namespace Infrastructure.Persistance;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly DbContextOptions _options;
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        _options = options;
    }

    public DbSet<Blog> Blogs => Set<Blog>();
    public DbSet<Category> Categories => Set<Category>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}