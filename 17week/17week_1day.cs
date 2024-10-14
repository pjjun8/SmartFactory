using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharp002
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //그림 읽기
            Mat src = Cv2.ImRead("C:\\Temp\\a001.png", ImreadModes.Color);
            //흑백 변환
            Mat gray = new Mat();
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
            //저장
            Cv2.ImWrite("greay.png", gray);
            //출력
            Cv2.ImShow("칼라 뉴진스", src);
            Cv2.ImShow("흑백 뉴진스", gray);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
