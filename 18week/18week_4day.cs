using OpenCvSharp;

namespace _01.Bluring
{
    internal class Program
    {
        static void Filter(Mat img, out Mat dst, Mat mask)
        {
            dst = new Mat(img.Size(), MatType.CV_32F, Scalar.All(0));
            //Point h_m = mask.Size() / 2; 
            Point h_m = new Point(mask.Width / 2, mask.Height / 2);

            for (int i = h_m.Y; i < img.Rows - h_m.Y; i++)
            {
                for (int j = h_m.X; j < img.Cols - h_m.X; j++)
                {
                    float sum = 0;

                    for (int u = 0; u < mask.Rows; u++)
                    {
                        for (int v = 0; v < mask.Cols; v++)
                        {
                            int y = i + u - h_m.Y;
                            int x = j + v - h_m.X;
                            sum += mask.At<float>(u, v) * img.At<byte>(y, x);
                        }
                    }

                    dst.Set<float>(i, j, sum);
                }
            }
        }
        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/filter_blur.jpg";
            Mat src = Cv2.ImRead(path, ImreadModes.Grayscale);

            if (src.Empty())
                throw new Exception("이미지 로딩에 실패");

            float[] data1 =
            {
                1/9f, 1/9f, 1/9f,
                1/9f, 1/9f, 1/9f,
                1/9f, 1/9f, 1/9f
            };
            float[] data2 =
            {
                1/25f, 1/25f, 1/25f, 1/25f, 1/25f,
                1/25f, 1/25f, 1/25f, 1/25f, 1/25f,
                1/25f, 1/25f, 1/25f, 1/25f, 1/25f,
                1/25f, 1/25f, 1/25f, 1/25f, 1/25f,
                1/25f, 1/25f, 1/25f, 1/25f, 1/25f
            };

            //Mat mask = new Mat(3, 3, MatType.CV_32F, data1); //Error
            //직접 필터의 값을 넣어 봅니다.
            Mat mask = new Mat(5, 5, MatType.CV_32F);

            for (int i = 0; i < mask.Rows; i++)
            {
                for (int j = 0; j < mask.Cols; j++)
                {
                    mask.Set<float>(i, j, data2[i * mask.Cols + j]);
                }
            }


            Filter(src, out Mat blur, mask);

            blur.ConvertTo(blur, MatType.CV_8U);

            Cv2.ImShow("src", src);
            Cv2.ImShow("blur", blur);

            Cv2.WaitKey();
        }
    }
}
-----------------------------------------------
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluringDirect
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/filter_blur.jpg";
            Mat src = Cv2.ImRead(path, ImreadModes.Grayscale);

            if (src.Empty())
                throw new Exception("이미지 문제발생");

            Mat blur = new Mat();

            // OpenCV 함수인 GaussianBlur를 사용하여 블러링 처리
            Cv2.GaussianBlur(src, blur, new Size(3, 3), 0); //Size 함수의 3을 5로 바꾸면 5 * 5 필터가 되고 1/25f가 입력됨

            Cv2.ImShow("src", src);
            Cv2.ImShow("blur", blur);

            Cv2.WaitKey();
        }
    }
}
===============================================
using OpenCvSharp;

namespace Sharpening
{
    internal class Program
    {
        static void Filter(Mat img, out Mat dst, Mat mask)
        {
            dst = new Mat(img.Size(), MatType.CV_32F, Scalar.All(0));
            Point h_m = new Point(mask.Width / 2, mask.Height / 2);

            for (int i = h_m.Y; i < img.Rows - h_m.Y; i++)
            {
                for (int j = h_m.X; j < img.Cols - h_m.X; j++)
                {
                    float sum = 0;

                    for (int u = 0; u < mask.Rows; u++)
                    {
                        for (int v = 0; v < mask.Cols; v++)
                        {
                            int y = i + u - h_m.Y;
                            int x = j + v - h_m.X;
                            sum += mask.At<float>(u, v) * img.At<byte>(y, x);
                        }
                    }

                    dst.Set<float>(i, j, sum);
                }
            }
        }
        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/filter_sharpen.jpg";
            Mat src = Cv2.ImRead(path, ImreadModes.Grayscale);

            if (src.Empty())
                throw new Exception("Failed to load image");

            float[] data1 =
            {
                0, -1, 0,
                -1, 5, -1,
                0, -1, 0
            };

            float[] data2 =
            {
                -1, -1, -1,
                -1, 9, -1,
                -1, -1, -1
            };

            Mat mask1 = new Mat(3, 3, MatType.CV_32F);
            Mat mask2 = new Mat(3, 3, MatType.CV_32F);

