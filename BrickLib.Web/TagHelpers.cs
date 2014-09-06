using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace BrickLib.Web
{
    public enum Conditionals
    {
        None = 0,
        IE6 = 1,
        IE7,
        IE8,
        IE9,
        IE10
    }

    public static class TagHelpers
    {
        public static void AddCSS(this Control cph, String filename, Conditionals cond = Conditionals.None, bool absolute = false)
        {
            if (cond == Conditionals.None)
            {
                HtmlLink lnk = new HtmlLink();
                lnk.Href = absolute ? filename : cph.ResolveUrl(filename);
                lnk.Attributes.Add("rel", "stylesheet");
                lnk.Attributes.Add("type", "text/css");
                cph.Controls.Add(lnk);
            }
            else
            {
                String ifComment = String.Empty;
                if (cond == Conditionals.IE6) { ifComment = "<!--[if IE 6]>"; }
                if (cond == Conditionals.IE7) { ifComment = "<!--[if IE 7]>"; }
                if (cond == Conditionals.IE8) { ifComment = "<!--[if IE 8]>"; }
                if (cond == Conditionals.IE9) { ifComment = "<!--[if IE 9]>"; }
                if (cond == Conditionals.IE10) { ifComment = "<!--[if IE 10]>"; }
                cph.Controls.Add(new LiteralControl(ifComment + "<link rel=\"stylesheet\" href=\"" + (absolute ? filename : cph.ResolveUrl(filename)) + "\"><![endif]-->"));
            }
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
}
