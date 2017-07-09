using DataInvoice.SOLUTIONS.INVOICES.CONTACT;
using DataInvoice.SOLUTIONS.INVOICES.INVOICE.ENUM;
using DataInvoice.SOLUTIONS.INVOICES.INVOICE.FORM;
using DataInvoice.SOLUTIONS.INVOICES.INVOICE.USE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.INVOICE
{
    public class InvoiceProvider : NGLib.DATA.DATAPO.DataPOProviderSQL
    {

        public InvoiceProvider(NGLib.DATA.CONNECTOR.IDataConnector connector)
            : base(connector)
        {  }

        const int idAccount = 1; // A revoir

        public const string DefaultProcstockInvoice = "SEL_INVOICE_INFO";
        public Invoice GetInvoice (int IDInvoice)
        {
            Invoice retour = null;
            try
            {
                Dictionary<string,object> ins = new Dictionary<string,object>();
                ins.Add("p_IDInvoice", IDInvoice);
                System.Data.DataSet ret = this.Connector.QueryDataSet(DefaultProcstockInvoice, ins);
                if (ret == null || ret.Tables.Count == 0 || ret.Tables[0].Rows.Count == 0) return null;

                // -- tables[0]  INVOICE
                retour = new Invoice(ret.Tables[0].Rows[0]);

                // -- tables[1]  LINES
                retour.Lines = new InvoiceLineResults(); //NGLib.DATA.DATAPO.DataPOParser.ListFromDataTable<InvoiceLine>(ret.Tables[1]);
                retour.Lines.LoadFromDataTable(ret.Tables[1]);

                // -- tables[2]  LOGS
                retour.Logs = NGLib.DATA.DATAPO.DataPOParser.ListFromDataTable<InvoiceLog>(ret.Tables[2]);

                //-- tables[3]  ADDRESS
                List<SOLUTIONS.INVOICES.CONTACT.Address> laddress = NGLib.DATA.DATAPO.DataPOParser.ListFromDataTable<SOLUTIONS.INVOICES.CONTACT.Address>(ret.Tables[3]);
                foreach (SOLUTIONS.INVOICES.CONTACT.Address item in laddress)
                {
                    if (item.IDAddress == retour.SellerIDAddress) retour.SellerAddress = item;
                    if (item.IDAddress == retour.BuyerIDAddress) retour.BuyerAddress = item;
                }



            }
            catch (Exception ex)
            {
                throw new Exception("getInvoice "+ex.Message);
            }
            return retour;
        }       

        public InvoiceLine GetLine(long idLine)
        {
            Dictionary<string, object> ins = new Dictionary<string, object>();
            ins.Add("idLine", idLine);
           return base.GetOneDefault<InvoiceLine>(ins);
        }



        public void SetInvoiceState(Invoice invoicepo, ENUM.InvoiceStateEnum state, string msg=null)
        {
            invoicepo.InvoiceState  =state;
            this.UpdateBubble(invoicepo, "InvoiceState", state);
        }



        /// <summary>
        /// Update selon le nom sur différent objets
        /// </summary>
        public FORM.InvoiceDataUpdtAjaxPoco UpdateField(Invoice invoicepo, string Name, string Value, bool saveInBase= true)
        {
            FORM.InvoiceDataUpdtAjaxPoco retour = new InvoiceDataUpdtAjaxPoco();
            if (invoicepo.IDInvoice < 0) saveInBase = false; // impossible de copier en base

            List<string> nameAlloweds = new List<string>(new string[] { "Invoice", "RefInvoice", "DateInvoice", "BuyerAddress", 
                "Adress1", "Adress2", "Adress3", "Postcode","City","Country", "postCode","ContactMail","ContactPhone","Identity","Compagny",
                "RefBuyer", "RefSeller","SellerAddress", "Lines","LineQuantity","LineAmount","LineLabel" });

            if (Name.Split('_').Count() < 2) throw new Exception("Paramètres non autorisés");
            string NameObject = Name.Split('_')[Name.Split('_').Count() - 2].Split('|')[0];
            string IdObject = Name.Split('_')[Name.Split('_').Count() - 2].Split('|').Count() > 1 ? Name.Split('_')[Name.Split('_').Count() - 2].Split('|')[1] : "";
            string NameField = Name.Split('_')[Name.Split('_').Count() - 1];


            if (!nameAlloweds.Contains(NameObject) || !nameAlloweds.Contains(NameField)) throw new Exception("Paramètres non autorisés");
            switch (NameObject)
            {
                    // Modification de la facture
                case "Invoice":
                    invoicepo[NameField] = Value;
                    if (saveInBase) this.UpdateBubble(invoicepo, NameField, Value);
                    break;

                    // modification des ligne de la facture
                case "Lines":
                    InvoiceLine line = invoicepo.Lines.FirstOrDefault(l => l.IDLine == int.Parse(IdObject));
                    line[NameField] = Value;
                    if (saveInBase) this.UpdateBubble(line, NameField, Value);
                    break;

                    // Contact Acheteur
                case "BuyerAddress":
                    if (invoicepo.BuyerAddress == null)
                        invoicepo.BuyerAddress = new Address();
                    invoicepo.BuyerAddress[NameField] = Value;
                    if (saveInBase)
                    {
                        SOLUTIONS.INVOICES.CONTACT.AddressProvider addressProvide = new AddressProvider(this.Connector);
                        addressProvide.SaveAddress(invoicepo.BuyerAddress);
                        if(invoicepo.BuyerIDAddress != invoicepo.BuyerAddress.IDAddress)
                            this.UpdateBubble(invoicepo, "BuyerIDAddress", invoicepo.BuyerAddress.IDAddress);
                    }
                    break;

                    // Contact Vendeur
                case "SellerAddress":
                    if (invoicepo.SellerAddress == null)
                        invoicepo.SellerAddress = new Address();
                    invoicepo.SellerAddress[NameField] = Value;
                    if (saveInBase)
                    {
                        SOLUTIONS.INVOICES.CONTACT.AddressProvider addressProvide = new AddressProvider(this.Connector);
                        addressProvide.SaveAddress(invoicepo.SellerAddress);
                        if (invoicepo.SellerIDAddress != invoicepo.SellerAddress.IDAddress)
                            this.UpdateBubble(invoicepo, "SellerIDAddress", invoicepo.SellerAddress.IDAddress);
                    }
                    break;

                    //NA
                default:
                    break;
            }

            // retour des données sur la factur (calcul montant)
            // !!! normalement ne devrai pas etre ici faire une méthode spécial dans le controlleur, qui retourne un JSON qui permet de mettre a jour la facture
            if (NameObject == "Lines")
            {
                InvoiceLine line = invoicepo.Lines.FirstOrDefault(l => l.IDLine == int.Parse(IdObject));
                double somme = invoicepo.Lines.Sum(ln => ln.LineQuantity * ln.LineAmount);
                invoicepo.FinalAmount = somme;
                if (saveInBase) this.UpdateBubble(invoicepo, "FinalAmount", somme);
                retour.LastUpdtline = new InvoiceDataUpdtAjaxPoco.LineUpdtAjaxPoco();
                retour.LastUpdtline.FromPo(line);
                //return "€ " + (line.LineQuantity * line.LineAmount).ToString() + "|€ " + somme;
            }
            retour.FromPo(invoicepo);
            return retour;
        }





        public int GetLastInvoiceSubNumber(INVOICE.USE.InvoiceGenerateNumberResult generatedata)
        {
            string sql = "SELECT InvoiceSubNumber FROM Invoices WHERE IDAccount=@IDAccount AND";
            return 1;
        }


        public bool InvoiceNumberExist(int idAccount, string InvoiceNumber)
        {
            return false;
        }



        // Divers
        //$n => 15          Compteur
        //$nnnnn =>  00015 
        // Date :
        //$dd => 01 (day of month)
        //$ddd => 125 (day of year)
        //$mm > 08 (month)
        //$yyyy => 2016 (year)
        //$yy => 16 
        public InvoiceGenerateNumberResult GenerateInvoiceNumber(Invoice invoice, string strFormatParam)
        {
            InvoiceGenerateNumberResult retour = new InvoiceGenerateNumberResult();
            try
            {
                DateTime invoiceDate = (DateTime)invoice.DateInvoice;


                // Gestion des dates

                retour.InvoiceNumber = strFormatParam;
                if (retour.InvoiceNumber.Contains("$dd"))
                {
                    if (retour.CoWhereDate < DateLevelEnum.DAY) retour.CoWhereDate = DateLevelEnum.DAY;
                    retour.InvoiceNumber = retour.InvoiceNumber.Replace("$dd", invoiceDate.ToString("dd"));
                }
                if (retour.InvoiceNumber.Contains("$ddd"))
                {
                    if (retour.CoWhereDate < DateLevelEnum.DAY) retour.CoWhereDate = DateLevelEnum.DAY;
                    retour.InvoiceNumber = retour.InvoiceNumber.Replace("$ddd", NGLib.DATA.FORMAT.StringUtilities.Complete(invoiceDate.DayOfYear.ToString(), 3, true));
                }
                if (retour.InvoiceNumber.Contains("$mm"))
                {
                    if (retour.CoWhereDate < DateLevelEnum.MONTH) retour.CoWhereDate = DateLevelEnum.MONTH;
                    retour.InvoiceNumber = retour.InvoiceNumber.Replace("$mm", NGLib.DATA.FORMAT.StringUtilities.Complete(invoiceDate.Month.ToString(), 2, true));
                }
                if (retour.InvoiceNumber.Contains("$yyyy"))
                {
                    if (retour.CoWhereDate < DateLevelEnum.YEAR) retour.CoWhereDate = DateLevelEnum.YEAR;
                    retour.InvoiceNumber = retour.InvoiceNumber.Replace("$yyyy", invoiceDate.ToString("yyyy"));
                }
                if (retour.InvoiceNumber.Contains("$yy"))
                {
                    if (retour.CoWhereDate < DateLevelEnum.YEAR) retour.CoWhereDate = DateLevelEnum.YEAR;
                    retour.InvoiceNumber = retour.InvoiceNumber.Replace("$yy", invoiceDate.ToString("yy"));
                }


                if (retour.InvoiceNumber.Contains("$n"))
                {
                    int sizeCounter = CountSameFollowingChar(retour.InvoiceNumber.Substring(retour.InvoiceNumber.IndexOf("$n") + 1), 'n');
                    string realreplace = "$";
                    realreplace = realreplace.PadLeft(sizeCounter, 'n');
                    int invoiceSubNumber = this.GetLastInvoiceSubNumber(retour);
                    string value = NGLib.DATA.FORMAT.StringUtilities.Complete(invoiceSubNumber.ToString(), sizeCounter, false);
                    retour.InvoiceNumber = retour.InvoiceNumber.Replace(realreplace, value);
                }



                // Vérifier si existe pas déja
                if (this.InvoiceNumberExist(invoice.IDAccount, retour.InvoiceNumber))
                    throw new Exception("Invoice Number already exist");

            }
            catch (Exception)
            {

                throw;
            }
            return retour;
        }


        public static int CountSameFollowingChar(string chaine, char val)
        {
            int retour = 0;
            for (int i = 0; i < 10000; i++)
            {
                if (chaine.Length < i - 1) break;
                if (chaine[i] == val) retour++;
                else break;
            }


            return retour;
        }


        // CreateInvoice (form)
        public Invoice CreateInvoice( InvoiceCreateForm form)
        {
            try
            {
                Invoice nouveau = new Invoice();
                nouveau.FromObject(form);
                nouveau.InvoiceState = InvoiceStateEnum.PREPARE;
                
                nouveau.IDAccount = idAccount;


                // Insert
                this.InsertInvoice(nouveau);


                return nouveau;
            }
            catch (Exception ex)
            {
                throw new Exception("create " + ex.Message, ex);
            }
        }





        /// <summary>
        /// Enregistre ou insert les objets nécessaire sur la facture
        /// </summary>
        /// <param name="invoicepo"></param>
        public void SaveFullInvoice(Invoice invoicepo)
        {
            try
            {
                SOLUTIONS.INVOICES.CONTACT.AddressProvider addressProvider = new AddressProvider(this.Connector);
                if (invoicepo.IDInvoice < 0) this.InsertInvoice(invoicepo); // mode insertion

                // ENREGISTREMENT DES ADDRESS
                if (invoicepo.BuyerAddress != null)
                {
                    addressProvider.SaveAddress(invoicepo.BuyerAddress);
                    if (invoicepo.BuyerAddress.IDAddress != invoicepo.BuyerIDAddress) invoicepo.BuyerIDAddress = invoicepo.BuyerAddress.IDAddress;
                }
                if (invoicepo.SellerAddress != null)
                {
                    addressProvider.SaveAddress(invoicepo.SellerAddress);
                    if (invoicepo.SellerAddress.IDAddress != invoicepo.SellerIDAddress) invoicepo.SellerIDAddress = invoicepo.SellerAddress.IDAddress;
                }

                // ENREGISTREMENT DES LIGNES
                foreach (InvoiceLine item in invoicepo.Lines)
                {
                    this.SaveLine(item, invoicepo);
                }

                
                // ENREGISTREMENT FINAL
                this.SaveInvoice(invoicepo);

            }
            catch (Exception ex) { throw new Exception("SaveInvoice" + ex.Message, ex); }
        }

        public void SaveInvoice(Invoice invoicepo)
        {
            base.SaveBubble(invoicepo);
        }

        public void SaveLine(InvoiceLine item, Invoice invoicepo=null)
        {
            if (item.IDLine < 0) this.InsertLine(item, invoicepo); // mode insertion
            else this.SaveBubble(item);
        }


        /// <summary>
        /// Charge les champs principaux pour instancier une facture
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public Invoice PrepareInvoice(SOLUTIONS.GENERAL.ACCOUNT.Account Account, CAMPAIGN.Campaign Campagne)
        {
            if (Account == null || Account.IDAccount == 0) throw new Exception("AccountError");
            Invoice invoice = new Invoice();
            invoice.IDInvoice = -1; // sera créer au prochain Save
            invoice.IDAccount = Account.IDAccount;
            invoice.DateInvoice = DateTime.Now.Date;
            if (Campagne != null)
            {
                invoice.IDCampaign = Campagne.IDCampaign;
                invoice.InvoiceLogo = Campagne.DefaultLogoUrl;
                invoice.BuyerIDAddress = Campagne.IdAddresseBuyerDefault;
                invoice.BuyerAddress = Campagne.AddresseBuyerDefault;
                invoice.SellerIDAddress = Campagne.IdAddresseSellerDefault;
                invoice.SellerAddress = Campagne.AddresseSellerDefault;
                //var _campaignProvider = new CAMPAIGN.CampaignProvider(Connector);
                //_campaignProvider.getCampagne()
            }

            //invoice.Lines.NewLine()
            return invoice;
        }





        public void InsertInvoice(Invoice invoicepo)
        {
            try
            {
                if (invoicepo.IDAccount == 0) throw new Exception("IDAccount empty");
                invoicepo.DateCreate = DateTime.Now;
                invoicepo["IDInvoice"] = DBNull.Value;
                this.InsertBubble(invoicepo, false, true);
            }
            catch (Exception ex)
            {
                throw new Exception("InsertInvoice " + ex.Message, ex);
            }
        }


        public Invoice UpdateInvoice(InvoiceCreateForm form)
        {
            try
            {
                Invoice invoice = GetInvoice(form.IDInvoice);
                if (invoice != null) 
                {
                    invoice.FromObject(form);
                    invoice.IDAccount = idAccount;                
                    base.SaveBubble(invoice);
                }
               

                return invoice;
            }
            catch (Exception ex)
            {
                throw new Exception("UpdateInvoice " + ex.Message, ex);
            }
        }


        public InvoiceLine UpdateInvoiceLine(InvoiceLine form)
        {
            try
            {
                InvoiceLine invoiceline = GetLine(form.IDLine);
                if (invoiceline != null)
                {
                   // invoiceline.FromObject(form);
                 if(form.LineQuantity != 0)   invoiceline.LineQuantity = form.LineQuantity;
                  if(!string.IsNullOrEmpty(form.LineLabel))  invoiceline.LineLabel = form.LineLabel;
                  if (form.LineAmount != 0) invoiceline.LineAmount = form.LineAmount;
                    
                    base.SaveBubble(invoiceline);
                }


                return invoiceline;
            }
            catch (Exception ex)
            {
                throw new Exception("UpdateInvoiceLine " + ex.Message, ex);
            }
        }

        public InvoiceLine InsertLine(InvoiceLine nouveau, Invoice invoice)
        {
            try
            {
                if (nouveau.IDInvoice == 0) throw new Exception();
                nouveau.IDInvoice = invoice.IDInvoice;
                nouveau.IDAccount = invoice.IDAccount;
                //nouveau.LineTax = invoice.DefaultTaxeValue;
                // Insert
                nouveau["IDLine"] = DBNull.Value;
                this.InsertBubble(nouveau, false, true);
                return nouveau;
            }
            catch (Exception ex)
            {
                throw new Exception("createLine " + ex.Message, ex);
            }
        }

        

        public INVOICE.InvoiceResults SearchInvoice(InvoiceSearchForm form)
        {
            // string sql = "SELECT * FROM invoices where 1=1";
            Dictionary<string, object> ins = new Dictionary<string, object>();
            if (form == null)
                form = new InvoiceSearchForm();

            List<string> wheres = new List<string>();
            try
            {
                if (!string.IsNullOrWhiteSpace(form.CustomerRef)) { ins.Add("CustomerRef", form.CustomerRef); wheres.Add(" CustomerRef=@CustomerRef"); }
                if (!string.IsNullOrWhiteSpace(form.InvoiceTitle)) { ins.Add("InvoiceTitle", form.InvoiceTitle); wheres.Add(" InvoiceTitle=@InvoiceTitle"); }
                if (!string.IsNullOrWhiteSpace(form.InvoiceType)) { ins.Add("InvoiceType", form.InvoiceType); wheres.Add(" InvoiceType=@InvoiceType"); }
                if (!string.IsNullOrWhiteSpace(form.ProviderRef)) { ins.Add("ProviderRef", form.ProviderRef); wheres.Add(" ProviderRef=@ProviderRef"); }
                if (!string.IsNullOrWhiteSpace(form.RefInvoice)) { ins.Add("RefInvoice", form.RefInvoice); wheres.Add(" RefInvoice=@RefInvoice"); }
                //if (form.DateAcq.HasValue)  {ins.Add("DateAcq", form.DateAcq.Value); wheres.Add(" and InvoiceTitle=@InvoiceTitle"); }
                //if (form.DateCreate.HasValue)  {ins.Add("DateCreate", form.DateCreate.Value); wheres.Add(" and InvoiceTitle=@InvoiceTitle"); }
                if (form.DateInvoice.HasValue) { ins.Add("DateInvoice", form.DateInvoice.Value); wheres.Add(" DateInvoice=@DateInvoice"); }
                //if (form.DatePaid.HasValue)  {ins.Add("DatePaid", form.DatePaid.Value); wheres.Add(" and InvoiceTitle=@InvoiceTitle"); }
                //if (form.DateSend.HasValue) { ins.Add("DateSend", form.DateSend.Value); wheres.Add(" and InvoiceTitle=@InvoiceTitle"); }
                //if (form.DateValidate.HasValue)  { ins.Add("DateValidate", form.DateValidate.Value); wheres.Add( " and InvoiceTitle=@InvoiceTitle"); }

                string sql = " FROM invoices ";
                if (wheres.Count > 0)
                    sql += string.Join(" AND ", wheres.ToArray());

                InvoiceResults retour = new InvoiceResults();
                int nb = Convert.ToInt32(this.Connector.QueryScalar("SELECT COUNT(*) as nb " + sql, ins));
                retour.TotalCountResults = nb;

                System.Data.DataTable ret = this.Connector.Query("SELECT * " + sql, ins);

                retour.LoadFromDataTable(ret);
                return retour;
            }
            catch (Exception ex)
            {
                throw new Exception("SearchInvoice " + ex.Message, ex);
            }
        }

        public Invoice ChangeState(Invoice invoice, InvoiceStateEnum newstate)
        {
            try
            {
                invoice.InvoiceState = newstate;
                base.SaveBubble(invoice);
                return invoice;
            }
            catch (Exception ex) { throw new Exception("ChangeState " + ex.Message, ex); }
        }

        public bool CreateLog(Invoice invoice, string MessageText, int MessageLevel=1, bool InternalLog= false)
        {
            try
            {
                InvoiceLog nouveau = new InvoiceLog { IDInvoice = invoice.IDInvoice, IDAccount = invoice.IDAccount, 
                    InternalLog = InternalLog, MessageLevel = MessageLevel, MessageText = MessageText, 
                    DateCreate = DateTime.Now };
                nouveau["IDLog"] = DBNull.Value;
                base.InsertBubble(nouveau);
                return true;
            
            }
            catch (Exception ex) { return false; }
        }


        #region gestion des fichiers


        private static NGLib.COMPONENTS.FILE.STORAGE.SwiftFileStorage DatainvoiceSwiftStore = null;

        private NGLib.COMPONENTS.FILE.STORAGE.SwiftFileStorage GetswiftStore()
        {
            if (DatainvoiceSwiftStore != null) return DatainvoiceSwiftStore;

            NGLib.COMPONENTS.FILE.STORAGE.SwiftFileStorage swift = new NGLib.COMPONENTS.FILE.STORAGE.SwiftFileStorage(null);

            //swift.authUser = "QSSDFG89rZ23";
            //swift.authPassword = "FdYmYwXq3vycgaSxy863bhy2KnCzCnvj";
            ////swift.authToken = "eb6f7bac33654389b98b8cfa2a58a2c8"; // token obtenu via authenticate
            //swift.serviceUrl = "https://storage.gra1.cloud.ovh.net/v1/";
            //swift.authUrl = "https://auth.cloud.ovh.net/v2.0/";
            //swift.Account = "AUTH_ffa7db5e3ea142659421c00897aa0cf1";
            //swift.tenantName = "3400672520369917";

            swift.authUser = "4RUBBz4ZpWJ3";
            swift.authPassword = "MnYNmAKNmAQhtNp7FcPC5sRw8eXnTy73";
            //swift.authToken = "eb6f7bac33654389b98b8cfa2a58a2c8"; // token obtenu via authenticate
            swift.serviceUrl = "https://storage.gra3.cloud.ovh.net/v1/";
            swift.authUrl = "https://auth.cloud.ovh.net/v2.0/";
            swift.Account = "AUTH_047b336dc7614317beff6f17bdaa316b";
            swift.tenantName = "7175350708785552";

            swift.Open();
            Console.WriteLine("IsAuthenticated " + swift.IsAuthenticated());
            Console.WriteLine("COPROP_Test exist " + swift.GetObjectList("COPROP_Test2"));

            DatainvoiceSwiftStore = swift;


            return swift;
        }


        public string AddFile(Invoice invoice, byte[] filedata)
        {
            using (MemoryStream fistream = new MemoryStream(filedata))
            {
                return AddFile(invoice, fistream);
            }
        }

        public string AddFile(Invoice invoice, System.IO.Stream fileStream)
        {
            try
            {
                string IDLocalFile = string.Empty;
                byte[] fileBinary = new byte[fileStream.Length];
                fileStream.Read(fileBinary, 0, (int)fileStream.Length);
                NGLib.COMPONENTS.FILE.STORAGE.SwiftFileStorage storer = GetswiftStore();

                string conteneurNameTest = "invc" + invoice.IDAccount.ToString();
                if (!storer.ContainerExists(conteneurNameTest)) storer.CreateContainer(conteneurNameTest);
                
                IDLocalFile = storer.Upload("invc"+invoice.IDAccount.ToString(), fileBinary);
                invoice.IDFile = IDLocalFile;
                base.SaveBubble(invoice);
                return IDLocalFile;
            }
            catch (Exception ex) { throw new Exception("AddFile Invoice " + ex.Message, ex); }
        }



        public byte[] DownloadFile(Invoice invoice)
        {
            try
            {
                if (string.IsNullOrEmpty(invoice.IDFile)) return null;
                NGLib.COMPONENTS.FILE.STORAGE.SwiftFileStorage storer = GetswiftStore();
                byte[] fileBytes = storer.Download("invc" + invoice.IDAccount.ToString(), invoice.IDFile);
                //Stream stream = new MemoryStream(fileBytes);
                return fileBytes;
            }
            catch (Exception ex) { throw new Exception("DownloadFile Invoice " + ex.Message, ex); }
        }





        public bool DeleteFile(Invoice invoice)
        { try
            {
                if (string.IsNullOrEmpty(invoice.IDFile)) return false;
                NGLib.COMPONENTS.FILE.STORAGE.SwiftFileStorage storer = GetswiftStore();
                if (storer.Delete(invoice.IDAccount.ToString(), invoice.IDFile))
                 {
                     invoice["IDFile"] = null;
                     base.SaveBubble(invoice);
                     return true;
                 }
                 return false;
            }
        catch (Exception ex) { throw new Exception("DeleteFile Invoice " + ex.Message, ex); }
        }


        #endregion







    }
}
