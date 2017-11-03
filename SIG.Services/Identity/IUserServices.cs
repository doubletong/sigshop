using System;
using System.Collections.Generic;
using System.Text;
using SIG.Data.Entity.Identity;

namespace SIG.Services.Identity
{
    public interface IUserServices
    {
        int CreateUser(string userName, string email, string password, string realName);
        bool IsExistEmail(string email);
        bool IsExistEmail(string email, Guid id);
        bool IsExistUserName(string userName);
        User SignIn(string username, string password);
    }
}
