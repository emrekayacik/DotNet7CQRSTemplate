using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;
public class Blog : BaseAuditableEntity
{
    public string? Header { get; set; }
    public string? TextContent { get; set; }
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }
    public BlogState State { get; set; }
}

