using Phonebook.Bussines;
using Phonebook.Presentation.Contracts.DataTransferObjects;
using Phonebook.Presentation.Contracts.Exceptions;
using Phonebook.Presentation.Contracts.LuceneSearchHelper;
using Phonebook.Presentation.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Phonebook.Presentation.Web.Controllers
{
    public class HomeController : Controller
    {
        private IBussinesLayer _businesLayer;

        public HomeController(IBussinesLayer businesLayer)
        {
            _businesLayer = businesLayer;
        }

        [Authorize]
        public ActionResult Index()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            //var ime = System.Web.HttpContext.Current.User.Identity.Name;
            //var imme = HttpContext.User.Identity.Name;

            int id_user = _businesLayer.UserService.GetUserByEmail(ticket.Name).id;

            IEnumerable<ContactVM> lista = _businesLayer.ContactService.ContactsByUser(id_user).Select(it => (ContactVM)it);

            return View(lista);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Search(SearchVM searchModel)
        {
            var userEmail = System.Web.HttpContext.Current.User.Identity.Name;
            string searchString = searchModel.SearchTextBox;

            List<string> listOfChecked = new List<string>();

            foreach (var pproperty in searchModel.GetType().GetProperties())
            {
                string name = pproperty.Name;
                var value = pproperty.GetValue(searchModel, null);

                if (name != "SearchTextBox")
                {
                    if ((bool?)value == true && (bool?)value != null)
                    {
                        switch (name)
                        {
                            case "ime":
                                listOfChecked.Add(FieldNames.Name);
                                break;
                            case "prezime":
                                listOfChecked.Add(FieldNames.LName);
                                break;
                            case "email":
                                listOfChecked.Add(FieldNames.Email);
                                break;
                            case "broj":
                                listOfChecked.Add(FieldNames.Number);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            int id_user = _businesLayer.UserService.GetUserByEmail(userEmail).id;
            IEnumerable<ContactDto> listToSearch = _businesLayer.ContactService.ContactsByUser(id_user);

            IEnumerable<ContactVM> listAfterSearch = _businesLayer.SearchService.SearchListOfContacts(listToSearch, searchString, listOfChecked).Select(it => (ContactVM)it);

            return PartialView("_Contacts", listAfterSearch);
        }

        [Authorize]
        public ActionResult Create()
        {
            ContactVM contact = new ContactVM();

            return View(contact);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(ContactVM contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                    _businesLayer.ContactService.CreateContact((ContactDto)contact, ticket.Name);

                    return RedirectToAction("Index", "Home");
                }
                catch (ServiceDuplicateDataException ex)
                {
                    // todo log
                    ModelState.AddModelError("email", "Email is already taken.");
                    return View(contact);
                }
                catch (ServiceInvalidDataException ex)
                {
                    // todo log
                    ViewData["ErrorMessage"] = ex.Message;
                    return View(contact);
                }
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id)
        {
            _businesLayer.ContactService.RemoveContact(id);

            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            int id_user = _businesLayer.UserService.GetUserByEmail(ticket.Name).id;
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            ContactVM contact = (ContactVM)_businesLayer.ContactService.GetById(id);

            return View(contact);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(ContactVM contact)
        {
            try
            {
                _businesLayer.ContactService.UpdateContact((ContactDto)contact);

                return RedirectToAction("Index", "Home");
            }
            catch (ServiceDuplicateDataException ex)
            {
                // todo log
                ModelState.AddModelError("email", "Email is already taken.");
                return View(contact);
            }
            catch (ServiceInvalidDataException ex)
            {
                // todo log
                ViewData["ErrorMessage"] = ex.Message;
                return View(contact);
            }
        }

        [Authorize]
        public ActionResult SendEmail(string EmailTo)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            EmailVM email = new EmailVM();
            email.To = EmailTo;
            email.From = ticket.Name;

            return View(email);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SendEmail(EmailVM email)
        {
            _businesLayer.MailService.SendEmail((EmailDto)email);

            return RedirectToAction("Index", "Home");
        }
    }
}