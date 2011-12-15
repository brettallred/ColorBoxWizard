using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColorBoxWizard.Controllers
{
    public class BaseController : Controller
    {
        protected ActionResult RespondTo(Action<RequestFormatResponder> block)
        {
            var responder = new RequestFormatResponder();

            if (block != null)
                block(responder);

            var result = responder.Respond(ControllerContext);
            if (result != null)
                return result;

            throw new HttpException(400, "Unable to respond to requested format.");
        }


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.ShowLayout = true;
            
            base.OnActionExecuting(filterContext);
        }

    }
}
