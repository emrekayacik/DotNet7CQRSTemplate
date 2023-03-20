namespace WebAPI.Models
{
    public class UpdateCategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int UpdatedBy { get; set; }
    }
}
