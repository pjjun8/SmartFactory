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
