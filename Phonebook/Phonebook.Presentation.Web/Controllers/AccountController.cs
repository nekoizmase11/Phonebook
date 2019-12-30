using Phonebook.Bussines;
using Phonebook.Presentation.Contracts.DataTransferObjects;
using Phonebook.Presentation.Contracts.Exceptions;
using Phonebook.Presentation.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Phonebook.Presentation.Web.Controllers
{
    public class AccountController : Controller
    {
        private IBussinesLayer _businesLayer;

        public AccountController(IBussinesLayer businesLayer)
        {
            _businesLayer = businesLayer;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginVM login = new LoginVM();

            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                if (_businesLayer.UserService.Login(model.email, model.password))
                {
                    FormsAuthentication.SetAuthCookie(model.email, false);
                    Session["UserName"] = model.email;
                    //int id_user = _businesLayer.UserService.GetUserByEmail(model.email).id;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["InfoMessage"] = "Incorrect email or password, please try again.";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {

            return View(new RegisterVM());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                //fali provera zo tome da li je mail vec zauzet, preko sql exceptiona
                //takodje kod logina viewa dodaj dugm za registraciju
                UserDto newUser = new UserDto(model.id, model.email, model.password);
                _businesLayer.UserService.RegisterUser(newUser);

                //prikazi stranicu sa info da je registracija uspesnai ponudi redirekciju na login
                TempData["InfoMessage"] = "Registration succeeded, you can login now.";

                return RedirectToAction("Login");
            }
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            EmailAddressVM emailAddress = new EmailAddressVM();

            return View(emailAddress);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(EmailAddressVM emailAddress)
        {
            if (ModelState.IsValid)
            {

                if (_businesLayer.UserService.GetUserByEmail(emailAddress.email) != null)
                {
                    ChangePasswordEmailDto email = new ChangePasswordEmailDto();
                    email.emailTo = emailAddress.email;
                    email.guid = Guid.NewGuid().ToString();

                    try
                    {
                        _businesLayer.ChangePasswordRequestService.SaveRequest(email);
                        _businesLayer.MailService.SendChangePasswordLink(email);

                        TempData["InfoMessage"] = "Check your mail to change password, than you can login here with new password.";
                        return RedirectToAction("Login");
                    }
                    catch (ServiceInvalidDataException ex)
                    {
                        // todo log
                        ViewData["ErrorMessage"] = ex.Message;
                        return View(emailAddress);
                    }
                }
                else
                {
                    ModelState.AddModelError("email", "Wrong email address.");
                    return View(emailAddress);
                }
            }
            else
            {
                return View(emailAddress);
            }
        }

        [AllowAnonymous]
        public ActionResult ChangeForgottenPassword(string guid)
        {
            ChangedPasswordVM changedPasswordInfo = new ChangedPasswordVM();
            changedPasswordInfo.guid = guid;

            return View(changedPasswordInfo);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeForgottenPassword(ChangedPasswordVM PasswordInfo)
        {
            if (ModelState.IsValid)
            {
                _businesLayer.ChangePasswordRequestService.ChangeForgottenPassword((ChangedPasswordDto)PasswordInfo);

                TempData["InfoMessage"] = "Your password is changed, you can login now.";
                return RedirectToAction("Login");
            }
            else
            {
                return View(PasswordInfo);
            }
        }
    }
}