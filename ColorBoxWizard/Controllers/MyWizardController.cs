using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColorBoxWizard.Controllers
{
    [HandleError]
    public class MyWizardController : BaseController
    {
        //
        // GET REQUESTS
        public ActionResult Step1()
        {
            return RespondTo(request =>
            {
                request[AcceptsFormatType.Html] = () => View();
                request[AcceptsFormatType.Ajax] = () => PartialView();
            });
        }

        public ActionResult Step2(string userId = null)
        {

            ViewBag.UserId = userId;

            return RespondTo(request =>
            {
                request[AcceptsFormatType.Html] = () => View();
                request[AcceptsFormatType.Ajax] = () => PartialView();
            });

        }


        public ActionResult Step3()
        {
            return RespondTo(request =>
            {
                request[AcceptsFormatType.Html] = () => View();
                request[AcceptsFormatType.Ajax] = () => PartialView();
            });

        }

        public ActionResult ThrowError()
        {
            throw new Exception("Error Message");
        }

        //
        // POST REQUESTS

        public ActionResult RegisterUser(User_RegisterCommand cmd)
        {
            if (!ModelState.IsValid)
            {
                return RespondTo(request =>
                {
                    request[AcceptsFormatType.Html] = () => View("Step1", cmd);
                    request[AcceptsFormatType.Ajax] = () => PartialView("Step1", cmd);
                });
            }


            cmd.Id = Guid.NewGuid().ToString();

            Send(cmd);

            return RedirectToAction("Step2", new { userId = cmd.Id });
        }

        public ActionResult UpdateProfile(User_UpdateProfileCommand cmd)
        {
            if (!ModelState.IsValid)
            {
                return RespondTo(request =>
                {
                    request[AcceptsFormatType.Html] = () => View("Step2", cmd);
                    request[AcceptsFormatType.Ajax] = () => PartialView("Step2", cmd);
                    
                });
            }

            Send(cmd);

            return RedirectToAction("Step3");
        }


        private void Send(object cmd)
        {

        }
    }


    public class UserViewModel
    {
        public string Name { get; set; }
        public string Id { get; set; }

    }


    public class User_RegisterCommand
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
    }


    public class User_UpdateProfileCommand
    {
        public string FavoriteSport { get; set; }
        public string FavoriteColor { get; set; }
        public string FavoriteFood { get; set; }

    }

}
