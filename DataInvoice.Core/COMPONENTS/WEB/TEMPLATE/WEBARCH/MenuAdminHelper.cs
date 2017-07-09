using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataInvoice.Core.COMPONENTS.WEB.TEMPLATE.WEBARCH
{
    public class MenuAdminHelper
    {


        public static HtmlString ShowLinksLi(Dictionary<string, string> labellinks)
        {
            StringBuilder html = new StringBuilder();
            if (labellinks!=null)
            foreach (string item in labellinks.Keys) //<i class="fa fa-cogs" aria-hidden="true"></i> Gestion Logiciel
            {
                html.AppendFormat("<li><a href='{1}'>{0}</a></li>",item,labellinks[item]);
                html.AppendLine();
            }

            return new HtmlString(html.ToString());
        }




    }
}
