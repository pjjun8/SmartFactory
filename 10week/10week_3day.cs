var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=UseViewBag}/{id?}"
    );

app.Run();
-----------------------------------
using Microsoft.AspNetCore.Mvc;

namespace ViewDataEmpty.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            TempData["person1"] = "홍길동";
            ViewData["person2"] = "이순신";
            return View();
        }

        public IActionResult UseViewBag()
        {
            ViewBag.data1 = "data1";
            ViewBag.data2 = 100;
            ViewBag.data3 = DateTime.Now.ToShortDateString();

            string[] arr = { "사과", "배", "딸기" };
            ViewBag.data4 = arr;

            ViewBag.data5 = new List<string>() { "축구", "야구", "농구" };

            return View();
        }

        public IActionResult UseTempData()
        {
            ViewData["data1"] = "홍길동";
            ViewBag.data2 = "이순신";
            TempData["data3"] = "채호성";
            TempData["data4"] = new List<string>() { "축구", "야구", "농구" };
            TempData.Keep("data3");
            return View();
        }
        public IActionResult About()
        {
            TempData.Keep("data3");
            return View();
        }
    }
}
-----------------------------------------------

@{
    ViewData["Title"] = "Index";
}

<h1>Index</h1>

<h3>@TempData["person1"]</h3>
<h3>@ViewData["person2"]</h3>
-----------------------------------------------

@{
    ViewData["Title"] = "UseViewBag";
}

<h1>UseViewBag</h1>

<p></p>
<spen>첫번째 데이터 : </spen><b>@ViewBag.data1 </b>
<br/>
<spen>두번째 데이터 : </spen><b>@ViewBag.data2 </b>
<br />
<spen>세번째 데이터 : </spen><b>@ViewBag.data3 </b>
<br />
<hr />
@{
    foreach(var item in ViewBag.data4)
    {
        <h3>@item</h3>
    }
}
<hr />
@{
    foreach(var item in ViewBag.data5)
    {
        <h3>@item</h3>
    }
}
---------------------------------------------

@{
    ViewData["Title"] = "UseTempData";
}

<h1>UseTempData</h1>

<h2>@ViewData["data1"]</h2>
<h2>@TempData["data3"]</h2>
---------------------------------------------

@{
    ViewData["Title"] = "About";
}

<h1>About</h1>

<h2>@ViewData["data1"]</h2>
<h2>@TempData["data3"]</h2>
=========================================================================
//디자인 패턴 빌드패턴
namespace BuilderPatternConsole
{
    public class Computer
    {
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string Storage { get; set; }
        public string GraphicsCard { get; set; }

        public override string ToString()
        {
            return $"CPU: {CPU}, RAM: {RAM}, Storage: {Storage}, Graphics Card: {GraphicsCard}";
        }
    }

    public interface IComputerBuilder
    {
        void SetCPU();
        void SetRAM();
        void SetStorage();
        void SetGraphicsCard();
        Computer GetComputer();
    }

    public class GamingComputerBuilder : IComputerBuilder
    {
        private Computer computer = new Computer();

        public void SetCPU()
        {
            computer.CPU = "Intel Core i9";
        }

        public void SetRAM()
        {
            computer.RAM = "32GB";
        }

        public void SetStorage()
        {
            computer.Storage = "1TB SSD";
        }

        public void SetGraphicsCard()
        {
            computer.GraphicsCard = "NVIDIA RTX 3080";
        }

        public Computer GetComputer()
        {
            return computer;
        }
    }

    public class OfficeComputerBuilder : IComputerBuilder
    {
        private Computer computer = new Computer();

        public void SetCPU()
        {
            computer.CPU = "Intel Core i5";
        }

        public void SetRAM()
        {
            computer.RAM = "16GB";
        }
        public void SetStorage()
        {
            computer.Storage = "512GB SSD";
        }
        public void SetGraphicsCard()
        {
            computer.GraphicsCard = "Integrated Graphics";
        }
        public Computer GetComputer()
        {
            return computer;
        }
    }
    public class Director
    {
        public Computer BuildComputer(IComputerBuilder builder)
        {
            builder.SetCPU();
            builder.SetRAM();
            builder.SetStorage();
            builder.SetGraphicsCard();
            return builder.GetComputer();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Director director = new Director();

            IComputerBuilder gamingBuilder = new GamingComputerBuilder();
            Computer gamingComputer = director.BuildComputer(gamingBuilder);
            Console.WriteLine("게이밍 컴퓨터:");
            Console.WriteLine(gamingComputer);

            IComputerBuilder officeBuilder = new OfficeComputerBuilder();
            Computer officeComputer = director.BuildComputer(officeBuilder);
            Console.WriteLine("오피스 컴퓨터:");
            Console.WriteLine(officeComputer);
        }
    }
}

