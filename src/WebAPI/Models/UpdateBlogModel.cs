using Domain.Enums;

namespace WebAPI.Models
{
    public class UpdateBlogModel
    {
        public Guid Id { get; set; }
        public BlogState State { get; set; }
        public string TextContent { get; set; }
        public string Header { get; set; }
        public int UpdatedBy { get; set; }

    }
}
