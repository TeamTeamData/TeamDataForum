namespace TeamDataForum.Web
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public partial class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}