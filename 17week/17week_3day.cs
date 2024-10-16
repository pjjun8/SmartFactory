using OpenCvSharp;

namespace cvKeyboard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat image = new Mat(300, 400, MatType.CV_8UC1, new Scalar(255));
            string title1 = "창 크기변경1 - AUTOSIZE";
            string title2 = "창 크기변경2 - NORMAL";

            Cv2.NamedWindow(title1, WindowFlags.AutoSize);
            Cv2.NamedWindow(title2, WindowFlags.Normal);

            Cv2.ResizeWindow(title1, 500, 200);
            Cv2.ResizeWindow(title2, 500, 200);

            Cv2.ImShow(title1, image);
            Cv2.ImShow(title2, image);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
=========================================
using OpenCvSharp;

namespace cvKeyboard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Cv2.GetVersionString());
            Mat image1 = new Mat(300, 400, MatType.CV_8U, new Scalar(255));
            string title1 = "white창 제어";

            Cv2.NamedWindow(title1, WindowFlags.AutoSize);

            Cv2.ImShow(title1, image1);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
=========================================
using OpenCvSharp;

namespace cvKeyboard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using(Mat image = new Mat(200, 300, MatType.CV_8U, new Scalar(255)))
            {
                Cv2.NamedWindow("키보드 이벤트", WindowFlags.AutoSize);
                Cv2.ImShow("키보드 이벤트", image);
                while (true)
                {
                    int key = Cv2.WaitKeyEx(1000);

                    if (key == 27) // ESC key
                        break;

                    switch (key)
                    {
                        case (int)'A':
                            Console.WriteLine("A키 입력");
                            break;
                        case (int)'b':
                            Console.WriteLine("b키 입력");
                            break;
                        case (int)'B':
                            Console.WriteLine("B키 입력");
                            break;

                        case 0x250000:
                            Console.WriteLine("왼쪽 화살표 입력");
                            break;
                        case 0x260000:
                            Console.WriteLine("윗쪽 화살표 입력");
                            break;
                        case 0x270000:
                            Console.WriteLine("오른쪽 화살표 입력");
                            break;
                        case 0x280000:
                            Console.WriteLine("아래쪽 화살표 입력");
                            break;
                        default:
                            Console.WriteLine(key);
                            break;

                    }
                }

                Cv2.WaitKey(0);
                Cv2.DestroyAllWindows();
            }
            
        }
    }
}
=========================================
using OpenCvSharp;

namespace CVBasicCamera02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VideoCapture capture = new VideoCapture(0);

            if (!capture.IsOpened())
            {
                Console.WriteLine("Camera is not connected");
                return;
            }
            Console.WriteLine("Camera is connected");

            //카메라 속성
            Console.WriteLine("너비 : " + capture.Get(VideoCaptureProperties.FrameWidth));
            Console.WriteLine("높이 : " + capture.Get(VideoCaptureProperties.FrameHeight));
            Console.WriteLine("노출 : " + capture.Get(VideoCaptureProperties.Exposure));
            Console.WriteLine("밝기 : " + capture.Get(VideoCaptureProperties.Brightness));

            //핵심코드
            Mat frame = new Mat();

            while (true)
            {
                //카메라에서 프레임 읽기
                capture.Read(frame);
                if (frame.Empty())
                {
                    Console.WriteLine("frame has something wrong!");
                    return;
                }
                Cv2.ImShow("default camera", frame);

                //종료법
                if(Cv2.WaitKey(30) >= 0)
                    break;
            }
            //Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
===================================
using OpenCvSharp;

namespace CVBasicCamera02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VideoCapture capture = new VideoCapture(0);

            if (!capture.IsOpened())
            {
                Console.WriteLine("Camera is not connected");
                return;
            }
            Console.WriteLine("Camera is connected");

            //카메라 속성
            Console.WriteLine("너비 : " + capture.Get(VideoCaptureProperties.FrameWidth));
            Console.WriteLine("높이 : " + capture.Get(VideoCaptureProperties.FrameHeight));
            Console.WriteLine("노출 : " + capture.Get(VideoCaptureProperties.Exposure));
            Console.WriteLine("밝기 : " + capture.Get(VideoCaptureProperties.Brightness));

            //핵심코드
            Mat frame = new Mat();

            while (true)
            {
                //카메라에서 프레임 읽기
                capture.Read(frame);
                if (frame.Empty())
                {
                    Console.WriteLine("frame has something wrong!");
                    return;
                }
                //흑백효과
                Mat black = frame.Clone();
                Cv2.CvtColor(frame, black, ColorConversionCodes.BGR2GRAY);

                Cv2.ImShow("default camera", black);
                //Cv2.ImShow("default camera", frame);

                //종료법
                if (Cv2.WaitKey(30) >= 0)
                    break;
            }
            //Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
=================================
using OpenCvSharp;
using System;

namespace TrackBarApp
{
    internal class Program
    {
        private static string title = "트랙바 이벤트";
        private static Mat image;
        static void Main(string[] args)
        {
            int value = 130;
            image = new Mat(300, 400, MatType.CV_8UC1, new Scalar(120));

            Cv2.NamedWindow(title, WindowFlags.AutoSize);
            Cv2.CreateTrackbar("밝기값", title, ref value, 255, OnChange);

            Cv2.ImShow(title, image);
            Cv2.WaitKey(0);
        }
        private static void OnChange(int value, IntPtr userdata)
        {
            int add_value = value - 130;
            Console.WriteLine($"추가 화소값 {add_value}");

            //Mat tmp = image + add_value;
            Mat tmp = new Mat();
            Cv2.Add(image, new Scalar(add_value), tmp); // Mat에 스칼라 값 더하기
            Cv2.ImShow(title, tmp);
        }
    }
}
==============================
using OpenCvSharp;
using System;

namespace Event_Mouse
{
    internal class Program
    {
        private static void onMouse(MouseEventTypes @event, int x, int y, MouseEventFlags flags, IntPtr userdata)
        {
            switch (@event)
            {
                case MouseEventTypes.LButtonDown:
                    Console.WriteLine("마우스 왼쪽 버튼이 눌러졌습니다.");
                    break;
                case MouseEventTypes.RButtonDown:
                    Console.WriteLine("마우스 오른쪽 버튼이 눌러졌습니다.");
                    break;
            }
        }
        static void Main(string[] args)
        {
            Mat image = new Mat(200, 300, MatType.CV_8UC3, new Scalar(255, 255, 255));
            Cv2.ImShow("마우스 이벤트1", image);

            Cv2.SetMouseCallback("마우스 이벤트1", onMouse);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
