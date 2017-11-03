using System.ComponentModel.DataAnnotations;

namespace SIG.Data.Enums
{
    public enum EmailType : short
    {
        [Display(Name = "激活帐号")]
        ACTIVEACCOUNT = 1,      
        [Display(Name = "重设密码")]
        RESETPASSWORD = 2
      
    }
}
