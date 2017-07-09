using DataInvoice.GLOBAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.Api.Controllers
{
    public class BaseApiController : Controller
    {
        public NGLib.DATA.CONNECTOR.IDataConnector Connector
        {
            get { if (_Connector == null) _Connector = new DataInvoice.GLOBAL.DataInvoiceEnv(); return _Connector.Connector; }
        }
        public DataInvoice.GLOBAL.DataInvoiceEnv _Connector = null;

    }
}