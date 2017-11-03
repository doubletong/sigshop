using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace SIG.Data.Entity.Identity
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
       
        public bool IsInRole(string role)
        {
            string[] arrayRoles = role.Split(',');

            if (Roles.Any(r => arrayRoles.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsInMenu(string area, string controller,string action, List<Menu> Menus)
        {
            if(Menus == null)
                return false;

            if (Menus.Any(p=> controller.Equals(p.Controller,StringComparison.InvariantCultureIgnoreCase) 
                    && action.Equals(p.Action, StringComparison.InvariantCultureIgnoreCase)
                    && area.Equals(p.Area, StringComparison.InvariantCultureIgnoreCase)))
            {
                return true;
            }           
            return false;            
        }

        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public Guid UserId { get; set; }
        public string RealName { get; set; }
        public string Avatar { get; set; }
        public string[] Roles { get; set; }
        //public List<UserMenuDTO> Menus { get; set; }
    }

    public class CustomPrincipalSerializeModel
    {
        public Guid UserId { get; set; }
        public string RealName { get; set; }
        public string Avatar { get; set; }
        public string[] Roles { get; set; }
        //public List<UserMenuDTO> Menus { get; set; }
    }
}