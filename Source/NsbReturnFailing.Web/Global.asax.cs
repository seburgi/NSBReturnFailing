using System.Web.Mvc;
using System.Web.Routing;
using NServiceBus;
using NServiceBus.Installation.Environments;

namespace NsbReturnFailing.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private IStartableBus _startableBus;

        public static IBus Bus;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            _startableBus = Configure.With()
                                     .DefaultBuilder()
                                     .UseTransport<SqlServer>().PurgeOnStartup(false)
                                     .UnicastBus().RunHandlersUnderIncomingPrincipal(false)
                                     .CreateBus();

            Configure.Instance.ForInstallationOn<Windows>().Install();

            Bus = _startableBus.Start();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_End()
        {
            _startableBus.Dispose();
        }
    }
}