using System;
using System.Collections.Generic;
using System.Text;

namespace SIG.Data.Entity
{
    public interface IAuditedEntity
    {
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        string UpdatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}
