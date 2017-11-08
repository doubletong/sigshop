using SIG.Resources.Admin;
using System.ComponentModel.DataAnnotations;


namespace SIG.Model.Admin.InputModel.Menus
{
    public class MenuCategoryIM
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(Labels), Name = "Title")]
        [Required(ErrorMessageResourceType = typeof(Validations), ErrorMessageResourceName = "Required")]
        public string Title { get; set; }
        
        [Display(ResourceType = typeof(Labels), Name = "Importance")]
        public int Importance { get; set; }
        [Display(ResourceType = typeof(Labels), Name = "Active")]
        public bool Active { get; set; }

    }
}
