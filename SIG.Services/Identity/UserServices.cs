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

        public User SignIn(string username, string password)
        {
            var user = _unitOfWork.GetRepository<User>().GetFirstOrDefault(u=>u,u=>u.UserName == username );
            if (user == null) return null;

            var salt = Convert.FromBase64String(user.SecurityStamp);
            var pwdHash = Hash.HashPasswordWithSalt(password, salt);

            return user.PasswordHash == pwdHash ? user : null;
          
        }
        public int CreateUser(string userName, string email, string password, string realName)
        {
            
            if (IsExistEmail(email))
            {
                return 1; //1 邮箱已存在
            }
           
            if (IsExistUserName(userName))
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

        public bool IsExistEmail(string email)
        {
           var result = _unitOfWork.GetRepository<User>().Count(u => u.Email == email);  
           return result > 0;
        }
        public bool IsExistEmail(string email, Guid id)
        {
            var result = _unitOfWork.GetRepository<User>().Count(u => u.Email == email && u.Id!=id);
            return result > 0;
        }

        public bool IsExistUserName(string userName)
        {
            var result = _unitOfWork.GetRepository<User>().Count(u => u.UserName == userName);
            return result > 0;
        }

    }
}
