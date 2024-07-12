using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OOPDelegateApp
{
    class Report
    {
        public string Police(){return "경찰서 신고";}
        public string fire() { return "소방서 신고"; }
        public string revenue() { return "국세청 신고"; }
    }
    internal class Program
    {
        public delegate string Call();
        static void Main(string[] args)
        {
            Report report = new Report();

            Call call= report.Police;
            Console.WriteLine(call());
            call = report.fire;
            Console.WriteLine(call());
            call = report.revenue;
            Console.WriteLine(call());
            
        }
    }
}
---------------------------------------------------------------------------
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OOPDelegateApp
{
    class Report
    {
        public void Police(){ Console.WriteLine("경찰서 신고"); }
        public void fire() { Console.WriteLine("소방서 신고"); }
        public void revenue() { Console.WriteLine("국세청 신고"); }
    }
    internal class Program
    {
        public delegate void Call();
        static void Main(string[] args)
        {
            Report report = new Report();
            report.Police();
            report.fire();
            report.revenue();
            Call call = report.Police;
            call += report.fire;
            call += report.revenue;

            call();
            call -= report.Police;
            call();

                        
        }
    }
}
-------------------------------------------------------------------------
namespace ListTest01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> numberList = new List<int>();
            int num = 10;
            for (int i = 0; i < 5; i++)
            {
                numberList.Add(num);
                num += 10;
            }

            Console.WriteLine($"리스트 요소의 수 : {numberList.Count}");
            Console.WriteLine($"리스트가 가질 수 있는 최대 자료의 수 : {numberList.Capacity}");
            numberList.RemoveAt(3); //index 제거, 전체 크기가 하나 줄어듦
            numberList.Remove(20);  //값으로 제거, 전체크기는 줄지 않음, 중복되면 앞쪽값 1개 제거
            numberList.Insert(0, 5);    // 0 번 자리에 5 추가
            numberList.Sort();  //오름차순 정렬
            numberList.Reverse();   // 값을 역순으로..
            Console.WriteLine($"리스트 요소의 수 : {numberList.Count}");
            Console.WriteLine($"리스트가 가질 수 있는 최대 자료의 수 : {numberList.Capacity}");



            foreach (int i in numberList)
            {
                Console.WriteLine(i);
            }
        }
    }
}
--------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace AddressTest01
{
    class Information
    {
        public int Id {  get; set; }
        public string Name {  get; set; }
        public string Hp {  get; set; }
        public void PrintMenu()
        {
            Console.WriteLine($"\n1. 데이터 삽입");
            Console.WriteLine("2. 데이터 삭제");
            Console.WriteLine("3. 데이터 조회");
            Console.WriteLine("4. 데이터 수정");
            Console.WriteLine("5. 프로그램 종료");
            Console.Write($"\n메뉴 : ");
        }
    }
    class Test
    {
        public void Funtion(Information inf, List<Information> list)
        {
            while (true)
            {
                int set;
                int num = int.Parse(Console.ReadLine());
                switch (num)
                {
                    case 1:
                        inf = new Information();
                        Console.Write($"\nID를 입력해 주세요 : ");
                        inf.Id = int.Parse(Console.ReadLine());
                        Console.Write($"\n이름을 입력해 주세요 : ");
                        inf.Name = Console.ReadLine();
                        Console.WriteLine($"\n전화번호를 입력해 주세요 : ");
                        inf.Hp = Console.ReadLine();
                        list.Add(inf);
                        Console.WriteLine($"\n데이터가 정상적으로 입력되었습니다.");
                        break;
                    case 2:
                        Console.Write($"\n삭제할 리스트 번호입력 (0부터 시작) : ");
                        list.RemoveAt(int.Parse(Console.ReadLine()));
                        Console.WriteLine($"\n삭제 완료.");
                        break;
                    case 3:
                        Console.WriteLine($"\n저장된 데이터 리스트.");
                        foreach (Information i in list)
                        {
                            Console.WriteLine(i.Id);
                            Console.WriteLine(i.Name);
                            Console.WriteLine(i.Hp);
                        }
                        break;
                    case 4:
                        Console.Write($"\n수정할 리스트 번호입력 (0부터 시작) : ");
                        list.RemoveAt(set = int.Parse(Console.ReadLine()));
                        inf = new Information();
                        Console.Write($"\n수정할 ID를 입력해 주세요 : ");
                        inf.Id = int.Parse(Console.ReadLine());
                        Console.Write($"\n수정할 이름을 입력해 주세요 : ");
                        inf.Name = Console.ReadLine();
                        Console.WriteLine($"\n수정할전화번호를 입력해 주세요 : ");
                        inf.Hp = Console.ReadLine();
                        list.Insert(set, inf);
                        break;
                    case 5:
                        Console.WriteLine($"프로그램 종료.");
                        break;
                    default:
                        break;
                }
                if (num == 5)
                    break;
                inf.PrintMenu();
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Test test = new Test();
            Information inf = new Information();
            List<Information> list = new List<Information>();
            inf.PrintMenu();
            test.Funtion(inf, list);


        }
    }
}

