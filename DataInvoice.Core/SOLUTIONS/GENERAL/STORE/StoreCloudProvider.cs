using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.STORE
{
    public class StoreCloudProvider : NGLib.DATA.DATAPO.DataPOProviderSQL
    {

        public StoreCloudProvider(NGLib.DATA.CONNECTOR.IDataConnector connector)
            : base(connector)
        {
            //if (swift != null) this._swift = swift;
        }

        private static NGLib.COMPONENTS.FILE.STORAGE.SwiftFileStorage _swift;
        private NGLib.COMPONENTS.FILE.STORAGE.SwiftFileStorage swift
        {
            get
            {
                if (_swift == null)
                {
                    _swift = new NGLib.COMPONENTS.FILE.STORAGE.SwiftFileStorage(null);
                    //_swift.authUser = "QSSDFG89rZ23";
                    //_swift.authPassword = "FdYmYwXq3vycgaSxy863bhy2KnCzCnvj";
                    ////swift.authToken = "eb6f7bac33654389b98b8cfa2a58a2c8"; // token obtenu via authenticate
                    //_swift.serviceUrl = "https://storage.gra1.cloud.ovh.net/v1/";
                    //_swift.authUrl = "https://auth.cloud.ovh.net/v2.0/";
                    //_swift.Account = "AUTH_ffa7db5e3ea142659421c00897aa0cf1";
                    //_swift.tenantName = "3400672520369917";
                    swift.authUser = "4RUBBz4ZpWJ3";
                    swift.authPassword = "MnYNmAKNmAQhtNp7FcPC5sRw8eXnTy73";
                    //swift.authToken = "eb6f7bac33654389b98b8cfa2a58a2c8"; // token obtenu via authenticate
                    swift.serviceUrl = "https://storage.gra3.cloud.ovh.net/v1/";
                    swift.authUrl = "https://auth.cloud.ovh.net/v2.0/";
                    swift.Account = "AUTH_047b336dc7614317beff6f17bdaa316b";
                    swift.tenantName = "7175350708785552";
                    _swift.Open();
                }
                return _swift;
            }
        }


        public const string DefaultProcstockCloud = "SEL_STORECLOUD_INFO";

        #region StoreCloud Container


        /// <summary>
        /// Obtient un cloudStore
        /// </summary>
        /// <param name="IDStore"></param>
        /// <returns></returns>
        public StoreCloud GetStoreCloud(int IDStore, bool Complete = true)
        {
            StoreCloud retour = null;
            try
            {

                // !!! Charger tous les storecloud avec une procstock (plusieur table dans un dataset)
                Dictionary<string, object> ins = new Dictionary<string, object>();
                ins.Add("p_idstore", IDStore);
                System.Data.DataSet ret = this.Connector.QueryDataSet(DefaultProcstockCloud, ins);
                if (ret == null || ret.Tables.Count == 0 || ret.Tables[0].Rows.Count == 0) return null;

                // -- tables[0]  StoreCloud
                retour = new StoreCloud(ret.Tables[0].Rows[0]);

                // -- tables[1]  StoreCloudDirectory
                retour.VirtualPaths = NGLib.DATA.DATAPO.DataPOParser.ListFromDataTable<StoreCloudDirectory>(ret.Tables[1]);

                // -- tables[2]  StoreCloudItem
                retour.Items = NGLib.DATA.DATAPO.DataPOParser.ListFromDataTable<StoreCloudItem>(ret.Tables[2]);


            }
            catch (Exception ex)
            {
                throw new Exception("GetStoreCloud " + ex.Message, ex);
            }
            return retour;
        }








        public StoreCloud CreateStoreCloud(string Title, string IDCentity = null)
        {
            StoreCloud po = null;

            try
            {
                //controles
                //if (!NGLib.DATA.FORMAT.StringUtilities.IsValidForXML(form.ti)) throw new Exception("IDStore invalide");
                //if (form.IDStore.Length < 8) throw new Exception("IDStore minLength");

                // insertion
                po = new StoreCloud();
                po["IDStore"] = DBNull.Value;

                po.Valid = ENUMS.StoreCloudValid.CANCEL;
                base.InsertBubble(po, false, true); //,false,true);

                // Création
                swift.CreateContainer(po.IDStore.ToString());

                // Si tous est pret on active
                po.Valid = ENUMS.StoreCloudValid.OK;
                base.SaveBubble(po);
                return po;
            }
            catch (Exception ex)
            {
                throw new Exception("CreateStoreCloud " + ex.Message, ex);
            }
        }


        #endregion



        #region StoreCloud FICHIERS/item

        /// <summary>
        /// Obtenir les fichiers
        /// </summary>
        /// <param name="category"></param>
        /// <param name="CountResultsMax"></param>
        /// <returns></returns>
        public List<StoreCloudItem> GetItemsByDirectory(StoreCloud storeCloud, string VirtualPath, int CountResultsMax = 100)
        {
            try
            {
                Dictionary<string, object> ins = new Dictionary<string, object>();
                ins.Add("IDStore", storeCloud.IDStore);
                ins.Add("VirtualPath", VirtualPath);
                ins.Add("CountResultsMax", CountResultsMax);
                string sql = "SELECT * FROM StoreCloud_Items WHERE IDStore=@IDStore AND VirtualPath=@VirtualPath  LIMIT @CountResultsMax"; // ORDER BY DateCreate DESC ajouter le a la requette CountResultsMax
                System.Data.DataTable ret = this.Connector.Query(sql, ins);
                List<StoreCloudItem> retour = NGLib.DATA.DATAPO.DataPOParser.ListFromDataTable<StoreCloudItem>(ret); // transformer DataTable en liste<DataPo>
                return retour;
            }
            catch (Exception ex)
            {
                throw new Exception("GetItems " + ex.Message, ex);
            }
        }


        public StoreCloudItem GetFile(long IDItem)
        {
            Dictionary<string, object> ins = new Dictionary<string, object>();
            ins.Add("IDItem", IDItem);
            StoreCloudItem retour = base.GetOneDefault<StoreCloudItem>(ins);

            return retour;
        }


        public StoreCloudItem UploadFile(StoreCloud storeCloud, string VirtualPath, System.IO.FileInfo Fichier, string newFileName = null)
        {
            if (string.IsNullOrWhiteSpace(VirtualPath)) VirtualPath = "/";
            VirtualPath = ValidateVirtualPath(VirtualPath);
            System.IO.Stream fstream = Fichier.Open(System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
            if (string.IsNullOrWhiteSpace(newFileName)) newFileName = Fichier.Name;
            return this.UploadFile(storeCloud, VirtualPath, fstream, newFileName);
        }



        public StoreCloudItem UploadFile(StoreCloud storeCloud, string VirtualPath, System.IO.Stream Fichier, string newFileName = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(VirtualPath)) VirtualPath = "/"; // alors prendre la catégorie principale du storecloud (virtualpath = / ), jamais vide
                using (var memoryStream = new MemoryStream())
                {
                    Fichier.CopyTo(memoryStream);
                    var fich = memoryStream.ToArray();
                    string id = swift.Upload(storeCloud.IDStore.ToString(), fich);
                    StoreCloudItem nouveau = new StoreCloudItem();
                    nouveau["IDItem"] = null;
                    nouveau.IDPhysicalFile = id;
                    nouveau.NameFile = newFileName;
                    nouveau.IDStore = storeCloud.IDStore;
                    nouveau.VirtualPath = VirtualPath;

                    // Insert
                    base.InsertBubble(nouveau, false, true); // usekey=false car incrémenté , getlastautoincrement = true car incrémenté, si c'était des clefs prédéfinis il faudra inverser

                    return nouveau;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("UploadFile " + ex.Message, ex);
            }
        }



        public System.IO.Stream DownloadFile(StoreCloudItem storeClouditem)
        {
            try
            {
                return new MemoryStream(swift.Download(storeClouditem.IDStore.ToString(), storeClouditem.IDPhysicalFile));

            }
            catch (Exception ex)
            {
                throw new Exception("DownloadFile " + ex.Message, ex);
            }
        }



        /// <summary>
        /// Telecharger une fichier
        /// </summary>
        /// <param name="storeCloud">Nom de dossier</param>
        /// <param name="IDLocalFile">identifiant de fichier</param>
        /// <returns></returns>
        public System.IO.Stream DownloadFile(StoreCloud storeCloud, string IDLocalFile)
        {
            try
            {
                return new MemoryStream(swift.Download(storeCloud.IDStore.ToString(), IDLocalFile));

            }
            catch (Exception ex)
            {
                throw new Exception("DownloadFile " + ex.Message, ex);
            }
        }

        // !!! Ajouter SaveFile
        /// <summary>
        ///// Telecharger une fichier
        ///// </summary>
        ///// <param name="storeCloud">Nom de dossier</param>
        ///// <param name="IDLocalFile">identifiant de fichier</param>
        ///// <returns></returns>
        //public void SaveFile(StoreCloud storeCloud, string IDLocalFile, System.IO.FileInfo myFile)
        //{
        //    try
        //    {
        //        LocalFilerProvider lfProv = new LocalFilerProvider();
        //        lfProv.SaveFile(storeCloud.IDStore.ToString(), IDLocalFile, myFile);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("SaveFile " + ex.Message, ex);
        //    }
        //}


        /// <summary>
        /// Supprimer fichier
        /// </summary>
        /// <param name="containerName">Nom de dossier</param>
        /// <param name="IDLocalFile">Id fichier</param>
        public void DeleteFile(StoreCloud storeCloud, long IDFile)
        {
            try
            {
                StoreCloudItem csi = GetFile(IDFile);
                if (csi != null)
                {
                    // swift.Delete(storeCloud.IDStore.ToString(), csi.IDPhysicalFile);
                    base.DeleteBubble(csi);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DeleteFile " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Deplacer fichier
        /// </summary>
        /// <param name="fichier">fichier à deplacer</param>
        public void MoveFile(FORM.StoreCloudItemForm fichier)
        {
            try
            {
                StoreCloudItem csi = GetFile(fichier.IDItem);
                csi.NameFile = fichier.NameFile;
                csi.VirtualPath = fichier.VirtualPath;
                base.SaveBubble(csi);
            }
            catch (Exception ex)
            {
                throw new Exception("MoveFile " + ex.Message, ex);
            }

        }

        /// <summary>
        /// Deplacer fichier
        /// </summary>
        /// <param name="fichier">fichier à deplacer</param>
        public void EditFile(FORM.StoreCloudItemForm fichier)
        {
            try
            {
                StoreCloudItem csi = GetFile(fichier.IDItem);
                if (csi.ItemSealed) throw new Exception("Sealed Item");
                csi.NameFile = fichier.NameFile;
                csi.LabelFile = fichier.LabelFile;
                base.SaveBubble(csi);
            }
            catch (Exception ex)
            {
                throw new Exception("EditFile " + ex.Message, ex);
            }

        }

        /// <summary>
        /// Get base64String
        /// </summary>
        /// <param name="idfichier">fichier à deplacer</param>
        public string Getbase64String(int idstore, long idfichier, bool safe = true)
        {
            try
            {
                StoreCloudItem csi = GetFile(idfichier);

                byte[] imageBytes = swift.Download(idstore.ToString(), idfichier.ToString());

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;

            }
            catch (Exception ex)
            {
                if (safe) return null;
                else throw new Exception("Getbase64String " + ex.Message, ex);
            }

        }




        #endregion





        #region StoreCloud DIRECTORY

        /// <summary>
        /// Nettoie le nom virtualpath
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public string ValidateVirtualPath(string virtualPath)
        {
            return StoreCloudTools.CorrectVirtualPath(virtualPath);
        }



        public List<string> GetVirtualPath(StoreCloud storeCloud)
        {
            Dictionary<string, object> ins = new Dictionary<string, object>();
            ins.Add("IDStore", storeCloud.IDStore);
            string sql = "SELECT DISTINCT VirtualPath FROM storecloud_virtalpaths WHERE VirtualPath is not null and IDStore=@IDStore";
            System.Data.DataTable ret = this.Connector.Query(sql, ins);

            return (from System.Data.DataRow rw in ret.Rows select rw[0].ToString()).ToList();
        }





        public StoreCloudDirectory CreateDirectory(StoreCloud storeCloud, string virtualPath)
        {
            virtualPath = StoreCloudTools.CorrectVirtualPath(virtualPath);
            StoreCloudDirectory dir = null;

            try
            {
                //controles
                //if (!NGLib.DATA.FORMAT.StringUtilities.IsValidForXML(form.ti)) throw new Exception("IDStore invalide");
                //if (form.IDStore.Length < 8) throw new Exception("IDStore minLength");

                // insertion
                dir = new StoreCloudDirectory();
                dir.IDStore = storeCloud.IDStore.ToString();
                dir.VirtualPath = virtualPath;

                base.InsertBubble(dir); //,false,true);

                //// Création
                //swift.CreateContainer(dir.IDStore.ToString());

                //// Si tous est pret on active
                //dir.Valid = ENUMS.StoreCloudValid.OK;
                //base.SaveBubble(dir);
                return dir;
            }
            catch (Exception ex)
            {
                throw new Exception("CreateDirectory " + ex.Message, ex);
            }

        }


        public void deleteDirectory(StoreCloud storeCloud, string virtualPath)
        {
            virtualPath = StoreCloudTools.CorrectVirtualPath(virtualPath);
            StoreCloudDirectory dir = null;

            try
            {
                dir = GetDirectorys(storeCloud.IDStore.ToString(), virtualPath).FirstOrDefault();
                var items = GetItemsByDirectory(storeCloud, virtualPath);
                var iditems = GetItemsByDirectory(storeCloud, virtualPath).Select(i => i.IDItem);
                foreach (var item in iditems)
                {
                    base.DeleteBubble(items.FirstOrDefault(i => i.IDItem == item));
                }

                base.DeleteBubble(dir);

            }
            catch (Exception ex)
            {
                throw new Exception("CreateDirectory " + ex.Message, ex);
            }

        }


        ////<summary>
        ////Obtient les catégorys d'un store
        ////</summary>
        ////<param name="IDStore"></param>
        ////<returns></returns>
        public List<StoreCloudDirectory> GetDirectorys(string IDStore, string path = null)
        {
            try
            {
                Dictionary<string, object> ins = new Dictionary<string, object>();
                ins.Add("IDStore", IDStore);
                string sql = "SELECT * FROM StoreCloud_VirtalPaths WHERE IDStore=@IDStore";
                if (path != null)
                {
                    ins.Add("path", path);
                    sql += " and VirtualPath=@path";
                }
                System.Data.DataTable ret = this.Connector.Query(sql, ins);
                List<StoreCloudDirectory> retour = NGLib.DATA.DATAPO.DataPOParser.ListFromDataTable<StoreCloudDirectory>(ret, "VirtualPath"); // transformer DataTable en dictionary<DataPo>
                return retour;
            }
            catch (Exception ex)
            {
                throw new Exception("GetDirectorys " + ex.Message, ex);
            }
        }




        #endregion





    }
}