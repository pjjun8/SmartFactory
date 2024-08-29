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
            ViewData["st1"] = "zzz";
            ViewData["st2"] = new List<string>() { "zzzz", "ssss", "ddddd", "qqqq" };

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
    var st1 = ViewData["st1"];
    var st2 = ViewData["st2"] as List<string>;
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
@st1
<br />
@{
    foreach (var item in st2)
    {
        @item
        <br />
    }
}
================================================================================
namespace StrongTypeView    // 프로그램 cs
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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
--------------------------------------------
namespace StrongTypeView.Models //모델
{
    public class Employee
    {
        public int EmpId {  get; set; }
        public string EmpName { get; set; }
        public string Designation { get; set; }
        public int Salary { get; set; }

    }
}
-------------------------------------------
using Microsoft.AspNetCore.Mvc;
using StrongTypeView.Models;
using System.Diagnostics;

namespace StrongTypeView.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Employee obj = new Employee()
            //{
            //    EmpId = 100,
            //    EmpName = "홍길동",
            //    Designation = "대리",
            //    Salary = 4000000
            //};

            var emps = new List<Employee>()
            {
                new Employee {EmpId = 1, EmpName = "홍길동", Designation = "대리", Salary = 4000000},
                new Employee {EmpId = 2, EmpName = "이순신", Designation = "부장", Salary = 5000000},
                new Employee {EmpId = 3, EmpName = "강감찬", Designation = "상무", Salary = 6000000}
            };

            TempData["emps"] = emps;

            //return View();
            return View(emps);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

---------------------------------------
@*model StrongTypeView.Models.Employee*@
@* @model List<StrongTypeView.Models.Employee> *@
@model IEnumerable<StrongTypeView.Models.Employee> // 위의@model List<StrongTypeView.Models.Employee> 와 같음
@{
    ViewData["Title"] = "Home Page";
    var emps = TempData["emps"] as List<Employee>;
}

<div class="text-center">
    @*<h1 class="display-4">Welcome</h1>
    <hr />
    <h3>@Model.EmpId</h3>
    <h3>@Model.EmpName</h3>
    <h3>@Model.Designation</h3>
    <h3>@Model.Salary</h3>
    *@
    <hr/>
    @{
        foreach(var item in Model){
            @item.EmpId 
            @item.EmpName
            @item.Designation
            @item.Salary<br/>
        }
    }
        <hr />
    @{
        foreach (var item in emps)
        {
            @item.EmpId
            @item.EmpName
            @item.Designation
            @item.Salary <br />
        }
    }
    
</div>

