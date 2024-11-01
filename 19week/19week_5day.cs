//안되는듯
using OpenCvSharp;
using OpenCvSharp.ML;

namespace test05
{
    internal class FindPlates
    {
        // 학습 데이터 읽기 함수
        public static void ReadTrainData(string fn, out Mat trainingData, out Mat classes)
        {
            using var fs = new FileStorage(fn, FileStorage.Modes.Read);
            if (!fs.IsOpened())
                throw new Exception("파일 열기 실패");

            // FileNode를 사용하여 데이터를 읽고 Mat으로 변환
            var trainingDataNode = fs["TrainingData"];
            var classesNode = fs["classes"];

            trainingData = new Mat();
            classes = new Mat();

            trainingDataNode.ReadMat(trainingData);
            classesNode.ReadMat(classes);

            trainingData.ConvertTo(trainingData, MatType.CV_32FC1);
        }

        // 이미지 전처리 함수
        public static Mat Preprocessing(Mat image, out Mat morph)
        {
            Mat gray = new Mat(), sobel = new Mat(), thImg = new Mat();
            morph = new Mat(); // out 매개변수 초기화

            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(5, 25));
            Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);
            Cv2.Blur(gray, gray, new Size(5, 5));
            Cv2.Sobel(gray, gray, MatType.CV_8U, 1, 0, 3);

            Cv2.Threshold(gray, thImg, 120, 255, ThresholdTypes.Binary);
            Cv2.MorphologyEx(thImg, morph, MorphTypes.Close, kernel);

