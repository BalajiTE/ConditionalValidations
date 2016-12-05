using ConditionalValidations.ValidationHelpers;
using System.Web.Http;

namespace ConditionalValidations
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.Filters.Add(new ValidationFilterAtttibute());
        }
    }
}
