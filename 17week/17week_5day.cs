using OpenCvSharp;

namespace ConsoleApp31
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 3행 6열의 8비트 단일채널 행렬 초기화
            Mat m1 = new Mat(3, 6, MatType.CV_8UC1, Scalar.All(10));
            Mat m2 = new Mat(3, 6, MatType.CV_8UC1, Scalar.All(50));

            Mat m_add1 = new Mat();
            Mat m_add2 = new Mat();
            Mat m_sub = new Mat();
            Mat m_div1 = new Mat();
            Mat m_div2 = new Mat();
            Mat mask = new Mat(m1.Size(), MatType.CV_8UC1); // 마스크 행렬 (8비트 단일채널)

            // 관심영역 지정 (3,0)에서 (6,3)까지의 사각형 영역
            Rect rect = new Rect(new Point(3, 0), new Size(3, 3));
            mask.SetTo(0);
            mask[rect].SetTo(1);  // 마스크 영역을 1로 설정

            // 행렬 덧셈
            Cv2.Add(m1, m2, m_add1);
            Cv2.Add(m1, m2, m_add2, mask); // 마스크를 사용한 덧셈

            // 나눗셈
            Cv2.Divide(m1, m2, m_div1);
            m1.ConvertTo(m1, MatType.CV_32F);
            m2.ConvertTo(m2, MatType.CV_32F);
            Cv2.Divide(m1, m2, m_div2);

            // 결과 출력
            Console.WriteLine("m1 = \n" + m1.Dump());
            Console.WriteLine("m2 = \n" + m2.Dump());
            Console.WriteLine("mask = \n" + mask.Dump());
            Console.WriteLine("m_add1 = \n" + m_add1.Dump());
            Console.WriteLine("m_add2 = \n" + m_add2.Dump());
            Console.WriteLine("m_div1 = \n" + m_div1.Dump());
            Console.WriteLine("m_div2 = \n" + m_div2.Dump());
        }
    }
}
==============================
using OpenCvSharp;

namespace ConsoleApp31
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<float> v1 = new List<float> { 1, 2, 3 };
            List<float> v_exp = new List<float>();
            List<float> v_log = new List<float>();


            //Mat m1 = new Mat(1, 5, MatType.CV_32F, new float[] { 1, 2, 3, 5, 10 });
            Mat m1 = new Mat(1, 5, MatType.CV_32F);
            float[] data = { 1.0f, 2.0f, 3.0f, 5.0f, 10.0f };
            for (int i = 0; i < data.Length; i++)
            {
                m1.Set<float>(0, i, data[i]);
            }

            Mat m_exp = new Mat();
            Mat m_sqrt = new Mat();
            Mat m_pow = new Mat();

            // 벡터에 대한 exp 연산
            v_exp = v1.ConvertAll(x => (float)Math.Exp(x));

            // 행렬에 대한 exp 연산
            Cv2.Exp(m1, m_exp);

            // 행렬에 대한 log 연산 (출력은 리스트)
            for (int i = 0; i < m1.Cols; i++)
            {
                float val = m1.At<float>(0, i);
                v_log.Add((float)Math.Log(val));
            }

            // sqrt, pow 연산
            Cv2.Sqrt(m1, m_sqrt);
            Cv2.Pow(m1, 3, m_pow);

            // 출력
            Console.WriteLine("[m1] = \n" + m1.Dump() + "\n");
            Console.WriteLine("[v_exp] = " + String.Join(", ", v_exp));
            Console.WriteLine("[m_exp] = \n" + m_exp.Dump());
            Console.WriteLine("[v_log] = " + String.Join(", ", v_log) + "\n");
            Console.WriteLine("[m_sqrt] = \n" + m_sqrt.Dump());
            Console.WriteLine("[m_pow] = \n" + m_pow.Dump());
        }
    }
}
===========================
using OpenCvSharp;

namespace BitwiseOperationOpenCV
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat image1 = new Mat(250, 250, MatType.CV_8UC1, Scalar.All(0));
            Mat image2 = new Mat(250, 250, MatType.CV_8UC1, Scalar.All(100));
            Mat image3 = new Mat();
            Mat image4 = new Mat();
            Mat image5 = new Mat();
            Mat image6 = new Mat();

            Point center = new Point(image1.Width / 2, image1.Height / 2);
            Cv2.Circle(image1, center, 80, Scalar.All(255), -1);
            Cv2.Rectangle(image2, new Point(0, 0), new Point(125, 250), Scalar.All(255), -1);

            // Bitwise operations
            Cv2.BitwiseOr(image1, image2, image3);
            Cv2.BitwiseAnd(image1, image2, image4);
            Cv2.BitwiseXor(image1, image2, image5);
            Cv2.BitwiseNot(image1, image6);

            Cv2.ImShow("image1", image1);
            Cv2.ImShow("image2", image2);
            Cv2.ImShow("bitwise_or", image3);
            Cv2.ImShow("bitwise_and", image4);
            Cv2.ImShow("bitwise_xor", image5);
            Cv2.ImShow("bitwise_not", image6);
            Cv2.WaitKey(0);
        }
    }
}
==============================
using OpenCvSharp;

