﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DataInvoice.Manager.Startup))]
namespace DataInvoice.Manager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