            // data1 값을 mask1에 설정
            for (int i = 0; i < mask1.Rows; i++)
            {
                for (int j = 0; j < mask1.Cols; j++)
                {
                    mask1.Set<float>(i, j, data1[i * mask1.Cols + j]);
                }
            }

            // data2 값을 mask2에 설정
            for (int i = 0; i < mask2.Rows; i++)
            {
                for (int j = 0; j < mask2.Cols; j++)
                {
                    mask2.Set<float>(i, j, data2[i * mask2.Cols + j]);
                }
            }

            Filter(src, out Mat sharpen1, mask1);
            Filter(src, out Mat sharpen2, mask2);

            sharpen1.ConvertTo(sharpen1, MatType.CV_8U);
            sharpen2.ConvertTo(sharpen2, MatType.CV_8U);

            Cv2.ImShow("sharpen1", sharpen1);
            Cv2.ImShow("sharpen2", sharpen2);
            Cv2.ImShow("src", src);

            Cv2.WaitKey();
        }
    }
}
-------------------------------------------------
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeningDirect
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/filter_sharpen.jpg";
            Mat src = Cv2.ImRead(path, ImreadModes.Grayscale);

            if (src.Empty())
                throw new Exception("Failed to load image");

            float[] data1 =
            {
                0, -1, 0,
                -1, 5, -1,
                0, -1, 0
            };

            float[] data2 =
            {
                -1, -1, -1,
                -1, 9, -1,
                -1, -1, -1
            };

            Mat mask1 = new Mat(3, 3, MatType.CV_32F);
            Mat mask2 = new Mat(3, 3, MatType.CV_32F);

            // data1 값을 mask1에 설정
            for (int i = 0; i < mask1.Rows; i++)
            {
                for (int j = 0; j < mask1.Cols; j++)
                {
                    mask1.Set<float>(i, j, data1[i * mask1.Cols + j]);
                }
            }

            // data2 값을 mask2에 설정
            for (int i = 0; i < mask2.Rows; i++)
            {
                for (int j = 0; j < mask2.Cols; j++)
                {
                    mask2.Set<float>(i, j, data2[i * mask2.Cols + j]);
                }
            }

            Mat sharpen1 = new Mat();
            Mat sharpen2 = new Mat();

            Cv2.Filter2D(src, sharpen1, MatType.CV_32F, mask1);
            Cv2.Filter2D(src, sharpen2, MatType.CV_32F, mask2);

            // 결과를 CV_8U로 변환
            sharpen1.ConvertTo(sharpen1, MatType.CV_8U);
            sharpen2.ConvertTo(sharpen2, MatType.CV_8U);

            Cv2.ImShow("sharpen1", sharpen1);
            Cv2.ImShow("sharpen2", sharpen2);
            Cv2.ImShow("src", src);

            Cv2.WaitKey();
        }
    }
}
==================================================
using OpenCvSharp;

namespace DifferenceOperationEdge
{
    internal class Program
    {
        static void DifferOp(Mat img, out Mat dst, int maskSize)
        {
            dst = new Mat(img.Size(), MatType.CV_8U, Scalar.All(0));
            Point h_m = new Point(maskSize / 2, maskSize / 2);

            for (int i = h_m.Y; i < img.Rows - h_m.Y; i++)
            {
                for (int j = h_m.X; j < img.Cols - h_m.X; j++)
                {
                    List<byte> mask = new List<byte>();

                    for (int u = 0; u < maskSize; u++)
                    {
                        for (int v = 0; v < maskSize; v++)
                        {
                            int y = i + u - h_m.Y;
                            int x = j + v - h_m.X;
                            mask.Add(img.At<byte>(y, x));
                        }
                    }

                    byte max = 0;
                    for (int k = 0; k < mask.Count / 2; k++)
                    {
                        int start = mask[k];
                        int end = mask[mask.Count - k - 1];

                        byte difference = (byte)Math.Abs(start - end);
                        if (difference > max) max = difference;
                    }

                    dst.Set<byte>(i, j, max);
                }
            }
        }
        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/edge_test.jpg";
            Mat src = Cv2.ImRead(path, ImreadModes.Grayscale);

            if (src.Empty())
                throw new Exception("Failed to load image");

            Mat edge;
            DifferOp(src, out edge, 3);

            // 결과 출력
            Cv2.ImShow("src", src);
            Cv2.ImShow("edge", edge);

            Cv2.WaitKey();
        }
    }
}
===================================================
using OpenCvSharp;

