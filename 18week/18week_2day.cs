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
====================================
using OpenCvSharp;

namespace hue_histogram
{
    internal class Program
    {
        // Hue 채널에 대한 히스토그램을 계산하는 함수
        static void CalcHisto(Mat image, out Mat hist, int bins, int rangeMax = 256)
        {
            int[] histSize = { bins };            // 히스토그램 계급 개수
            Rangef[] ranges = { new Rangef(0, rangeMax) };  // 히스토그램 범위
            int[] channels = { 0 };                // 채널 목록

            hist = new Mat();
            Cv2.CalcHist(new[] { image }, channels, null, hist, 1, histSize, ranges);
        }

        // hue 채널에 대한 색상 팔레트 행렬 생성
        static Mat MakePalette(int rows)
        {
            Mat hsv = new Mat(rows, 1, MatType.CV_8UC3);
            for (int i = 0; i < rows; i++)
            {
                byte hue = (byte)((float)i / rows * 180);
                hsv.At<Vec3b>(i, 0) = new Vec3b(hue, 255, 255);
            }

            //hsv.CvtColor(ColorConversionCodes.HSV2BGR); // C++처럼만 하면 변환이 안됨!!!
            //return hsv;                               //복사본을 만들어 출력
            Mat bgrPalette = new Mat();
            Cv2.CvtColor(hsv, bgrPalette, ColorConversionCodes.HSV2RGB);
            return bgrPalette;
        }

        // Hue 히스토그램을 그려주는 함수
        static void DrawHistHue(Mat hist, out Mat histImg, Size size)
        {
            Mat hsvPalette = MakePalette(hist.Rows);
            histImg = new Mat(size, MatType.CV_8UC3, new Scalar(255, 255, 255));
            float bin = (float)histImg.Cols / hist.Rows;
            Cv2.Normalize(hist, hist, 0, histImg.Rows, NormTypes.MinMax);

            for (int i = 0; i < hist.Rows; i++)
            {
                float start_x = i * bin;
                float end_x = (i + 1) * bin;
                Point pt1 = new Point((int)start_x, 0);
                Point pt2 = new Point((int)end_x, (int)hist.At<float>(i));

                //Scalar color = hsvPalette.At<Vec3b>(i, 0);
                Vec3b colorVec = hsvPalette.At<Vec3b>(i, 0);
                Scalar color = new Scalar(colorVec.Item2, colorVec.Item1, colorVec.Item0);

                Cv2.Rectangle(histImg, pt1, pt2, color, -1);
            }
            Cv2.Flip(histImg, histImg, FlipMode.X);
        }
        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead("C:\\Temp\\opencv\\hue_hist.jpg", ImreadModes.Color);
            if (image.Empty())
            {
                Console.WriteLine("이미지를 로드할 수 없습니다.");
                return;
            }

            Mat hsvImg = new Mat();
            Cv2.CvtColor(image, hsvImg, ColorConversionCodes.BGR2HSV);
            Mat[] hsvChannels = Cv2.Split(hsvImg);

            Mat histHue, histHueImg;
            CalcHisto(hsvChannels[0], out histHue, 18, 180);
            Size size = new Size(256, 200);
            DrawHistHue(histHue, out histHueImg, size);

            Cv2.ImShow("hist_hue_img", histHueImg);
            Cv2.ImShow("image", image);
            Cv2.WaitKey();
        }
    }
}
============================
using OpenCvSharp;

namespace histogram_stretching
{
    internal class Program
    {
        static void CalcHisto(Mat image, out Mat hist, int bins, int rangeMax = 256)
        {
            int[] histSize = { bins };
            Rangef[] ranges = { new Rangef(0, rangeMax) };
            int[] channels = { 0 };

            hist = new Mat();
            Cv2.CalcHist(new Mat[] { image }, channels, null, hist, 1, histSize, ranges);
        }

