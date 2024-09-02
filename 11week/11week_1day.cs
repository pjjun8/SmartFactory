using Microsoft.EntityFrameworkCore;

namespace ConsoleApp30
{
    public class Person
    {
        
        public int ID { get; set; }
        public string NAME { get; set; }
        public int AGE { get; set; }
        public string JOB { get; set; }
    }
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (local)\\SQLEXPRESS; " +
                        "Database = MyDb; " +
                        "Trusted_Connection = True;" +
                        "Encrypt=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()   //Primary key 지정
                .HasKey(p => p.ID);

            modelBuilder.Entity<Person>()   //Varchar2(30) 30크기를 정할 때
                .Property(p => p.NAME)
                .HasMaxLength(30);

            modelBuilder.Entity<Person>()
                .Property(p => p.JOB)
                .HasMaxLength(30);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // [Entity Framework] + Linq
            //1. 모듈 받기, 4개정도...
            //2. MOdel 만들기 --> Stuent, person ... 객체
            //3. DBContext 물리적인 모델 맵핑
            //4. Add-Migration [prjName] & update-database

            using (var context = new PersonDbContext())
            {
                // 데이터베이스와 테이블 생성
                context.Database.EnsureDeleted(); //기존의 테이블이 있을경우 삭제를 단행하는데 DB자체를 지우는 명령어라 타 테이블도 삭제됩니다.
                //조심해서 사용해야할 필요가 있습니다.
                context.Database.EnsureCreated();   //테이블 또는 DB를 만드는 명령어인데 기존에 존재하는 파일이 있다면 아무 작업도 하지 않습니다.
                Console.WriteLine("데이터베이스 테이블이 생성되었습니다.");
            }
        }
    }
}
================================================================================
// 그냥 콘솔 앱 Add-Migration InitCRUD / update-database
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EF8_All
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string HP { get; set; }
    }

    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (local)\\SQLEXPRESS; " +
                        "Database = MyDb; " +
                        "Trusted_Connection = True;" +
                        "Encrypt=False");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new StudentDbContext();

            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //데이터 삽입Create, Insert)
            var st = new Student();
            st.Name = "유재석";
            st.HP = "010-1111-1111";

            context.Students.Add(st);
            context.SaveChanges();
            Console.WriteLine("드감");

            st = new Student();
            st.Name = "강호동";
            st.HP = "010-2222-2222";

            context.Students.Add(st);
            context.SaveChanges();
            Console.WriteLine("드감");

            context.Dispose();
        }
    }
}
=========================================
    using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EF8_All
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string HP { get; set; }
    }

    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (local)\\SQLEXPRESS; " +
                        "Database = MyDb; " +
                        "Trusted_Connection = True;" +
                        "Encrypt=False");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new StudentDbContext();

            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //데이터 삽입Create, Insert)
            var st = new Student();
            st.Name = "유재석";
            st.HP = "010-1111-1111";

            context.Students.Add(st);
            context.SaveChanges();
            Console.WriteLine("드감");

            st = new Student();
            st.Name = "강호동";
            st.HP = "010-2222-2222";

            context.Students.Add(st);
            context.SaveChanges();
            Console.WriteLine("드감");

            //조회(select)
            var list = context.Students.ToList();
            foreach(var student in list)
            {
                Console.WriteLine(student.Name);
            }
            //수정(update)
            var s1 = context.Students.First();
            s1.Name = "박상원";
            context.Dispose();
        }
    }
}
====================================================================
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication13.Models;

namespace WebApplication13.Controllers
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
        [HttpPost]
        public IActionResult Index(Student std)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }

            return View();
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
---------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace WebApplication13.Models
{
    public class Student
    {
        [Display(Name = "이름을 입력하세요:")]
        //[Required]
        [Required(ErrorMessage = "이름란에 이름이 빠졌습니다.")]
        //[MaxLength(15)]
        [StringLength(15, MinimumLength = 2,
            ErrorMessage = "이름은 두자 이상 작성이 가능합니다.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email을 적어주세요..")]
        //[EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", ErrorMessage = "이메일 패턴이 맞지 않습니다.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "나이를 적어주세요..")]
        [Range(20, 70, ErrorMessage = "20-70세까지 작성할 수 있습니다.")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "패스워드를 적어주세요.")]
        [RegularExpression(@"(?=.*[a-zA-Z])(?=.*[0-9]).{8,25}$", ErrorMessage = "영문자로 대소문자와 숫자조합으로 8자리이상")]
        public string Password { get; set; }

    }
}
-----------------------------------------------------------
@model Student

@{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    <h1 class="display-4">Welcome</h1>
</div>
<div>
    <form asp-controller="Home" asp-action="Index" method="post">
        <!----------------------이름------------------------------------------>
        <label asp-for="Name">이름 : </label>
        <input asp-for="Name" class="form-control" placeholder="이름을 넣어 주세요"/>
        <span asp-validation-for="Name" style="color:red"></span>
        <!----------------------나이------------------------------------------>
        <label asp-for="Age">나이 : </label>
        <input asp-for="Age" class="form-control" placeholder="나이를 넣어 주세요" />
        <span asp-validation-for="Age" style="color:red"></span>
        <!----------------------패스워드------------------------------------------>
        <label asp-for="Password">비밀번호 : </label>
        <input asp-for="Password" class="form-control" placeholder="패스워드를 작성해주세요." />
        <span asp-validation-for="Password" style="color:red"></span>
        <!----------------------버튼------------------------------------------>
        <input type="submit"value="OK" class="btn btn-outline-primary"
    </form>
</div>