namespace SobelEdge
{
    internal class Program
    {
        static void Differential(Mat image, out Mat dst, float[] data1, float[] data2)
        {
            Mat mask1 = new Mat(3, 3, MatType.CV_32F);
            Mat mask2 = new Mat(3, 3, MatType.CV_32F);

            // data1 값을 mask1에 설정
            for (int i = 0; i < mask1.Rows; i++)
            {
                for (int j = 0; j < mask1.Cols; j++)
                {
                    mask1.Set<float>(i, j, data1[i * mask1.Cols + j]);
                }
            }

            // data2 값을 mask2에 설정
            for (int i = 0; i < mask2.Rows; i++)
            {
                for (int j = 0; j < mask2.Cols; j++)
                {
                    mask2.Set<float>(i, j, data2[i * mask2.Cols + j]);
                }
            }

            Mat dst1 = new Mat();
            Mat dst2 = new Mat();
            dst = new Mat();

            Cv2.Filter2D(image, dst1, MatType.CV_32F, mask1);  // OpenCV 제공 회선 함수
            Cv2.Filter2D(image, dst2, MatType.CV_32F, mask2);
            Cv2.Magnitude(dst1, dst2, dst);
            dst.ConvertTo(dst, MatType.CV_8U);

            Cv2.ConvertScaleAbs(dst1, dst1);  // 절대값 및 형변환 동시 수행 함수
            Cv2.ConvertScaleAbs(dst2, dst2);
            Cv2.ImShow("dst1 - 수직 마스크", dst1);
            Cv2.ImShow("dst2 - 수평 마스크", dst2);
        }
        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/edge_test1.jpg";
            Mat image = Cv2.ImRead(path, ImreadModes.Grayscale);

            if (image.Empty())
                throw new Exception("Failed to load image");

            float[] data1 =
            {
                -1, 0, 1,
                -2, 0, 2,
                -1, 0, 1
            };
            float[] data2 =
            {
                -1, -2, -1,
                0, 0, 0,
                1, 2, 1
            };

            Mat dst;
            Differential(image, out dst, data1, data2);  // 두 방향 소벨 회선 및 크기 계산

            // OpenCV 제공 소벨 에지 계산
            Mat dst3 = new Mat();
            Mat dst4 = new Mat();

            Cv2.Sobel(image, dst3, MatType.CV_32F, 1, 0, 3);  // x방향 미분 - 수직 마스크
            Cv2.Sobel(image, dst4, MatType.CV_32F, 0, 1, 3);  // y방향 미분 - 수평 마스크
            Cv2.ConvertScaleAbs(dst3, dst3);  // 절대값 및 uchar 형변환
            Cv2.ConvertScaleAbs(dst4, dst4);

            Cv2.ImShow("image", image);
            Cv2.ImShow("소벨에지", dst);
            Cv2.ImShow("dst3-수직_OpenCV", dst3);
            Cv2.ImShow("dst4-수평_OpenCV", dst4);

            Cv2.WaitKey();
        }
    }
}
==================================================
using OpenCvSharp;

namespace LogDogEdge
{
    internal class Program
    {
        static Mat GetLoGMask(Size size, double sigma)
        {
            double ratio = 1 / (Math.PI * Math.Pow(sigma, 4.0));
            int center = size.Height / 2;
            Mat dst = new Mat(size, MatType.CV_64F);

            for (int i = 0; i < size.Height; i++)
            {
                for (int j = 0; j < size.Width; j++)
                {
                    int x2 = (j - center) * (j - center);
                    int y2 = (i - center) * (i - center);

                    double value = (x2 + y2) / (2 * sigma * sigma);
                    dst.Set(i, j, -ratio * (1 - value) * Math.Exp(-value));
                }
            }

            double scale = (center * 10 / ratio);
            dst *= scale;
            return dst;
        }
        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/laplacian_test.jpg";
            Mat image = Cv2.ImRead(path, ImreadModes.Grayscale);

            if (image.Empty())
                throw new Exception("Failed to load image");

            //sigma값이 작으면 필터는 더 날카로워지고 커지면 더 부드럽게 되어 넓은 영역을 흐리게 함 
            double sigma = 1.4;
            Mat logMask = GetLoGMask(new Size(9, 9), sigma);

            Console.WriteLine(logMask);
            Console.WriteLine(Cv2.Sum(logMask));

            Mat dst1 = new Mat();
            Mat dst2 = new Mat();
            Mat gausImg = new Mat();

            // LoG 필터 적용
            Cv2.Filter2D(image, dst1, MatType.CV_32F, logMask);
            Cv2.GaussianBlur(image, gausImg, new Size(9, 9), sigma, sigma);
            Cv2.Laplacian(gausImg, dst2, MatType.CV_32F, 5);

            // DoG 계산
            Mat dst3 = new Mat();
            Mat dst4 = new Mat();
            Cv2.GaussianBlur(image, dst3, new Size(1, 1), 0.0);
            Cv2.GaussianBlur(image, dst4, new Size(9, 9), 1.6);
            Mat dstDoG = new Mat();
            Cv2.Subtract(dst3, dst4, dstDoG);

