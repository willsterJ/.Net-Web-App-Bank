using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BankProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            
            routes.MapRoute(
                name: "Transaction List",
                url: "Bank/Account/{accountid}/Transaction/{action}/{id}",
                defaults: new { controller = "Transactions", action = "Index", id = UrlParameter.Optional }
            );
            
            
            routes.MapRoute(
                name: "Bank Account",
                url: "Bank/Account/{action}/{id}",
                defaults: new { controller = "BankAccounts", action = "List", id = UrlParameter.Optional }
            );
           

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
