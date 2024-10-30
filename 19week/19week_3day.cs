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
========================================================
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test03
{
    internal class CorrectAngle
    {
        public static Point2f CalcCenter(Rect obj)
        {
            Point2f c = new Point2f(obj.Width / 2.0f, obj.Height / 2.0f);
            Point2f center = obj.TopLeft + c;
            return center;
        }

        public static Mat CalcRotMap(Point2f faceCenter, List<Point2f> points)
        {
            Point2f delta = (points[0].X > points[1].X) ? points[0] - points[1] : points[1] - points[0];
            float angle = Cv2.FastAtan2(delta.Y, delta.X);
            Mat rotMat = Cv2.GetRotationMatrix2D(faceCenter, angle, 1);
            return rotMat;
        }

        public static Mat CorrectImage(Mat image, Mat rotMat, ref List<Point2f> eyesCenter)
        {
            Mat correctImg = new Mat();
            Cv2.WarpAffine(image, correctImg, rotMat, image.Size(), InterpolationFlags.Cubic);

            for (int i = 0; i < eyesCenter.Count; i++)
            {
                // Point2f 형식으로 변환하여 eyesCenter의 좌표를 업데이트
                var newPoint = new Point2f(
                    (float)(rotMat.At<double>(0, 0) * eyesCenter[i].X + rotMat.At<double>(0, 1) * eyesCenter[i].Y + rotMat.At<double>(0, 2)),
                    (float)(rotMat.At<double>(1, 0) * eyesCenter[i].X + rotMat.At<double>(1, 1) * eyesCenter[i].Y + rotMat.At<double>(1, 2))
                );

                eyesCenter[i] = newPoint;
            }

            return correctImg;
        }
    }
}
--------------------------------------------
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
----------------------------------
using OpenCvSharp;

namespace test03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CascadeClassifier faceCascade = new CascadeClassifier();
            CascadeClassifier eyesCascade = new CascadeClassifier();

            Preprocess.LoadCascade(faceCascade, "haarcascade_frontalface_alt2.xml"); // 얼굴 탐지기 로드
            Preprocess.LoadCascade(eyesCascade, "haarcascade_eye.xml"); // 눈 탐지기 로드

            Mat image = Cv2.ImRead("C:\\Users\\Admin\\Desktop\\OpenCv\\opencv_ppt_cpp\\예제_11.3\\face_img\\face\\59.jpg", ImreadModes.Color);
            if (image.Empty())
                throw new Exception("이미지를 찾을 수 없습니다.");

            Mat gray = Preprocess.Preprocessing(image);

            List<Rect> faces = new List<Rect>();
            List<Rect> eyes = new List<Rect>();
            List<Point2f> eyesCenter = new List<Point2f>();

            Rect[] detectedFaces = faceCascade.DetectMultiScale(gray, 1.1, 2, HaarDetectionTypes.ScaleImage, new Size(100, 100));
            if (detectedFaces.Length > 0)
            {
                Rect[] detectedEyes = eyesCascade.DetectMultiScale(gray[detectedFaces[0]], 1.15, 7, HaarDetectionTypes.ScaleImage, new Size(25, 20));
                if (detectedEyes.Length == 2)
                {
                    eyesCenter.Add(CorrectAngle.CalcCenter(detectedEyes[0] + detectedFaces[0].TopLeft));
                    eyesCenter.Add(CorrectAngle.CalcCenter(detectedEyes[1] + detectedFaces[0].TopLeft));

                    Point2f faceCenter = CorrectAngle.CalcCenter(detectedFaces[0]);
                    Mat rotMat = CorrectAngle.CalcRotMap(faceCenter, eyesCenter);
                    Mat correctImg = CorrectAngle.CorrectImage(image, rotMat, ref eyesCenter);

                    foreach (var eye in eyesCenter)
                    {
                        Cv2.Circle(correctImg, new Point((int)eye.X, (int)eye.Y), 5, new Scalar(0, 255, 0), 2);
                    }

                    Cv2.ImShow("correct_img", correctImg);
                    Cv2.WaitKey();
                }
            }
        }
    }
}
=========================================
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test03
{
    internal class CorrectAngle
    {
        public static Point2f CalcCenter(Rect obj)
        {
            Point2f c = new Point2f(obj.Width / 2.0f, obj.Height / 2.0f);
            Point2f center = obj.TopLeft + c;
            return center;
        }

        public static Mat CalcRotMap(Point2f faceCenter, List<Point2f> points)
        {
            Point2f delta = (points[0].X > points[1].X) ? points[0] - points[1] : points[1] - points[0];
            float angle = Cv2.FastAtan2(delta.Y, delta.X);
            Mat rotMat = Cv2.GetRotationMatrix2D(faceCenter, angle, 1);
            return rotMat;
        }

        public static Mat CorrectImage(Mat image, Mat rotMat, ref List<Point2f> eyesCenter)
        {
            Mat correctImg = new Mat();
            Cv2.WarpAffine(image, correctImg, rotMat, image.Size(), InterpolationFlags.Cubic);

            for (int i = 0; i < eyesCenter.Count; i++)
            {
                // Point2f 형식으로 변환하여 eyesCenter의 좌표를 업데이트
                var newPoint = new Point2f(
                    (float)(rotMat.At<double>(0, 0) * eyesCenter[i].X + rotMat.At<double>(0, 1) * eyesCenter[i].Y + rotMat.At<double>(0, 2)),
                    (float)(rotMat.At<double>(1, 0) * eyesCenter[i].X + rotMat.At<double>(1, 1) * eyesCenter[i].Y + rotMat.At<double>(1, 2))
                );

                eyesCenter[i] = newPoint;
            }

            return correctImg;
        }
    }
}
-------------------------------------
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test03
{
    internal class DetectArea
    {
        public static Rect DetectLip(Point2d faceCenter, Rect face)
        {
            Point2d lipCenter = faceCenter + new Point2d(0, face.Height * 0.32);
            Point2d gapSize = new Point2d(face.Width * 0.18, face.Height * 0.1);

            Point lipStart = (lipCenter - gapSize).ToPoint();
            Size lipSize = new Size((int)(gapSize.X * 2), (int)(gapSize.Y * 2));

            return new Rect(lipStart, lipSize);
        }

        public static void DetectHair(Point2d faceCenter, Rect face, List<Rect> hairRects)
        {
            Point2d hGap = new Point2d(face.Width * 0.45, face.Height * 0.65);
            Point2d pt1 = faceCenter - hGap;
            Point2d pt2 = faceCenter + hGap;
            // pt1, pt2를 Point로 변환해서 Rect 생성
            Size size = new Size((int)(pt2.X - pt1.X), (int)(pt2.Y - pt1.Y));
            Rect hair = new Rect(new Point((int)pt1.X, (int)pt1.Y), size);

            // Size 사용 시 형변환 문제 해결
            Size size1 = new Size((int)hair.Width, (int)(hair.Height * 0.40));
            Rect hair1 = new Rect(hair.TopLeft, size1);
            Rect hair2 = new Rect(new Point(hair.BottomRight.X - size1.Width, hair.BottomRight.Y - size1.Height), size1);


            hairRects.Add(hair1);
            hairRects.Add(hair2);
            hairRects.Add(hair);
        }
    }
}
--------------------------------------
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
---------------------------------
using OpenCvSharp;

