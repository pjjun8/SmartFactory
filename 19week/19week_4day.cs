using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test04
{
    internal class Utils
    {
        public static List<Mat> MakeCoinImages(Mat img, List<RotatedRect> circles)
        {
            List<Mat> coinImages = new List<Mat>();

            foreach (var circle in circles)
            {
                Mat coinImg = new Mat();
                Cv2.GetRectSubPix(img, new Size((int)circle.Size.Width, (int)circle.Size.Height), circle.Center, coinImg);
                coinImages.Add(coinImg);
            }

            return coinImages;
        }

        public static void DrawCircles(Mat img, List<RotatedRect> circles, List<int> groups)
        {
            for (int i = 0; i < circles.Count; i++)
            {
                Scalar color = groups[i] == 0 ? Scalar.Red : Scalar.Blue;
                Cv2.Ellipse(img, circles[i], color, 2);
            }
        }
    }
}
-------------------------------
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test04
{
    internal class Histogram
    {
        public static List<Mat> CalculateCoinHistograms(List<Mat> coinImages, int hueBins)
        {
            List<Mat> histograms = new List<Mat>();
            int[] histSize = { hueBins };
            Rangef[] ranges = { new Rangef(0, 180) };

            foreach (var coinImg in coinImages)
            {
                Mat hsv = new Mat();
                Cv2.CvtColor(coinImg, hsv, ColorConversionCodes.BGR2HSV);

                Mat hist = new Mat();
                Cv2.CalcHist(new Mat[] { hsv }, new int[] { 0 }, null, hist, 1, histSize, ranges);
                Cv2.Normalize(hist, hist, 0, 1, NormTypes.MinMax);

                histograms.Add(hist);
            }

            return histograms;
        }
    }
}
----------------------------
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test04
{
    internal class Classify
    {
        public static List<int> Grouping(List<Mat> hists)
        {
            var w = new List<Mat>();
            Mat mat1 = new Mat(32, 1, MatType.CV_32F);
            Mat mat2 = new Mat(32, 1, MatType.CV_32F);

            // 데이터를 설정하는 방식
            float[] data1 = { 0, 0, 2, 2, 3, 5, 5, 4, 4, 3, 3, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 3, 3, 2, 1, 0, 0, 0 };
            float[] data2 = { 0, 0, 0, 0, 1, 2, 3, 4, 4, 5, 6, 7, 6, 5, 4, 3, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < data1.Length; i++)
            {
                mat1.Set<float>(i, 0, data1[i]);
                mat2.Set<float>(i, 0, data2[i]);
            }

            w.Add(mat1);
            w.Add(mat2);

            List<int> groups = new List<int>();

            for (int i = 0; i < hists.Count; i++)
            {
                Mat s0 = new Mat(), s1 = new Mat();
                Cv2.Multiply(w[0], hists[i], s0);
                Cv2.Multiply(w[1], hists[i], s1);

                int group = Cv2.Sum(s0)[0] > Cv2.Sum(s1)[0] ? 0 : 1;
                groups.Add(group);
            }
            return groups;
        }

        public static void ClassifyCoins(List<RotatedRect> circles, List<int> groups, int[] Ncoins)
        {
            for (int i = 0; i < circles.Count; i++)
            {
                int coin = 0;
                int radius = (int)Math.Round(circles[i].Size.Width / 2);

                if (groups[i] == 0)
                {
                    if (radius > 48) coin = 3;
                    else if (radius > 46) coin = 2;
                    else if (radius > 25) coin = 0;
                }
                else
                {
                    if (radius > 48) coin = 3;
                    else if (radius > 43) coin = 2;
                    else if (radius > 36) coin = 1;
                }

                Ncoins[coin]++;
            }
        }

        public static int CalculateTotal(int[] Ncoins)
        {
            int total = 0;
            int[] coinValues = { 10, 50, 100, 500 };

            for (int i = 0; i < Ncoins.Length; i++)
            {
                total += coinValues[i] * Ncoins[i];
                Console.WriteLine($"{coinValues[i]}원: {Ncoins[i]} 개");
            }

            return total;
        }
    }
}
----------------------------
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test04
{
    internal class Preprocess
    {
        // 회전된 사각형을 그리는 함수
        public static void DrawRotatedRect(Mat img, RotatedRect mr, Scalar color, int thickness = 2)
        {
            Point2f[] pts = mr.Points();
            for (int i = 0; i < 4; ++i)
            {
                Cv2.Line(img, pts[i].ToPoint(), pts[(i + 1) % 4].ToPoint(), color, thickness);
            }
        }

        // 이미지를 전처리하는 함수
        public static Mat Preprocessing(Mat img)
        {
            Mat gray = new Mat(), thImg = new Mat();
            Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY); // 그레이스케일 변환
            Cv2.GaussianBlur(gray, gray, new Size(7, 7), 2, 2); // 가우시안 블러 적용
            Cv2.Threshold(gray, thImg, 130, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu); // Otsu 임계처리
            Cv2.MorphologyEx(thImg, thImg, MorphTypes.Open, new Mat(), new Point(-1, -1), 1); // 열림 연산
            return thImg;
        }

        // 이미지에서 동전 찾기 함수
        public static List<RotatedRect> FindCoins(Mat img)
        {
            Point[][] contours; // OpenCvSharp.Point[][] 형식으로 선언
            HierarchyIndex[] hierarchy; // HierarchyIndex 배열 사용

            Cv2.FindContours(img.Clone(), out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            Mat col = new Mat();
            Cv2.CvtColor(img, col, ColorConversionCodes.GRAY2BGR);

            List<RotatedRect> circles = new List<RotatedRect>();
            foreach (var contour in contours)
            {
                RotatedRect mr = Cv2.MinAreaRect(contour);
                float radius = (mr.Size.Width + mr.Size.Height) / 4.0f;
                if (radius > 10)
                {
                    circles.Add(mr);
                }
            }

            return circles;
        }


    }
}
-------------------------------
using OpenCvSharp;

namespace test04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int coinNo, hueBin = 32;
            Console.Write("동전 영상번호: ");
            coinNo = int.Parse(Console.ReadLine());
            string fname = $"C:/Users/Admin/Desktop/OpenCv/opencv_ppt_cpp/예제_12.1/image/{coinNo:D2}.png";
            Mat image = Cv2.ImRead(fname);

            if (image.Empty())
            {
                Console.WriteLine("이미지를 불러오지 못했습니다.");
                return;
            }

            Mat thImg = Preprocess.Preprocessing(image);               // 전처리
            List<RotatedRect> circles = Preprocess.FindCoins(thImg);   // 동전 영역 검색
            List<Mat> coinsImg = Utils.MakeCoinImages(image, circles); // 동전 이미지 생성
            List<Mat> coinsHist = Histogram.CalculateCoinHistograms(coinsImg, hueBin); // 색상 히스토그램

            int[] Ncoins = new int[4];
            List<int> groups = Classify.Grouping(coinsHist);           // 동전 영상 그룹화
            Classify.ClassifyCoins(circles, groups, Ncoins);           // 동전 분류
            int coinCount = Classify.CalculateTotal(Ncoins);           // 총 금액 계산

            // 결과 출력
            string resultStr = $"total coin: {coinCount} Won";
            Console.WriteLine(resultStr);
            Cv2.PutText(image, resultStr, new Point(10, 50), HersheyFonts.HersheySimplex, 2, Scalar.Green, 2);

            Utils.DrawCircles(image, circles, groups);
            Cv2.ImShow("동전영상", image);
            Cv2.WaitKey();
        }
    }
}
