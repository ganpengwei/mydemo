using Nancy;

namespace FormsApp.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", r =>
            {
                return "a";
            });
            Get("", r =>
            {
                return "a";
            });
            Get("/home", r =>
            {
                return "a";
            });
            Get("/home/index", r =>
            {
                return View["Index"];
            });
            //Get["/"] = r =>
            //{
            //    return "";
            //};
        }
    }
}