            Cv2.Normalize(dstDoG, dstDoG, 0, 255, NormTypes.MinMax);

            // 결과 출력
            Cv2.ImShow("image", image);
            Cv2.ImShow("dst1 - LoG_filter2D", dst1);
            Cv2.ImShow("dst2 - LOG_OpenCV", dst2);
            Cv2.ImShow("dst_DoG - DOG_OpenCV", dstDoG);
            Cv2.WaitKey();
        }
    }
}
==================================================
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFomBasicCamera
{
    public partial class Form1 : Form
    {
        private VideoCapture capture;  // 카메라 캡처 객체
        private Mat frame;             // 현재 프레임을 저장할 객체
        private bool isRunning = false;  // 카메라가 실행 중인지 확인하는 변수
        private enum CVMode { COLOR, BLACK, BGR, BLUR, SAPEN, SOBEL};     // 컬러 모드인지 확인하는 변수
        private CVMode currentMode = CVMode.COLOR;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            capture = new VideoCapture(0);  // 카메라 장치 연결
            frame = new Mat();
            capture.Set(VideoCaptureProperties.FrameWidth, 640);  // 프레임 너비 설정
            capture.Set(VideoCaptureProperties.FrameHeight, 480); // 프레임 높이 설정

            btnStart_Click(sender, e);

        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (isRunning)  // 이미 카메라가 실행 중이면
            {
                isRunning = false;  // 실행 중 상태를 false로 변경
                btnStart.Text = "카메라 시작";  // 버튼 텍스트 변경
                return;
            }

            btnStart.Text = "카메라 멈춤";  // 버튼 텍스트 변경
            isRunning = true;  // 실행 중 상태를 true로 변경

            while (isRunning)  // 카메라가 실행 중이면
            {
                if (capture.IsOpened())  // 카메라가 연결되어 있으면
                {
                    capture.Read(frame);  // 프레임 읽기

                    switch(currentMode)
                    {
                        case CVMode.COLOR:
                            //
                            break;
                        case CVMode.BLACK:
                            Cv2.CvtColor(frame, frame, ColorConversionCodes.BGR2GRAY);  
                            break;
                        case CVMode.BGR:
                            Cv2.CvtColor(frame, frame, ColorConversionCodes.RGB2BGR);
                            break;
                        case CVMode.BLUR:
                            Cv2.GaussianBlur(frame, frame, new OpenCvSharp.Size(25, 25), 0);
                            break;
                        case CVMode.SAPEN:
                            float[,] mask = new float[,]
                            {
                                {0, -1, 0 },
                                {-1, 5, -1 },
                                {0, -1, 0 },
                            };
                            Mat kernel = new Mat(3, 3, MatType.CV_32F);
                            for(int i=0; i<kernel.Rows; i++)
                            {
                                for(int j=0; j<kernel.Cols; j++)
                                {
                                    kernel.Set(i, j, mask[i, j]);
                                }
                            }
                            Cv2.Filter2D(frame, frame, frame.Depth(), kernel);
                            break;
                        case CVMode.SOBEL:
                            Mat gradX = new Mat();
                            Mat gradY = new Mat();
                            Mat absGradX = new Mat();
                            Mat absGradY = new Mat();
                            
                            Cv2.Sobel(frame, gradX, MatType.CV_16S, 1, 0, 3);
                            Cv2.Sobel(frame, gradY, MatType.CV_16S, 0, 1, 3);

                            //절대값 변환
                            Cv2.ConvertScaleAbs(gradX, absGradX);
                            Cv2.ConvertScaleAbs(gradY, absGradY);

                            //x축 y축 경계 결과 합침
                            Cv2.AddWeighted(absGradX, 0.6, absGradY, 0.6, 0, frame);
                            break;
                    }

                    ///////////////
                    Mat[] channels = Cv2.Split(frame);
                    for (int i = 0; i < channels.Length; i++)
                    {
                        double minVal, maxVal;
                        Cv2.MinMaxIdx(channels[i], out minVal, out maxVal);
                        double ratio = (maxVal - minVal) / 320.0;
                        Cv2.Subtract(channels[i], new Scalar(minVal), channels[i]);
                        Cv2.Divide(channels[i], new Scalar(ratio), channels[i]);
                        channels[i].ConvertTo(channels[i], MatType.CV_8U);
                    }

                    Mat dst = new Mat();
                    Cv2.Merge(channels, dst);
                    ///////////////
                    pictureBox1.Image = BitmapConverter.ToBitmap(dst);  // PictureBox에 영상 출력
                }
                await Task.Delay(33);  // 대략 30 fps
            }
        }

        private void btnDispose_Click(object sender, EventArgs e)
        {
            isRunning = false;  // 카메라 중지
            capture.Release();  // 카메라 자원 해제
            Dispose();       // 프로그램 종료
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            currentMode = CVMode.BLACK;
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            currentMode = CVMode.COLOR;
        }

        private void btnBGR_Click(object sender, EventArgs e)
        {
            currentMode = CVMode.BGR;
        }

        private void btnBlur_Click(object sender, EventArgs e)
        {
            currentMode= CVMode.BLUR;
        }

        private void btnSharpen_Click(object sender, EventArgs e)
        {
            currentMode = CVMode.SAPEN;
        }

        private void btnSobel_Click(object sender, EventArgs e)
        {
            currentMode = CVMode.SOBEL;
        }
    }
}
================================================
using OpenCvSharp;

