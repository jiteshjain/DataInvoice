using DataInvoice.Core.COMPONENTS.WEB.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataInvoice.SOLUTIONS.GENERAL.STORE;
using DataInvoice.SOLUTIONS.GENERAL.STORE.FORM;
using System.IO;
using DataInvoice.SOLUTIONS.INVOICES.CAMPAIGN;

namespace DataInvoice.Manager.Controllers
{
    [DataInvoice.COMPONENTS.WEB.MVC.MyAuthorize]
    public class CloudStoreController : DataInvoiceBaseMvcController
    {


        #region Providers et Objets Principaux



        protected StoreCloudProvider storeCloudProvider
        { get { if (_storeCloudProvider == null) _storeCloudProvider = new StoreCloudProvider(this.Connector); return _storeCloudProvider; } }
        private StoreCloudProvider _storeCloudProvider = null;


        protected StoreCloud StoreCloudAssociation = null;

        private CampaignProvider CampaignProvider
        { get { if (_CampaignProvider == null) _CampaignProvider = new CampaignProvider(this.Connector); return _CampaignProvider; } }
        private CampaignProvider _CampaignProvider = null;

        public Campaign GetCampagne(int idCampaign)
        {
            if (idCampaign == 0) return null;
            if (idCampaign == -1) return new Campaign();
            Campaign campaign = CampaignProvider.getCampagne(idCampaign);
            ViewBag.campaign = campaign;
            return campaign;
        }


        protected StoreCloud GetStoreCloud()
        {
            try
            {
                int idCamp = 0;
                if(Request.QueryString["IDCampaign"] != null)
                {
                    idCamp = int.Parse(Request.QueryString["IDCampaign"].Trim());
                    Session["IDCampaign"] = idCamp;
                }
                else idCamp = int.Parse(Session["IDCampaign"].ToString());

                Campaign camp = GetCampagne(idCamp);

                StoreCloud retour = storeCloudProvider.GetStoreCloud(camp.idStoreCloud, true); // possibilité d'utiliser Session comme cache plustard
                if (retour == null) throw new Exception("StoreCloud not found");
                ViewBag.StoreCloud = retour;
                return retour;
            }
            catch (Exception ex)
            {
                throw new Exception("GetStoreCloud " + ex.Message, ex);
            }
        }


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.StoreCloudAssociation = GetStoreCloud();


            if (this.StoreCloudAssociation == null) // si non dispo on affiche pas
            {
                this.PageInfo.CreateAlert("La gestion de document n'est pas installé pour cette copropriété", 3);
                RedirectResult redir = new RedirectResult(this.Url.Action("index", "association"));
                filterContext.Result = redir;
            }
            else
                base.OnActionExecuting(filterContext);
        }



