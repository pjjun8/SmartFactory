//퀴즈 1
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using WebQuiz.Models;



namespace WebQuiz.Controllers

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

            return View();

        }



        public IActionResult Privacy()

        {

            return View();

        }

        public IActionResult Day()

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

-----------------------------------------------------
@{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    <h1 class="display-4">Welcome</h1>
</div>

<div class="text-center">
    <button type="button" class="btn btn-primary"><a class="nav-link text-light" asp-area="" asp-controller="Home" asp-action="Day">Day</a></button>
    
    <form action="/" method="post"></form>
</div>
-------------------------------------------------------

@{
    ViewData["Title"] = "Day";
}

<h1>Day</h1>

@{

    <h3>오늘은 @DateTime.Now.ToString("yyyy년MM월dd")일 입니다</h3>
}
=================================================================================
//퀴즈 2
<!DOCTYPE html>

<html lang="en">

<head>

    <meta charset="utf-8" />

    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>@ViewData["Title"] - WebQuiz_2</title>

    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />

    <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />

    <link rel="stylesheet" href="~/WebQuiz_2.styles.css" asp-append-version="true" />

    <style>

        nav{

            background-color: #0fF;

        }

    </style>

</head>

<body>

    <header>

        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light border-bottom box-shadow mb-3">

            <div class="container-fluid">

                <a class="navbar-brand" asp-area="" asp-controller="Home" asp-action="Index">WebQuiz_2</a>

                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"

                        aria-expanded="false" aria-label="Toggle navigation">

                    <span class="navbar-toggler-icon"></span>

                </button>

                <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">

                    <ul class="navbar-nav flex-grow-1">

                        <li class="nav-item">

                            <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>

                        </li>

                        <li class="nav-item">

                            <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>

                        </li>

                    </ul>

                </div>

            </div>

        </nav>

    </header>

    <div class="container">

        <main role="main" class="pb-3">

            @RenderBody()

        </main>

    </div>



    <footer class="border-top footer text-muted">

        <div class="container">

            pjjun8@naver.com

        </div>

    </footer>

    <script src="~/lib/jquery/dist/jquery.min.js"></script>

    <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>

    <script src="~/js/site.js" asp-append-version="true"></script>

    @await RenderSectionAsync("Scripts", required: false)

</body>

</html>
==================================================================
퀴즈3
@{

    ViewData["Title"] = "Home Page";

}



<div>

    <h2>두 정수를 넣어주세요</h2>

    <form method="post">

        <label for="number1">숫자 1:</label>

        <input type="number" id="number1" name="number1" required />

        <br />

        <label for="number2">숫자 2:</label>

        <input type="number" id="number2" name="number2" required />

        <br />

        <input type="submit" value="사칙연산" />

    </form>

    @if (ViewBag.Sum != null)

    {

        <h3>sum: @ViewBag.Sum </h3><br />

        <h3>Minus: @ViewBag.Minus</h3><br />

        <h3>Mul: @ViewBag.Mul</h3><br />

        <h3>Divide: @ViewBag.Divide</h3>

    }
</div>
=========================================================
퀴즈4
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using WebQuiz04.Models;



namespace WebQuiz04.Controllers

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

            return View();

        }



        public IActionResult Privacy()

        {

            return View();

        }



        public IActionResult Input(int height, int weight, string gender)

        {

            double height1 = (double)height / 100;

            if(gender == "남")

            {

               double avg = height1*height1*22;

                ViewBag.gender = ((double)weight / avg) * 100;

            }

            if (gender == "여")

            {

                double avg = height1 * height1 * 21;

                ViewBag.gender = ((double)weight / avg) * 100;

            }

            return View();

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()

        {

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        }

    }

}

-----------------------------------

@{
    ViewData["Title"] = "Input";
}

<h1>Input</h1>

<form action="Home/Input" method="post">
    <label for="number">키: </label>
    <input type="number" id="height" name="height" required /><br />

    <label for="number2">몸무게: </label>
    <input type="number" id="weight" name="weight" required /><br />

    <label for="number3">성별(남/여): </label>
    <input type="text" id="gender" name="gender" required /><p></p>

    <button type="submit">BMI 계산</button>
    
   
     @if(ViewBag.gender != null)
    {
        
        <h3>@ViewBag.gender %</h3>
        <h2>정상 체중 여부를 알고 싶다면 결제하세요.</h2>
    } 
   
    @* <int type="button" value="사칙연산"></int> *@
</form>
-----------------------------------------------------
namespace WebQuiz04
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
                pattern: "{controller=Home}/{action=Input}/{id?}");

            app.Run();
        }
    }
}