namespace CannyEgdeAlgorithm
{
    public static class CvUtils
    {
        // 이 함수는 각 픽셀의 x 및 y 방향 그레디언트를 이용해 경사 방향을 계산합니다.
        // 계산된 방향은 4개의 범주 중 하나로 양자화됩니다 (0, 1, 2, 3).
        public static void CalcDirect(Mat Gy, Mat Gx, out Mat direct)
        {
            direct = new Mat(Gy.Size(), MatType.CV_8U);

            for (int i = 0; i < direct.Rows; i++)
            {
                for (int j = 0; j < direct.Cols; j++)
                {
                    float gx = Gx.At<float>(i, j);
                    float gy = Gy.At<float>(i, j);
                    int theta = (int)(Cv2.FastAtan2(gy, gx) / 45);
                    direct.Set(i, j, (byte)(theta % 4));
                }
            }
        }

        // 이 함수는 그레디언트 크기 이미지 (sobel)에서 비최대값 억제를 수행하여 에지를 얇게 만듭니다.
        // 현재 픽셀이 그레디언트 방향에서 지역 최대값인지 확인하고, 최대값이 아닌 경우 억제합니다.
        public static void SuppNonMax(Mat sobel, Mat direct, out Mat dst)
        {
            dst = new Mat(sobel.Size(), MatType.CV_32F, Scalar.All(0));

            for (int i = 1; i < sobel.Rows - 1; i++)
            {
                for (int j = 1; j < sobel.Cols - 1; j++)
                {
                    int dir = direct.At<byte>(i, j);
                    float v1, v2;
                    if (dir == 0)
                    {
                        v1 = sobel.At<float>(i, j - 1);
                        v2 = sobel.At<float>(i, j + 1);
                    }
                    else if (dir == 1)
                    {
                        v1 = sobel.At<float>(i + 1, j + 1);
                        v2 = sobel.At<float>(i - 1, j - 1);
                    }
                    else if (dir == 2)
                    {
                        v1 = sobel.At<float>(i - 1, j);
                        v2 = sobel.At<float>(i + 1, j);
                    }
                    else
                    {
                        v1 = sobel.At<float>(i + 1, j - 1);
                        v2 = sobel.At<float>(i - 1, j + 1);
                    }

                    float center = sobel.At<float>(i, j);
                    dst.Set(i, j, (center > v1 && center > v2) ? center : 0);
                }
            }
        }

        // 이 함수는 히스테리시스에 의한 에지 추적을 수행합니다.
        // 강한 에지를 따라 재귀적으로 추적하며 약한 에지들을 연결합니다.
        public static void Trace(Mat maxSo, Mat posCk, Mat hyImg, Point pt, float low)
        {
            Rect rect = new Rect(new Point(0, 0), posCk.Size());
            if (!rect.Contains(pt)) return;

            if (posCk.At<byte>(pt.Y, pt.X) == 0 && maxSo.At<float>(pt.Y, pt.X) > low)
            {
                posCk.Set(pt.Y, pt.X, (byte)1);
                hyImg.Set(pt.Y, pt.X, (byte)255);

                Trace(maxSo, posCk, hyImg, pt + new Point(-1, -1), low);
                Trace(maxSo, posCk, hyImg, pt + new Point(0, -1), low);
                Trace(maxSo, posCk, hyImg, pt + new Point(1, -1), low);
                Trace(maxSo, posCk, hyImg, pt + new Point(-1, 0), low);
                Trace(maxSo, posCk, hyImg, pt + new Point(1, 0), low);
                Trace(maxSo, posCk, hyImg, pt + new Point(-1, 1), low);
                Trace(maxSo, posCk, hyImg, pt + new Point(0, 1), low);
                Trace(maxSo, posCk, hyImg, pt + new Point(1, 1), low);
            }
        }

