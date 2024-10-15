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
==========================
using OpenCvSharp;

namespace OpenCvSharp01
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Scalar blue = new Scalar(255, 0, 0);
            Scalar red = new Scalar(0, 0, 255);
            Scalar green = new Scalar(0, 255, 0);
            Scalar white = new Scalar(255, 255, 255);
            Scalar yellow = new Scalar(0, 255, 255);

            Mat image = new Mat(400, 600, MatType.CV_8UC3, white);
            Point pt1 = new Point(50, 130);
            Point pt2 = new Point(200, 300);
            Point pt3 = new Point(300, 150);
            Point pt4 = new Point(400, 50);
            Rect rect = new Rect(pt3, new Size(200, 150));

            //라인
            Cv2.Line(image, pt1, pt2, red, 1, LineTypes.AntiAlias);
            Cv2.Line(image, pt3, pt4, green, 2, LineTypes.AntiAlias);
            Cv2.Line(image, pt3, pt4, green, 3, LineTypes.Link8, 1);

            //사각형 그리기
            Cv2.Rectangle(image, rect, blue, 2, LineTypes.AntiAlias);
            Cv2.Rectangle(image, rect, blue, -1, LineTypes.AntiAlias, 1);
            Cv2.Rectangle(image, pt1, pt2, red, 3);

            //출력
            Cv2.ImShow("image", image);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();

        }
    }
}
===================================
using OpenCvSharp;

namespace OpenCvSharp01
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Scalar blue = new Scalar(255, 0, 0);
            Scalar red = new Scalar(0, 0, 255);
            Scalar green = new Scalar(0, 255, 0);
            Scalar white = new Scalar(255, 255, 255);
            Scalar yellow = new Scalar(0, 255, 255);
            Scalar black = new Scalar(0, 0, 0);

            Mat image = new Mat(600, 1000, MatType.CV_8UC3, white);
            //Point pt1 = new Point(50, 100);
            //Point pt2 = new Point(150, 100);
            //Point pt3 = new Point(50, 150);
            //Point pt4 = new Point(150, 150);

            Point pt5 = new Point(400, 300); // 왼쪽 위 
            Point pt6 = new Point(600, 500); // 오른쪽 아래
            Point pt7 = new Point(600, 300); // 오른쪽 위
            Point pt8 = new Point(400, 500); // 왼쪽 아래
            Point pt9 = new Point(500, 200); // 삼각형 위
            Point pt10 = new Point(500, 400); //원 중심좌표
            Rect rect = new Rect(pt5, new Size(200, 200));

            //라인
            Cv2.Line(image, pt5, pt6, blue, 2, LineTypes.AntiAlias);
            Cv2.Line(image, pt7, pt8, blue, 2, LineTypes.AntiAlias);
            //Cv2.Line(image, pt3, pt4, green, 3, LineTypes.Link8, 1);

            ////사각형 그리기
            Cv2.Rectangle(image, rect, black, 2, LineTypes.AntiAlias);
            //Cv2.Rectangle(image, rect, blue, -1, LineTypes.AntiAlias, 1);
            //Cv2.Rectangle(image, pt1, pt2, red, 3);

            //삼각형 그리기
            //삼각형을 그리기 위한 점 배열
            Point[] trianglePoints = { pt5, pt7, pt9 };

            Cv2.FillPoly(image, new[] { trianglePoints }, red, LineTypes.AntiAlias);
            //Cv2.Line(image, pt5, pt9, red, 2, LineTypes.AntiAlias);
            //Cv2.Line(image, pt7, pt9, red, 2, LineTypes.AntiAlias);
            //Cv2.Line(image, pt5, pt7, red, 2, LineTypes.AntiAlias);

            //원 그리기
            Cv2.Circle(image, pt10, 100, blue, -1);
            //출력
            Cv2.ImShow("image", image);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();

        }
    }
}
