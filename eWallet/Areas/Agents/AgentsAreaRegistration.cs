using System.Web.Mvc;

namespace eWallet.Areas.Agents
{
    public class AgentsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Agents";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Agents_default",
                "Agents/{controller}/{action}/{id}",
                new { action = "Index", controller="Dashboard", id = UrlParameter.Optional }
            );
        }
    }
}