박싱, 언박싱, 업캐스팅, 다운캐스팅
github.com
int number = 42;
Int32 boxed = number; //Boxing
int unboxed = boxed; //UnBoxing

object obj = number; //UpCasting, Boxing
int downed = (int)obj; //강제형변환, DownCasting
-------------------------------------------------------------
namespace DeepCopy
{
    class Myclass
    {
        public int MyField1;
        public int MyField2;
        //
        public void DeepCopy()
        {
            Myclass myclass = new Myclass();
            myclass.MyField1 = this.MyField1;
            myclass.MyField2 = this.MyField2;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("얉은 복사(Shallow Copy)");
            {
                Myclass source = new Myclass();

                source.MyField1 = 10;
                source.MyField2 = 20;

                Myclass target = source;    //얇은 복사??
                target.MyField2 = 30;
                Console.WriteLine(source.MyField2);
                Console.WriteLine(target.MyField2);
            }
            Console.WriteLine("깊은 복사(Deep Copy)");
            {
                Myclass source = new Myclass();

                source.MyField1 = 10;
                source.MyField2 = 20;

                Myclass target = source.DeepCopy();
                target.MyField2 = 30;

                Console.WriteLine(source.MyField2);
                Console.WriteLine(target.MyField2);
            }
        }//end of Main
    }//end of Program
}//end of DeepCopy
--------------------------------------------------------------
