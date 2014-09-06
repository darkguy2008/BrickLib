using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickLib.Web
{
    public static class Extensions
    {
        // public static string RelativePath(this HttpServerUtility srv, string path, HttpRequest context) { return path.Replace(context.ServerVariables["APPL_PHYSICAL_PATH"], "~/").Replace(@"\", "/"); }

        // TODO: Optimize with BPs (best-practices)
        public static String ToQueryString(this Dictionary<String, String> dict)
        {
            String rv = String.Empty;

            if (dict.Count > 0)
            {
                rv = "?";
                foreach (KeyValuePair<String, String> kv in dict)
                    rv += kv.Key + "=" + kv.Value + "&";
                rv = rv.Substring(0, rv.Length - 1);
            }

            return rv;
        }
    }
}
