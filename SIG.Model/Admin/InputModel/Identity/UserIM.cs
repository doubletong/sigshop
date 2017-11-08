using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using SIG.Data.Enums;

namespace SIG.Model.Admin.InputModel.Identity
{
    public class UserIM
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "邮箱")]

        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string RealName { get; set; }
        public string PhotoUrl { get; set; }
        [Display(Name = "激活")]
        public bool IsActive { get; set; }
        [Display(Name = "创建日期"), DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "最后登录时间")]
        public DateTime? LastActivityDate { get; set; }

        //public Nullable<int> DepartmentId { get; set; }
        //public Nullable<int> PositionId { get; set; }

        [Display(Name = "生日"), DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? Birthday { get; set; }
        [Display(Name = "性别")]
        public Gender Gender { get; set; }
        [Display(Name = "性别")]
        public string GenderName
        {
            get
            {
                var enumType = typeof(Gender);
                var field = enumType.GetFields()
                           .First(x => x.Name == System.Enum.GetName(enumType, Gender));
                var attribute = field.GetCustomAttribute<DisplayAttribute>();
                return attribute.Name;

            }
        }
        public string QQ { get; set; }
        public string Mobile { get; set; }

    }
}
