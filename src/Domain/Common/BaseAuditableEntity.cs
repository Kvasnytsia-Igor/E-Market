namespace Domain.Common;

public abstract class BaseAuditableEntity
{
    public Guid Id { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime LastModified { get; set; }

    public  string LastModifiedBy { get; set; } = string.Empty;
}