        // 이 함수는 비최대 억제를 거친 그레디언트 크기 이미지 (maxSo)에 대해 히스테리시스 임계값을 적용합니다.
        // 강한 에지를 찾고, 이를 기준으로 낮은 임계값 이상의 연결된 에지를 추적합니다.
        public static void HysteresisTh(Mat maxSo, out Mat hyImg, float low, float high)
        {
            Mat posCk = new Mat(maxSo.Size(), MatType.CV_8U, Scalar.All(0));
            hyImg = new Mat(maxSo.Size(), MatType.CV_8U, Scalar.All(0));

            for (int i = 0; i < maxSo.Rows; i++)
            {
                for (int j = 0; j < maxSo.Cols; j++)
                {
                    if (maxSo.At<float>(i, j) > high)
                    {
                        Trace(maxSo, posCk, hyImg, new Point(j, i), low);
                    }
                }
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/cannay_tset.jpg";
            Mat image = Cv2.ImRead(path, ImreadModes.Grayscale);

            if (image.Empty())
                throw new Exception("Failed to load image");

            Mat gauImg = new Mat();
            Mat Gx = new Mat();
            Mat Gy = new Mat();
            Mat direct = new Mat();
            Mat sobel = new Mat();
            Mat maxSobel = new Mat();
            Mat dst = new Mat();
            Mat canny = new Mat();

            Cv2.GaussianBlur(image, gauImg, new Size(5, 5), 0.3);
            Cv2.Sobel(gauImg, Gx, MatType.CV_32F, 1, 0, 3);
            Cv2.Sobel(gauImg, Gy, MatType.CV_32F, 0, 1, 3);
            Cv2.Magnitude(Gx, Gy, sobel);

            CvUtils.CalcDirect(Gy, Gx, out direct);
            CvUtils.SuppNonMax(sobel, direct, out maxSobel);
            CvUtils.HysteresisTh(maxSobel, out dst, 100, 150);

            Cv2.Canny(image, canny, 100, 150);

            Cv2.ImShow("image", image);
            Cv2.ImShow("canny", dst);
            Cv2.ImShow("OpenCV_canny", canny);
            Cv2.WaitKey();
        }
    }
}
---------------------------------------------------------
using OpenCvSharp;

namespace CannyEgdeAlgorithm
{
    public static class CvUtils
    {
        // 이 함수는 각 픽셀의 x 및 y 방향 그레디언트를 이용해 경사 방향을 계산합니다.
        // 계산된 방향은 4개의 범주 중 하나로 양자화됩니다 (0, 1, 2, 3).
        public static void CalcDirect(Mat Gy, Mat Gx, out Mat direct)
        {
            direct = new Mat(Gy.Size(), MatType.CV_8U);

            for (int i = 0; i < direct.Rows; i++)
            {
                for (int j = 0; j < direct.Cols; j++)
                {
                    float gx = Gx.At<float>(i, j);
                    float gy = Gy.At<float>(i, j);
                    int theta = (int)(Cv2.FastAtan2(gy, gx) / 45);
                    direct.Set(i, j, (byte)(theta % 4));
                }
            }
        }

        // 이 함수는 그레디언트 크기 이미지 (sobel)에서 비최대값 억제를 수행하여 에지를 얇게 만듭니다.
        // 현재 픽셀이 그레디언트 방향에서 지역 최대값인지 확인하고, 최대값이 아닌 경우 억제합니다.
        public static void SuppNonMax(Mat sobel, Mat direct, out Mat dst)
        {
            dst = new Mat(sobel.Size(), MatType.CV_32F, Scalar.All(0));

            for (int i = 1; i < sobel.Rows - 1; i++)
            {
                for (int j = 1; j < sobel.Cols - 1; j++)
                {
                    int dir = direct.At<byte>(i, j);
                    float v1, v2;
                    if (dir == 0)
                    {
                        v1 = sobel.At<float>(i, j - 1);
                        v2 = sobel.At<float>(i, j + 1);
                    }
                    else if (dir == 1)
                    {
                        v1 = sobel.At<float>(i + 1, j + 1);
                        v2 = sobel.At<float>(i - 1, j - 1);
                    }
                    else if (dir == 2)
                    {
                        v1 = sobel.At<float>(i - 1, j);
                        v2 = sobel.At<float>(i + 1, j);
                    }
                    else
                    {
                        v1 = sobel.At<float>(i + 1, j - 1);
                        v2 = sobel.At<float>(i - 1, j + 1);
                    }

                    float center = sobel.At<float>(i, j);
                    dst.Set(i, j, (center > v1 && center > v2) ? center : 0);
                }
            }
        }

