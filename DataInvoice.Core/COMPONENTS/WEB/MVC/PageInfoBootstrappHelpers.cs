using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.Core.COMPONENTS.WEB.MVC
{
    public static class PageInfoBootstrappHelpers
    {


        public static string ShowAlertMessageBox(PageInfoManager pageinfo)
        {
            if (pageinfo == null || pageinfo.AlertsMessages == null || pageinfo.AlertsMessages.Count == 0)
                return string.Empty;
            try
            {
                StringBuilder build = new StringBuilder();
                foreach (FORM.PageAlertMessageForm alert in pageinfo.AlertsMessages)
                {
                    string amle = "info";
                    if (alert.Level == 2) { amle = "success"; }
                    else if (alert.Level == 3) { amle = "warning"; }
                    else if (alert.Level > 3) { amle = "danger"; }
                    build.AppendFormat("<div role=\"alert\" class=\"alert alert-{0}\"> <b>{1}</b> {2}</div>", amle, alert.Title, alert.Message);
                }

                return build.ToString();

            }
            catch (Exception)
            {
                return "<i>ShowAlertMessageBox Error</i>";
            }
        }



        public static string ShowBreadcrumb(PageInfoManager pageinfo, string MasterBC = null)
        {
            try
            {
                StringBuilder html = new StringBuilder();
                html.Append("<ul class=\"breadcrumb page-breadcrumb \">");
                if (!string.IsNullOrWhiteSpace(MasterBC))
                    html.Append("<li><p>" + MasterBC + "</p></li>");
                foreach (FORM.breadcrumbForm bread in pageinfo.Breadcrumbs.Values)
                {
                    if (string.IsNullOrWhiteSpace(bread.Url)) html.AppendFormat("<li><span>{0}</span></li>", bread.Label);
                    else html.AppendFormat("<li><a href=\"{0}\">{1}</a></li>", bread.Url, bread.Label);
                }
                html.Append("</ul>");
                return html.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string ShowBigBlockTitleHeader(PageInfoManager pageinfo)
        {
            if (pageinfo == null || pageinfo.Title == null)
                return string.Empty;
            try
            {
                StringBuilder html = new StringBuilder();

                //    <!-- PAGE TITLE -->
                html.Append("<section id=\"page-title\">");
                html.Append("<div class=\"container\">");
                html.Append("<div class=\"page-title col-md-8\">");
                html.AppendFormat("<h1>{0}</h1>", pageinfo.Title);
                if (pageinfo.SubTitle != null) html.AppendFormat("<span>@form.Subtitle</span>", pageinfo.SubTitle);
                html.Append("</div>");
                if (pageinfo.Breadcrumbs.Count > 0)
                {
                    html.Append("<div class=\"breadcrumb col-md-4\">");
                    html.Append("<ul>");
                    foreach (FORM.breadcrumbForm bread in pageinfo.Breadcrumbs.Values)
                    {
                        html.AppendFormat("<li><a href=\"{0}\">{1}</a></li>", bread.Url, bread.Label);
                    }
                    html.Append(" </ul>");
                    html.Append("</div>");
                }
                html.Append("</div>");
                html.Append("</section>");
                //<!-- END: PAGE TITLE -->
                return html.ToString();
            }
            catch (Exception)
            {
                return "<i>ShowBigBlockTitleHeader Error</i>";
            }
        }






    }
}
