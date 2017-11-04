using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIG.Model.Front.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SIG.SIGCMS.Controllers
{
    public abstract class BaseController : Controller
    {
        public AjaxResultVM AR = new AjaxResultVM();
       // protected ICompositeViewEngine viewEngine;

       

     

        //public BaseController(ICompositeViewEngine viewEngine)
        //{
        //    this.viewEngine = viewEngine;
        //}

        //使用方法
        //public TutorialController(ICompositeViewEngine viewEngine) : base(viewEngine) { }
        //public string Index(int? id)
        //{
        //    var model = new MyModel { Name = "My Name" };

        //    return RenderViewAsString(model);
        //}
        //protected string RenderViewAsString(object model, string viewName = null)
        //{
        //    viewName = viewName ?? ControllerContext.ActionDescriptor.ActionName;
        //    ViewData.Model = model;

        //    using (StringWriter sw = new StringWriter())
        //    {
        //        IView view = viewEngine.FindView(ControllerContext, viewName, true).View;
        //        ViewContext viewContext = new ViewContext(ControllerContext, view, ViewData, TempData, sw, new HtmlHelperOptions());

        //        view.RenderAsync(viewContext).Wait();

        //        return sw.GetStringBuilder().ToString();
        //    }
        //}


        //// using a Model
        //string html = view.Render("Emails/Test", new Product("Apple"));

        //// using a Dictionary<string, object>
        //var viewData = new Dictionary<string, object>();
        //viewData["Name"] = "123456";

        //string html = view.Render("Emails/Test", viewData);



        protected string GetModelErrorMessage()
        {
            string validationErrors = string.Join("|",
                ModelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray());
            return validationErrors;
        }


    }

}