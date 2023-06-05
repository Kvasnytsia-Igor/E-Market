namespace Domain.Common;

public abstract class BaseAuditableEntity
{
    public Guid Id { get; set; }

    public DateTime Created { get; set; } = DateTime.Now;

    public string? CreatedBy { get; set; } 

    public DateTime? LastModified { get; set; }

    public  string? LastModifiedBy { get; set; }
}
