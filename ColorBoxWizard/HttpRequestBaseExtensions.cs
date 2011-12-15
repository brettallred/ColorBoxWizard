using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColorBoxWizard
{
    public static class HttpRequestBaseExtensions
    {
        public static bool CanAccept(this HttpRequestBase request, string[] types, bool exact)
        {
            return Array.Exists(
                request.AcceptTypes,
                a => (a == "*/*" && exact == false) || Array.Exists(
                    types,
                    t => t.Equals(a, StringComparison.OrdinalIgnoreCase)
                )
            );
        }
    }


    public static class AcceptsFormatType
    {
        public static Predicate<ControllerContext> Html = (c) =>
            c.HttpContext.Request.CanAccept(new[] { "text/html" }, false);
        public static Predicate<ControllerContext> Ajax = (c) =>
            c.HttpContext.Request.CanAccept(new[] { "text/html" }, false) && c.HttpContext.Request.IsAjaxRequest();
        public static Predicate<ControllerContext> Json = (c) =>
            c.HttpContext.Request.CanAccept(new[] { "application/json" }, true);
        public static Predicate<ControllerContext> Xml = (c) =>
            c.HttpContext.Request.CanAccept(new[] { "application/xml" }, true);
    }

    public static class HasUrlExtension
    {
        public static Predicate<ControllerContext> None = (c) =>
            String.IsNullOrEmpty(c.RouteData.Values["format"] as string);
        public static Predicate<ControllerContext> Json = (c) =>
            "json".Equals(c.RouteData.Values["format"] as string, StringComparison.OrdinalIgnoreCase);
        public static Predicate<ControllerContext> Xml = (c) =>
            "xml".Equals(c.RouteData.Values["format"] as string, StringComparison.OrdinalIgnoreCase);
        public static Predicate<ControllerContext> Wizard = (c) =>
            "wizard".Equals(c.RouteData.Values["format"] as string, StringComparison.OrdinalIgnoreCase);
    }

    public class RequestFormatResponder
    {
        protected class SupportedFormat
        {
            public Predicate<ControllerContext> Predicate { get; set; }
            public Func<ActionResult> Result { get; set; }
        }

        protected List<SupportedFormat> Supported { get; set; }

        public RequestFormatResponder()
        {
            Supported = new List<SupportedFormat>();
        }

        public Func<ActionResult> this[Predicate<ControllerContext> predicate]
        {
            set { Supported.Add(new SupportedFormat { Predicate = predicate, Result = value }); }
        }

        public ActionResult Respond(ControllerContext context)
        {
            var match = Supported.LastOrDefault(s => s.Predicate(context));

            if (match != null)
                return match.Result();

            return null;
        }

        public Func<ActionResult> Html { set { this[AcceptsFormatType.Html] = value; } }
        public Func<ActionResult> HtmlAsync { set { this[AcceptsFormatType.Ajax] = value; } }
        public Func<ActionResult> Json { set { this[AcceptsFormatType.Json] = value; } }
        public Func<ActionResult> Xml { set { this[AcceptsFormatType.Xml] = value; } }
    }    





}
