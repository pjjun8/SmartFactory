namespace ModelDataMVC02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=My}/{action=Output}/{id?}");

            app.Run();
        }
    }
}
-------------------------------------
using Microsoft.AspNetCore.Mvc;

namespace ModelDataMVC02.Controllers
{
    public class MyController : Controller
    {
        public IActionResult Output()
        {
            string[] food = { "된장국", "김치찌게", "소금빵", "두루치기" };
            ViewBag.Book = new List<string>() { "C#프로그래밍", "Java 정복", "HTML5", "CSS 하루만에하기" };
            TempData["foods"] = food;

            return View();
        }
    }
}
--------------------------------------

@{
    ViewData["Title"] = "Output";
}

<h1>Output</h1>

@{
    ViewData["Title"] = "Output";
    var book = ViewBag.Book as List<string>;
    string[] food = TempData["foods"] as string[];
}

<h1>Output</h1>
<hr />


@{
    foreach (var item in book)
    {
        @item
        <br />
    }
}

 @{
    foreach (var item in food)
    {
        @item
        <br />
    }
}
