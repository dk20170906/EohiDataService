using System.Web.Mvc;

namespace EohiDataServerApi.Areas.WF
{
    public class WFAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "WF";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "WF_default",
                "WF/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
