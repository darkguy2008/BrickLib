using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace BrickLib.Web
{
    public static class TagHelpers
    {
        public static void AddCSS(this Control cph, String filename)
        {
            HtmlLink lnk = new HtmlLink();
            lnk.Href = cph.ResolveUrl(filename);
            lnk.Attributes.Add("rel", "stylesheet");
            lnk.Attributes.Add("type", "text/css");
            cph.Controls.Add(lnk);
        }
        public static void AddJS(this Control cph, String filename)
        {
            HtmlGenericControl ctl = new HtmlGenericControl();
            ctl.TagName = "script";
            ctl.Attributes["type"] = "text/javascript";
            ctl.Attributes["src"] = cph.ResolveUrl(filename);
            cph.Controls.Add(ctl);
        }
    }

    /*
    public static partial class Extensions
    {
        public static string RelativePath(this HttpServerUtility srv, string path, HttpRequest context)
        {
            return path.Replace(context.ServerVariables["APPL_PHYSICAL_PATH"], "~/").Replace(@"\", "/");
        }
    }
    */
}
