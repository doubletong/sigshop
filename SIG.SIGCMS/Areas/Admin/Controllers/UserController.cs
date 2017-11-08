using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using SIG.Data.Entity.Identity;
using SIG.Infrastructure.Configs;
using SIG.Infrastructure.Helper;
using SIG.Model.Admin.InputModel.Identity;
using SIG.Model.Admin.ViewModel;
using SIG.Model.Admin.ViewModel.Identity;
using SIG.Resources.Admin;
using SIG.Services.Identity;
using TZGCMS.Model.Admin.ViewModel.Identity;

namespace SIG.SIGCMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : BaseController
    {

        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;
        private readonly IMapper _mapper;

        public UserController(IUserServices userServices, IRoleServices roleServices, IMapper mapper)
        {
            _userServices = userServices;
            _roleServices = roleServices;
            _mapper = mapper;

        }

        // GET: User
        // [CustomAuthorize(Roles = "Admin")]
        public IActionResult Index(int? page, string keyword, DateTime? startDate, DateTime? endDate, int? roleId)
        {
            var userListVM = new UserListVM
            {
                StartDate = startDate,
                EndDate = endDate,
                Keyword = keyword,
                RoleId = roleId,
                PageIndex = page ?? 1,
                PageSize = SettingsManager.User.PageSize,
                SetPasswordIM = new SetPasswordIM()
            };

            int totalCount;


            userListVM.Users = _userServices.GetPagedElements(userListVM.PageIndex - 1, userListVM.PageSize, userListVM.Keyword,
                userListVM.StartDate, userListVM.EndDate, userListVM.RoleId, out totalCount);

            userListVM.TotalCount = totalCount;

            // var userList = _mapper.Map<IList<User>, IList<UserVM>>(users);
            //userListVM.Users = new StaticPagedList<User>(users, userListVM.PageIndex, userListVM.PageSize, userListVM.TotalCount);
            // ViewBag.OnePageOfUsers = usersAsIPagedList;
            ViewBag.PageSizes = new SelectList(Site.PageSizes());

            var roleList = _roleServices.GetAll().Where(m => m.Id != SettingsManager.Role.Founder).ToList();
            var roles = new SelectList(roleList, "Id", "RoleName");
            ViewBag.Roles = roles;

            return View(userListVM);

        }


        [HttpPost]
        public JsonResult PageSizeSet(int pageSize)
        {
            try
            {
                var xmlFile = PlatformServices.Default.MapPath("~/Config/UserSettings.config");
                xmlFile = xmlFile.ToLower().Replace(@"\bin\debug\netcoreapp2.0", "");
              //  var xmlFile = HttpHelper.HttpContext.MapPath("~/Config/UserSettings.config");
                XDocument doc = XDocument.Load(xmlFile);

                var item = doc.Descendants("Settings").FirstOrDefault();
                item.Element("PageSize").SetValue(pageSize);
                doc.Save(xmlFile);

                return Json(AR);
            }
            catch (Exception ex)
            {
                AR.Setfailure(ex.Message);
                return Json(AR);
            }
        }

        // GET: User/Details/5
        [HttpGet]
        public IActionResult Details(Guid id)
        {

            var user = _userServices.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return PartialView("_Detail", user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            RegisterIM vm = new RegisterIM();
            return PartialView("_UserCreate", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(RegisterIM model)
        {
            if (!ModelState.IsValid)
            {
                var errorMes = GetModelErrorMessage();
                AR.Setfailure(errorMes);
                return Json(AR);
            }


            var result = _userServices.CreateUser(model.UserName, model.Email, model.Password, model.DisplayName);
            if (result == 1)
            {
                AR.Setfailure(Messages.CannotRegisterEmail);
                return Json(AR);
            }

            if (result == 2)
            {
                AR.Setfailure(Messages.CannotRegisterUserName);
                return Json(AR);
            }


            int count;
            int pageSize = SettingsManager.User.PageSize;
            var list = _userServices.GetPagedElements(0, pageSize, string.Empty, null, null, null, out count);
            //    List<UserVM> userList = _mapper.Map<List<User>, List<UserVM>>(list);
            //AR.Data = RenderPartialViewToString("_UserList", list);

            AR.SetSuccess(string.Format(Messages.AlertCreateSuccess, EntityNames.User));
            return Json(AR);



        }


        public JsonResult IsUserNameUnique(string UserName)
        {
            var result = _userServices.IsExistUserName(UserName);

            return result
                ? Json(false)
                : Json(true);
        }

        public JsonResult IsEmailUnique(string Email)
        {
            var result = _userServices.IsExistEmail(Email);

            return result
                ? Json(false)
                : Json(true);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(Guid? id)
        {
            ProfileIM Profiles = new ProfileIM();
            if (id == null)
            {
                return PartialView("_UserEdit", Profiles);
            }
            var user = _userServices.GetById(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            Profiles = _mapper.Map<ProfileIM>(user);
            return PartialView("_UserEdit", Profiles);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(ProfileIM profile)
        {
            if (!ModelState.IsValid)
            {
                var errorMes = GetModelErrorMessage();
                AR.Setfailure(errorMes);
                return Json(AR);
            }

            var user = _userServices.GetById(profile.Id);
            if (user == null)
            {
                AR.Setfailure("不存在此用户！");
                return Json(AR);
                // return Json(false);
            }
            try
            {
                user.Email = profile.Email;
                user.RealName = profile.RealName;
                user.IsActive = profile.IsActive;
                //user.DepartmentId = profile.DepartmentId;
                //user.PositionId = profile.PositionId;
                user.QQ = profile.QQ;
                user.Mobile = profile.Mobile;
                user.Gender = profile.Gender;
                user.Birthday = profile.Birthday;

                _userServices.Update(user);

                // var userVM = _mapper.Map<UserVM>(user);

                AR.Id = user.Id;
               // AR.Data = RenderPartialViewToString("_UserItem", user);


                AR.SetSuccess(string.Format(Messages.AlertUpdateSuccess, EntityNames.User));
                return Json(AR);
            }
            catch (Exception ex)
            {
                AR.Setfailure(ex.Message);
                return Json(AR);
            }

        }
        public JsonResult IsEmailUniqueAtEdit(string email, Guid id)
        {
            var result = _userServices.IsExistEmail(email, id);

            return result
                ? Json(false)
                : Json(true);
        }


        [HttpGet]
        public PartialViewResult SetRole(Guid id)
        {
            var user = _userServices.GetById(id);
            var userRoles = user.UserRoles;
            SetUserRolesVM vm = new SetUserRolesVM
            {
                UserId = id,
                RoleIds = userRoles.Select(r => r.RoleId).ToArray(),
                Roles = _roleServices.GetAll().Where(r => r.Id != SettingsManager.Role.Founder)
            };

            return PartialView("_SetRole", vm);
        }


        [HttpPost]
        public JsonResult SetRole(Guid UserId, int[] RoleId)
        {
            try
            {
                // _userServices.SetUserRoles(UserId, RoleId);

                //var user = _userServices.GetById(UserId);
                //var roles = _roleServices.GetAll().Where(r => RoleId.Contains(r.Id)).ToList();

                //user.Roles.Clear();
                //foreach (Role r in roles)
                //{
                //    user.Roles.Add(r);
                //}

                //_userServices.Update(user);
                var user = _userServices.SetRole(UserId, RoleId);


                if (User.Identity.Name == user.UserName)
                {
                   // SetUserCookies(true, user);
                }


                //    var userVM = _mapper.Map<UserVM>(user);
                AR.Id = user.Id;
              //  AR.Data = RenderPartialViewToString("_UserItem", user);

                AR.SetSuccess(Messages.AlertActionSuccess);
                return Json(AR);
            }
            catch (Exception ex)
            {
                AR.Setfailure(ex.Message);
                return Json(AR);
            }
        }


        //public void SetUserCookies(bool isPersist, User user)
        //{
        //    var roles = _roleServices.GetAll().Where(r => r.UserRoles.Any(ur => ur.UserId == user.Id))
        //        .Select(m => m.RoleName).ToArray();  
        //    //user.Roles.Select(m => m.RoleName).ToArray();

        //    CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel()
        //    {
        //        UserId = user.Id,
        //        RealName = user.RealName,
        //        Avatar = string.IsNullOrEmpty(user.PhotoUrl) ? SettingsManager.User.DefaultAvatar : user.PhotoUrl,
        //        Roles = roles
        //    };
        //    //serializeModel.Menus = GetUserMenus(user.);
        //    TimeSpan timeout = FormsAuthentication.Timeout;
        //    DateTime expire = DateTime.Now.Add(timeout);


        //    string userData = JsonConvert.SerializeObject(serializeModel, Formatting.Indented,
        //        new JsonSerializerSettings()
        //        {
        //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //        });


        //    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
        //             1,
        //             user.UserName,
        //             DateTime.Now,
        //             expire,
        //             isPersist,
        //             userData);

        //    string encTicket = FormsAuthentication.Encrypt(authTicket);
        //    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
        //    System.Web.HttpContext.Current.Response.Cookies.Add(faCookie);
        //}


        [HttpGet]
        public ActionResult SetPassword(Guid id)
        {
            SetPasswordIM model = new SetPasswordIM
            {
                UserId = id
            };
            return PartialView("_SetPassword", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SetPassword(SetPasswordIM model)
        {
            if (!ModelState.IsValid)
            {
                var errorMes = GetModelErrorMessage();
                AR.Setfailure(errorMes);
                return Json(AR);
            }


            var result = _userServices.SetPassword(model.UserId, model.NewPassword);
            if (result)
            {

                AR.SetSuccess(Messages.AlertActionSuccess);
                return Json(AR);
            }
            AR.Setfailure(Messages.AlertActionFailure);
            return Json(AR);

        }


        [HttpPost]
        public JsonResult IsActive(Guid id)
        {
            var user = _userServices.GetById(id);
            if (user != null)
            {
                user.IsActive = !user.IsActive;
                _userServices.Update(user);

                //    var userVM = _mapper.Map<UserVM>(user);

                AR.Id = user.Id;
             //   AR.Data = RenderPartialViewToString("_UserItem", user);

                AR.SetSuccess(Messages.AlertActionSuccess);
                return Json(AR);

            }
            AR.Setfailure(Messages.AlertActionFailure);
            return Json(AR);

        }


        // POST: Users/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Delete(Guid id)
        {
            var user = _userServices.GetById(id);

            if (user == null)
            {
                AR.Setfailure("未找到此用户！");
                return Json(AR);
            }

            if (user.Id.ToString() == SettingsManager.User.Founder)
            {
                AR.SetWarning("创始人帐号，不可以删除！");
                return Json(AR);
            }

            _userServices.Delete(user);
            return Json(AR);
        }
    }
}