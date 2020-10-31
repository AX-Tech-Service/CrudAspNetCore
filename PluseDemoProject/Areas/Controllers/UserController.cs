using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PluseDemoProject.Areas.Services.ServiceInterface;
using PluseDemoProject.Areas.ViewModel;
using PluseDemoProject.Attribute;
using System.Threading.Tasks;

namespace PluseDemoProject.Areas.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserService moUserService;

        public UserController(IUserService foUserService)
        {
            moUserService = foUserService;
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> Index(IndexModel foIndexModel, string fsSortKeyword, int pageindex = 1)
        {
            if (foIndexModel == null)
            {
                foIndexModel = new IndexModel();
            }
            foIndexModel.PageNumber = pageindex;
            if (!string.IsNullOrWhiteSpace(foIndexModel.stFilterKeyword) || !string.IsNullOrWhiteSpace(fsSortKeyword))
            {
                foIndexModel.stSortKeyword = fsSortKeyword;
                //loUserList = moUserService.sortAndSearch(loUserList, foIndexModel.stFilterKeyword, fsSortKeyword);
            }
            //loUserList = moUserService.getUsers();
            foIndexModel.aUserModels = moUserService.getUsers(foIndexModel);
        
            
            return View(foIndexModel);
        }

        [HttpGet]
        [ImportModelState]
        public ActionResult Create()
        {
            UserModel foUserModel = new UserModel();
            object foUserModeljson = "";
            TempData.TryGetValue("UserModel", out foUserModeljson);
            if (!string.IsNullOrEmpty((string)foUserModeljson))
            {
                foUserModel = JsonConvert.DeserializeObject<UserModel>((string)foUserModeljson);
            }
            return View(foUserModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ExportModelState]
        public ActionResult CreateUser(UserModel foUserModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["UserModel"] = JsonConvert.SerializeObject(foUserModel);
                return RedirectToAction("Create");
            }
            bool lbEmailExist = moUserService.insertUser(foUserModel);
            if (lbEmailExist)
            {
                ModelState.AddModelError("EmailAddress", "Email Already Exist");
                TempData["UserModel"] = JsonConvert.SerializeObject(foUserModel);
                return RedirectToAction("Create");
            }
            TempData.Remove("UserModel");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [ImportModelState]
        public ActionResult Edit(int fiId)
        {
            UserModel userModel = new UserModel();
            userModel = moUserService.getUser(fiId);
            if (userModel != null)
            {
                return View(userModel);
            }
            return RedirectToAction("NotFound");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ExportModelState]
        public ActionResult Edit(UserModel foUserModel)
        {
            bool lbEmailExist = moUserService.updateUser(foUserModel);
            if (lbEmailExist)
            {
                ModelState.AddModelError("EmailAddress", "Email Already Exist");
                return RedirectToAction("Edit", new { fiId = foUserModel.Id });
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int fiId)
        {
            try
            {
                moUserService.deleteUser(fiId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]

        public ActionResult NotFound()
        {
            return View("404NotFound");
        }

        #region DeleteUser
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        _userService.DeleteUserSP(id);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        #endregion

        #region Edit
        //// POST: UserController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[SetTempDataModelState]
        //public ActionResult Edit(/*int id,*/ UserModel userModel)
        //{
        //    try
        //    {
        //        //userModel.Id = id;
        //        bool isEmailExist = _userService.EditUserSp(userModel);
        //        if (isEmailExist)
        //        {
        //            ModelState.AddModelError("EmailAddress", "Email Already Exist");
        //            return RedirectToAction("Edit", userModel);
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception e)
        //    {
        //        return View();
        //    }
        //}
        #endregion


    }
}
