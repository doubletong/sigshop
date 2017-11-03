using System;
using System.Collections.Generic;
using System.Text;

namespace SIG.Services.Identity
{
    public interface IUserServices
    {
        int CreateUser(string userName, string email, string password, string realName);
    }
}
