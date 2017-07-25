﻿using System.Web;
using System.Web.Mvc;

namespace eWallet
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AppHarbor.Web.RequireHttpsAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
