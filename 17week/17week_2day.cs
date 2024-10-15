using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharp008
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat src = Cv2.ImRead("C:\\Temp\\a001.png", ImreadModes.Color);
            //경로 에러처리
            if (src.Empty())
            {
                Console.WriteLine("파일 경로가 잘못되었거나, 이미지가 문제 있습니다.");
                return;
            }
            Mat dst = new Mat();
            Cv2.CvtColor(src, dst, ColorConversionCodes.BGR2GRAY);

            Cv2.ImShow("흑백사진", dst);
            Cv2.ImShow("원본사진", src);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
===============================
using System.Drawing;
using System;
using OpenCvSharp;
using System.Runtime.InteropServices;

namespace OpenCV_dotnet6._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[] data =
            {
                1.2, 2.3, 3.2,
                4.5, 5.0, 6.5
            };

            Mat m1 = new Mat(2, 3, MatType.CV_8U, new Scalar(0));
            Mat m2 = new Mat(2, 3, MatType.CV_8U, new Scalar(255));
            Mat m3 = new Mat(2, 3, MatType.CV_16S, new Scalar(300));
            Mat m4 = new Mat(2, 3, MatType.CV_32F);
            Marshal.Copy(data, 0, m4.Data, data.Length); ;

            OpenCvSharp.Size sz = new OpenCvSharp.Size(2, 3);
            Mat m5 = new Mat(sz, MatType.CV_64F, new Scalar(0));

            Console.WriteLine("m1 : \n" + m1.Dump());
            Console.WriteLine("m2 : \n" + m2.Dump());
            Console.WriteLine("m3 : \n" + m3.Dump());
            Console.WriteLine("m4 : \n" + m3.Dump());
            Console.WriteLine("m5 : \n" + m5.Dump());

            m1.Dispose();
            m2.Dispose();
            m3.Dispose();
            m4.Dispose();
            m5.Dispose();
        }
    }
}
=========================
using OpenCvSharp;

namespace Mat02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat m1 = Mat.Ones(3, 5, MatType.CV_8UC1);
            Mat m2 = Mat.Zeros(3, 5, MatType.CV_8UC1);
            Mat m3 = Mat.Eye(3, 3, MatType.CV_8UC1);

            Console.WriteLine("m1 : \n" + m1.Dump());
            Console.WriteLine("m2 : \n" + m2.Dump());
            Console.WriteLine("m3 : \n" + m3.Dump());
        }
    }
}
==========================
using OpenCvSharp;

namespace Mat_Attr
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Mat m1 = new Mat(4, 3, MatType.CV_32FC3, new Scalar(0, 0, 0));

            Console.WriteLine($"m1 : \n{m1.Dump()}");
            Console.WriteLine($"차원 수 : {m1.Dims}");
            Console.WriteLine($"행 개수 : {m1.Rows}");
            Console.WriteLine($"열 개수 : {m1.Cols}");
            Console.WriteLine($"행열 크기 : {m1.Size()}");

            Console.WriteLine($"전체 원소 개수 : {m1.Total()}");
            Console.WriteLine($"한 원소의 크기 : {m1.ElemSize()}");
            Console.WriteLine($"채널당 한 원소의 크기 : {m1.ElemSize1()}");

            Console.WriteLine($"타입 : {m1.Type()}");
            Console.WriteLine($"타입(채널 수 | 길이) : {m1.Channels()} | {m1.Depth()}");
        }
    }
}
