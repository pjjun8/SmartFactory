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
