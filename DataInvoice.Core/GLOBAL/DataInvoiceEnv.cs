using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.GLOBAL
{
    public class DataInvoiceEnv : NGLib.COMPONENTS.APP.ENV.BaseGlobalEnvironnement, NGLib.COMPONENTS.APP.ENV.IGlobalEnv
    {

        public string SqlEngine = "MYSQL";
        public string SqlString = "SERVER=dbmaria1.nuegy.info; Database=DATAINVOICE_dev;Port=21396; UID=nuegydevuser; Password=everYs92;";


        // !!! récupération du sqlstring depuis le fichier configuration




        public DataInvoiceEnv()
        {

            this._ConnectorSgbd = new NGLib.DATA.CONNECTOR.MYSQLConnector();
            this._ConnectorSgbd.SetConnectionString(SqlString); // rendre dynamique depuis defaultconnexion

            this.SetString("/param/redis", "22ec9a54-2921-43af-a4e9-9b1e7aff2b9b.pdb.ovh.net");
            this.SetAttribute("/param/redis", "port", "21784");
            this.SetAttribute("/param/redis", "password", "redng75Oj82p");
        }

        public NGLib.DATA.CONNECTOR.IDataConnector Connector
        { get { return this._ConnectorSgbd; } }




    }
}
