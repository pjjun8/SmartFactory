using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test03
{
    internal class Preprocess
    {
        public static void LoadCascade(CascadeClassifier cascade, string filename)
        {
            string path = "C:\\Users\\Admin\\Desktop\\OpenCv\\opencv_ppt_cpp\\예제_11.3\\haarcascades\\"; // 분류기 폴더
            string fullPath = path + filename;

            if (!cascade.Load(fullPath))
                throw new Exception("분류기 로드 실패.");
        }

        public static Mat Preprocessing(Mat image)
        {
            Mat gray = new Mat();
            Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY); // 그레이스케일로 변환
            Cv2.EqualizeHist(gray, gray); // 히스토그램 평활화
            return gray;
        }
    }
}
-------------------------------------------------------
using OpenCvSharp;

namespace test03
{
    internal class Program
    {
        public static Point2d CalcCenter(Rect obj)
        {
            Point2d c = new Point2d(obj.Width / 2.0, obj.Height / 2.0);
            return obj.TopLeft + c;
        }
        static void Main(string[] args)
        {
            CascadeClassifier faceCascade = new CascadeClassifier();
            CascadeClassifier eyesCascade = new CascadeClassifier();

            Preprocess.LoadCascade(faceCascade, "haarcascade_frontalface_alt2.xml"); // 얼굴 탐지기 로드
            Preprocess.LoadCascade(eyesCascade, "haarcascade_eye.xml"); // 눈 탐지기 로드

            Mat image = Cv2.ImRead("C:\\Users\\Admin\\Desktop\\OpenCv\\opencv_ppt_cpp\\예제_11.3\\face_img\\face\\63.jpg", ImreadModes.Color); // 얼굴 이미지 로드
            if (image.Empty())
                throw new Exception("이미지를 찾을 수 없습니다.");

            List<Rect> faces = new List<Rect>();
            List<Rect> eyes = new List<Rect>();
            List<Point2d> eyesCenter = new List<Point2d>();

            Mat gray = Preprocess.Preprocessing(image); // 전처리 수행
            Rect[] detectedFaces = faceCascade.DetectMultiScale(gray, 1.1, 2, HaarDetectionTypes.ScaleImage, new Size(100, 100));

            if (detectedFaces.Length > 0) // 얼굴이 탐지되었을 경우
            {
                // 눈 탐지 수행
                Rect[] detectedEyes = eyesCascade.DetectMultiScale(gray[detectedFaces[0]], 1.15, 7, HaarDetectionTypes.ScaleImage, new Size(25, 20));

                if (detectedEyes.Length == 2) // 눈이 두 개 탐지되었을 경우
                {
                    foreach (var eye in detectedEyes)
                    {
                        Point center = new Point((int)CalcCenter(eye + detectedFaces[0].TopLeft).X, (int)CalcCenter(eye + detectedFaces[0].TopLeft).Y); // 중심 좌표 계산
                        Cv2.Circle(image, center, 5, new Scalar(0, 255, 0), 2); // 눈 중심에 원 그리기
                    }
                }

                Cv2.Rectangle(image, detectedFaces[0], new Scalar(255, 0, 0), 2); // 얼굴 영역에 사각형 그리기
                Cv2.ImShow("image", image);
                Cv2.WaitKey();
            }
        }
    }
}

