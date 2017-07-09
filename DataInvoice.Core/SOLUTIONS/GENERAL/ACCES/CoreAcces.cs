using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.Core.SOLUTIONS.GENERAL.ACCES
{
    public static class CoreAcces
    {



        public static NGLib.DATA.CONNECTOR.IDataConnector GetConnector()
        {
            string engine = "MYSQL";
            string connectstring = "";
            NGLib.DATA.CONNECTOR.ConnectorFactory.ImportConnectorType(typeof(NGLib.DATA.CONNECTOR.MYSQLConnector), "MYSQL");
            NGLib.DATA.CONNECTOR.IDataConnector retour = NGLib.DATA.CONNECTOR.ConnectorFactory.GetConnector(engine, connectstring);
            return retour;
        }

        /// <summary>
        /// Permet de générer le connecteur MYSQL par defaut sur le serveur de test
        /// </summary>
        /// <returns></returns>
        public static NGLib.DATA.CONNECTOR.IDataConnector GetTestConnector()
        {
            NGLib.DATA.CONNECTOR.IDataConnector connector = null;
            connector = new NGLib.DATA.CONNECTOR.MYSQLConnector();
            connector.SetConnectionString("SERVER=servtest.nuegy.info; Database=datainvoice; UID=root; Password=everYs92;");
            return connector;
        }







    }
}