        static void DrawHisto(Mat hist, out Mat histImg, Size size)
        {
            histImg = new Mat(size, MatType.CV_8U, new Scalar(255));
            float bin = (float)histImg.Cols / hist.Rows;
            Cv2.Normalize(hist, hist, 0, size.Height, NormTypes.MinMax);

            for (int i = 0; i < hist.Rows; i++)
            {
                float idx1 = i * bin;
                float idx2 = (i + 1) * bin;
                Point pt1 = new Point((int)idx1, 0);
                Point pt2 = new Point((int)idx2, (int)hist.At<float>(i));

                if (pt2.Y > 0)
                    Cv2.Rectangle(histImg, pt1, pt2, Scalar.Black, -1);
            }
            Cv2.Flip(histImg, histImg, FlipMode.X);
        }

        static int SearchValueIdx(Mat hist, int bias = 0)
        {
            for (int i = 0; i < hist.Rows; i++)
            {
                int idx = Math.Abs(bias - i);
                if (hist.At<float>(idx) > 0) return idx;
            }
            return -1;
        }
        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead("c:\\Temp\\opencv\\histo_test.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                Console.WriteLine("앙 안됐띠.");
                return;
            }

            Mat hist, histDst, histImg, histDstImg;
            int histSize = 64, ranges = 256;
            CalcHisto(image, out hist, histSize, ranges);

            float binWidth = (float)ranges / histSize;
            int lowValue = (int)(SearchValueIdx(hist, 0) * binWidth);
            int highValue = (int)(SearchValueIdx(hist, hist.Rows - 1) * binWidth);
            Console.WriteLine($"high_value = {highValue}");
            Console.WriteLine($"low_value = {lowValue}");

            int dValue = highValue - lowValue;
            // Mat dst = new Mat();
            // Cv2.Multiply((image - lowValue), new Mat(image.Size(), MatType.CV_8U, new Scalar(255.0 / dValue)), dst);

            Mat dst = new Mat();
            // 이미지의 각 픽셀 값에서 최소값(lowValue)을 빼는 연산
            // 이는 이미지의 화소 값을 "lowValue"만큼 하향 시키는 것과 동일
            // 예를 들면, 최소 밝기 값을 0으로 만들기 위해 전체 이미지를 어둡게 만드는 효과를 가진다.
            Cv2.Subtract(image, new Scalar(lowValue), dst);
            // 빼기 연산 후, 결과 이미지의 화소 값을 스트레칭(stretching)한다.
            // 여기서 스트레칭이란, 이미지의 화소 값 범위를 조절하는 것을 의미한다.
            // (255.0 / dValue)는 스트레칭 비율을 의미하며, 이미지의 화소 값 범위를 [lowValue, highValue]에서 [0, 255]로 맞추기 위한 값이다.
            Cv2.Multiply(dst, new Scalar(255.0 / dValue), dst);
            // 255보다 큰 화소 값들을 255로 제한(클리핑)한다. 
            // 이렇게 함으로써, 이미지의 화소 값이 255를 초과하는 경우를 방지한다.
            Cv2.Threshold(dst, dst, 255, 255, ThresholdTypes.Trunc);
            // 0보다 작은 화소 값들을 0으로 제한(클리핑)한다.
            // 이렇게 함으로써, 이미지의 화소 값이 0 미만인 경우를 방지한다.
            Cv2.Threshold(dst, dst, 0, 0, ThresholdTypes.Tozero);

            CalcHisto(dst, out histDst, histSize, ranges);
            DrawHisto(hist, out histImg, new Size(256, 200));
            DrawHisto(histDst, out histDstImg, new Size(256, 200));

            Cv2.ImShow("image", image);
            Cv2.ImShow("dst-stretching", dst);
            Cv2.ImShow("img_hist", histImg);
            Cv2.ImShow("dst_hist", histDstImg);
            Cv2.WaitKey();
        }
    }
}
===========================
using OpenCvSharp;

namespace histogram_equalize
{
    internal class Program
    {
        // 이미지의 히스토그램을 계산하는 함수
        static void CalcHisto(Mat image, out Mat hist, Vec3i bins, Vec3f range)
        {
            // 각 채널 별 계급 개수 설정
            int[] histSize = { bins.Item0, bins.Item1, bins.Item2 };

            // 각 채널의 화소 범위 설정
            Rangef[] ranges = {
                new Rangef(0, range.Item0),
                new Rangef(0, range.Item1),
                new Rangef(0, range.Item2)
            };

            // 사용할 채널 목록 (0, 1, 2 채널)
            int[] channels = { 0, 1, 2 };

            hist = new Mat();

            // 히스토그램 계산
            Cv2.CalcHist(new Mat[] { image }, channels, null, hist, image.Channels(), histSize, ranges);
        }

