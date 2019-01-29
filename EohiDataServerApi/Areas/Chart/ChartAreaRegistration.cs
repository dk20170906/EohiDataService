using System.Web.Mvc;

namespace EohiDataServerApi.Areas.Chart
{
    public class ChartAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Chart";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Chart_default",
                "Chart/{controller}/{action}/{id}",
               
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
