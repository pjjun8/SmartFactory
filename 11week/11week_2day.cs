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