        // 이 함수는 히스테리시스에 의한 에지 추적을 수행합니다.
        // 강한 에지를 따라 재귀적으로 추적하며 약한 에지들을 연결합니다.
        public static void Trace(Mat maxSo, Mat posCk, Mat hyImg, Point pt, float low)
        {
            Rect rect = new Rect(new Point(0, 0), posCk.Size());
            if (!rect.Contains(pt)) return;

            if (posCk.At<byte>(pt.Y, pt.X) == 0 && maxSo.At<float>(pt.Y, pt.X) > low)
            {
                posCk.Set(pt.Y, pt.X, (byte)1);
                hyImg.Set(pt.Y, pt.X, (byte)255);

                Trace(maxSo, posCk, hyImg, pt + new Point(-1, -1), low);
                Trace(maxSo, posCk, hyImg, pt + new Point(0, -1), low);
                Trace(maxSo, posCk, hyImg, pt + new Point(1, -1), low);
                Trace(maxSo, posCk, hyImg, pt + new Point(-1, 0), low);
                Trace(maxSo, posCk, hyImg, pt + new Point(1, 0), low);
                Trace(maxSo, posCk, hyImg, pt + new Point(-1, 1), low);
                Trace(maxSo, posCk, hyImg, pt + new Point(0, 1), low);
                Trace(maxSo, posCk, hyImg, pt + new Point(1, 1), low);
            }
        }

        // 이 함수는 비최대 억제를 거친 그레디언트 크기 이미지 (maxSo)에 대해 히스테리시스 임계값을 적용합니다.
        // 강한 에지를 찾고, 이를 기준으로 낮은 임계값 이상의 연결된 에지를 추적합니다.
        public static void HysteresisTh(Mat maxSo, out Mat hyImg, float low, float high)
        {
            Mat posCk = new Mat(maxSo.Size(), MatType.CV_8U, Scalar.All(0));
            hyImg = new Mat(maxSo.Size(), MatType.CV_8U, Scalar.All(0));

            for (int i = 0; i < maxSo.Rows; i++)
            {
                for (int j = 0; j < maxSo.Cols; j++)
                {
                    if (maxSo.At<float>(i, j) > high)
                    {
                        Trace(maxSo, posCk, hyImg, new Point(j, i), low);
                    }
                }
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/cannay_tset.jpg";
            Mat image = Cv2.ImRead(path, ImreadModes.Grayscale);

            if (image.Empty())
                throw new Exception("이미지 로드 실패");

            Mat canny = new Mat();

            // OpenCV의 Canny 함수를 사용하여 에지 검출 수행
            // Canny 함수의 세 번째와 네 번째 매개변수는 각각 낮은 임계값(100)과 높은 임계값(150)을 의미합니다.
            // 이 두 임계값을 통해 에지를 구분하며, 높은 임계값(150)보다 큰 경사 값은 확실한 에지로 간주되고,
            // 낮은 임계값(100)과 높은 임계값 사이에 있는 값은 연결된 경우에만 에지로 간주됩니다.
            Cv2.Canny(image, canny, 100, 150);

            // 원본 이미지 및 Canny 에지 결과 출력
            Cv2.ImShow("image", image);
            Cv2.ImShow("OpenCV_canny", canny);
            Cv2.WaitKey();
        }
    }
}
====================================================
using OpenCvSharp;

namespace fileter_minMax
{
    internal class Program
    {
        static void MinMaxFilter(Mat img, out Mat dst, int size, int flag = 1)
        {
            dst = new Mat(img.Size(), MatType.CV_8U, Scalar.All(0));
            Size msize = new Size(size, size);

            Point h_m = new Point(msize.Width / 2, msize.Height / 2);   //h_m half of mask의 약자 mask의 중심점

            for (int i = h_m.Y; i < img.Rows - h_m.Y; i++)
            {
                for (int j = h_m.X; j < img.Cols - h_m.X; j++)
                {
                    Point start = new Point(j, i) - h_m;
                    Rect roi = new Rect(start, msize); // 마스크 영역 사각형
                    Mat mask = new Mat(img, roi); // 마스크 영역 참조

                    double minVal, maxVal;
                    Cv2.MinMaxLoc(mask, out minVal, out maxVal); // 마스크 영역 최소, 최대값
                    dst.Set(i, j, (byte)((flag != 0) ? maxVal : minVal));
                }
            }
        }
        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/median_test1.jpg"; //시계그림 이름이 바뀌어 있네요 체크~!
            Mat image = Cv2.ImRead(path, ImreadModes.Grayscale);

            if (image.Empty())
                throw new Exception("이미지를 불러올 수 없습니다.");

            Mat minImg, maxImg;
            MinMaxFilter(image, out minImg, 5, 0); // 5x5 마스크 최소값 필터링
            MinMaxFilter(image, out maxImg, 5, 1); // 5x5 마스크 최대값 필터링

            Mat cvMinImg = new Mat();
            Mat cvMaxImg = new Mat();

