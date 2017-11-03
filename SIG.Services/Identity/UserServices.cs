using System;
using System.Collections.Generic;
using System.Text;
using SIG.Data.Entity.Identity;
using SIG.Infrastructure.Helper;
using SIG.Repository;

namespace SIG.Services.Identity
{
    public class UserServices :IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public int CreateUser(string userName, string email, string password, string realName)
        {
            var orgUsers = _unitOfWork.GetRepository<User>().GetFirstOrDefault(u => u, u => u.Email == email);
            if (orgUsers != null)
            {
                return 1; //1 邮箱已存在
            }

            orgUsers = _unitOfWork.GetRepository<User>().GetFirstOrDefault(u => u, u => u.UserName == userName);
            if (orgUsers != null)
            {
                return 2; //1 用户名已存在
            }


            var securityStamp = Hash.GenerateSalt();
            var passwordHash = Hash.HashPasswordWithSalt(password, securityStamp);

            var newUser = new User()
            {
                UserName = userName,
                RealName = realName,
                Email = email,
                SecurityStamp = Convert.ToBase64String(securityStamp),
                PasswordHash = passwordHash,
                CreateDate = DateTime.Now,
                IsActive = true
            };

            //_logger.LogInformation(string.Format(Logs.CreateMessage, EntityNames.User, userName));
            _unitOfWork.GetRepository<User>().Insert(newUser);
            _unitOfWork.SaveChanges();
            // SetUserCookies(false, newUser);

            return 0;

        }
    }
}
