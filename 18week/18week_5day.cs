using OpenCvSharp;

namespace ColorCannyEdgeTrackbar
{
    internal class Program
    {
        private static Mat image, gray, edge;
        private static int th = 50; // 캐니 에지 낮은 임계값

        private static void OnTrackbar(int pos, IntPtr userdata)
        {
            th = pos;
            // 가우시안 블러링 및 캐니 에지 수행
            edge = new Mat();

            Cv2.GaussianBlur(gray, edge, new OpenCvSharp.Size(3, 3), 0.7);
            Cv2.Canny(edge, edge, th, th * 2, 3);

            Mat colorEdge = new Mat();
            image.CopyTo(colorEdge, edge); // 에지 영역만 복사

            // 결과 이미지 표시
            Cv2.ImShow("컬러 에지", colorEdge);
        }
        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/smoothing.jpg";
            image = Cv2.ImRead(path, ImreadModes.Color);

            if (image.Empty())
            {
                Console.WriteLine("이미지를 불러올 수 없습니다.");
                return;
            }

            gray = new Mat();
            Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);

            // OpenCV 창 설정 및 트랙바 생성
            Cv2.NamedWindow("컬러 에지", WindowFlags.AutoSize);
            Cv2.CreateTrackbar("Canny th", "컬러 에지", ref th, 100, OnTrackbar);
            OnTrackbar(0, IntPtr.Zero); // 초기 트랙바 함수 호출

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
-------------------------------------------------
//윈폼으로 만들기
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace ColorCannyEdgeWinForm
{
    public partial class Form1 : Form
    {
        private Mat image, gray, edge;
        private int th = 50; // 캐니 에지 낮은 임계값
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = "C:/Temp/opencv/smoothing.jpg";
            image = Cv2.ImRead(path, ImreadModes.Color);

            if (image.Empty())
            {
                MessageBox.Show("이미지를 불러올 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            gray = new Mat();
            Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);

            pictureBox1.Image = BitmapConverter.ToBitmap(image);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            th = trackBar1.Value * 10;
            edge = new Mat();

            textBox1.Text = trackBar1.Value.ToString();

            // 가우시안 블러링 및 캐니 에지 수행
            Cv2.GaussianBlur(gray, edge, new OpenCvSharp.Size(3, 3), 0.7);
            Cv2.Canny(edge, edge, th, th * 2, 3);

            Mat colorEdge = new Mat();
            image.CopyTo(colorEdge, edge); // 에지 영역만 복사

            pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(colorEdge);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

==================================================
using OpenCvSharp;

namespace MopologyErosion
{
    class CVUtils
    {
        public bool CheckMatch(Mat img, Point start, Mat mask, int mode = 0)
        {
            for (int u = 0; u < mask.Rows; u++)
            {
                for (int v = 0; v < mask.Cols; v++)
                {
                    Point pt = new Point(v, u);
                    byte m = mask.Get<byte>(u, v); // 마스크 계수
                    byte p = img.Get<byte>(start.Y + u, start.X + v); // 해당 위치 입력화소

                    bool ch = (p == 0); // 일치 여부 비교 (검은 바탕에 하얀 글씨)
                    if (m == 1 && ch == (mode == 0))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void Erosion(Mat img, Mat dst, Mat mask)
        {
            dst.Create(img.Size(), MatType.CV_8UC1);
            dst.SetTo(new Scalar(0));
            if (mask.Empty())
            {
                mask = Mat.Ones(3, 3, MatType.CV_8UC1);
            }

            Point h_m = new Point(mask.Cols / 2, mask.Rows / 2);

            for (int i = h_m.Y; i < img.Rows - h_m.Y; i++)
            {
                for (int j = h_m.X; j < img.Cols - h_m.X; j++)
                {
                    Point start = new Point(j, i) - h_m;
                    bool check = CheckMatch(img, start, mask, 0);
                    dst.Set<byte>(i, j, (byte)(check ? 255 : 0));
                }
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead(@"C:/Temp/opencv/morph_test1.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                throw new System.Exception("이미지를 로드할 수 없습니다.");
            }

            Mat thImg = new Mat();
            Mat dst1 = new Mat();
            Mat dst2 = new Mat();

            Cv2.Threshold(image, thImg, 128, 255, ThresholdTypes.Binary);

            Mat mask = new Mat(3, 3, MatType.CV_8UC1, new Scalar(0));
            mask.Set<byte>(0, 1, 1);
            mask.Set<byte>(1, 0, 1);
            mask.Set<byte>(1, 1, 1);
            mask.Set<byte>(1, 2, 1);
            mask.Set<byte>(2, 1, 1);

            CVUtils cvUtils = new CVUtils();
            cvUtils.Erosion(thImg, dst1, mask);
            Cv2.MorphologyEx(thImg, dst2, MorphTypes.Erode, mask);

            Cv2.ImShow("image", image);
            Cv2.ImShow("이진 영상", thImg);
            Cv2.ImShow("User_erosion", dst1);
            Cv2.ImShow("OpenCV_erosion", dst2);

            Cv2.WaitKey();
        }
    }
}
============================================
using OpenCvSharp;

namespace MorphologyDilation
{
    internal class Program
    {
        static bool CheckMatch(Mat img, Point start, Mat mask, int mode)
        {
            for (int u = 0; u < mask.Rows; u++)
            {
                for (int v = 0; v < mask.Cols; v++)
                {
                    int m = mask.At<byte>(u, v); // 마스크 계수
                    int p = img.At<byte>(start.Y + u, start.X + v); // 해당 위치 입력화소

                    bool ch = (p == 255); // 일치 여부 비교
                    if (m == 1 && ch == (mode == 1)) return false;
                }
            }
            return true;
        }

        // 팽창 연산을 수행하는 함수
        static void Dilation(Mat img, Mat dst, Mat mask)
        {
            // 결과 이미지 초기화
            dst.SetTo(0);

            // 마스크가 제공되지 않았다면 기본 3x3 마스크 생성
            if (mask.Empty())
                mask = Mat.Ones(3, 3, MatType.CV_8UC1);

            // 마스크의 중심 좌표 계산
            Point maskCenter = new Point(mask.Cols / 2, mask.Rows / 2);
            
            // 이미지 순회
            for (int i = maskCenter.Y; i < img.Rows - maskCenter.Y; i++)
            {
                for (int j = maskCenter.X; j < img.Cols - maskCenter.X; j++)
                {
                    // 현재 픽셀을 중심으로 마스크를 적용할 시작 좌표 계산
                    Point start = new Point(j, i) - maskCenter;
                    
                    // 이미지의 해당 부분과 마스크가 일치하는지 확인
                    bool check = CheckMatch(img, start, mask, 1);

                    // 팽창 연산 수행
                    dst.Set<byte>(i, j, check ? (byte)0 : (byte)255);
                }
            }
        }

        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead("c:\\Temp\\opencv\\morph_test1.jpg", ImreadModes.Grayscale);
            if (image.Empty())
                throw new Exception("이미지를 불러올 수 없습니다.");

            Mat thImg = new Mat();
            Cv2.Threshold(image, thImg, 128, 255, ThresholdTypes.Binary);

            var mask = new Mat(3, 3, MatType.CV_8UC1, new Scalar(0));
            mask.Set<byte>(0, 1, 1);
            mask.Set<byte>(1, 0, 1);
            mask.Set<byte>(1, 1, 1);
            mask.Set<byte>(1, 2, 1);
            mask.Set<byte>(2, 1, 1);
            mask.Set<byte>(2, 2, 1);

            Mat dst1 = image.Clone();
            Dilation(thImg, dst1, mask);

            Mat dst2 = new Mat();
            Cv2.MorphologyEx(thImg, dst2, MorphTypes.Dilate, mask);

            Cv2.ImShow("image", image);
            Cv2.ImShow("팽창 사용자정의 함수", dst1);
            Cv2.ImShow("OpenCV 팽창 함수", dst2);

            Cv2.WaitKey();
        }
    }
}
===========================================================
using OpenCvSharp;

namespace MorphologyOpenClose
{
    class CvUtils
    {
        // 두 이미지와 마스크가 일치하는지 여부를 확인하는 함수
        public bool CheckMatch(Mat img, Point start, Mat mask, int mode = 0)
        {
            for (int u = 0; u < mask.Rows; u++)
            {
                for (int v = 0; v < mask.Cols; v++)
                {
                    Point pt = new Point(v, u);
                    byte m = mask.Get<byte>(u, v); // 마스크 계수
                    byte p = img.Get<byte>(start.Y + u, start.X + v); // 해당 위치 입력화소

                    bool ch = (p == 0); // 일치 여부 비교 (검은 바탕에 하얀 글씨)
                    if (m == 1 && ch == (mode == 0))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // 침식 연산 함수
        public void Erosion(Mat img, Mat dst, Mat mask)
        {
            dst.Create(img.Size(), MatType.CV_8UC1);
            dst.SetTo(Scalar.Black);
            Point h_m = new Point(mask.Cols / 2, mask.Rows / 2);

            for (int i = h_m.Y; i < img.Rows - h_m.Y; i++)
            {
                for (int j = h_m.X; j < img.Cols - h_m.X; j++)
                {
                    Point start = new Point(j, i) - h_m;
                    bool check = CheckMatch(img, start, mask, 0);
                    dst.Set<byte>(i, j, (byte)(check ? 255 : 0));
                }
            }
        }

        // 팽창 연산 함수
        public void Dilation(Mat img, Mat dst, Mat mask)
        {
            dst.Create(img.Size(), MatType.CV_8UC1);
            dst.SetTo(Scalar.White);
            Point h_m = new Point(mask.Cols / 2, mask.Rows / 2);

            for (int i = h_m.Y; i < img.Rows - h_m.Y; i++)
            {
                for (int j = h_m.X; j < img.Cols - h_m.X; j++)
                {
                    Point start = new Point(j, i) - h_m;
                    bool check = CheckMatch(img, start, mask, 1);
                    dst.Set<byte>(i, j, (byte)(check ? 0 : 255));
                }
            }
        }

        // 열기 연산 함수
        public void Opening(Mat img, Mat dst, Mat mask)
        {
            Mat tmp = new Mat();
            Erosion(img, tmp, mask);
            Dilation(tmp, dst, mask);
        }

        // 닫기 연산 함수
        public void Closing(Mat img, Mat dst, Mat mask)
        {
            Mat tmp = new Mat();
            Dilation(img, tmp, mask);
            Erosion(tmp, dst, mask);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // 이미지 읽기 - 그림의 경로는 "C:/Temp/opencv/"로 지정
            Mat image = Cv2.ImRead(@"C:/Temp/opencv/morph_test1.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                Console.WriteLine("이미지를 불러올 수 없습니다. 경로를 확인해주세요.");
                return;
            }

            // 이진화 작업
            Mat th_img = new Mat();
            Cv2.Threshold(image, th_img, 128, 255, ThresholdTypes.Binary);

            // 마스크 행렬 정의 (3x3 형태)
            Mat mask = new Mat(3, 3, MatType.CV_8UC1, new Scalar(0));
            mask.Set(0, 1, 1);
            mask.Set(1, 0, 1);
            mask.Set(1, 1, 1);
            mask.Set(1, 2, 1);
            mask.Set(2, 1, 1);

            // 열기 연산 수행 (사용자 정의)
            Mat dst1 = new Mat();
            CvUtils cvUtils = new CvUtils();
            cvUtils.Opening(th_img, dst1, mask);

            // 닫기 연산 수행 (사용자 정의)
            Mat dst2 = new Mat();
            cvUtils.Closing(th_img, dst2, mask);

            //// OpenCV 제공 열기 연산 함수 사용
            Mat dst3 = new Mat();
            Cv2.MorphologyEx(th_img, dst3, MorphTypes.Open, mask);

            // OpenCV 제공 닫기 연산 함수 사용
            Mat dst4 = new Mat();
            Cv2.MorphologyEx(th_img, dst4, MorphTypes.Close, mask);

            // 결과 이미지 출력
            Cv2.ImShow("image", image);
            Cv2.ImShow("th_img", th_img);
            Cv2.ImShow("User_opening", dst1);
            Cv2.ImShow("User_closing", dst2);
            Cv2.ImShow("OpenCV_opening", dst3);
            Cv2.ImShow("OpenCV_closing", dst4);
            Cv2.WaitKey();
        }
    }
}
==================================================
using OpenCvSharp;

namespace DefectPlate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("차량 영상 번호( 0:종료 ) : ");
                if (!int.TryParse(Console.ReadLine(), out int no) || no == 0)
                    break;

                string fname = $"C:/Temp/opencv/test_car/{no:D2}.jpg"; //두 자리로 꼭 사용하세요. 숫자 01, 11, 12 등
                Mat image = new Mat(fname, ImreadModes.Color);

                if (image.Empty())
                {
                    Console.WriteLine($"{no}번 영상 파일이 없습니다.");
                    continue;
                }

                Mat gray = new Mat();
                Mat sobel = new Mat();
                Mat thImg = new Mat();
                Mat morph = new Mat();

                // 닫힘 연산 마스크 생성
                Mat kernel = Mat.Ones(new Size(31, 5), MatType.CV_8UC1);

                // 명암도 영상 변환
                Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);

                // 블러링을 통한 전처리
                Cv2.Blur(gray, gray, new Size(5, 5));

                // 소벨 알고리즘으로 경계(에지 검출한다)
                Cv2.Sobel(gray, sobel, MatType.CV_8U, 1, 0, 3);

                // 이진화를 수행한다.
                Cv2.Threshold(sobel, thImg, 120, 255, ThresholdTypes.Binary);

                // 닫음 연산 - 팽창 후 침식을 통해 경계를 명확히 분리한다
                Cv2.MorphologyEx(thImg, morph, MorphTypes.Close, kernel);

                // 결과 표시
                Cv2.ImShow("image", image);
                Cv2.ImShow("이진 영상", thImg);
                Cv2.ImShow("닫음 연산", morph);
                Cv2.WaitKey(2000);
            }
        }
    }
}
================================================
using OpenCvSharp;

namespace Scaling
{
    internal class Program
    {
        static void Scaling(Mat img, out Mat dst, Size size)
        {
            dst = new Mat(size, img.Type(), new Scalar(0));
            double ratioY = (double)size.Height / img.Rows; // 세로 변경 비율
            double ratioX = (double)size.Width / img.Cols;  // 가로 변경 비율

            for (int i = 0; i < img.Rows; i++) // 입력영상 순회 - 순방향 사상
            {
                for (int j = 0; j < img.Cols; j++)
                {
                    int x = (int)(j * ratioX);
                    int y = (int)(i * ratioY);
                    dst.Set(y, x, img.At<byte>(i, j));
                }
            }
        }
        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead(@"c:/Temp/opencv/scaling_test.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                Console.WriteLine("Image load failed!");
                return;
            }

            Mat dst1, dst2;
            Scaling(image, out dst1, new Size(150, 200)); // 크기변경 수행 - 축소
            Scaling(image, out dst2, new Size(300, 400)); // 크기변경 수행 - 확대

            Cv2.ImShow("image", image);
            Cv2.ImShow("dst1-축소", dst1);
            Cv2.ImShow("dst2-확대", dst2);
            Cv2.ResizeWindow("dst1-축소", 200, 200); // 윈도우 크기 확장
            Cv2.WaitKey();
        }
    }
}
====================================================
using OpenCvSharp;

namespace NearSetScaling
{
    internal class Program
    {
        static void Scaling(Mat img, out Mat dst, Size size)
        {
            dst = new Mat(size, img.Type(), new Scalar(0));
            double ratioY = (double)size.Height / img.Rows; // 세로 변경 비율
            double ratioX = (double)size.Width / img.Cols;  // 가로 변경 비율

            for (int i = 0; i < img.Rows; i++) // 입력영상 순회 - 순방향 사상
            {
                for (int j = 0; j < img.Cols; j++)
                {
                    int x = (int)(j * ratioX);
                    int y = (int)(i * ratioY);
                    dst.Set(y, x, img.At<byte>(i, j));
                }
            }
        }

        static void ScalingNearest(Mat img, out Mat dst, Size size)
        {
            dst = new Mat(size, MatType.CV_8U, new Scalar(0));
            double ratioY = (double)size.Height / img.Rows;
            double ratioX = (double)size.Width / img.Cols;
            for (int i = 0; i < dst.Rows; i++) // 목적영상 순회 - 역방향 사상
            {
                for (int j = 0; j < dst.Cols; j++)
                {
                    int x = (int)Math.Round(j / ratioX);
                    int y = (int)Math.Round(i / ratioY);
                    dst.Set(i, j, img.At<byte>(y, x));
                }
            }
        }
        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead(@"c:/Temp/opencv/interpolation_test.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                Console.WriteLine("Image load failed!");
                return;
            }

            Mat dst1, dst2;
            Scaling(image, out dst1, new Size(300, 300)); // 크기변경 - 기본
            ScalingNearest(image, out dst2, new Size(300, 300)); // 크기변경 – 최근접 이웃

            Cv2.ImShow("image", image);
            Cv2.ImShow("dst1-순방향사상", dst1);
            Cv2.ImShow("dst2-최근접 이웃보간", dst2);

            Cv2.WaitKey();
        }
    }
}
==========================================
using OpenCvSharp;

namespace BilinearScaling
{
    internal class Program
    {
        static void ScalingNearest(Mat img, out Mat dst, Size size) // 최근접 이웃 보간
        {
            dst = new Mat(size, MatType.CV_8U, new Scalar(0));
            double ratioY = (double)size.Height / img.Rows;
            double ratioX = (double)size.Width / img.Cols;

            for (int i = 0; i < dst.Rows; i++) // 목적영상 순회 - 역방향 사상
            {
                for (int j = 0; j < dst.Cols; j++)
                {
                    int x = (int)Math.Round(j / ratioX);
                    int y = (int)Math.Round(i / ratioY);
                    dst.Set(i, j, img.At<byte>(y, x));
                }
            }
        }

        static byte BilinearValue(Mat img, double x, double y) // 단일 화소 양선형 보간
        {
            if (x >= img.Cols - 1) x--;
            if (y >= img.Rows - 1) y--;

            Point pt = new Point((int)x, (int)y);
            byte A = img.At<byte>(pt.Y, pt.X);                       // 왼쪽상단 화소
            byte B = img.At<byte>(pt.Y + 1, pt.X);     // 왼쪽하단 화소
            byte C = img.At<byte>(pt.Y, pt.X + 1);     // 오른쪽상단 화소
            byte D = img.At<byte>(pt.Y + 1, pt.X + 1);     // 오른쪽하단 화소

            double alpha = y - pt.Y;
            double beta = x - pt.X;
            int M1 = A + (int)Math.Round(alpha * (B - A));  // 1차 보간
            int M2 = C + (int)Math.Round(alpha * (D - C));
            int P = M1 + (int)Math.Round(beta * (M2 - M1)); // 2차 보간

            return (byte)(P < 0 ? 0 : (P > 255 ? 255 : P));
        }

        static void ScalingBilinear(Mat img, out Mat dst, Size size) // 크기변경 – 양선형 보간
        {
            dst = new Mat(size, img.Type(), new Scalar(0));
            double ratioY = (double)size.Height / img.Rows;
            double ratioX = (double)size.Width / img.Cols;

            for (int i = 0; i < dst.Rows; i++) // 목적영상 순회 - 역방향 사상
            {
                for (int j = 0; j < dst.Cols; j++)
                {
                    double y = i / ratioY;
                    double x = j / ratioX;
                    dst.Set(i, j, BilinearValue(img, x, y)); // 화소 양선형 보간
                }
            }
        }
        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead(@"c:/Temp/opencv/interpolation_test.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                Console.WriteLine("Image load failed!");
                return;
            }

            Mat dst1 = new Mat();
            Mat dst2 = new Mat();
            Mat dst3 = new Mat();
            Mat dst4 = new Mat();
            ScalingBilinear(image, out dst1, new Size(300, 300)); // 크기변경 – 양선형 보간
            ScalingNearest(image, out dst2, new Size(300, 300));  // 크기변경 – 최근접 보간
            Cv2.Resize(image, dst3, new Size(300, 300), 0, 0, InterpolationFlags.Linear); // OpenCV 함수 적용 - 양선형 보간
            Cv2.Resize(image, dst4, new Size(300, 300), 0, 0, InterpolationFlags.Nearest); // OpenCV 함수 적용 - 최근접 보간

            Cv2.ImShow("image", image);
            Cv2.ImShow("dst1-양선형", dst1);
            Cv2.ImShow("dst2-최근접이웃", dst2);
            Cv2.ImShow("OpenCV-양선형", dst3);
            Cv2.ImShow("OpenCV-최근접이웃", dst4);

            Cv2.WaitKey();
        }
    }
}
======================================================
using OpenCvSharp;

namespace TranslationTest
{
    internal class Program
    {
        static void Translation(Mat img, out Mat dst, Point pt) // 평행이동 함수
        {
            Rect rect = new Rect(new Point(0, 0), img.Size()); // 입력영상 범위 사각형
            dst = new Mat(img.Size(), img.Type(), new Scalar(0));

            for (int i = 0; i < dst.Rows; i++) // 목적영상 순회 - 역방향 사상
            {
                for (int j = 0; j < dst.Cols; j++)
                {
                    Point dstPt = new Point(j, i); // 목적영상 좌표
                    Point imgPt = dstPt - pt; // 입력영상 좌표
                    if (rect.Contains(imgPt)) // 입력영상 범위 확인
                    {
                        dst.Set(i, j, img.At<byte>(imgPt.Y, imgPt.X));
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead(@"c:/Temp/opencv/translation_test.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                Console.WriteLine("Image load failed!");
                return;
            }

            Mat dst1, dst2;
            Translation(image, out dst1, new Point(30, 80)); // 평행이동 수행
            Translation(image, out dst2, new Point(-80, -50));

            Cv2.ImShow("image", image);
            Cv2.ImShow("dst1 - (30, 80) 이동", dst1);
            Cv2.ImShow("dst2 - (-80, -50) 이동", dst2);
            Cv2.WaitKey();
        }
    }
}
=====================================================
using OpenCvSharp;

namespace AffineTransformTest
{
    internal class Program
    {
        // 주어진 좌표 (x, y)에서 픽셀의 양선형 보간 값을 계산합니다.
        // 이 함수는 주변 네 개의 픽셀을 사용하여 그 사이의 값을 결정합니다.
        static byte BilinearValue(Mat img, double x, double y)
        {
            if (x >= img.Cols - 1) x--;
            if (y >= img.Rows - 1) y--;

            // 4개 화소 가져옴
            Point pt = new Point((int)x, (int)y);
            int A = img.At<byte>((int)pt.Y, (int)pt.X);
            int B = img.At<byte>((int)(pt.Y + 1), (int)pt.X);
            int C = img.At<byte>((int)pt.Y, (int)(pt.X + 1));
            int D = img.At<byte>((int)(pt.Y + 1), (int)(pt.X + 1));

            // 1차 보간
            double alpha = y - pt.Y;
            double beta = x - pt.X;
            int M1 = A + (int)Math.Round(alpha * (B - A));
            int M2 = C + (int)Math.Round(alpha * (D - C));

            // 2차 보간
            int P = M1 + (int)Math.Round(beta * (M2 - M1));
            return (byte)(P < 0 ? 0 : (P > 255 ? 255 : P));
        }

        // 주어진 변환 행렬을 사용하여 입력 이미지에 어파인 변환을 적용합니다.
        // 이 함수는 변환 행렬을 수동으로 반전하고, 양선형 보간법을 사용하여 출력 픽셀 값을 계산합니다.
        static void AffineTransform(Mat img, out Mat dst, Mat map)
        {
            dst = new Mat(img.Size(), img.Type(), Scalar.All(0));
            Rect rect = new Rect(new Point(0, 0), img.Size());

            Mat invMap = new Mat();
            Cv2.InvertAffineTransform(map, invMap);

            for (int i = 0; i < dst.Rows; i++)
            {
                for (int j = 0; j < dst.Cols; j++)
                {
                    Mat coordinateMatrix = new Mat(3, 1, MatType.CV_64F);
                    double[] values = { j, i, 1.0 };
                    for (int k = 0; k < 3; k++)
                    {
                        coordinateMatrix.Set(k, 0, values[k]);
                    }
                    Mat xy = invMap * coordinateMatrix;
                    Point2d pt = new Point2d(xy.At<double>(0), xy.At<double>(1));

                    if (rect.Contains(new Point((int)pt.X, (int)pt.Y)))
                    {
                        dst.Set(i, j, BilinearValue(img, pt.X, pt.Y));
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead(@"C:/Temp/opencv/affine_test3.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                throw new Exception("Image not found!");
            }

            Point2f[] pt1 = { new Point2f(10, 200), new Point2f(200, 150), new Point2f(280, 280) };
            Point2f[] pt2 = { new Point2f(10, 10), new Point2f(250, 10), new Point2f(280, 280) };

            Point2f center = new Point2f(200, 100);
            double angle = 30.0;
            double scale = 1;

            Mat affMap = Cv2.GetAffineTransform(pt1, pt2);
            Mat rotMap = Cv2.GetRotationMatrix2D(center, angle, scale);

            Mat dst1, dst2, dst3 = new Mat(), dst4 = new Mat();
            AffineTransform(image, out dst1, affMap);
            AffineTransform(image, out dst2, rotMap);

            Cv2.WarpAffine(image, dst3, affMap, image.Size(), InterpolationFlags.Linear);
            Cv2.WarpAffine(image, dst4, rotMap, image.Size(), InterpolationFlags.Linear);

            Cv2.CvtColor(image, image, ColorConversionCodes.GRAY2BGR);
            Cv2.CvtColor(dst1, dst1, ColorConversionCodes.GRAY2BGR);

            for (int i = 0; i < 3; i++)
            {
                Cv2.Circle(image, (Point)pt1[i], 3, new Scalar(0, 0, 255), 2);
                Cv2.Circle(dst1, (Point)pt2[i], 3, new Scalar(0, 0, 255), 2);
            }

            Cv2.ImShow("image", image);
            Cv2.ImShow("dst1-어파인", dst1);
            Cv2.ImShow("dst2-어파인 회전", dst2);
            Cv2.ImShow("dst3-어파인_OpenCV", dst3);
            Cv2.ImShow("dst4-어파인-회전_OpenCV", dst4);

            Cv2.WaitKey();
        }
    }
}
==============================================
using OpenCvSharp;

namespace AffineCombinationTest
{
    internal class Program
    {
        /// 주어진 좌표 (x, y)에서 픽셀의 양선형 보간 값을 계산합니다.
        /// 이 함수는 주변 네 개의 픽셀을 사용하여 그 사이의 값을 결정합니다.
        static byte BilinearValue(Mat img, double x, double y)
        {
            if (x >= img.Cols - 1) x--;
            if (y >= img.Rows - 1) y--;

            // 4개의 화소 값을 가져옴 (주변 픽셀)
            Point pt = new Point((int)x, (int)y);
            int A = img.At<byte>((int)pt.Y, (int)pt.X);
            int B = img.At<byte>((int)(pt.Y + 1), (int)pt.X);
            int C = img.At<byte>((int)pt.Y, (int)(pt.X + 1));
            int D = img.At<byte>((int)(pt.Y + 1), (int)(pt.X + 1));

            // 1차 보간 (가로 방향)
            double alpha = y - pt.Y;
            double beta = x - pt.X;
            int M1 = A + (int)Math.Round(alpha * (B - A));
            int M2 = C + (int)Math.Round(alpha * (D - C));

            // 2차 보간 (세로 방향)
            int P = M1 + (int)Math.Round(beta * (M2 - M1));
            return (byte)(P < 0 ? 0 : (P > 255 ? 255 : P)); // 범위를 0~255로 제한하여 반환
        }

        /// 주어진 변환 행렬을 사용하여 입력 이미지에 어파인 변환을 적용합니다.
        /// 이 함수는 변환 행렬을 수동으로 반전하고, 양선형 보간법을 사용하여 출력 픽셀 값을 계산합니다.
        static void AffineTransform(Mat img, out Mat dst, Mat map, Size size)
        {
            dst = new Mat(size, img.Type(), Scalar.All(0)); // 초기화된 출력 이미지 생성
            Rect rect = new Rect(new Point(0, 0), img.Size()); // 이미지 범위를 나타내는 직사각형 정의

            // 어파인 변환 행렬의 역변환 계산
            Mat invMap = new Mat();
            Cv2.InvertAffineTransform(map, invMap);

            for (int i = 0; i < size.Height; i++)
            {
                for (int j = 0; j < size.Width; j++)
                {
                    // 역변환 행렬을 사용하여 원본 이미지의 좌표 계산
                    Mat coordinateMatrix = new Mat(3, 1, MatType.CV_64F);
                    double[] values = { j, i, 1.0 };
                    for (int k = 0; k < 3; k++)
                    {
                        coordinateMatrix.Set(k, 0, values[k]);
                    }
                    Mat xy = invMap * coordinateMatrix;
                    Point2d pt = new Point2d(xy.At<double>(0), xy.At<double>(1));

                    // 계산된 좌표가 원본 이미지 내부에 있는 경우 보간 값을 설정
                    if (rect.Contains(new Point((int)pt.X, (int)pt.Y)))
                    {
                        dst.Set(i, j, BilinearValue(img, pt.X, pt.Y));
                    }
                }
            }
        }

        /// 어파인 변환 매핑을 생성하는 함수입니다.
        /// 회전, 스케일링, 평행이동 등을 포함합니다.
        static Mat GetAffineMap(Point2d center, double degree, double fx = 1, double fy = 1, Point2d translate = default)
        {
            // 회전 행렬, 중심 이동, 원점 이동, 스케일링, 평행 이동을 위한 행렬 초기화
            Mat rotMap = Mat.Eye(3, 3, MatType.CV_64F);
            Mat centerTrans = Mat.Eye(3, 3, MatType.CV_64F);
            Mat orgTrans = Mat.Eye(3, 3, MatType.CV_64F);
            Mat scaleMap = Mat.Eye(3, 3, MatType.CV_64F);
            Mat transMap = Mat.Eye(3, 3, MatType.CV_64F);

            // 회전 변환 정의
            double radian = degree / 180 * Math.PI;
            rotMap.Set(0, 0, Math.Cos(radian));
            rotMap.Set(0, 1, Math.Sin(radian));
            rotMap.Set(1, 0, -Math.Sin(radian));
            rotMap.Set(1, 1, Math.Cos(radian));

            // 중심 이동 변환
            centerTrans.Set(0, 2, center.X);
            centerTrans.Set(1, 2, center.Y);
            orgTrans.Set(0, 2, -center.X);
            orgTrans.Set(1, 2, -center.Y);

            // 스케일링 및 평행 이동 변환
            scaleMap.Set(0, 0, fx);
            scaleMap.Set(1, 1, fy);
            transMap.Set(0, 2, translate.X);
            transMap.Set(1, 2, translate.Y);

            // 최종 어파인 변환 행렬 계산
            Mat retMap = centerTrans * rotMap * transMap * scaleMap * orgTrans;
            retMap = retMap.RowRange(0, 2); // 2x3 변환 행렬로 반환
            return retMap;
        }
        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead(@"c:/Temp/opencv/affine_test5.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                throw new Exception("Image not found!");
            }

            Point2f center = new Point2f(image.Width / 2, image.Height / 2);
            Point tr = new Point(100, 0);
            double angle = 30.0;
            Mat dst1 = new Mat();
            Mat dst2 = new Mat();
            Mat dst3 = new Mat();
            Mat dst4 = new Mat();

            // 어파인 변환 행렬을 생성
            Point2d centerD = new Point2d(center.X, center.Y);
            Mat affMap1 = GetAffineMap(centerD, angle);
            Mat affMap2 = GetAffineMap(new Point2d(0, 0), 0, 2.0, 1.5);
            Mat affMap3 = GetAffineMap(centerD, angle, 0.7, 0.7);
            Mat affMap4 = GetAffineMap(centerD, angle, 0.7, 0.7, tr);

            // OpenCV의 warpAffine 함수를 사용하여 이미지 변환
            Cv2.WarpAffine(image, dst1, affMap1, image.Size()); // 회전만 적용
            Cv2.WarpAffine(image, dst2, affMap2, image.Size()); // 크기 변경만 적용
            AffineTransform(image, out dst3, affMap3, image.Size()); // 사용자 정의 회전 + 크기 변경
            AffineTransform(image, out dst4, affMap4, image.Size()); // 사용자 정의 회전 + 크기 변경 + 평행 이동

            Cv2.ImShow("image", image);
            Cv2.ImShow("dst1-회전만", dst1);
            Cv2.ImShow("dst2-크기변경만", dst2);
            Cv2.ImShow("dst3-회전+크기변경", dst3);
            Cv2.ImShow("dst4-회전+크기변경+평행이동", dst4);

            Cv2.WaitKey();
        }
    }
}