            // OpenCV의 Erode()와 Dilate() 함수를 사용한 최소값 및 최대값 필터링
            Cv2.Erode(image, cvMinImg, new Mat(), iterations: 1); // 최소값 필터링
            Cv2.Dilate(image, cvMaxImg, new Mat(), iterations: 1); // 최대값 필터링

            // 결과 출력
            Cv2.ImShow("image", image);
            Cv2.ImShow("minFilter_img", minImg);
            Cv2.ImShow("maxFilter_img", maxImg);
            Cv2.ImShow("cvMinFilter_img", cvMinImg);
            Cv2.ImShow("cvMaxFilter_img", cvMaxImg);
            Cv2.WaitKey();
        }
    }
}
=================================================
using OpenCvSharp;

namespace AverageFilterTest
{
    internal class Program
    {
        static void AverageFilter(Mat img, out Mat dst, int size)
        {
            dst = new Mat(img.Size(), MatType.CV_8U, Scalar.All(0));

            for (int i = 0; i < img.Rows; i++)
            {
                for (int j = 0; j < img.Cols; j++)
                {
                    Point pt1 = new Point(j - size / 2, i - size / 2);
                    Point pt2 = pt1 + new Point(size, size);

                    if (pt1.X < 0) pt1.X = 0;
                    if (pt1.Y < 0) pt1.Y = 0;
                    if (pt2.X > img.Cols) pt2.X = img.Cols;
                    if (pt2.Y > img.Rows) pt2.Y = img.Rows;

                    Rect maskRect = new Rect(pt1.X, pt1.Y, pt2.X - pt1.X, pt2.Y - pt1.Y);
                    Mat mask = new Mat(img, maskRect);
                    dst.Set(i, j, (byte)Cv2.Mean(mask)[0]);
                }
            }
        }
        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/avg_filter.jpg";
            Mat image = Cv2.ImRead(path, ImreadModes.Grayscale);

            if (image.Empty())
                throw new Exception("이미지를 불러올 수 없습니다.");

            Mat avgImg = new Mat();
            Mat blurImg = new Mat();
            Mat boxImg = new Mat();

            AverageFilter(image, out avgImg, 5); // 사용자 정의 함수
            Cv2.Blur(image, blurImg, new Size(5, 5)); // OpenCV 제공 블러 필터
            Cv2.BoxFilter(image, boxImg, -1, new Size(5, 5)); // OpenCV 제공 박스 필터

            Cv2.ImShow("원본", image);
            Cv2.ImShow("avg_Filter 구현 알고리즘", avgImg);
            Cv2.ImShow("OpenCV blur", blurImg);
            Cv2.ImShow("OpenCV BoxFilter", boxImg);

            Cv2.WaitKey();
        }
    }
}
===========================================
using OpenCvSharp;

namespace filter_median
{
    internal class Program
    {
        static void MedianFilter(Mat img, out Mat dst, int size)
        {
            dst = new Mat(img.Size(), MatType.CV_8U, Scalar.All(0));
            Size msize = new Size(size, size);
            Point h_m = new Point(msize.Width / 2, msize.Height / 2);

            for (int i = h_m.Y; i < img.Rows - h_m.Y; i++)
            {
                for (int j = h_m.X; j < img.Cols - h_m.X; j++)
                {
                    Point start = new Point(j, i) - h_m;
                    Rect roi = new Rect(start, msize);
                    Mat mask = new Mat(img, roi);

                    // 플랫하게 벡터로 변환하는 방식으로 수정 (직접 순환하면서 값을 넣음)
                    byte[] maskArray = new byte[mask.Rows * mask.Cols];
                    int index = 0;
                    for (int y = 0; y < mask.Rows; y++)
                    {
                        for (int x = 0; x < mask.Cols; x++)
                        {
                            maskArray[index++] = mask.At<byte>(y, x);
                        }
                    }
                    Array.Sort(maskArray);

                    int mediIdx = maskArray.Length / 2; // 중간 위치
                    dst.Set(i, j, maskArray[mediIdx]); // 중간값
                }
            }
        }
        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/median_test.jpg";
            Mat image = Cv2.ImRead(path, ImreadModes.Color);

            if (image.Empty())
                throw new Exception("이미지를 불러올 수 없습니다.");

            Mat gray = new Mat();
            Mat medImg1 = new Mat();
            Mat medImg2 = new Mat();
            Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);

            MedianFilter(gray, out medImg1, 5); // 사용자 정의 함수
            Cv2.MedianBlur(gray, medImg2, 5); // OpenCV 제공 함수

            Cv2.ImShow("gray", gray);
            Cv2.ImShow("median-User", medImg1);
            Cv2.ImShow("median-OpenCV", medImg2);

            Cv2.WaitKey();
        }
    }
}
