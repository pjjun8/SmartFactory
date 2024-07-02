namespace DoWhile
{
    internal class Program
    {
        static void Call()
        {
            Console.WriteLine("[메뉴선택]");
            Console.WriteLine("1. 데이터 베이스 입력");
            Console.WriteLine("2. 데이터 베이스 검색");
            Console.WriteLine("3. 데이터 베이스 수정");
            Console.WriteLine("4. 데이터 베이스 삭제");
            Console.WriteLine("5. 프로그램 종료");
            Console.Write("선택 : ");
        }
        static void Menu()
        {
            int i = 0;
            do
            {
                Call();
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        Console.WriteLine("데이터베이스 입력을 선택하셨습니다.");
                        break;
                    case 2:
                        Console.WriteLine("데이터베이스 검색을 선택하셨습니다.");
                        break;
                    case 3:
                        Console.WriteLine("데이터베이스 수정을 선택하셨습니다.");
                        break;
                    case 4:
                        Console.WriteLine("데이터베이스 삭제을 선택하셨습니다.");
                        break;
                    case 5:
                        Console.WriteLine("프로그램을 종료합니다.");
                        i++;
                        break;
                    default:
                        Console.WriteLine("오류! 다시입력하세요");
                        break;
                }
            } while (i < 1);
        }
        static void Main(string[] args)
        {
            Menu();
        }
    }
}


---------------------------------------------------------------------------------

