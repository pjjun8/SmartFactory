namespace BitConverterTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] boolBytes = BitConverter.GetBytes(true);
            byte[] shortBytes = BitConverter.GetBytes((short)32000); //직렬화 byte 배열로 변환
            byte[] intBytes = BitConverter.GetBytes(1652300);

            bool boolResult = BitConverter.ToBoolean(boolBytes, 0); //역직렬화
            short shortResult = BitConverter.ToInt16(shortBytes, 0);
            int intResult = BitConverter.ToInt32(intBytes, 0);
            Console.WriteLine(boolResult + " " + " " + shortResult+ " " + " " +intResult);
        }
    }
}
====================================================================
using System.Text;

namespace BitConverterTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //byte[] boolBytes = BitConverter.GetBytes(true);
            //byte[] shortBytes = BitConverter.GetBytes((short)32000); //직렬화 byte 배열로 변환
            //byte[] intBytes = BitConverter.GetBytes(1652300);

            //bool boolResult = BitConverter.ToBoolean(boolBytes, 0); //역직렬화
            //short shortResult = BitConverter.ToInt16(shortBytes, 0);
            //int intResult = BitConverter.ToInt32(intBytes, 0);
            //Console.WriteLine(boolResult + " " + " " + shortResult+ " " + " " +intResult);
            StreamReader streamReader = new StreamReader(@"C:\Temp\abc.txt", Encoding.UTF8);
            string txt = streamReader.ReadToEnd();
            //Console.WriteLine(s);
            MemoryStream memoryStream = new MemoryStream();
            byte[] strBytes = Encoding.UTF8.GetBytes(txt);  //문자열 직렬화 - byte 배열로 만들었다!!!
            memoryStream.Write(strBytes, 0, strBytes.Length);
            memoryStream.Position = 0;
            
            StreamReader sr = new StreamReader(memoryStream, Encoding.UTF8, true);
            txt = sr.ReadToEnd();
            Console.WriteLine(txt);


        }
    }
}
====================================================================
