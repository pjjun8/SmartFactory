using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_exem01
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ADDR_ID { get; set; }
        [MaxLength(100)]
        public string NAME { get; set; }
        [MaxLength(100)]
        public string HP { get; set; }
    }
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Talent { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (local)\\SQLEXPRESS; " +
                        "Database = AndongDb; " +
                        "Trusted_Connection = True;" +
                        "Encrypt=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()   //Primary key 지정
                .HasKey(p => p.ADDR_ID);

            modelBuilder.Entity<Product>()   //Varchar2(30) 30크기를 정할 때
                .Property(p => p.NAME)
                .HasMaxLength(30);

            modelBuilder.Entity<Product>()
                .Property(p => p.HP)
                .HasMaxLength(30);
        }
    }
    
    internal class Program
    {
        static int Menu()
        {
            Console.WriteLine();
            Console.WriteLine("1. 데이터 삽입\r\n2. 데이터 삭제\r\n3. 데이터 조회\r\n4. 데이터 수정");
            Console.Write("메뉴 : "); int menuNum = int.Parse(Console.ReadLine());
            Console.WriteLine();
            return menuNum;
        }

        static void Main(string[] args)
        {
            using (var context = new ProductDbContext())
            {
                /////////////////////////// 데이터베이스와 테이블 생성///////////////////////
                context.Database.EnsureDeleted(); //기존의 테이블이 있을경우 삭제를 단행하는데 DB자체를 지우는 명령어라 타 테이블도 삭제됩니다.
                //조심해서 사용해야할 필요가 있습니다.
                context.Database.EnsureCreated();   //테이블 또는 DB를 만드는 명령어인데 기존에 존재하는 파일이 있다면 아무 작업도 하지 않습니다.
                //Console.WriteLine("데이터베이스 테이블이 생성되었습니다.");
                while (true)
                {
                    int MenuNum = Menu();
                    switch (MenuNum)
                    {
                        case 1:
                            ////////////////////////////////// 데이터 삽입 //////////////////////////////////
                            Console.Write("이름을 입력해 주세요 : ");
                            string name = Console.ReadLine();
                            Console.WriteLine();
                            Console.Write("전화번호를 입력해 주세요 : ");
                            string hp = Console.ReadLine();
                            Console.WriteLine();
                            var product = new Product()
                            {
                                NAME = name,
                                HP = hp
                            };
                            context.Talent.Add(product);
                           
                            //var product = new Product();
                            //product.ADDR_ID = 1;
                            //product.NAME = "Test";
                            //product.HP = "Testzz";

                            context.SaveChanges();
                            Console.WriteLine("데이터 삽입 성공!");
                            break;
                        case 2:
                            /////////////////////////////////삭제///////////////////////////////////////
                            Console.Write("삭제할 ID를 입력해 주세요 : ");
                            int del = int.Parse(Console.ReadLine());

                            var s2 = context.Talent.First(t => t.ADDR_ID == del);
                            context.Talent.Remove(s2);
                            context.SaveChanges();
                            Console.WriteLine("데이터 삭제 성공!");
                            break;
                        case 3:
                            ///////////////////////////// 데이터 조회 //////////////////////////////////
                            var list = context.Talent.ToList();
                            Console.WriteLine();
                            Console.WriteLine("ADDR_ID  NAME         HP         ");
                            foreach (var item in list)
                            {
                                Console.WriteLine($"{item.ADDR_ID}        {item.NAME}       {item.HP}");
                            }
                            Console.WriteLine();
                            break;
                        case 4:
                            ///////////////////////////// 데이터 수정 //////////////////////////////////
                            Console.Write("수정할 ID를 입력해 주세요 : ");
                            int update = int.Parse(Console.ReadLine());
                            var s1 = context.Talent.First(t => t.ADDR_ID == update);

                            Console.Write("이름을 입력해 주세요 : ");
                            string upName = Console.ReadLine();
                            Console.WriteLine();
                            Console.Write("전화번호를 입력해 주세요 : ");
                            string upHp = Console.ReadLine();
                            Console.WriteLine();
                            s1.NAME = upName;
                            s1.HP = upHp;
                            context.SaveChanges();
                            Console.WriteLine("데이터 수정 성공!");
                            break;
                    }
                    //context.Dispose();
                }//end of while
            } //end of using
        }// end of Main
    } // end of Program class
}
===========================================================================================================
//오후에 한거
// 컨트롤러
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAddressBook.Models;

namespace WebAddressBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly PersonDbContext context;

        public HomeController(PersonDbContext _context, ILogger<HomeController> _logger)
        {
            logger = _logger;
            context = _context;
        }

        public IActionResult Index()
        {
            var persons = context.Persons.ToList<Person>();

            return View(persons);

        }

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
        public IActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                context.Persons.Add(person);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(person);
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || context.Persons == null)
            {
                return NotFound();
            }
            var personData = context.Persons.Find(id);

            if (personData == null)
            {
                return NotFound();
            }
            return View(personData);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                //_context.Student.Update(std);
                context.Update(person);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(person);

        }

        public IActionResult Details(int? id)
        {
            if (id == null || context.Persons == null)
            {
                return NotFound();
            }

            var personData = context.Persons.FirstOrDefault(x => x.Id == id);

            if (personData == null)
            {
                return NotFound();
            }

            return View(personData);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || context.Persons == null)
            {
                return NotFound();
            }
            var personData = context.Persons.FirstOrDefault((x => x.Id == id));

            if (personData == null)
            {
                return NotFound();
            }
            return View(personData);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            var personData = context.Persons.Find(id);
            if (personData != null)
            {
                context.Persons.Remove(personData);
            }
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
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
---------------------------------------------------------------------
//모델
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAddressBook.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hp { get; set; }
    }
}
-----------------------------------------------------------------------
//모델
using Microsoft.EntityFrameworkCore;

