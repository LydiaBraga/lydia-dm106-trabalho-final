using System.Web;
using System.Web.Mvc;

namespace Trabalho_Final_Lydia_Dm106
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
