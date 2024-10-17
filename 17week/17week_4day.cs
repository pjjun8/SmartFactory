using OpenCvSharp;

namespace ReadImage
{
    class ImageUtil
    {
        public void PrintMatInfo(string name, Mat img)
        {
            string str;
            int depth = img.Depth();

            if (depth == MatType.CV_8U) str = "CV_8U";
            else if (depth == MatType.CV_8S) str = "CV_8S";
            else if (depth == MatType.CV_16U) str = "CV_16U";
            else if (depth == MatType.CV_16S) str = "CV_16S";
            else if (depth == MatType.CV_32S) str = "CV_32S";
            else if (depth == MatType.CV_32F) str = "CV_32F";
            else if (depth == MatType.CV_64F) str = "CV_64F";
            else str = "Unknown";

            Console.WriteLine($"{name}: depth({depth}) channels({img.Channels()}) -> 자료형: {str}C{img.Channels()}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string filename1 = @"C:/Temp/opencv/read_gray.jpg";
            Mat gray2gray = Cv2.ImRead(filename1, ImreadModes.Grayscale);
            Mat gray2color = Cv2.ImRead(filename1, ImreadModes.Color);

            if (gray2gray.Empty() || gray2color.Empty())
            {
                Console.WriteLine("이미지를 불러오는 데 실패했습니다.");
                return;
            }

            // ROI 영역 설정 (100, 100 위치의 1x1 픽셀)
            Rect roi = new Rect(100, 100, 1, 1);
            Console.WriteLine("행렬 좌표 (100,100) 화소값");
            Console.WriteLine($"gray2gray: {gray2gray.SubMat(roi).Dump()}");
            Console.WriteLine($"gray2color: {gray2color.SubMat(roi).Dump()}\n");

            ImageUtil iu = new ImageUtil();
            iu.PrintMatInfo("gray2gray", gray2gray);
            iu.PrintMatInfo("gray2color", gray2color);

            Cv2.ImShow("gray2gray", gray2gray);
            Cv2.ImShow("gray2color", gray2color);
            Cv2.WaitKey(0);
        }
    }
}
====================================
using OpenCvSharp;

namespace WriteImage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat img8 = Cv2.ImRead(@"C:/Temp/opencv/read_color.jpg", ImreadModes.Color);
            if (img8.Empty())
            {
                Console.WriteLine("이미지를 불러오는 데 실패했습니다.");
                return;
            }

            int[] paramsJpg = { (int)ImwriteFlags.JpegQuality, 50 };  // JPEG 품질 50으로 설정
            int[] paramsPng = { (int)ImwriteFlags.PngCompression, 9 };  // PNG 압축 레벨 9로 설정
                                                                        // JPEG와 PNG 저장 파라미터 설정

            //out 폴더를 미리 만들어 주세요. 폴더생성과 예외처리를 넣으면 길어져서 생략해 봅니다.
            Cv2.ImWrite(@"C:/Temp/opencv/out/write_test1.jpg", img8); // 기본 설정으로 JPG 저장
            Cv2.ImWrite(@"C:/Temp/opencv/out/write_test2.jpg", img8, paramsJpg); // 품질 50으로 JPG 저장
            Cv2.ImWrite(@"C:/Temp/opencv/out/write_test.png", img8, paramsPng); // 압축 레벨 9로 PNG 저장
            Cv2.ImWrite(@"C:/Temp/opencv/out/write_test.bmp", img8); // BMP로 저장

