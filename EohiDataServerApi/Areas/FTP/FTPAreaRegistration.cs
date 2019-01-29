using System.Web.Mvc;

namespace EohiDataServerApi.Areas.FTP
{
    public class FTPAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FTP";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FTP_default",
                "FTP/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
