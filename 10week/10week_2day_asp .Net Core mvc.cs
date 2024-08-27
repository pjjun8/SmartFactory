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
==============================================================
//postman 사용
namespace MVCEmptyApp03
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/Home", async (context) =>
                {
                    context.Response.ContentType = "text/plain; charset=utf-8"; //한글문제 해법
                    await context.Response.WriteAsync("홈페이지 입니다. - Get");
                });
                endpoints.MapPost("/Home", async (context) =>
                {
                    await context.Response.WriteAsync("홈페이지 입니다. - Post");
                });
                endpoints.MapDelete("/Home", async (context) =>
                {
                    await context.Response.WriteAsync("홈페이지 입니다. - Delete");
                });
                endpoints.MapPut("/Home", async (context) =>
                {
                    await context.Response.WriteAsync("홈페이지 입니다. - Put");
                });

            });

            //app.MapGet("/Home", () => "Hello World! --- GET");
            //app.MapPost("/Home", () => "Hello World! --- POST");
            //app.MapPut("/Home", () => "Hello World! --- PUT");
            //app.MapDelete("/Home", () => "Hello World! --- DELETE");

            app.Run(async (HttpContext context) =>
            {
                context.Response.ContentType = "text/plain; charset=utf-8";
                await context.Response.WriteAsync("페이지를 찾을 수 없습니다.");
            });

            app.Run();
        }
    }
}
===================================================================
namespace WebApplication8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");

            app.Use(async (context, next) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync("<b>Main</b> 페이지 입니다.");
                await next();
            });


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("<p>ASP .NET Core 공부해 봅시다!!!!!");
            });
            
            app.Run();
        }
    }
}
================================================================
//asp .Net Core mvc 로 별 그리기
namespace WebApplication10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseStaticFiles();   //정적파일, 절대경로 접근가능!!!
            app.UseRouting();   //사용자 라우팅

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=index}/{id?}");
            });

            //app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
------------------------------
using Microsoft.AspNetCore.Mvc;

namespace WebApplication10.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string number1, int number2)
        {
            string star = "<br>";
            for(int i = 0; i < number2; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    star += number1;
                }
                star += "<br>";
            }
            
            
            ViewBag.Result = star;
            return View();
        }
    }
}
--------------------------------
@page
<h2>두 정수를 넣어주세요.</h2>

<form method="post">
    <label for="number1">문자 입력:</label>
    <input type="text" id="number1" name="number1" required />
    <br />
    <label for="number2">문자 층수:</label>
    <input type="number" id="number2" name="number2" required />
    <br />
    <input type="submit" value="Calculate" />
</form>

@if (ViewBag.Result != null)
{
    <h3>Result: @Html.Raw(@ViewBag.Result)</h3>
}
===============================================================