namespace test03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CascadeClassifier faceCascade = new CascadeClassifier();
            CascadeClassifier eyesCascade = new CascadeClassifier();

            Preprocess.LoadCascade(faceCascade, "haarcascade_frontalface_alt2.xml"); // 얼굴 탐지기 로드
            Preprocess.LoadCascade(eyesCascade, "haarcascade_eye.xml"); // 눈 탐지기 로드

            Mat image = Cv2.ImRead("C:\\Users\\Admin\\Desktop\\OpenCv\\opencv_ppt_cpp\\예제_11.3\\face_img\\face\\59.jpg", ImreadModes.Color);
            if (image.Empty())
                throw new Exception("이미지를 찾을 수 없습니다.");

            Mat gray = Preprocess.Preprocessing(image);

            //List<Rect> faces = new List<Rect>();
            //List<Rect> eyes = new List<Rect>();
            List<Rect> subObj = new List<Rect>();
            List<Point2f> eyesCenter = new List<Point2f>();

            Rect[] detectedFaces = faceCascade.DetectMultiScale(gray, 1.1, 2, HaarDetectionTypes.ScaleImage, new Size(100, 100));
            if (detectedFaces.Length > 0)
            {
                Rect[] detectedEyes = eyesCascade.DetectMultiScale(gray[detectedFaces[0]], 1.15, 7, HaarDetectionTypes.ScaleImage, new Size(25, 20));
                if (detectedEyes.Length == 2)
                {
                    eyesCenter.Add(CorrectAngle.CalcCenter(detectedEyes[0] + detectedFaces[0].TopLeft));
                    eyesCenter.Add(CorrectAngle.CalcCenter(detectedEyes[1] + detectedFaces[0].TopLeft));

                    Point2f faceCenter = CorrectAngle.CalcCenter(detectedFaces[0]);
                    Mat rotMat = CorrectAngle.CalcRotMap(faceCenter, eyesCenter);
                    Mat correctImg = CorrectAngle.CorrectImage(image, rotMat, ref eyesCenter);

                    Point2d faceCenterD = new Point2d(faceCenter.X, faceCenter.Y);
                    DetectArea.DetectHair(faceCenterD, detectedFaces[0], subObj);
                    subObj.Add(DetectArea.DetectLip(faceCenterD, detectedFaces[0]));

                    Cv2.ImShow("sub_obj[0]", correctImg[subObj[0]]);
                    Cv2.ImShow("sub_obj[1]", correctImg[subObj[1]]);

                    Cv2.Rectangle(correctImg, subObj[2], new Scalar(255, 0, 0), 2);
                    Cv2.Rectangle(correctImg, subObj[3], new Scalar(0, 255, 0), 2);


                    foreach (var eye in eyesCenter)
                    {
                        Cv2.Circle(correctImg, new Point((int)eye.X, (int)eye.Y), 5, new Scalar(0, 255, 0), 2);
                    }

                    Cv2.ImShow("correct_img", correctImg);
                    
                }
                Cv2.WaitKey();
            }
        }
    }
}

