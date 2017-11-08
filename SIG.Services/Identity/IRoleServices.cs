using System;
using System.Collections.Generic;
using System.Text;
using SIG.Data.Entity.Identity;

namespace SIG.Services.Identity
{
    public interface IRoleServices
    {
        IEnumerable<Role> GetAll();
    }
}
