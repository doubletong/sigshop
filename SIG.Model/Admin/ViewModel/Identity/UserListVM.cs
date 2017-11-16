
using System;
using SIG.Data.Entity.Identity;
using SIG.Model.Admin.InputModel.Identity;
using SIG.Repository.PagedList;

namespace SIG.Model.Admin.ViewModel.Identity
{
    public class UserListVM
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public string Keyword { get; set; }

        //[DateLessThan("EndDate", ErrorMessage = "开始日期必须小于截止日期")]
        public DateTime? StartDate { get; set; }
       
        public DateTime? EndDate { get; set; }
        public int? RoleId { get; set; }

        public IPagedList<User> Users { get; set; }

        public SetPasswordIM SetPasswordIM { get; set; }

    }
}
