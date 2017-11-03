using System.ComponentModel.DataAnnotations;

namespace SIG.Data.Enums
{
    public enum Gender : short
    {
        [Display(Name = "未知")]
        Unknown = 0,
        [Display(Name = "男")]
        Male = 1,
        [Display(Name = "女")]
        Female = 2

    }
}
