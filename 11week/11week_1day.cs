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
