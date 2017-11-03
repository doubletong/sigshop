using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using SIG.Data.Enums;
using SIG.Infrastructure.Helper;

namespace SIG.Data.Entity.Identity
{

    public partial class User
    {
        public Guid Id { get; set; }
        public User()
        {
            this.Id = IdentityGenerator.SequentialGuid();
        }
        [Microsoft.Build.Framework.Required]
        public string UserName { get; set; }
        [Microsoft.Build.Framework.Required]

        public string Email { get; set; }
        [Microsoft.Build.Framework.Required]
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        public string RealName { get; set; }

        public string PhotoUrl { get; set; }



        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastActivityDate { get; set; }

        //public Nullable<int> DepartmentId { get; set; }
        //public Nullable<int> PositionId { get; set; }


        public DateTime? Birthday { get; set; }

        public Gender Gender { get; set; }

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

        public virtual ICollection<UserRole> UserRoles { get; set; }
        
    }
}
