using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using SIG.Basic.Extensions;
using SIG.Data.Entity.Identity;
using SIG.Infrastructure.Helper;
using SIG.Repository;
using SIG.Repository.PagedList;

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

        public User GetById(Guid id)
        {
            return  _unitOfWork.GetRepository<User>().Find(id);
        }

        public void Update(User user)
        {
            _unitOfWork.GetRepository<User>().Update(user);
        }

        public User SetRole(Guid userId, int[] roleId)
        {
            var user = GetById(userId);
            var roles = _unitOfWork.GetRepository<Role>().GetAll().Where(r => roleId.Contains(r.Id)).ToList();

            user.UserRoles.Clear();
            foreach (Role r in roles)
            {
                user.UserRoles.Add(new UserRole{RoleId = r.Id,UserId = userId});
            }

            Update(user);
            return user;
        }

        /// <summary>
        /// 重设密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool SetPassword(Guid userId, string password)
        {
            var user = GetById(userId);
            //try
            //{

            var securityStamp = Hash.GenerateSalt();
            var pwdHash = Hash.HashPasswordWithSalt(password, securityStamp);

            user.SecurityStamp = Convert.ToBase64String(securityStamp);
            user.PasswordHash = pwdHash;
            this.Update(user);
            //log 

            //_logger.Info(string.Format(Logs.RestPwdMessage, user.EntityName, user.UserName));
            return true;
            //}
            //catch (Exception er)
            //{
            //    var message = String.Format(Logs.ErrorRestPwdMessage, user.EntityName);
            //    _logger.Error(message, er);
            //    return false;
            //}



        }
        public bool Delete(User user)
        {
             _unitOfWork.GetRepository<User>().Delete(user);
            return true;
        }

        public IPagedList<User> GetPagedElements(int pageIndex, int pageSize, string keyword, DateTime? startDate, DateTime? endDate,
            int? roleId, out int count)
        {
            
            Expression<Func<User, bool>> expression = d => true;

            if (!string.IsNullOrEmpty(keyword))
                expression = expression.AndAlso(g => g.UserName.Contains(keyword));
            if (startDate != null)
                expression = expression.AndAlso(m => m.CreateDate >= startDate);
            if (endDate != null)
                expression = expression.AndAlso(m => m.CreateDate <= endDate);
            if (roleId > 0)
                expression = expression.AndAlso(g => g.UserRoles.Any(m => m.RoleId == (int)roleId));

           
            count = _unitOfWork.GetRepository<User>().Count(expression);

            var result = _unitOfWork.GetRepository<User>().GetPagedList(predicate: expression, 
                orderBy: d => d.OrderBy(l => l.CreateDate), 
                pageIndex: pageIndex, pageSize: pageSize);
            

            return result;

        }

    }
}
