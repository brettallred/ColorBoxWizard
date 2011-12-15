using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColorBoxWizard.Helper
{
    public class MyWizardHelper : Controller
    {

        public ActionResult Step1(Controller controller)
        {
            return RespondTo(request =>
            {
                request[AcceptsFormatType.Html] = () => View();
                request[AcceptsFormatType.Ajax] = () => PartialView();

            }, controller);
        }
        
        
        private ActionResult RespondTo(Action<RequestFormatResponder> block, Controller controller)
        {
            var responder = new RequestFormatResponder();

            if (block != null)
                block(responder);

            var result = responder.Respond(controller.ControllerContext);
            if (result != null)
                return result;

            throw new HttpException(400, "Unable to respond to requested format.");
        }
    }



}