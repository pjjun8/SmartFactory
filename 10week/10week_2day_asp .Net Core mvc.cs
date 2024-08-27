namespace WebApplication6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");
            app.MapGet("/greet", () => "안녕하세요");

            app.Run();
        }
    }
}
=============================================================
namespace WebApplication7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=index}/{id?}"
                );
            app.Run();
        }
    }
}
==============================================================
namespace WebApplication7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=About}/{id?}"
            //    );
            //app.Run();
            app.MapControllers();    //Map으로 감추기
            app.Run();
        }
    }
}
=============================================================
using Microsoft.AspNetCore.Mvc;

namespace WebApplication7.Controllers
{
    [Route("[controller]")] //controller 지정해주기
    public class HomeController : Controller
    {
        [Route("[Action]")] //같은 이름으로 지정 --> index 경로
        //[Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        //[Route("/About")]
        [Route("[Action]")] //같은 이름으로 지정 --> About 경로
        public IActionResult About()
        {
            return View();
        }

        [Route("/Help/{id?}")]  //--> Help/5 경로 하면 5리턴
        public int Help(int? id)    //아무 숫자도 안넣으면 100 리턴
        {
            return id ?? 100;   //아무 숫자도 안넣으면 100 리턴
        }
    }
}