        #endregion










        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string path)
        {
            try
            {
                List<string> paths = storeCloudProvider.GetVirtualPath(GetStoreCloud());
                string _path = string.Empty;
                ViewBag.StoreCloud = this.StoreCloudAssociation;

                if (string.IsNullOrEmpty(path)) path = "'/'";
                if (!string.IsNullOrEmpty(path))
                {
                    ViewBag.Files = storeCloudProvider.GetItemsByDirectory(GetStoreCloud(), path);
                }

                ViewBag.path = path;
                ViewBag.paths = paths;
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }



        /// <summary>
        /// Editer fichier
        /// </summary>
        /// <param name="idItem">id fichier</param>
        /// <returns></returns>
        public ActionResult EditerFichier(long id)
        {
            StoreCloudItem file = storeCloudProvider.GetFile(id);
            StoreCloudItemForm frm = new StoreCloudItemForm { IDItem = file.IDItem, LabelFile = file.LabelFile, NameFile = file.NameFile };
            return PartialView(frm);
        }

        /// <summary>
        /// Validation l'edition
        /// </summary>
        /// <param name="sci">fichier</param>
        /// <returns></returns>
        public ActionResult ValiderEditerFichier(StoreCloudItemForm sci, string Path)
        {
            try
            {
                storeCloudProvider.EditFile(sci);
                PageInfo.CreateAlert("Modification terminé", 2);
            }
            catch (Exception ex)
            {
                PageInfo.CreateAlert(ex.Message, 4);
                //throw;
            }

            return RedirectToAction("Index", new { path = Path });

        }

        /// <summary>
        /// Validation l'edition
        /// </summary>
        /// <param name="sci">fichier</param>
        /// <returns></returns>
        public ActionResult SupprimerFichier(long idItem, string Path)
        {
            try
            {
                storeCloudProvider.DeleteFile(this.StoreCloudAssociation, idItem);
                PageInfo.CreateAlert("Suppression terminé", 2);
            }
            catch (Exception ex)
            {
                PageInfo.CreateAlert(ex.Message, 4);
            }

            return RedirectToAction("Index", new { path = Path });
        }

        /// <summary>
        /// Telecharger
        /// </summary>
        /// <param name="idItem">id fichier</param>
        /// <returns></returns>
        public FileStreamResult Download(string idItem)
        {
            StoreCloudItem file = storeCloudProvider.GetFile(long.Parse(idItem));
            return File(storeCloudProvider.DownloadFile(GetStoreCloud(), file.IDPhysicalFile), file.NameFile);
        }

        /// <summary>
        /// Ajouter fichier
        /// </summary>
        /// <returns></returns>
        public ActionResult AjouterFichier()
        {
            return View();
        }






        /// <summary>
        /// Upload file
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFile(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path)) throw new Exception("Path Empty");
                foreach (string upload in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[upload];
                    if (file == null || file.ContentLength <= 0) continue;
                    HttpPostedFileBase postedFile = Request.Files[upload];
                    byte[] buf = new byte[postedFile.InputStream.Length];
                    postedFile.InputStream.Read(buf, 0, (int)postedFile.InputStream.Length);
                    MemoryStream ms = new MemoryStream(buf);
                    storeCloudProvider.UploadFile(GetStoreCloud(), path, ms, postedFile.FileName);
                    ViewBag.Files = storeCloudProvider.GetItemsByDirectory(GetStoreCloud(), path);
                    ViewBag.path = path;
                }
                PageInfo.CreateAlert("Upload des fichiers terminé", 2);
            }
            catch (Exception ex)
            {
                PageInfo.CreateAlert(ex.Message, 4);
            }
            return RedirectToAction("Index", new { path = path });

        }

        /// <summary>
        /// Upload Image
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadImage(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path)) throw new Exception("Path Empty");
                foreach (string upload in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[upload];
                    if (file == null || file.ContentLength <= 0 || file.ContentLength > (1024 * 500)) throw new Exception("Taille image dépasse 500 ko");
                    HttpPostedFileBase postedFile = Request.Files[upload];
                    byte[] buf = new byte[postedFile.InputStream.Length];
                    postedFile.InputStream.Read(buf, 0, (int)postedFile.InputStream.Length);
                    MemoryStream ms = new MemoryStream(buf);
                    System.Drawing.Bitmap btp = new System.Drawing.Bitmap(ms);
                    if (btp == null || btp.Size.Height < 400 || btp.Size.Width < 600) throw new Exception("Dimension image inférieur à 400x600");
                    Stream stream = new MemoryStream();
                    btp.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    stream.Position = 0;
                    StoreCloudItem img = storeCloudProvider.UploadFile(GetStoreCloud(), path, stream, postedFile.FileName);

                    ViewBag.Files = storeCloudProvider.GetItemsByDirectory(GetStoreCloud(), path);
                    ViewBag.path = path;

                }
                PageInfo.CreateAlert("Upload image terminé", 2);
            }
            catch (Exception ex)
            { PageInfo.CreateAlert(ex.Message, 4); }
            return RedirectToAction("Index", new { path = path });
        }

        /// <summary>
        /// Preview image
        /// </summary>
        /// <param name="idstore">id store</param>
        /// /// <param name="Idfile">id fichier</param>
        /// <returns></returns>
        public FileStreamResult ImagePreview(int idstore, long Idfile)
        {
            try
            {
                StoreCloudItem file = storeCloudProvider.GetFile(Idfile);
                if (file == null) return null;
                Stream streamfile = storeCloudProvider.DownloadFile(file);

                return File(streamfile, file.NameFile);
            }
            catch (Exception)
            {
                return null;
            }
        }





    }
}