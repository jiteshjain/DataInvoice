using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.Core.COMPONENTS.WEB.MVC.FORM
{
    public class PageAlertMessageForm
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int Level { get; set; }
    }

    public class breadcrumbForm
    {
        public string Label { get; set; }
        public string Url { get; set; }
    }
}
