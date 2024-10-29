using OpenCvSharp;

namespace test02
{
    internal class Program
    {
        static void CalcHisto(Mat img, out Mat hist, Vec3i bins, Vec3f range, int dims)
        {
            dims = (dims <= 0) ? img.Channels() : dims; // 히스토그램 차원 수
            int[] channels = { 0, 1, 2 };
            int[] histSize = { bins.Item0, bins.Item1, bins.Item2 };

            Rangef[] ranges = {
                new Rangef(0, range.Item0),
                new Rangef(0, range.Item1),
                new Rangef(0, range.Item2)
            };

            hist = new Mat();
            Cv2.CalcHist(new[] { img }, channels, new Mat(), hist, dims, histSize, ranges);
            Cv2.Normalize(hist, hist, 0, 1, NormTypes.MinMax); // 정규화
        }

        static Mat DrawHisto(Mat hist)
        {
            if (hist.Dims != 2)
            {
                Console.WriteLine("히스토그램이 2차원 데이터가 아닙니다.");
                Environment.Exit(1);
            }

            float ratioValue = 512f;
            float ratioHue = 180f / hist.Rows; // 색상 스케일 비율
            float ratioSat = 256f / hist.Cols; // 채도 스케일 비율

            Mat graph = new Mat(hist.Size(), MatType.CV_32FC3);
            for (int i = 0; i < hist.Rows; i++)
            {
                for (int j = 0; j < hist.Cols; j++)
                {
                    float value = hist.At<float>(i, j) * ratioValue;
                    float hue = i * ratioHue;
                    float sat = j * ratioSat;
                    graph.Set(i, j, new Vec3f(hue, sat, value));
                }
            }

            graph.ConvertTo(graph, MatType.CV_8U);
            Cv2.CvtColor(graph, graph, ColorConversionCodes.HSV2BGR);
            Cv2.Resize(graph, graph, new Size(), 10, 10, InterpolationFlags.Nearest);

            return graph;
        }
        static List<Mat> LoadHisto(Vec3i bins, Vec3f ranges, int nImages)
        {
            List<Mat> dbHists = new List<Mat>();

            for (int i = 0; i < nImages; i++)
            {
                string fname = $"C:/Users/Admin/Desktop/OpenCv/opencv_ppt_cpp/예제_11.2/DB/img_{i:D2}.jpg";
                Mat img = Cv2.ImRead(fname, ImreadModes.Color);

                if (img.Empty())
                    continue;

                Mat hsv = new Mat();
                Mat hist = new Mat();

                // HSV 컬러 변환
                Cv2.CvtColor(img, hsv, ColorConversionCodes.BGR2HSV);

                // 히스토그램 계산
                CalcHisto(hsv, out hist, bins, ranges, 2);

                dbHists.Add(hist);
            }

            Console.WriteLine($"{dbHists.Count} 개의 파일을 로드 및 히스토그램 계산 완료");
            return dbHists;
        }
        static Mat QueryImage()
        {
            while (true)
            {
                Console.Write("질의 영상 번호를 입력하세요 : ");
                int qNo = Convert.ToInt32(Console.ReadLine());

                string fname = $"C:/Users/Admin/Desktop/OpenCv/opencv_ppt_cpp/예제_11.2/DB/img_{qNo:D2}.jpg";
                Mat query = Cv2.ImRead(fname, ImreadModes.Color);

                if (query.Empty())
                {
                    Console.WriteLine("질의 영상 번호가 잘못되었습니다.");
                }
                else
                {
                    return query;
                }
            }
        }
        static Mat CalcSimilarity(Mat queryHist, List<Mat> dbHists)
        {
            Mat dbSimilarities = new Mat();
            foreach (var dbHist in dbHists)
            {
                double compare = Cv2.CompareHist(queryHist, dbHist, HistCompMethods.Correl);
                Mat compareMat = new Mat(1, 1, MatType.CV_64F, new Scalar(compare)); // `compare`를 `Scalar`로 감쌈
                dbSimilarities.PushBack(compareMat);
            }
            return dbSimilarities;
        }

        static void SelectView(double sinc, Mat dbSimilarities)
        {
            Mat sortedSim = new Mat();
            Mat mIdx = new Mat();
            //int flag = SortFlags.EveryColumn | SortFlags.Descending;

            // 유사도 정렬
            Cv2.Sort(dbSimilarities, sortedSim, SortFlags.Descending);
            Cv2.SortIdx(dbSimilarities, mIdx, SortFlags.Descending);

            for (int i = 0; i < sortedSim.Total(); i++)
            {
                double sim = sortedSim.At<double>(i);
                if (sim > sinc)
                {
                    int idx = mIdx.At<int>(i);
                    string fname = $"C:/Users/Admin/Desktop/OpenCv/opencv_ppt_cpp/예제_11.2/DB/img_{idx:D2}.jpg";

                    if (File.Exists(fname))
                    {
                        Mat img = Cv2.ImRead(fname, ImreadModes.Color);
                        string title = $"img_{idx:D3} - {sim:F2}";
                        Console.WriteLine(title);
                        Cv2.ImShow(title, img);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Vec3i bins = new Vec3i(30, 42, 0);
            Vec3f ranges = new Vec3f(180, 256, 0);

            // DB 히스토그램 로드
            List<Mat> dbHists = LoadHisto(bins, ranges, 100);
            Mat query = QueryImage();

            Mat hsv = new Mat();
            Mat queryHist = new Mat();

            // HSV 컬러 변환 및 히스토그램 계산
            Cv2.CvtColor(query, hsv, ColorConversionCodes.BGR2HSV);
            CalcHisto(hsv, out queryHist, bins, ranges, 2);
            Mat histImg = DrawHisto(queryHist);

            // 유사도 계산
            Mat dbSimilarities = CalcSimilarity(queryHist, dbHists);

            // 기준 유사도 입력받기
            Console.Write("기준 유사도 입력: ");
            double sinc = Convert.ToDouble(Console.ReadLine());

            // 유사도 기준에 따라 필터링하여 이미지 출력
            SelectView(sinc, dbSimilarities);

            // 이미지 출력
            Cv2.ImShow("Query Image", query);
            Cv2.ImShow("Histogram Image", histImg);
            Cv2.WaitKey();
        }
    }
}
