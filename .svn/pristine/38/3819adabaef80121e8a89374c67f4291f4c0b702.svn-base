using System.Web.Mvc;

namespace EohiDataServerApi.Areas.DB
{
    public class DBAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "DB";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "DB_default",
                "DB/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