            Console.WriteLine("이미지 저장이 완료되었습니다.");
        }
    }
}
=================================
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpBasicCamera02
{
    public partial class Form1 : Form
    {
        private VideoCapture capture;  // 카메라 캡처 객체
        private Mat frame;             // 현재 프레임을 저장할 객체
        private bool isRunning = false;  // 카메라가 실행 중인지 확인하는 변수
        private bool isColor = true;     // 컬러 모드인지 확인하는 변수
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
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (isRunning)  // 이미 카메라가 실행 중이면
            {
                isRunning = false;  // 실행 중 상태를 false로 변경
                btnStart.Text = "Start";  // 버튼 텍스트 변경
                return;
            }

            btnStart.Text = "Stop";  // 버튼 텍스트 변경
            isRunning = true;  // 실행 중 상태를 true로 변경

            while (isRunning)  // 카메라가 실행 중이면
            {
                if (capture.IsOpened())  // 카메라가 연결되어 있으면
                {
                    capture.Read(frame);  // 프레임 읽기

                    if (!isColor)  // 흑백 모드이면
                    {
                        Cv2.CvtColor(frame, frame, ColorConversionCodes.BGR2GRAY);  // 컬러를 흑백으로 변경
                        //Cv2.CvtColor(frame, frame, ColorConversionCodes.GRAY2BGR);  // 흑백을 다시 컬러로 변경 (PictureBox 호환을 위해)
                    }

                    pictureBox1.Image = BitmapConverter.ToBitmap(frame);  // PictureBox에 영상 출력
                }
                await Task.Delay(33);  // 대략 30 fps
            }
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            isColor = false;  // 흑백 모드로 변경
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            isColor = true;   // 컬러 모드로 변경
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            isRunning = false;  // 카메라 중지
            capture.Release();  // 카메라 자원 해제
            this.Close();       // 프로그램 종료
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
==================================
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OpenCvSharp.Stitcher;

namespace CSharpBasicCamera02
{
    public partial class Form1 : Form
    {
        private VideoCapture capture;  // 카메라 캡처 객체
        private Mat frame;             // 현재 프레임을 저장할 객체
        private bool isRunning = false;  // 카메라가 실행 중인지 확인하는 변수
        private enum CVMode { COLOR, BLACK, BGR };     // 컬러 모드인지 확인하는 변수
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
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (isRunning)  // 이미 카메라가 실행 중이면
            {
                isRunning = false;  // 실행 중 상태를 false로 변경
                btnStart.Text = "Start";  // 버튼 텍스트 변경
                return;
            }

            btnStart.Text = "Stop";  // 버튼 텍스트 변경
            isRunning = true;  // 실행 중 상태를 true로 변경

            while (isRunning)  // 카메라가 실행 중이면
            {
                if (capture.IsOpened())  // 카메라가 연결되어 있으면
                {
                    capture.Read(frame);  // 프레임 읽기

                    switch (currentMode)
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
                    }

                    pictureBox1.Image = BitmapConverter.ToBitmap(frame);  // PictureBox에 영상 출력
                }
                await Task.Delay(33);  // 대략 30 fps
            }
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            currentMode = CVMode.BLACK;
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            currentMode = CVMode.COLOR;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            currentMode = CVMode.BGR;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
========================================
using OpenCvSharp;

namespace OpenCVSharpTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat ch0 = new Mat(3, 4, MatType.CV_8U, new Scalar(10));
            Mat ch1 = new Mat(3, 4, MatType.CV_8U, new Scalar(20));
            Mat ch2 = new Mat(3, 4, MatType.CV_8U, new Scalar(30));

            Mat[] bgr_arr = { ch0, ch1, ch2 };
            Mat bgr = new Mat();
            Cv2.Merge(bgr_arr, bgr);

            Mat[] bgr_vec = Cv2.Split(bgr);

            Console.WriteLine("[ch0] = \n" + ch0.Dump());
            Console.WriteLine("[ch1] = \n" + ch1.Dump());
            Console.WriteLine("[ch2] = \n" + ch2.Dump() + "\n");

            Console.WriteLine("[bgr] = \n" + bgr.Dump() + "\n");
            Console.WriteLine("[bgr_vec[0]] = \n" + bgr_vec[0].Dump());
            Console.WriteLine("[bgr_vec[1]] = \n" + bgr_vec[1].Dump());
            Console.WriteLine("[bgr_vec[2]] = \n" + bgr_vec[2].Dump());
        }
    }
}
=====================================
using OpenCvSharp;

namespace OpenCVSharpTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 채널 0, 1, 2 생성 (각각의 값: 10, 20, 30)
            Mat ch0 = new Mat(new Size(4, 3), MatType.CV_8UC1, new Scalar(10));
            Mat ch1 = new Mat(new Size(4, 3), MatType.CV_8UC1, new Scalar(20));
            Mat ch2 = new Mat(new Size(4, 3), MatType.CV_8UC1, new Scalar(30));
            Mat ch_012 = new Mat();

            // 벡터로 병합
            List<Mat> vec_012 = new List<Mat> { ch0, ch1, ch2 };
            Cv2.Merge(vec_012.ToArray(), ch_012);

            // ch_13: 2채널, ch_2: 1채널
            Mat ch_13 = new Mat(ch_012.Size(), MatType.CV_8UC2);
            Mat ch_2 = new Mat(ch_012.Size(), MatType.CV_8UC1);

            Mat[] outMats = { ch_13, ch_2 };
            int[] from_to = { 0, 0, 2, 1, 1, 2 };

            // mixChannels 함수 사용
            Cv2.MixChannels(new Mat[] { ch_012 }, outMats, from_to);

            // 결과 출력
            Console.WriteLine("[ch_123] = ");
            Console.WriteLine(ch_012.Dump());
            Console.WriteLine();

            Console.WriteLine("[ch_13] = ");
            Console.WriteLine(ch_13.Dump());
            Console.WriteLine();

            Console.WriteLine("[ch_2] = ");
            Console.WriteLine(ch_2.Dump());
        }
    }
}