        // 히스토그램을 그리는 함수
        static void DrawHisto(Mat hist, out Mat histImg, Size size)
        {
            // 히스토그램 이미지 초기화 (흰색 배경)
            histImg = new Mat(size, MatType.CV_8UC1, new Scalar(255));

            // 바(bar)의 폭 계산
            float bin = (float)histImg.Cols / hist.Rows;

            // 히스토그램 정규화 (0 ~ size.Height 사이의 값으로)
            Cv2.Normalize(hist, hist, 0, size.Height, NormTypes.MinMax);

            // 히스토그램 그리기
            for (int i = 0; i < hist.Rows; i++)
            {
                float idx1 = i * bin;
                float idx2 = (i + 1) * bin;
                Point pt1 = new Point(idx1, 0);
                Point pt2 = new Point(idx2, (int)hist.At<float>(i));

                if (pt2.Y > 0)
                    Cv2.Rectangle(histImg, pt1, pt2, Scalar.Black, -1); // 검은색 바 그리기
            }

            // 이미지 상하 반전 (y축을 기준으로)
            Cv2.Flip(histImg, histImg, FlipMode.X);
        }

        // 히스토그램 계산과 그리기를 한 번에 수행하는 함수
        static Mat CreateHist(Mat img, out Mat hist)
        {
            Mat histImg;
            Point3i histSize = new Point3i(256, 0, 0); // 히스토그램 크기 설정
            Point3f ranges = new Point3f(256, 0, 0);   // 히스토그램 범위 설정
            CalcHisto(img, out hist, histSize, ranges); // 히스토그램 계산
            DrawHisto(hist, out histImg, new Size(256, 200)); // 히스토그램 그리기

            return histImg;
        }
        static void Main(string[] args)
        {
            // 이미지 로드 (흑백 이미지로)
            Mat image = Cv2.ImRead("c:\\Temp\\opencv\\equalize_test.jpg", ImreadModes.Grayscale);
            if (image.Empty())
                throw new Exception("이미지를 로드하지 못했습니다.");

            Mat hist;
            Mat dst1 = new Mat();
            Mat dst2 = new Mat();

            // 원본 이미지의 히스토그램 생성
            Mat histImg1 = CreateHist(image, out hist);

            // 누적 히스토그램 초기화
            Mat accumHist = Mat.Zeros(hist.Size(), hist.Type());

            // 누적 히스토그램 계산
            accumHist.Set<float>(0, hist.At<float>(0));
            for (int i = 1; i < accumHist.Rows; i++)
                accumHist.Set<float>(i, accumHist.At<float>(i - 1) + hist.At<float>(i));

            // 누적 히스토그램 정규화
            Cv2.Normalize(accumHist, accumHist, 0, 255, NormTypes.MinMax);
            accumHist.ConvertTo(accumHist, MatType.CV_8U);

            // Look-Up Table을 이용한 히스토그램 평활화
            Cv2.LUT(image, accumHist, dst1);

            // OpenCV의 EqualizeHist 메소드를 사용한 히스토그램 평활화
            Cv2.EqualizeHist(image, dst2);

            // 평활화된 결과의 히스토그램 생성
            Mat histImg2 = CreateHist(dst1, out hist);
            Mat histImg3 = CreateHist(dst2, out hist);

            // 이미지와 히스토그램 표시
            Cv2.ImShow("image", image);
            Cv2.ImShow("dst1", dst1);
            Cv2.ImShow("dst2", dst2);
            Cv2.ImShow("histImg1", histImg1);
            Cv2.ImShow("histImg2", histImg2);
            Cv2.ImShow("histImg3", histImg3);

            Cv2.WaitKey();
        }
    }
}
=================================

    
