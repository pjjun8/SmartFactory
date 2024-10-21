//금요일에 못했던 개인 과제 한혁이형꺼 복붙!
using OpenCvSharp;
using System;
namespace Min_MaxImg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 이미지 파일 경로
            string imagePath = @"c:/Temp/test.jpg";

            // 컬러 이미지 불러오기
            Mat image = Cv2.ImRead(imagePath);

            // 예외 처리
            if (image.Empty())
            {
                throw new Exception("이미지를 불러올 수 없습니다.");
            }

            Mat[] channels = Cv2.Split(image);

            for (int i = 0; i < channels.Length; i++)
            {
                double minVal, maxVal;
                Cv2.MinMaxIdx(channels[i], out minVal, out maxVal);
                double ratio = (maxVal - minVal) / 255.0;
                Cv2.Subtract(channels[i], new Scalar(minVal), channels[i]);
                Cv2.Divide(channels[i], new Scalar(ratio), channels[i]);
                channels[i].ConvertTo(channels[i], MatType.CV_8U);
            }

            Mat dst = new Mat();
            Cv2.Merge(channels, dst);

            // 결과 출력
            Console.WriteLine("이미지 대비 조정 완료.");

            // 원본 이미지와 조정된 이미지 출력
            Cv2.ImShow("Image", image);
            Cv2.ImShow("Adjust Image", dst);
            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }
    }
}
=============================
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synthesis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 이미지 파일 경로
            string imagePath = @"C:/Temp/picture/back.png";
            string logoPath = @"C:/Temp/picture/logo.png";

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
            Cv2.Threshold(logo, logoTh, 70, 255, ThresholdTypes.Binary); //logo 행렬에 이진화 수행 70보다 큰 화소는 255로 변경, 나머지는 0

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
            Cv2.BitwiseOr(masks[0], masks[1], mask1);
            Mat mask3 = new Mat();
            Cv2.BitwiseOr(masks[2], mask1, mask3);

            // 배경 통과 마스크
            Mat notMask = new Mat(mask1.Size(), MatType.CV_8UC1, new Scalar(255));
            Cv2.BitwiseNot(mask3, notMask);
            
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
            Cv2.ImShow("image", image);
            Cv2.WaitKey(0);
        }
    }
}
=========================
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCB_Finder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 이미지 파일 경로
            string importedPath = @"C:/Temp/pcb/pcb1.png"; //양품
            string defectivePath = @"C:/Temp/pcb/pcb2.png"; //불량품

            // 이미지 읽기
            Mat imported = Cv2.ImRead(importedPath, ImreadModes.Grayscale);
            Mat defective = Cv2.ImRead(defectivePath, ImreadModes.Grayscale);
            
            Mat judgment = new Mat();

            // 예외 처리
            if (imported.Empty() || defective.Empty())
            {
                throw new Exception("이미지를 불러올 수 없습니다.");
            }

            // Bitwise operations
            Cv2.BitwiseXor(imported, defective, judgment); //배타적논리합
            Cv2.ImShow("불량 판정 ", judgment);
            Cv2.WaitKey(0);
        }
    }
}
======================
//개인 과제 화질개선 카메라//
//콘솔//
using OpenCvSharp;
using static System.Net.Mime.MediaTypeNames;

