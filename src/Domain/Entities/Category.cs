using Domain.Common;

namespace Domain.Entities
{
    public class Category : BaseAuditableEntity
    {
        public string? Name { get; set; }
        public virtual IList<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
