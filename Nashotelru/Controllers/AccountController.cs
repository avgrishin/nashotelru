﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Nashotelru.Models;
using System.Data.Entity;
using CaptchaMvc.Attributes;
using Postal;

namespace Nashotelru.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    //public AccountController()
    //  : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
    //{
    //  (UserManager.UserValidator as UserValidator<ApplicationUser>).AllowOnlyAlphanumericUserNames = false;

    //  var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

    //  if (!roleManager.RoleExists("admin"))
    //  {
    //    var role = new IdentityRole();
    //    role.Name = "admin";
    //    roleManager.CreateAsync(role);
    //  }
    //  var user = UserManager.FindByName("avg1605");
    //  if (user != null)
    //  {
    //    if (!UserManager.IsInRole(user.Id, "admin"))
    //    {
    //      UserManager.AddToRole(user.Id, "admin");
    //    }
    //  }
    //}

    //public AccountController(UserManager<ApplicationUser> userManager)
    //{
    //  UserManager = userManager;
    //}

    //public UserManager<ApplicationUser> UserManager { get; private set; }
    public AccountController()
    {
    }

    public AccountController(ApplicationUserManager userManager)
    {
      UserManager = userManager;
    }

    private ApplicationUserManager _userManager;
    public ApplicationUserManager UserManager
    {
      get
      {
        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
      }
      private set
      {
        _userManager = value;
      }
    }

    //
    // GET: /Account/Login
    [AllowAnonymous]
    public ActionResult Login(string returnUrl)
    {
      ViewBag.ReturnUrl = returnUrl;
      return View();
    }

    private SignInHelper _helper;

    private SignInHelper SignInHelper
    {
      get
      {
        if (_helper == null)
        {
          _helper = new SignInHelper(UserManager, AuthenticationManager);
        }
        return _helper;
      }
    }
    //
    // POST: /Account/Login
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }

      // This doen't count login failures towards lockout only two factor authentication
      // To enable password failures to trigger lockout, change to shouldLockout: true
      var result = await SignInHelper.PasswordSignIn(model.Email, model.Password, model.RememberMe, shouldLockout: false);
      switch (result)
      {
        case SignInStatus.Success:
          return RedirectToLocal(returnUrl);
        case SignInStatus.LockedOut:
          return View("Lockout");
        case SignInStatus.RequiresTwoFactorAuthentication:
          return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
        case SignInStatus.Failure:
        default:
          ModelState.AddModelError("", "Invalid login attempt.");
          return View(model);
      }

      //if (ModelState.IsValid)
      //{
      //  var user = await UserManager.FindAsync(model.UserName, model.Password);
      //  if (user != null && user.NoUserInfo != null && user.NoUserInfo.IsConfirmed && !user.NoUserInfo.IsLocked)
      //  {
      //    await SignInAsync(user, model.RememberMe);
      //    return RedirectToLocal(returnUrl);
      //  }
      //  else
      //  {
      //    ModelState.AddModelError("", user == null ? "Ошибка авторизации. Проверьте правильность указания Логина и Пароля." : user.NoUserInfo.IsLocked ? "Ваш логин заблокирован" : !user.NoUserInfo.IsConfirmed ? "Ваш email не подтвержден" : "Ошибка авторизации.");
      //  }
      //}
      //// If we got this far, something failed, redisplay form
      //return View(model);
    }

    //
    // GET: /Account/ForgotPassword
    [AllowAnonymous]
    public ActionResult ForgotPassword()
    {
      return View();
    }

    //
    // POST: /Account/ForgotPassword
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = await UserManager.FindByNameAsync(model.Email);
        if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
        {
          // Don't reveal that the user does not exist or is not confirmed
          return View("ForgotPasswordConfirmation");
        }

        var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
        var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
        //ViewBag.Link = callbackUrl;
        dynamic email = new Email("RemindEmail");
        email.To = user.Email;
        email.From = "NashOTEL <nashotel@nm.ru>";
        email.UserName = user.UserName;
        email.callbackUrl = callbackUrl;
        await email.SendAsync();
        return View("ForgotPasswordConfirmation");
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }

    //
    // GET: /Account/ForgotPasswordConfirmation
    [AllowAnonymous]
    public ActionResult ForgotPasswordConfirmation()
    {
      return View();
    }

    //
    // GET: /Account/ResetPassword
    [AllowAnonymous]
    public ActionResult ResetPassword(string code)
    {
      return code == null ? View("Error") : View();
    }

    //
    // POST: /Account/ResetPassword
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      var user = await UserManager.FindByNameAsync(model.Email);
      if (user == null)
      {
        // Don't reveal that the user does not exist
        return RedirectToAction("ResetPasswordConfirmation", "Account");
      }
      var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
      if (result.Succeeded)
      {
        return RedirectToAction("ResetPasswordConfirmation", "Account");
      }
      AddErrors(result);
      return View();
    }

    //
    // GET: /Account/ResetPasswordConfirmation
    [AllowAnonymous]
    public ActionResult ResetPasswordConfirmation()
    {
      return View();
    }

    //[AllowAnonymous]
    //public ActionResult Remind(string returnUrl)
    //{
    //  ViewBag.ReturnUrl = returnUrl;
    //  return View();
    //}

    //[HttpPost]
    //[AllowAnonymous]
    //[ValidateAntiForgeryToken]
    //public async Task<ActionResult> Remind(RemindViewModel model, string returnUrl)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    ApplicationDbContext context = new ApplicationDbContext();
    //    ApplicationUser user = context.Users.FirstOrDefault(u => u.UserName == model.UserName || u.NoUserInfo.EMail == model.UserName);
    //    if (user != null && user.NoUserInfo.IsConfirmed && !user.NoUserInfo.IsLocked)
    //    {
    //      user.NoUserInfo.ReminderToken = Nashotelru.Helpers.ShortGuid.NewGuid();
    //      user.NoUserInfo.ReminderDT = DateTime.Now;
    //      DbSet<ApplicationUser> dbSet = context.Set<ApplicationUser>();
    //      dbSet.Attach(user);
    //      context.Entry(user).State = EntityState.Modified;
    //      await context.SaveChangesAsync();

    //      dynamic email = new Email("RemindEmail");
    //      email.To = user.NoUserInfo.EMail;
    //      email.From = "NashOTEL <nashotel@nm.ru>";
    //      email.UserName = user.UserName;
    //      email.ReminderToken = user.NoUserInfo.ReminderToken;
    //      await email.SendAsync();
    //      return RedirectToAction("RemindSent");
    //    }
    //    else
    //    {
    //      ModelState.AddModelError("", user == null ? "Ошибка. Проверьте правильность указания Логина и Email." : user.NoUserInfo.IsLocked ? "Ваш логин заблокирован" : !user.NoUserInfo.IsConfirmed ? "Ваш email не подтвержден" : "Ошибка авторизации. Проверьте правильность указания Логина и Email.");
    //    }
    //  }
    //  // If we got this far, something failed, redisplay form
    //  return View(model);
    //}

    //[AllowAnonymous]
    //public ActionResult RemindSent()
    //{
    //  return View();
    //}

    //[AllowAnonymous]
    //public ActionResult RemindConfirm(string Id)
    //{
    //  ApplicationDbContext context = new ApplicationDbContext();
    //  ApplicationUser user = context.Users.FirstOrDefault(u => u.NoUserInfo.ReminderToken == Id);
    //  if (user != null && user.NoUserInfo.IsConfirmed && user.NoUserInfo.ReminderDT.HasValue && user.NoUserInfo.ReminderDT.Value.AddDays(2) > DateTime.Now)
    //  {
    //    return View(new RemindConfirmViewModel { ReminderToken = Id });
    //  }
    //  return View("RemindFailure");
    //}

    //[HttpPost]
    //[AllowAnonymous]
    //[ValidateAntiForgeryToken]
    //public async Task<ActionResult> RemindConfirm(RemindConfirmViewModel model)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    ApplicationDbContext context = new ApplicationDbContext();
    //    ApplicationUser user = context.Users.SingleOrDefault(u => u.NoUserInfo.ReminderToken == model.ReminderToken);
    //    if (user != null && user.NoUserInfo.IsConfirmed && user.NoUserInfo.ReminderDT.HasValue && user.NoUserInfo.ReminderDT.Value.AddDays(2) > DateTime.Now)
    //    {
    //      await UserManager.RemovePasswordAsync(user.Id);
    //      var result = await UserManager.AddPasswordAsync(user.Id, model.Password);
    //      await SignInAsync(user, isPersistent: false);
    //      if (result.Succeeded)
    //      {
    //        return RedirectToAction("Params", new { Message = "Пароль успешно изменен." });
    //      }
    //      else
    //      {
    //        AddErrors(result);
    //      }
    //    }
    //  }
    //  // If we got this far, something failed, redisplay form
    //  return View(model);
    //}

    //
    // GET: /Account/Register
    [AllowAnonymous]
    public ActionResult Register()
    {
      return View();
    }
    //
    // POST: /Account/Register
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [CaptchaVerify("Captcha result is not valid.")]
    public async Task<ActionResult> Register(RegisterViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await UserManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
          var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
          var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
          //await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
          //ViewBag.Link = callbackUrl;
          
          dynamic email = new Email("RegEmail");
          email.To = model.Email;
          email.From = "NashOTEL <nashotel@nm.ru>";
          email.UserName = model.Email;
          email.callbackUrl = callbackUrl;
          await email.SendAsync();
          ViewBag.EMail = model.Email;
          return View("Register2");

          //return View("DisplayEmail");
        }
        AddErrors(result);
      }

      // If we got this far, something failed, redisplay form
      return View(model);
      //if (ModelState.IsValid)
      //{
      //  ApplicationDbContext context = new ApplicationDbContext();
      //  if (context.Users.SingleOrDefault(u => u.NoUserInfo.EMail == model.EMail) == null)
      //  {
      //    var user = new ApplicationUser() { UserName = model.UserName };
      //    user.NoUserInfo = new NoUserInfo { EMail = model.EMail, IsLocked = false, ConfirmationToken = Nashotelru.Helpers.ShortGuid.NewGuid(), IsConfirmed = false };

      //    var result = await UserManager.CreateAsync(user, model.Password);
      //    if (result.Succeeded)
      //    {
      //      dynamic email = new Email("RegEmail");
      //      email.To = model.EMail;
      //      email.From = "NashOTEL <nashotel@nm.ru>";
      //      email.UserName = model.UserName;
      //      email.ConfirmationToken = user.NoUserInfo.ConfirmationToken;
      //      await email.SendAsync();
      //      ViewBag.EMail = model.EMail;
      //      return View("Register2");
      //    }
      //    else
      //    {
      //      AddErrors(result);
      //    }
      //  }
      //  else
      //  {
      //    ModelState.AddModelError("", "Пользователь с таким EMail уже существует.");
      //  }
      //}
      //// If we got this far, something failed, redisplay form
      //return View(model);
    }

    //
    // GET: /Account/ConfirmEmail
    [AllowAnonymous]
    public async Task<ActionResult> ConfirmEmail(string userId, string code)
    {
      if (userId == null || code == null)
      {
        return View("Error");
      }
      var result = await UserManager.ConfirmEmailAsync(userId, code);
      if (result.Succeeded)
      {
        return View("ConfirmEmail");
      }
      AddErrors(result);
      return View();
    }

    //[AllowAnonymous]
    //public async Task<ActionResult> RegisterConfirmation(string Id)
    //{
    //  ApplicationDbContext context = new ApplicationDbContext();
    //  ApplicationUser user = context.Users.SingleOrDefault(u => u.NoUserInfo.ConfirmationToken == Id);
    //  if (user != null && !user.NoUserInfo.IsConfirmed)
    //  {
    //    user.NoUserInfo.IsConfirmed = true;
    //    DbSet<ApplicationUser> dbSet = context.Set<ApplicationUser>();
    //    dbSet.Attach(user);
    //    context.Entry(user).State = EntityState.Modified;
    //    await context.SaveChangesAsync();
    //    //await SignInAsync(user, isPersistent: false);
    //    return RedirectToAction("Params", new { Message = "Поздравляем! Вы успешно завершили регистрацию." });
    //  }
    //  return RedirectToAction("ConfirmationFailure");
    //}

    //[AllowAnonymous]
    //public ActionResult ConfirmationFailure()
    //{
    //  return View();
    //}

    //
    // POST: /Account/Disassociate
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
    {
      ManageMessageId? message = null;
      IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
      if (result.Succeeded)
      {
        message = ManageMessageId.RemoveLoginSuccess;
      }
      else
      {
        message = ManageMessageId.Error;
      }
      return RedirectToAction("Manage", new { Message = message });
    }

    public async Task<ActionResult> Params(string Message)
    {
      ViewBag.StatusMessage = Message;
      var currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

      var model = new ParamsUserViewModel();
      if (currentUser.NoUserInfo != null)
      {
        model.EMail = currentUser.NoUserInfo.EMail;
        model.FirstName = currentUser.NoUserInfo.FirstName;
        model.LastName = currentUser.NoUserInfo.LastName;
      }
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Params(ParamsUserViewModel model)
    {
      if (ModelState.IsValid)
      {
        var currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        if (currentUser.NoUserInfo == null) currentUser.NoUserInfo = new NoUserInfo { IsLocked = false };
        currentUser.NoUserInfo.EMail = model.EMail;
        currentUser.NoUserInfo.FirstName = model.FirstName;
        currentUser.NoUserInfo.LastName = model.LastName;
        UserManager.Update(currentUser);
        return RedirectToAction("Params", new { Message = "Профиль успешно обновлен." });
      }
      return View(model);
    }
    //
    // GET: /Account/Manage
    public ActionResult Manage(ManageMessageId? message)
    {
      ViewBag.StatusMessage =
          message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
          : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
          : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
          : message == ManageMessageId.Error ? "An error has occurred."
          : "";
      ViewBag.HasLocalPassword = HasPassword();
      ViewBag.ReturnUrl = Url.Action("Manage");
      return View();
    }

    //
    // POST: /Account/Manage
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Manage(ManageUserViewModel model)
    {
      bool hasPassword = HasPassword();
      ViewBag.HasLocalPassword = hasPassword;
      ViewBag.ReturnUrl = Url.Action("Manage");
      if (hasPassword)
      {
        if (ModelState.IsValid)
        {
          IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
          if (result.Succeeded)
          {
            return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
          }
          else
          {
            AddErrors(result);
          }
        }
      }
      else
      {
        // User does not have a password so remove any validation errors caused by a missing OldPassword field
        ModelState state = ModelState["OldPassword"];
        if (state != null)
        {
          state.Errors.Clear();
        }

        if (ModelState.IsValid)
        {
          IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
          if (result.Succeeded)
          {
            return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
          }
          else
          {
            AddErrors(result);
          }
        }
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }

    //
    // POST: /Account/ExternalLogin
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult ExternalLogin(string provider, string returnUrl)
    {
      // Request a redirect to the external login provider
      return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
    }

    //
    // GET: /Account/ExternalLoginCallback
    [AllowAnonymous]
    public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
    {
      var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
      if (loginInfo == null)
      {
        return RedirectToAction("Login");
      }

      // Sign in the user with this external login provider if the user already has a login
      var user = await UserManager.FindAsync(loginInfo.Login);
      if (user != null)
      {
        await SignInAsync(user, isPersistent: false);
        return RedirectToLocal(returnUrl);
      }
      else
      {
        // If the user does not have an account, then prompt the user to create an account
        ViewBag.ReturnUrl = returnUrl;
        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
      }
    }

    //
    // POST: /Account/LinkLogin
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult LinkLogin(string provider)
    {
      // Request a redirect to the external login provider to link a login for the current user
      return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
    }

    //
    // GET: /Account/LinkLoginCallback
    public async Task<ActionResult> LinkLoginCallback()
    {
      var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
      if (loginInfo == null)
      {
        return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
      }
      var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
      if (result.Succeeded)
      {
        return RedirectToAction("Manage");
      }
      return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
    }

    //
    // POST: /Account/ExternalLoginConfirmation
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
    {
      if (User.Identity.IsAuthenticated)
      {
        return RedirectToAction("Manage");
      }

      if (ModelState.IsValid)
      {
        // Get the information about the user from the external login provider
        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
          return View("ExternalLoginFailure");
        }
        var user = new ApplicationUser() { UserName = model.UserName };
        var result = await UserManager.CreateAsync(user);
        if (result.Succeeded)
        {
          result = await UserManager.AddLoginAsync(user.Id, info.Login);
          if (result.Succeeded)
          {
            await SignInAsync(user, isPersistent: false);
            return RedirectToLocal(returnUrl);
          }
        }
        AddErrors(result);
      }

      ViewBag.ReturnUrl = returnUrl;
      return View(model);
    }

    //
    // POST: /Account/LogOff
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult LogOff()
    {
      AuthenticationManager.SignOut();
      return RedirectToAction("Index", "Home");
    }

    //
    // GET: /Account/ExternalLoginFailure
    [AllowAnonymous]
    public ActionResult ExternalLoginFailure()
    {
      return View();
    }

    [ChildActionOnly]
    public ActionResult RemoveAccountList()
    {
      var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
      ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
      return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && UserManager != null)
      {
        UserManager.Dispose();
        UserManager = null;
      }
      base.Dispose(disposing);
    }

    #region Helpers
    // Used for XSRF protection when adding external logins
    private const string XsrfKey = "XsrfId";

    private IAuthenticationManager AuthenticationManager
    {
      get
      {
        return HttpContext.GetOwinContext().Authentication;
      }
    }

    private async Task SignInAsync(ApplicationUser user, bool isPersistent)
    {
      AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
      var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
      AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
    }

    private void AddErrors(IdentityResult result)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError("", error);
      }
    }

    private bool HasPassword()
    {
      var user = UserManager.FindById(User.Identity.GetUserId());
      if (user != null)
      {
        return user.PasswordHash != null;
      }
      return false;
    }

    public enum ManageMessageId
    {
      ChangePasswordSuccess,
      SetPasswordSuccess,
      RemoveLoginSuccess,
      Error
    }

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

    private class ChallengeResult : HttpUnauthorizedResult
    {
      public ChallengeResult(string provider, string redirectUri)
        : this(provider, redirectUri, null)
      {
      }

      public ChallengeResult(string provider, string redirectUri, string userId)
      {
        LoginProvider = provider;
        RedirectUri = redirectUri;
        UserId = userId;
      }

      public string LoginProvider { get; set; }
      public string RedirectUri { get; set; }
      public string UserId { get; set; }

      public override void ExecuteResult(ControllerContext context)
      {
        var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
        if (UserId != null)
        {
          properties.Dictionary[XsrfKey] = UserId;
        }
        context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
      }
    }
    #endregion
  }
}