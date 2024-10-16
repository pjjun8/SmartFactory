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
=======================================
using OpenCvSharp;

namespace DrawCircle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Scalar orange = new Scalar(0, 165, 255);
            Scalar blue = new Scalar(255, 0, 0);
            Scalar magenta = new Scalar(255, 0, 255);

            Mat image = new Mat(300, 500, MatType.CV_8UC3, new Scalar(255, 255, 255));

            Size size = image.Size();
            Point center = new Point(size.Width / 2, size.Height / 2);

            Point pt1 = new Point(70, 50);
            Point pt2 = new Point(350, 220);

            Cv2.Circle(image, center, 100, blue);
            Cv2.Circle(image, pt1, 80, orange, 2);
            Cv2.Circle(image, pt2, 60, magenta, -1);

            Cv2.PutText(image, "center_blue", center, HersheyFonts.HersheyComplex, 1.2, blue);
            Cv2.PutText(image, "pt1_orange", pt1, HersheyFonts.HersheyComplex, 0.8, orange);
            //Cv2.PutText(image, "pt2_magenta", pt2 + Point(2, 2), HersheyFonts.HersheyComplex, 0.5, new Scalar(0, 0, 0), 2);
            Point newPt2 = new Point(pt2.X + 2, pt2.Y + 2);
            Cv2.PutText(image, "pt2_magenta", newPt2, HersheyFonts.HersheyComplex, 0.5, new Scalar(0, 0, 0), 2);
            Cv2.PutText(image, "pt2_magenta", pt2, HersheyFonts.HersheyComplex, 0.5, magenta, 1);

            Cv2.ImShow("원그리기", image);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
==================================
using OpenCvSharp;

namespace Draw_Ellipse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Scalar orange = new Scalar(0, 165, 255);
            Scalar blue = new Scalar(255, 0, 0);
            Scalar magenta = new Scalar(255, 0, 255);

            Mat image = new Mat(300, 700, MatType.CV_8UC3, new Scalar(255, 255, 255));

            Point pt1 = new Point(120, 150);
            Point pt2 = new Point(550, 150);

            Cv2.Circle(image, pt1, 1, new Scalar(0), 1);
            Cv2.Circle(image, pt2, 1, new Scalar(0), 1);

            //타원 그리기
            Cv2.Ellipse(image, pt1, new Size(100, 60), 0, 0, 360, orange, 2);
            Cv2.Ellipse(image, pt1, new Size(100, 60), 0, 30, 270, blue, 4);

            //호 그리기
            Cv2.Ellipse(image, pt2, new Size(100, 60), 30, 0, 360, orange, 2);
            Cv2.Ellipse(image, pt2, new Size(100, 60), 30, -30, 160, magenta, 4);

            Cv2.ImShow("타원 및 호 그리기", image);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
=============================
using OpenCvSharp;

namespace SetCameraAttribute
{
    internal class Program
    {
        private static Mat frame = new Mat();
        private static VideoCapture capture = new VideoCapture(0);
        private static int zoomFactor = 1; // 줌 기본값
        private static int focusFactor = 40; // 포커스 기본값

        private static void ZoomBar(int value, IntPtr userdata)
        {
            zoomFactor = value;
        }
        private static void FocusBar(int value, IntPtr userdata)
        {
            focusFactor = value;
        }

        static void Main(string[] args)
        {
            capture.Open(0);
            if (!capture.IsOpened())
            {
                Console.WriteLine("카메라가 연결되지 않았습니다.");
                return;
            }

            // 카메라 속성 설정
            capture.Set(VideoCaptureProperties.FrameWidth, 800);
            capture.Set(VideoCaptureProperties.FrameHeight, 600);
            capture.Set(VideoCaptureProperties.AutoFocus, 0); // 수동 포커스 설정
            capture.Set(VideoCaptureProperties.Brightness, 150);

            // 창 및 트랙바 생성
            string title = "카메라 속성변경";
            Cv2.NamedWindow(title);
            Cv2.CreateTrackbar("Zoom", title, ref zoomFactor, 5, ZoomBar);
            Cv2.CreateTrackbar("Focus", title, ref focusFactor, 40, FocusBar);

            while (true)
            {
                capture.Read(frame);
                if (frame.Empty())
                    break;

                //트랙바와 Focus
                capture.Set(VideoCaptureProperties.Zoom, zoomFactor);
                capture.Set(VideoCaptureProperties.Focus, focusFactor);

                // 줌 적용된 이미지
                ShowZoomedImage(zoomFactor);

                // 30ms 간격으로 키 입력 대기, 아무 키나 누르면 종료
                if (Cv2.WaitKey(30) >= 0)
                    break;
            }

            capture.Release();
            Cv2.DestroyAllWindows();
        }

        // 줌을 적용하여 이미지 크롭 및 확대하는 함수
        private static void ShowZoomedImage(int zoomLevel)
        {
            // 줌 레벨이 1보다 작거나 같으면 원본 표시
            if (zoomLevel <= 1)
            {
                Cv2.ImShow("카메라 속성변경", frame);
                return;
            }
            // 중앙 부분을 크롭하여 줌 효과 적용
            int newWidth = frame.Width / zoomLevel;
            int newHeight = frame.Height / zoomLevel;
            int x = (frame.Width - newWidth) / 2;
            int y = (frame.Height - newHeight) / 2;

            Rect roi = new Rect(x, y, newWidth, newHeight);
            Mat zoomedFrame = new Mat(frame, roi);

            // 크롭한 이미지를 다시 원래 크기로 확대
            Cv2.Resize(zoomedFrame, zoomedFrame, new Size(frame.Width, frame.Height));

            // 결과 이미지를 동일한 창에 표시
            Cv2.ImShow("카메라 속성변경", zoomedFrame);
        }
    }
}