namespace mat12.Bitwise_Overlap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 이미지 파일 경로
            string imagePath = @"C:/Temp/opencv/bit_test.jpg";
            string logoPath = @"C:/Temp/opencv/logo.jpg";

            // 이미지 읽기
            Mat image = Cv2.ImRead(imagePath, ImreadModes.Color);
            Mat logo = Cv2.ImRead(logoPath, ImreadModes.Color);

            // 예외 처리
            if (image.Empty() || logo.Empty())
            {
                throw new Exception("이미지를 불러올 수 없습니다.");
            }

            // 결과 행렬 선언
            Mat logoTh = new Mat();
            Mat[] masks;

            // 로고 영상 이진화
            Cv2.Threshold(logo, logoTh, 70, 255, ThresholdTypes.Binary);

            // 로고 영상 채널 분리 (분리된 채널 수 확인)
            masks = Cv2.Split(logoTh); //알파채널이 분리가 안됨

            // 채널 수 확인
            if (masks.Length < 3)
            {
                throw new Exception("로고 이미지는 최소한 3채널이어야 합니다.");
                //확인해 보면 3개만 분리되고 mask[3]이 분리되어 나오지 않음!!!
                //masks.Length를 4로 해보고나 masks.Length == 3으로 해보면 확인할 수 있음
            }

            // 전경 통과 마스크
            Mat mask1 = new Mat();
            Cv2.ImShow("mask0", masks[0]);
            Cv2.ImShow("mask1", masks[1]);
            Cv2.ImShow("mask2", masks[2]);
            //Cv2.ImShow("mask3", masks[3]); //분리되지 않으면 당연히 안나옵니다. 

            Cv2.BitwiseOr(masks[0], masks[1], mask1);
            Mat mask3 = new Mat();
            Cv2.BitwiseOr(masks[2], mask1, mask3);
            Cv2.ImShow("3개 mask합성", mask3);

            // 배경 통과 마스크
            Mat notMask = new Mat(mask1.Size(), MatType.CV_8UC1, new Scalar(255));
            Cv2.BitwiseNot(mask3, notMask);
            Cv2.ImShow("원본로고", logo);
            Cv2.ImShow("bitwiseNot", notMask);

            // 영상 중심 좌표 계산
            Point center1 = new Point(image.Width / 2, image.Height / 2);  // 이미지 중심 좌표
            Point center2 = new Point(logo.Width / 2, logo.Height / 2);    // 로고 중심 좌표
            Point start = new Point(center1.X - center2.X, center1.Y - center2.Y); // 시작 좌표

            // 로고가 위치할 관심 영역 설정
            Rect roi = new Rect(start, logo.Size());

            // 비트 곱과 마스킹을 이용한 관심 영역의 복사
            Mat background = new Mat();
            Mat foreground = new Mat();
            Cv2.BitwiseAnd(logo, logo, foreground, mask3);
            Cv2.BitwiseAnd(new Mat(image, roi), new Mat(image, roi), background, notMask);

            // 전경과 배경 합성
            Mat dst = new Mat();
            Cv2.Add(background, foreground, dst);

            // 합성된 이미지를 원본 이미지의 관심영역에 복사
            dst.CopyTo(new Mat(image, roi));

            // 결과 이미지 출력
            Cv2.ImShow("background", background);
            Cv2.ImShow("foreground", foreground);
            Cv2.ImShow("dst", dst);
            Cv2.ImShow("image", image);
            Cv2.WaitKey(0);
        }
    }
}
==================================
using OpenCvSharp;

