using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace SkyCoApi.Helpers
{
    public static class FilterHelper
    {
        public static String GenerateFilter(Int32 top, String orderby, String ascending)
        {
            return "?top=" + top + "&orderby=" + orderby + "&ascending=" + ascending;
        }

        public static String GenerateFilter(HybridDictionary filterscollection, Int32 top, String orderby, String ascending)
        {
            String myfilters = "?";
            foreach (String clave in filterscollection.Keys)
            {
                myfilters += clave + "=" + filterscollection[clave].ToString() + "&";
            }
            return myfilters + "top=" + top + "&orderby=" + orderby + "&ascending=" + ascending;
        }
    }
}