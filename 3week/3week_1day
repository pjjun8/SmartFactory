namespace HashSetSortTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashSet<int> data = new HashSet<int>() { 5, 1, 3, 4, 2 };
            List<int> list = new List<int>(data);
            list.Sort();

            foreach (int i in list)
            {
                Console.Write(i + " ");
            }
        }
    }
}
-----------------------------------------------------------------
using System.Collections.Generic;

namespace Test00
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            HashSet<int> hash = new HashSet<int>();
            int bonus;

            while (hash.Count < 6)
            {
                hash.Add(random.Next(1, 47));
            }
            /* List<int> list = new List<int>(hash);
             bonus = list[0];
             list.RemoveAt(0);

             foreach (int i in list)
             {
                 Console.Write(i + " ");
             }
             Console.WriteLine($"\n{bonus}");*/
            do
            {
                bonus = random.Next(1, 47);
            } while (hash.Contains(bonus));

            foreach (int i in hash)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine($"\n{bonus}");
        }
    }
}
------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp02
{
    class Calculator
    {
        private double radius;
        private double Pi = Math.PI;
        private int width, height;
        public double ComputerTriangleArea(int width, int height)
        {
            this.width = width;
            this.height = height;
            return (this.width* this.height) / 2.0;
        }
        public double ComputerCirecleArea(int radius)
        {
            this.radius = radius;
            return this.radius * this.radius * Pi;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator cal = new Calculator();
            string a = Console.ReadLine();
            string[] b = a.Split(' ');
            int value1 = int.Parse(b[0]);
            int value2 = int.Parse(b[1]);
            int radius = int.Parse(b[2]);

            Console.WriteLine(cal.ComputerTriangleArea(value1, value2));
            Console.WriteLine($"{cal.ComputerCirecleArea(radius):F2}");
        }
    }
}
----------------------------------------------------------------------------------
