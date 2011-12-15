using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ColorBoxWizard.Helper;

namespace ColorBoxWizard.Controllers
{
    public class MyWizard2Controller : BaseController
    {
        MyWizardHelper _helper;

        public MyWizard2Controller()
        {
            _helper = new MyWizardHelper();
        }

        public ActionResult Step1()
        {
            return _helper.Step1(this);
        }

    }
}
