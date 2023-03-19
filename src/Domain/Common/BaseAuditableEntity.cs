
namespace Domain.Common;
public class BaseAuditableEntity : BaseEntity
{
    public int? CreatedBy { get; set; }
    public DateTime? CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? DeletedTime { get; set; }
    public int? DeletedBy { get; set; }
}
