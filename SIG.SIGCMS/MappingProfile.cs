using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SIG.Data.Entity.Identity;
using SIG.Model.Admin.InputModel.Menus;

namespace SIG.SIGCMS
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Menu, MenuIM>();
            CreateMap<MenuIM, Menu>();
        }
    }
}
