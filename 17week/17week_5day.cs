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