namespace Sudoku
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 이미지 파일 경로
            string imagePath = @"c:/temp/opencv/sudoku.png";

            // 이미지를 그레이스케일로 읽기
            Mat src = Cv2.ImRead(imagePath, ImreadModes.Grayscale);
            Cv2.ImShow("src", src);

            // Otsu의 이진화 적용
            Mat dst = new Mat();
            Cv2.Threshold(src, dst, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);
            Cv2.ImShow("dst", dst);

            // 적응형 이진화 (Mean C)
            Mat dst2 = new Mat();
            Cv2.AdaptiveThreshold(src, dst2, 255, AdaptiveThresholdTypes.MeanC, ThresholdTypes.Binary, 51, 7);
            Cv2.ImShow("dst2", dst2);

            // 적응형 이진화 (Gaussian C)
            Mat dst3 = new Mat();
            Cv2.AdaptiveThreshold(src, dst3, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 51, 7);
            Cv2.ImShow("dst3", dst3);

            // 대기 및 창 닫기
            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }
    }
}
=========================
// 이미지 파일 경로
            string imagePath1 = @"c:/Temp/opencv/abs_test1.jpg";
            string imagePath2 = @"c:/Temp/opencv/abs_test2.jpg";

            // 이미지를 그레이스케일로 읽기
            Mat image1 = Cv2.ImRead(imagePath1, ImreadModes.Grayscale);
            Mat image2 = Cv2.ImRead(imagePath2, ImreadModes.Grayscale);

            // 예외 처리
            if (image1.Empty() || image2.Empty())
            {
                throw new Exception("이미지를 불러올 수 없습니다.");
            }

            // 결과 행렬 선언
            Mat difImg = new Mat();
            Mat absDif1 = new Mat();
            Mat absDif2 = new Mat();

            // 이미지 타입을 CV_16S로 변환
            image1.ConvertTo(image1, MatType.CV_16S);
            image2.ConvertTo(image2, MatType.CV_16S);

            // 두 이미지의 차이를 계산
            Cv2.Subtract(image1, image2, difImg);

            // 관심 영역 출력
            Rect roi = new Rect(10, 10, 7, 3);
            Console.WriteLine("[difImg] = \n" + difImg[roi]);

            // 차이 이미지의 절대값 계산
            absDif1 = Cv2.Abs(difImg);

            // 이미지 타입을 다시 CV_8U로 변환
            image1.ConvertTo(image1, MatType.CV_8U);
            image2.ConvertTo(image2, MatType.CV_8U);
            difImg.ConvertTo(difImg, MatType.CV_8U);
            absDif1.ConvertTo(absDif1, MatType.CV_8U);

            // 두 이미지의 절대 차이 계산
            Cv2.Absdiff(image1, image2, absDif2);

            // 관심 영역 출력
            Console.WriteLine("[difImg] = \n" + difImg[roi] + "\n");
            Console.WriteLine("[absDif1] = \n" + absDif1[roi]);
            Console.WriteLine("[absDif2] = \n" + absDif2[roi]);

            // 결과 이미지 출력
            Cv2.ImShow("image1", image1);
            Cv2.ImShow("image2", image2);
            Cv2.ImShow("difImg", difImg);
            Cv2.ImShow("absDif1", absDif1);
            Cv2.ImShow("absDif2", absDif2);

            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
================================
using OpenCvSharp;

namespace CVAutoConstrast
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 이미지 파일 경로
            string imagePath = @"c:/Temp/opencv/minMax.jpg";

            // 이미지를 그레이스케일로 읽기
            Mat image = Cv2.ImRead(imagePath, ImreadModes.Grayscale);

            // 예외 처리
            if (image.Empty())
            {
                throw new Exception("이미지를 불러올 수 없습니다.");
            }

            // 최소값, 최대값 찾기
            double minVal, maxVal;
            Cv2.MinMaxIdx(image, out minVal, out maxVal);

            // 최소값과 최대값을 사용하여 이미지 정규화
            double ratio = (maxVal - minVal) / 255.0;
            Mat dst = new Mat();
            image.ConvertTo(dst, MatType.CV_64F);  // 계산을 위해 이미지 타입을 CV_64F로 변환
            Cv2.Subtract(dst, new Scalar(minVal), dst);  // dst에서 minVal을 빼기

            // 나누기 연산 수행
            Cv2.Divide(dst, new Scalar(ratio), dst);  // ratio로 나누기

            // 계산된 결과를 다시 CV_8U로 변환하여 시각화
            dst.ConvertTo(dst, MatType.CV_8U);

            // 결과 출력
            Console.WriteLine("최소값  = " + minVal);
            Console.WriteLine("최대값  = " + maxVal);

            // 이미지 출력
            Cv2.ImShow("image", image);
            Cv2.ImShow("dst", dst);
            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }
    }
}
================================
 