namespace WebAddressBook.Models
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public PersonDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
------------------------------------------------------------------------
//프로그램.cs
using Microsoft.EntityFrameworkCore;
using WebAddressBook.Models;

namespace WebAddressBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            /////////////////////////////////////////////////////////////////////
            var provider = builder.Services.BuildServiceProvider();
            var config = provider.GetRequiredService<IConfiguration>();
            builder.Services.AddDbContext<PersonDbContext>(item => item.UseSqlServer(config.GetConnectionString("DefaultConnection")));


            /////////////////////////////////////////////////////////////////////

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
---------------------------------------------------------------------------
// 크리에이트 뷰
@model WebAddressBook.Models.Person

@{
    ViewData["Title"] = "Create";
}

<h1>Create</h1>

<h4>Person</h4>
<hr />
<div class="row">
    <div class="col-md-4">
        <form asp-action="Create">
            <div asp-validation-summary="ModelOnly" class="text-danger"></div>
            <div class="form-group">
                <label asp-for="Name" class="control-label"></label>
                <input asp-for="Name" class="form-control" />
                <span asp-validation-for="Name" class="text-danger"></span>
            </div>
            <div class="form-group">
                <label asp-for="Hp" class="control-label"></label>
                <input asp-for="Hp" class="form-control" />
                <span asp-validation-for="Hp" class="text-danger"></span>
            </div>
            <div class="form-group">
                <input type="submit" value="Create" class="btn btn-primary" />
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-action="Index">Back to List</a>
</div>

@section Scripts {
    @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}
}
------------------------------------------------
// 델리트 뷰
@model WebAddressBook.Models.Person

@{
    ViewData["Title"] = "Delete";
}

<h1>Delete</h1>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>Person</h4>
    <hr />
    <dl class="row">
        <dt class = "col-sm-2">
            @Html.DisplayNameFor(model => model.Name)
        </dt>
        <dd class = "col-sm-10">
            @Html.DisplayFor(model => model.Name)
        </dd>
        <dt class = "col-sm-2">
            @Html.DisplayNameFor(model => model.Hp)
        </dt>
        <dd class = "col-sm-10">
            @Html.DisplayFor(model => model.Hp)
        </dd>
    </dl>
    
    <form asp-action="Delete">
        <input type="hidden" asp-for="Id" />
        <input type="submit" value="Delete" class="btn btn-danger" /> |
        <a asp-action="Index">Back to List</a>
    </form>
</div>
--------------------------------------------------
// 디테일 뷰
@model WebAddressBook.Models.Person

@{
    ViewData["Title"] = "Details";
}

<h1>Details</h1>

<div>
    <h4>Person</h4>
    <hr />
    <dl class="row">
        <dt class = "col-sm-2">
            @Html.DisplayNameFor(model => model.Name)
        </dt>
        <dd class = "col-sm-10">
            @Html.DisplayFor(model => model.Name)
        </dd>
        <dt class = "col-sm-2">
            @Html.DisplayNameFor(model => model.Hp)
        </dt>
        <dd class = "col-sm-10">
            @Html.DisplayFor(model => model.Hp)
        </dd>
    </dl>
</div>
<div>
    <a asp-action="Edit" asp-route-id="@Model?.Id">Edit</a> |
    <a asp-action="Index">Back to List</a>
</div>
-----------------------------------------------
// 에디트 뷰
@model WebAddressBook.Models.Person

@{
    ViewData["Title"] = "Edit";
}

<h1>Edit</h1>

<h4>Person</h4>
<hr />
<div class="row">
    <div class="col-md-4">
        <form asp-action="Edit">
            <div asp-validation-summary="ModelOnly" class="text-danger"></div>
            <input type="hidden" asp-for="Id" />
            <div class="form-group">
                <label asp-for="Name" class="control-label"></label>
                <input asp-for="Name" class="form-control" />
                <span asp-validation-for="Name" class="text-danger"></span>
            </div>
            <div class="form-group">
                <label asp-for="Hp" class="control-label"></label>
                <input asp-for="Hp" class="form-control" />
                <span asp-validation-for="Hp" class="text-danger"></span>
            </div>
            <div class="form-group">
                <input type="submit" value="Save" class="btn btn-primary" />
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-action="Index">Back to List</a>
</div>

@section Scripts {
    @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}
}
-------------------------------------------------------
//인덱스 뷰
@model IEnumerable<WebAddressBook.Models.Person>

@{
    ViewData["Title"] = "Index";
}

<h1>Index</h1>

<p>
    <a asp-action="Create">Create New</a>
</p>
<table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.Name)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Hp)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
@foreach (var item in Model) {
        <tr>
            <td>
                @Html.DisplayFor(modelItem => item.Id)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Name)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Hp)
            </td>
            <td>
                <a asp-action="Edit" asp-route-id="@item.Id">Edit</a> |
                <a asp-action="Details" asp-route-id="@item.Id">Details</a> |
                <a asp-action="Delete" asp-route-id="@item.Id">Delete</a>
            </td>
        </tr>
}
    </tbody>
</table>
-------------------------------------------------------
