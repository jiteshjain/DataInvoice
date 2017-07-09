using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.Core.COMPONENTS.WEB.MVC
{
    public class PageInfoManager
    {

        public SortedDictionary<int, FORM.breadcrumbForm> Breadcrumbs = new SortedDictionary<int, FORM.breadcrumbForm>();

        public List<FORM.PageAlertMessageForm> AlertsMessages = new List<FORM.PageAlertMessageForm>();


        public string ActionName { get; set; }
        public string controllerName { get; set; }
        public string AreaName { get; set; }


        /// <summary>
        /// Titre de la page
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// SousTitre de la page
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// spécifique : chemin image dans un header
        /// </summary>
        public string CustomHeaderImage { get; set; }


        /// <summary>
        /// Partie du titre visible dans le header
        /// </summary>
        public string HeadTitle
        {
            get { if (string.IsNullOrWhiteSpace(_HeadTitle)) return this.Title; else return _HeadTitle; }
            set { _HeadTitle = value; }
        }
        private string _HeadTitle = null;






        public void SetPageInfo(string Title, string SubTitle = null)
        {
            this.Title = Title;
            this.SubTitle = SubTitle;
        }


        public void SetBreadcrumb(int order, string Label, string Url = null)
        {
            FORM.breadcrumbForm form = new FORM.breadcrumbForm();
            form.Label = Label;
            form.Url = Url;
            if (this.Breadcrumbs.ContainsKey(order)) this.Breadcrumbs.Remove(order);
            this.Breadcrumbs.Add(order, form);
        }


        public void CreateAlert(string Message, int Level = 2, string Title = null)
        {
            try
            {
                FORM.PageAlertMessageForm form = new FORM.PageAlertMessageForm();
                form.Message = Message;
                form.Level = Level;
                form.Title = Title;
                AlertsMessages.Add(form);
            }
            catch (Exception)
            {
                // throw;
            }
        }





        public static PageInfoManager Open(System.Web.Mvc.ControllerBase origin)
        {
            PageInfoManager retour = null;
            if (retour == null && origin.TempData["PageInfo"] != null)
                retour = (PageInfoManager)origin.TempData["PageInfo"];
            else { retour = new COMPONENTS.WEB.MVC.PageInfoManager(); origin.TempData["PageInfo"] = retour; }


            retour.ActionName = origin.ControllerContext.RouteData.Values["action"].ToString();
            retour.controllerName = origin.ControllerContext.RouteData.Values["controller"].ToString();



            return retour;
        }



        public static PageInfoManager Open(System.Web.Mvc.TempDataDictionary origin)
        {
            PageInfoManager retour = null;
            if (retour == null && origin["PageInfo"] != null)
                retour = (PageInfoManager)origin["PageInfo"];
            else { retour = new COMPONENTS.WEB.MVC.PageInfoManager(); origin["PageInfo"] = retour; }

            return retour;
        }



    }
}