namespace CVBasicCamera
{
    class StringUtil
    {
        public void PutString(Mat frame, string text, Point pt, double value)
        {
            text += value.ToString();
            Point shade = new Point(pt.X + 2, pt.Y + 2);
            int font = (int)HersheyFonts.HersheySimplex;

            // 그림자 효과 
            Cv2.PutText(frame, text, shade, (HersheyFonts)font, 0.7, Scalar.Black, 2);

            // 실제 텍스트 
            Cv2.PutText(frame, text, pt, (HersheyFonts)font, 0.7, new Scalar(120, 200, 90), 2);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // 웹캠 연결
            VideoCapture capture = new VideoCapture(0);
            if (!capture.IsOpened())
            {
                Console.WriteLine("카메라가 연결되지 않았습니다.");
                return;
            }

            // 카메라 속성 출력
            Console.WriteLine("너비: " + capture.Get(VideoCaptureProperties.FrameWidth));
            Console.WriteLine("높이: " + capture.Get(VideoCaptureProperties.FrameHeight));
            Console.WriteLine("노출: " + capture.Get(VideoCaptureProperties.Exposure));
            Console.WriteLine("밝기: " + capture.Get(VideoCaptureProperties.Brightness));

            Mat frame = new Mat();
            while (true)
            {
                // 카메라에서 프레임 읽기
                capture.Read(frame);
                if (frame.Empty())
                    break;

                // 노출 정보 출력
                StringUtil su = new StringUtil();
                su.PutString(frame, "EXPOS: ", new Point(10, 40), capture.Get(VideoCaptureProperties.Exposure));

                ///////////////////////////////////////////
                //화질개선 코드
                Mat[] channels = Cv2.Split(frame);

                for (int i = 0; i < channels.Length; i++)
                {
                    double minVal, maxVal;
                    Cv2.MinMaxIdx(channels[i], out minVal, out maxVal);
                    double ratio = (maxVal - minVal) / 255.0;
                    Cv2.Subtract(channels[i], new Scalar(minVal), channels[i]);
                    Cv2.Divide(channels[i], new Scalar(0.5), channels[i]);
                    channels[i].ConvertTo(channels[i], MatType.CV_8U);
                }

                Mat dst = new Mat();
                Cv2.Merge(channels, dst);

                ///////////////////////////////////////////



                Cv2.ImShow("개선된 카메라 영상보기", dst);
                Cv2.ImShow("기본 카메라 영상보기", frame);

                // 키 입력 대기 (30ms)
                if (Cv2.WaitKey(30) >= 0)
                    break;
            }
            Cv2.DestroyAllWindows();
        }
    }
}
-------------
//윈폼//
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;

namespace pixelUpgradeCamera01
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

        private async void button1_Click(object sender, EventArgs e)
        {
            if (isRunning)  // 이미 카메라가 실행 중이면
            {
                isRunning = false;  // 실행 중 상태를 false로 변경
                button1.Text = "Start";  // 버튼 텍스트 변경
                return;
            }

            button1.Text = "Stop";  // 버튼 텍스트 변경
            isRunning = true;  // 실행 중 상태를 true로 변경

            while (isRunning)  // 카메라가 실행 중이면
            {
                if (capture.IsOpened())  // 카메라가 연결되어 있으면
                {
                    capture.Read(frame);  // 프레임 읽기

                    ///////////////////////////////////////////
                    //화질개선 코드
                    Mat[] channels = Cv2.Split(frame);

                    for (int i = 0; i < channels.Length; i++)
                    {
                        double minVal, maxVal;
                        Cv2.MinMaxIdx(channels[i], out minVal, out maxVal);
                        double ratio = (maxVal - minVal) / 350.0;
                        Cv2.Subtract(channels[i], new Scalar(minVal), channels[i]);
                        Cv2.Divide(channels[i], new Scalar(ratio), channels[i]);
                        channels[i].ConvertTo(channels[i], MatType.CV_8U);
                    }

                    Mat dst = new Mat();
                    Cv2.Merge(channels, dst);

                    ///////////////////////////////////////////

                    if (!isColor)  // 흑백 모드이면
                    {
                        Cv2.CvtColor(frame, frame, ColorConversionCodes.BGR2GRAY);  // 컬러를 흑백으로 변경
                        //Cv2.CvtColor(frame, frame, ColorConversionCodes.GRAY2BGR);  // 흑백을 다시 컬러로 변경 (PictureBox 호환을 위해)
                    }

                    pictureBox1.Image = BitmapConverter.ToBitmap(frame);  // PictureBox에 영상 출력
                    pictureBox2.Image = BitmapConverter.ToBitmap(dst);  // PictureBox에 영상 출력
                }
                await Task.Delay(33);  // 대략 30 fps
            }
        }
    }
}
===================================
using OpenCvSharp;

namespace mat_at
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat m1 = new Mat(3, 5, MatType.CV_32SC1);
            Mat m2 = new Mat(3, 5, MatType.CV_32FC1);
            Mat m3 = new Mat(3, 5, MatType.CV_8UC2);
            Mat m4 = new Mat(3, 5, MatType.CV_32SC3);

            for (int i = 0, k = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Cols; j++, k++)
                {
                    m1.At<int>(i, j) = k;
                    m2.At<float>(i, j) = (float)j;
                    m3.At<Vec2b>(i, j) = new Vec2b(0, 1);

                    m4.At<Vec3i>(i, j) = new Vec3i(0, 1, 2);
                }
            }

            Console.WriteLine("[m1] = ");
            Console.WriteLine(m1.Dump());
            Console.WriteLine("[m2] = ");
            Console.WriteLine(m2.Dump());
            Console.WriteLine("[m3] = ");
            Console.WriteLine(m3.Dump());
            Console.WriteLine("[m4] = ");
            Console.WriteLine(m4.Dump());
        }
    }
}