            return morph;
        }

        // 번호판 검증 함수
        public static bool VerifyPlate(RotatedRect mr)
        {
            float size = mr.Size.Width * mr.Size.Height;
            float aspect = mr.Size.Width / mr.Size.Height;
            if (aspect < 1) aspect = 1 / aspect;

            bool ch1 = size > 1000 && size < 50000;
            bool ch2 = aspect > 1.3 && aspect < 6.4;

            return ch1 && ch2;
        }

        // 후보 번호판 영역 찾기 함수
        public static void FindCandidates(Mat img, List<RotatedRect> candidates)
        {
            Cv2.FindContours(img.Clone(), out var contours, out _, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            foreach (var contour in contours)
            {
                RotatedRect rotRect = Cv2.MinAreaRect(contour);
                if (VerifyPlate(rotRect))
                    candidates.Add(rotRect);
            }
        }

        // 회전된 사각형 그리기 함수
        public static void DrawRotatedRect(Mat img, RotatedRect mr, Scalar color, int thickness = 2)
        {
            Point2f[] pts = mr.Points();
            for (int i = 0; i < 4; ++i)
            {
                Cv2.Line(img, pts[i].ToPoint(), pts[(i + 1) % 4].ToPoint(), color, thickness);
            }
        }

        // 후보 영역 개선 함수
        public static RotatedRect RefineCandidate(Mat image, RotatedRect candidate)
        {
            Size newSize = new Size(image.Size().Width + 2, image.Size().Height + 2);
            Mat fill = new Mat(newSize, MatType.CV_8UC1, Scalar.All(0));
            Scalar dif1 = new Scalar(25, 25, 25), dif2 = new Scalar(25, 25, 25);
            FloodFillFlags flags = FloodFillFlags.Link4 | FloodFillFlags.FixedRange;
            Random random = new Random();
            List<Point2f> randPts = new List<Point2f>();

            for (int i = 0; i < 10; i++)
            {
                randPts.Add(new Point2f((float)random.NextDouble() * 7, (float)random.NextDouble() * 7));
            }

            Rect imgRect = new Rect(new Point(0, 0), image.Size());
            foreach (var offset in randPts)
            {
                Point2f seed = candidate.Center + offset;
                if (imgRect.Contains(new Point((int)seed.X, (int)seed.Y)))
                {
                    Cv2.FloodFill(image, fill, new Point((int)seed.X, (int)seed.Y), Scalar.All(0), out _, dif1, dif2, flags);
                }
            }

            List<Point> fillPts = new List<Point>();
            for (int i = 0; i < fill.Rows; i++)
            {
                for (int j = 0; j < fill.Cols; j++)
                {
                    if (fill.At<byte>(i, j) == 255)
                        fillPts.Add(new Point(j, i));
                }
            }

            return Cv2.MinAreaRect(fillPts);
        }

        // 후보 번호판 영역 보정 함수
        public static Mat CorrectPlate(Mat input, RotatedRect roRect)
        {
            Size mSize = new Size((int)roRect.Size.Width, (int)roRect.Size.Height);
            float aspect = (float)mSize.Width / mSize.Height;
            float angle = roRect.Angle;

            if (aspect < 1)
            {
                angle += 90;
                mSize = new Size(mSize.Height, mSize.Width);
            }

            Mat rotMat = Cv2.GetRotationMatrix2D(roRect.Center, angle, 1);
            Mat rotImg = new Mat();
            Cv2.WarpAffine(input, rotImg, rotMat, input.Size());

            Mat correctImg = new Mat();
            Cv2.GetRectSubPix(rotImg, mSize, roRect.Center, correctImg);
            Cv2.Resize(correctImg, correctImg, new Size(144, 28), 0, 0, InterpolationFlags.Cubic);

            return correctImg;
        }

        // SVM 학습 함수
        public static SVM SVMTrain(string fn)
        {
            ReadTrainData(fn, out var trainingData, out var labels);

            var svm = SVM.Create();
            svm.Type = SVM.Types.CSvc;
            svm.KernelType = SVM.KernelTypes.Linear;
            svm.Gamma = 1;
            svm.C = 1;
            //svm.TermCriteria = new TermCriteria(TermCriteria.Type.Eps | TermCriteria.Type.MaxIter, 1000, 0.01);
            //svm.TermCriteria = new TermCriteria(CriteriaType.Eps | CriteriaType.MaxIter, 1000, 0.01);
            svm.TermCriteria = new TermCriteria(CriteriaTypes.Eps | CriteriaTypes.MaxIter, 1000, 0.01);
            svm.Train(trainingData, SampleTypes.RowSample, labels);

            return svm;
        }
        public static List<Mat> MakeCandidates(Mat img, List<RotatedRect> roRects)
        {
            var candidates = new List<Mat>();
            for (int i = 0; i < roRects.Count;)
            {
                roRects[i] = RefineCandidate(img, roRects[i]); // RefineCandidate에서 반환된 값을 사용하여 업데이트

                if (VerifyPlate(roRects[i]))
                {
                    Mat corrImg = CorrectPlate(img, roRects[i]);
                    Cv2.CvtColor(corrImg, corrImg, ColorConversionCodes.BGR2GRAY);
                    candidates.Add(corrImg); // 후보 이미지 추가

                    Cv2.ImShow("plate_img - " + i, corrImg);
                    Cv2.ResizeWindow("plate_img - " + i, 200, 50); // 창 크기 조정
                    i++;
                }
                else
                {
                    roRects.RemoveAt(i); // 조건에 맞지 않으면 제거
                }
            }
            return candidates;
        }

        // 후보 이미지 분류 함수
        public static int ClassifyPlates(SVM svm, List<Mat> candidateImages)
        {
            for (int i = 0; i < candidateImages.Count; i++)
            {
                Mat oneRow = candidateImages[i].Reshape(1, 1);
                oneRow.ConvertTo(oneRow, MatType.CV_32F);

                Mat results = new Mat();
                svm.Predict(oneRow, results);

                if (results.At<float>(0) == 1)
                    return i;
            }
            return -1;
        }
    }
    internal class Program
    {
        public static bool KeyCheck(ref int no)
        {
            int key = Cv2.WaitKey();
            if (key == 2621440) no++;
            else if (key == 2490368) no--;
            else if (key == 32 || key == 27) return false;
            return true;
        }
        static void Main(string[] args)
        {
            int no = 0;
            do
            {
                Cv2.DestroyAllWindows();
                string fn = $"C:/Users/Admin/Desktop/OpenCv/opencv_ppt_cpp/예제_12.2/image/test_car/{no:08}.jpg";
                Mat image = Cv2.ImRead(fn, ImreadModes.Color);
                if (image.Empty())
                    throw new Exception("이미지를 불러올 수 없습니다.");

                Mat morph;
                Mat plateImg = FindPlates.Preprocessing(image, out morph);
                List<RotatedRect> candidates = new List<RotatedRect>();
                FindPlates.FindCandidates(morph, candidates);

                List<Mat> candidateImgs = new List<Mat>();
                foreach (var candidate in candidates)
                {
                    Mat correctedImg = FindPlates.CorrectPlate(image, candidate);
                    Cv2.CvtColor(correctedImg, correctedImg, ColorConversionCodes.BGR2GRAY);
                    candidateImgs.Add(correctedImg);
                    Cv2.ImShow("Candidate Plate", correctedImg);
                }

                Cv2.ImShow("Original Image", image);
            }
            while (KeyCheck(ref no));
        }
    }
}
