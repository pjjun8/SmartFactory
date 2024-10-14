using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharp002
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //그림 읽기
            Mat src = Cv2.ImRead("C:\\Temp\\a001.png", ImreadModes.Color);
            //흑백 변환
            Mat gray = new Mat();
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
            //저장
            Cv2.ImWrite("greay.png", gray);
            //출력
            Cv2.ImShow("칼라 뉴진스", src);
            Cv2.ImShow("흑백 뉴진스", gray);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
=============================================================
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharp003
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat m1 = new Mat(300, 400, MatType.CV_8UC1, new Scalar(200));
            Cv2.ImShow("m1표현", m1);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
============================================================
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharp004
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Point pt1 = new Point(3, 4);
            Mat m2 = new Mat(200, 300, MatType.CV_8U, new Scalar(200));

            Cv2.ImShow("m2", m2);

            Console.WriteLine($"p11({pt1.X}, {pt1.Y})");

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
============================================================
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharp004
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Point pt1 = new Point(3, 4);
            Point2f pt2 = new Point2f(3.1f, 4.5f);
            Point3d pt3 = new Point3d(100, 200, 300);

            Mat m2 = new Mat(200, 300, MatType.CV_8U, new Scalar(200));

            Cv2.ImShow("m2", m2);

            Console.WriteLine($"p11({pt1.X}, {pt1.Y})");
            Console.WriteLine($"pt2({pt2.X}, {pt2.Y})");
            Console.WriteLine($"pt3({pt3.X}, {pt3.Y}, {pt3.Z})");

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
============================================================
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharp005
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Point pt1 = new Point(100, 200);
            Point2f pt2 = new Point2f(92.3f, 124.23f);
            Point2d pt3 = new Point2d(100.2, 300.9);

            Point pt4 = new Point(120, 69);
            Point2f pt5 = new Point2f(0.3f, 0.0f);
            Point2f pt6 = new Point2f(0.0f, 0.4f);
            Point2d pt7 = new Point2d(0.25, 06);

            //Point 연산
            Point pt8 = pt1 + (Point)pt2;
            Point2f pt9 = pt6 * 3.14f;
            Point2d pt10 = (pt3 + (Point)pt6 * 10);
            //출력
            Console.WriteLine($"pt8 = {pt8.X}, {pt8.Y}");
            Console.WriteLine($"pt9 = {pt9}");
            Console.WriteLine(pt2 == pt6);
            Console.WriteLine($"pt7과 pt8의 내적 = {pt7.DotProduct(pt8)}");
        }
    }
}
==========================================================
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharp006
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Size sz1 = new Size(100, 200);
            Size2f sz2 = new Size2f(192.3f, 25.3f);
            Size2d sz3 = new Size2d(100.2, 30.9);

            Size sz4 = new Size(120, 69);
            Size2f sz5 = new Size2f(0.3f, 0.0f);
            Size2d sz6 = new Size(0.25, 0.6);

            Point2d pt1 = new Point2d(0.25, 0.6);

            //Size sz77 = sz1 + (Size)sz2; //Error
            Size sz7 = new Size(
                sz1.Width + (int)sz2.Width,
                sz2.Height + (int)sz2.Height);

            Size2d sz8 = new Size2d(
                sz3.Width - (double)sz4.Width,
                sz3.Height - (double)sz4.Height);

            Size2d sz9 = new Size2d(
                sz5.Width + (double)pt1.X,
                sz5.Height + (double)pt1.Y);

            Console.Write($"sz1.width = {sz1.Width},  ");
            Console.WriteLine($"sz2.height = {sz1.Height}");
            Console.WriteLine($"넓이 : {sz1.Width * sz1.Height}");
            Console.WriteLine($"[sz7] : {sz7}");
            Console.WriteLine($"[sz8] : {sz8}");
            Console.WriteLine($"[sz9] : [{sz9.Width:F2} * {sz9.Height}]");
        }
    }
}
