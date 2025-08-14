using UniHub.Domain.Extensions;

namespace UniHub.Domain.Entities;

public class BaseEntity
{
    public BaseEntity()
    {
        Id = Guid.NewGuid();
        InternalIdentifier = UUIDExtensions.GenerateNanoId();
    }

    public virtual Guid Id { get; set; }
    public virtual string InternalIdentifier { get; set; }
    public virtual DateTime? CreationDate { get; set; }
    public virtual DateTime? UpdateDate { get; set; }
    public virtual DateTime? DeletionDate { get; set; }

    public virtual Guid GetIdentity() => Id;
    public virtual void SetIdentity(Guid id) => Id = id;
    public virtual void SetDates()
    {
        CreationDate = DateTime.UtcNow;
        UpdateDate = DateTime.UtcNow;
    }
}
