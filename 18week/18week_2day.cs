using OpenCvSharp;

namespace calc_histogram
{
    internal class Program
    {
        static void CalcHisto(Mat image, out Mat hist, int bins, int rangeMax = 256)
        {
            hist = new Mat(bins, 1, MatType.CV_32F, new Scalar(0));
            float gap = rangeMax / (float)bins;

            for (int i = 0; i < image.Rows; i++)
            {
                for (int j = 0; j < image.Cols; j++)
                {
                    int idx = (int)(image.At<byte>(i, j) / gap);
                    hist.Set<float>(idx, hist.At<float>(idx) + 1);
                }
            }
        }
        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead("c:\\Temp\\opencv\\pixel_test.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                Console.WriteLine("이미지를 로드할 수 없습니다.");
                return;
            }

            Mat hist;
            CalcHisto(image, out hist, 256);   // 히스토그램 계산

            //Console.Write(hist.Dump());
            for (int i = 0; i < hist.Rows; i++)
            {
                Console.Write(hist.At<float>(i) + ", ");
            }
            Console.WriteLine();

            Cv2.ImShow("image", image);
            Cv2.WaitKey();
        }
    }
}
==========================
using OpenCvSharp;

namespace calc_histogram
{
    internal class Program
    {
        static void CalcHisto(Mat image, out Mat hist, int bins, int rangeMax = 256)
        {
            hist = new Mat(bins, 1, MatType.CV_32F, new Scalar(0));
            float gap = rangeMax / (float)bins;

            for (int i = 0; i < image.Rows; i++)
            {
                for (int j = 0; j < image.Cols; j++)
                {
                    int idx = (int)(image.At<byte>(i, j) / gap);
                    hist.Set<float>(idx, hist.At<float>(idx) + 1);
                }
            }
        }

        // OpenCV의 calcHist 함수를 이용해 히스토그램을 계산하는 함수
        static void CalcHistoUsingOpenCV(Mat image, out Mat hist, int bins, int rangeMax = 256)
        {
            int[] histSize = { bins };            // 히스토그램 계급 개수
            Rangef[] ranges = { new Rangef(0, rangeMax) };  // 채널 화소값 범위
            int[] channels = { 0 };                // 채널 목록

            hist = new Mat();
            Cv2.CalcHist(new[] { image }, channels, null, hist, 1, histSize, ranges);
        }
        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead("c:\\Temp\\opencv\\pixel_test.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                Console.WriteLine("이미지를 로드할 수 없습니다.");
                return;
            }

            Mat hist;
            CalcHistoUsingOpenCV(image, out hist, 256);

            // 히스토그램 화소값 출력
            for (int i = 0; i < hist.Rows; i++)
            {
                Console.Write(hist.At<float>(i) + ", ");
            }
            Console.WriteLine();

            Cv2.ImShow("image", image);
            Cv2.WaitKey();
        }
    }
}
================================
using OpenCvSharp;

namespace draw_histogram
{
    internal class Program
    {
        static void draw_histo(Mat hist, out Mat hist_img)
        {
            Size size = new Size(256, 200);
            hist_img = new Mat(size, MatType.CV_8U, Scalar.All(255));
            float bin = (float)hist_img.Cols / hist.Rows;
            Cv2.Normalize(hist, hist, 0, hist_img.Rows, NormTypes.MinMax);

            for (int i = 0; i < hist.Rows; i++)
            {
                float sx = i * bin;
                float ex = (i + 1) * bin;
                Point pt1 = new Point(sx, 0);
                Point pt2 = new Point(ex, hist.At<float>(i));

                if (pt2.Y > 0) Cv2.Rectangle(hist_img, pt1, pt2, Scalar.All(0), -1);
            }
            Cv2.Flip(hist_img, hist_img, 0);
        }

        static void calc_Histo(Mat image, out Mat hist, int bins, int rangeMax = 256)
        {
            int[] histSize = { bins };
            Rangef[] ranges = { new Rangef(0, rangeMax) };
            int[] channels = { 0 };
            hist = new Mat();

            Cv2.CalcHist(new[] { image }, channels, null, hist, 1, histSize, ranges);
        }
        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead("C:\\Temp\\opencv\\pixel_test.jpg", ImreadModes.Grayscale);
            Mat hist, hist_img;
            if (image.Empty())
            {
                Console.WriteLine("이미지 오류");
                Environment.Exit(0);
            }

            calc_Histo(image, out hist, 256);
            draw_histo(hist, out hist_img);

            Cv2.ImShow("image", image);
            Cv2.ImShow("hist_img", hist_img);
            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }
    }
}
