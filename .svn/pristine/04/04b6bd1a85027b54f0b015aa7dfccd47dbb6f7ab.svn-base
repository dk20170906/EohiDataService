using System.Web.Mvc;

namespace EohiDataServerApi.Areas.ThreeD
{
    public class ThreeDAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ThreeD";
            }
        }

        public override void RegisterArea (AreaRegistrationContext context)
        {
            context.MapRoute(
                "ThreeD_default",
                "ThreeD/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
