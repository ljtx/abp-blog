using Abp.Domain.Repositories;
using ABPBlog.Entity;
using ABPBlog.IService;
using ABPBlog.Web.Utils;
using ABPBlog.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace ABPBlog.Web.Controllers
{
    public class AccountController: ABPBlogControllerBase
    {

        private IRepository<User> _userRepository;
        private IUserService _userService;
        public AccountController(IRepository<User> userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        //
        // GET: /Account/Login
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        public bool IsSignedIn()
        {
            //var session = HttpContext.Session;
            //if (session != null)
            //{
            //    string str = session.GetString("abpblogsession");
            //    if (string.IsNullOrWhiteSpace(str))
            //    {
            //        return false;
            //    }
            //    return true;
            //}
            return false;
        }
        public User GetUserByName(string userName)
        {
           return  _userService.GetUserByName(userName);
        }
        public string GetCurrentUserName()
        {
            string str = HttpContext.Session.GetString("abpblogsession");
            if (string.IsNullOrWhiteSpace(str))
            {
                return "ljtx";
            }
            str= CompressHelper.AES_Decrypt(str, "qwertyuiop", "1234567891234567");
            return str;
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _userService.SignIn(model.UserName,MD5.ToMD5String(model.Password,"ljtx"));
            if (result!=null)
            {
                HttpContext.Session.SetString("abpblogsession", CompressHelper.AES_Encrypt(model.UserName, "qwertyuiop","1234567891234567"));
                Logger.InfoFormat("Logged in {userName}.", model.UserName);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                Logger.WarnFormat("Failed to log in {userName}.", model.UserName);
                ModelState.AddModelError("", "用户名或密码错误");
                return View(model);
            }
        }

        //
        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName, PassWord=MD5.ToMD5String(model.Password,"ljtx"), CreateOn = DateTime.Now, LastTime = DateTime.Now };
                var result = await _userService.CreateAsync(user);
                if (result)
                {
                    Logger.InfoFormat("User {userName} was created.",user.UserName);
                    //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await MessageServices.SendEmailAsync(model.Email, "Confirm your account",
                    //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    //if (string.Equals(user.UserName, "admin", StringComparison.OrdinalIgnoreCase))
                    //{
                    //    await UserManager.AddClaimAsync(user, new Claim("Admin", "Allowed"));
                    //}
                    return RedirectToAction("Login");
                }
             
            }
            return View(model);
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOff()
        {
            HttpContext.Session.SetString("abpblogsession",null);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AccessDenied()
        {
            return RedirectToAction("Index", "Home");
        }

        #region 辅助方法
       

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}
