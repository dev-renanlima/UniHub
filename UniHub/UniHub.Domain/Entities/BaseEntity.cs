using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniHub.Domain.Entities;

public class BaseEntity
{
    #region privateProperties
    private long? _id = null;
    #endregion

    public virtual long? Id
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }

    public virtual long? GetIdentity() { return this.Id; }

    public virtual void SetIdentity(long? id)
    {
        this.Id = id;
    }

    public DateTime? CreationDate { get; set; } 

    public DateTime? UpdateDate { get; set; }

    public DateTime? DeletionDate { get; set; }
}
