using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.MANAGEMENT.ORGANIZEDREPOSITORY
{



    /// <summary>
    /// Gestion organisé des documents
    /// </summary>
    public class OrganizedRepository  : NGLib.DATA.DATAPO.DataPO
    {

        public OrganizedRepository()
        {
            this.DefineStructRow();
        }

        public OrganizedRepository(System.Data.DataRow row)
        {
            this.SetRow(row);
        }

        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("organizedrepository", new System.Data.DataColumn("IDRepository", typeof(int)));
        }


        /// <summary>
        /// DataValues (Flux XML) de données diverses
        /// </summary>
        public NGLib.DATA.DATAPO.DataPOFluxXML Flux
        {
            get { if (base._fluxxml == null)_fluxxml = new NGLib.DATA.DATAPO.DataPOFluxXML(this, "Fluxxml"); return _fluxxml; }
        }


        public int IDAccount
        {
            get { return this.GetInt("IDAccount", 0); }
            set { this["IDAccount"] = value; }
        }

        public int IDRepository
        {
            get { return this.GetInt("IDRepository", 0); }
            set { this["IDRepository"] = value; }
        }



        public string LabelRepository
        {
            get { return this.GetString("LabelRepository"); }
            set { this["LabelRepository"] = value; }
        }



        public DateTime DateCreate
        {
            get { return this.GetDateTime("DateCreate", DateTime.MinValue); }
            set { this["DateCreate"] = value; }
        }


        public ENUMS.RepositoryModeEnum RepositoryMode
        {
            get { return (ENUMS.RepositoryModeEnum)Enum.Parse(typeof(ENUMS.RepositoryModeEnum), this.GetString("RepositoryMode")); }
            set { this["RepositoryMode"] = value; }
        }






        public string FormatedDirectoryName
        {
            get { return this.GetString("FormatedDirectoryName"); }
            set { this["FormatedDirectoryName"] = value; }
        }

        public string FormatedFileName
        {
            get { return this.GetString("FormatedFileName"); }
            set { this["FormatedFileName"] = value; }
        }


        public bool CreateDirectoryIfNotExist
        {
            get { return this.Flux.IsTrue("CreateDirectoryIfNotExist"); }
            set { this.Flux.SetObject("CreateDirectoryIfNotExist", value); }
        }




        public List<NGLib.DATA.DATAVALUES.DataValues_data> GetCustomFileFields()
        {
            return Flux.GetDatas("/param/customfields/");
        }


        public void AddCustomField(string name, string descrition, string defaultValue = "")
        {
            NGLib.DATA.DATAVALUES.DataValues_data data = new NGLib.DATA.DATAVALUES.DataValues_data();
            data.name = "/param/customfields/" + name;
            data.value = defaultValue;
            data["Description"] = descrition;
            this.Flux.AddData(data);
        }




    }


}
