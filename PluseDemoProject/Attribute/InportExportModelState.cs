using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace PluseDemoProject.Attribute
{
    public abstract class ModelStateTransfer : ActionFilterAttribute
    {
        protected static readonly string Key = typeof(ModelStateTransfer).FullName;
    }

    public class ExportModelStateAttribute : ModelStateTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controller = filterContext.Controller as Controller;
            //Only export when ModelState is not valid
            if (!controller.ViewData.ModelState.IsValid)
            {
                //Export if we are redirecting
                if ((filterContext.Result is RedirectResult) || (filterContext.Result is RedirectToRouteResult) || (filterContext.Result is RedirectToActionResult))
                {
                    var modelState = controller?.ViewData.ModelState;
                    if (modelState != null)
                    {
                        var listError = modelState.Where(x => x.Value.Errors.Any())
                            .ToDictionary(m => m.Key, m => m.Value.Errors
                            .Select(s => s.ErrorMessage)
                            .FirstOrDefault(s => s != null));
                        controller.TempData[Key] = JsonConvert.SerializeObject(listError);
                    }
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }

    public class ImportModelStateAttribute : ModelStateTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controller = filterContext.Controller as Controller;
            var tempData = controller?.TempData?.Keys;
            if (controller != null && tempData != null)
            {
                if (tempData.Contains(Key) && filterContext.Result is ViewResult)
                {
                    var modelStateString = controller.TempData[Key].ToString();
                    var listError = JsonConvert.DeserializeObject<Dictionary<string, string>>(modelStateString);
                    var modelState = new ModelStateDictionary();
                    foreach (var item in listError)
                    {
                        modelState.AddModelError(item.Key, item.Value ?? "");
                    }

                    controller.ViewData.ModelState.Merge(modelState);
                }
                else
                {
                    //Otherwise remove it.
                    controller.TempData.Remove(Key);
                }
            }
            
            base.OnActionExecuted(filterContext);
        }
    }
    //public class SetTempDataModelStateAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuted(ActionExecutedContext filterContext)
    //    {
    //        base.OnActionExecuted(filterContext);

    //        var controller = filterContext.Controller as Controller;
    //        var modelState = controller?.ViewData.ModelState;
    //        if (modelState != null)
    //        {
    //            var listError = modelState.Where(x => x.Value.Errors.Any())
    //                .ToDictionary(m => m.Key, m => m.Value.Errors
    //                .Select(s => s.ErrorMessage)
    //                .FirstOrDefault(s => s != null));
    //            controller.TempData["ModelState"] = JsonConvert.SerializeObject(listError);
    //        }
    //    }
    //}
    //public class RestoreModelStateFromTempDataAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        base.OnActionExecuting(filterContext);

    //        var controller = filterContext.Controller as Controller;
    //        var tempData = controller?.TempData?.Keys;
    //        if (controller != null && tempData != null)
    //        {
    //            if (tempData.Contains("ModelState"))
    //            {
    //                var modelStateString = controller.TempData["ModelState"].ToString();
    //                var listError = JsonConvert.DeserializeObject<Dictionary<string, string>>(modelStateString);
    //                var modelState = new ModelStateDictionary();
    //                foreach (var item in listError)
    //                {
    //                    modelState.AddModelError(item.Key, item.Value ?? "");
    //                }

    //                controller.ViewData.ModelState.Merge(modelState);
    //            }
    //        }
    //    }
    //}

    //public class SetTempDataModelStateAttribute : ActionFilterAttribute
    //{
    //    public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
    //    {
    //        await base.OnActionExecutionAsync(filterContext, next);

    //        var controller = filterContext.Controller as Controller;
    //        var modelState = controller?.ViewData.ModelState;
    //        if (modelState != null)
    //        {
    //            var listError = modelState.Where(x => x.Value.Errors.Any())
    //                .ToDictionary(m => m.Key, m => m.Value.Errors
    //                .Select(s => s.ErrorMessage)
    //                .FirstOrDefault(s => s != null));
    //            var listErrorJson = await Task.Run(() => JsonConvert.SerializeObject(listError));
    //            controller.TempData["ModelState"] = listErrorJson;
    //        }
    //        await next();
    //    }
    //}
    //public class RestoreModelStateFromTempDataAttribute : ActionFilterAttribute
    //{
    //    public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
    //    {
    //        await base.OnActionExecutionAsync(filterContext, next);

    //        var controller = filterContext.Controller as Controller;
    //        var tempData = controller?.TempData?.Keys;
    //        if (controller != null && tempData != null)
    //        {
    //            if (tempData.Contains("ModelState"))
    //            {
    //                var modelStateString = controller.TempData["ModelState"].ToString();
    //                var listError = await Task.Run(() =>
    //                    JsonConvert.DeserializeObject<Dictionary<string, string>>(modelStateString));
    //                var modelState = new ModelStateDictionary();
    //                foreach (var item in listError)
    //                {
    //                    modelState.AddModelError(item.Key, item.Value ?? "");
    //                }

    //                controller.ViewData.ModelState.Merge(modelState);
    //            }
    //        }
    //        await next();
    //    }
    //}
}
