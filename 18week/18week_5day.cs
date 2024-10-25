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
