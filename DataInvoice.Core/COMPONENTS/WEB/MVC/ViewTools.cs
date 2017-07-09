using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataInvoice.COMPONENTS.WEB.MVC
{
    public static class ViewTools
    {

        public static HtmlString RawHtml(string valuer)
        {
            HtmlString myHtmlString = new HtmlString(valuer);
            return myHtmlString;

        }

    }
}
