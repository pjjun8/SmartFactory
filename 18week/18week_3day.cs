//////////////////////////
//(ASP.NET Core MVC + OpenCv)
//////////////////////////
//HomeController.cs
using Development_of_contrast_improvement_service01.Models;
using Microsoft.AspNetCore.Mvc;
using OpenCvSharp;
using System.Diagnostics;

namespace Development_of_contrast_improvement_service01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UploadSuccess()
        {
            return View();
        }

        // 이미지 업로드를 처리하는 액션 메서드
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            // 이미지 파일이 있는지 확인
            if (imageFile != null && imageFile.Length > 0)
            {
                // 저장할 경로 지정 (wwwroot/images 폴더에 저장)
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                // 디렉토리가 존재하지 않으면 생성
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // 파일명을 고유하게 지정 (GUID 사용)
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                // 파일을 서버에 저장
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // OpenCVSharp을 사용하여 이미지를 불러옴
                Mat img = Cv2.ImRead(filePath);

                // 히스토그램 평활화를 적용할 새로운 이미지 경로
                var enhancedFileName = "enhanced_" + fileName;
                var enhancedFilePath = Path.Combine(uploadPath, enhancedFileName);

                try
                {
                    // 히스토그램 평활화 적용
                    Mat equalizedImg = new Mat();
                    Cv2.CvtColor(img, equalizedImg, ColorConversionCodes.BGR2GRAY); // 흑백 변환
                    Cv2.EqualizeHist(equalizedImg, equalizedImg); // 히스토그램 평활화

                    // 평활화된 이미지를 저장
                    Cv2.ImWrite(enhancedFilePath, equalizedImg);

                    // 파일 생성 여부 확인
                    if (!System.IO.File.Exists(enhancedFilePath))
                    {
                        throw new Exception("Enhanced image file not created.");
                    }
                }
                catch (Exception ex)
                {
                    // 오류 발생 시 로그 출력 또는 예외 처리
                    Console.WriteLine("Error while processing image: " + ex.Message);
                    return View("UploadFailed");
                }

                // 원본 이미지와 평활화된 이미지 경로를 View로 전달
                ViewBag.OriginalImagePath = "/images/" + fileName;
                ViewBag.EnhancedImagePath = "/images/" + enhancedFileName;

                return View("UploadSuccess");
            }

            // 업로드 실패 시 처리
            return View("UploadFailed");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
------------------------
//Index.cshtml
@{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    <h1 class="display-4">Image Enhenced App</h1>
</div>

<div class="text-center">
    <form asp-controller="Home" asp-action="UploadImage" method="post" enctype="multipart/form-data">
        <div class="form-group">
            <label for="imageFile">이미지를 업로드 시켜 주세요.</label>
            <input type="file" class="form-control-file" id="imageFile" name="imageFile" accept="image/*" required>
        </div>
        <button type="submit" class="btn btn-primary">Upload</button>
    </form>
</div>


<div class="text-center  fixed-bottom">
    <p>Learn about <a href="https://docs.microsoft.com/aspnet/core">building Web apps with ASP.NET Core</a>.</p>
</div>
-----------------------
//UploadSuccess.cshtml
@{
    ViewData["Title"] = "Upload Success";
}

<div class="text-center">
    <h1 class="display-4">Image Uploaded Successfully!</h1>
    <p>Original and enhanced images are shown below.</p>

    <div class="row">
        <div class="col-md-6">
            <h3>Original Image</h3>
            <img src="@ViewBag.OriginalImagePath" alt="Original Image" class="img-fluid" />
        </div>
        <div class="col-md-6">
            <h3>Enhanced Image</h3>
            <img src="@ViewBag.EnhancedImagePath" alt="Enhanced Image" class="img-fluid" />
        </div>
    </div>
</div>
============================================================
============================================================
using OpenCvSharp;

namespace convert_CMY
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat BGR_img = new Mat(@"C:\Temp\opencv\color_model.jpg", ImreadModes.Color);
            if (BGR_img.Empty())
            {
                Console.WriteLine("오류났엉");
                Environment.Exit(1);
            }

            Scalar white = new Scalar(255, 255, 255);
            
            // CMY 이미지 생성 (white - BGR_img)
            Mat CMY_img = white - BGR_img;
						
						// CMY 채널 분리
            Mat[] CMY_arr = new Mat[3];
            CMY_arr = Cv2.Split(CMY_img);

            Cv2.ImShow("BGR_img", BGR_img);
            Cv2.ImShow("CMY_img", CMY_img);
            Cv2.ImShow("Yellow", CMY_arr[0]);
            Cv2.ImShow("Magenta", CMY_arr[1]);
            Cv2.ImShow("Cyan", CMY_arr[2]);

            Cv2.WaitKey(0);
        }
    }
}

============================================================
using OpenCvSharp;

namespace conver_CMYK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat BGR_img = new Mat(@"C:\Temp\opencv\color_model.jpg", ImreadModes.Color);
            if (BGR_img.Empty())
            {
                Console.WriteLine("오류났엉");
                Environment.Exit(1);
            }

            Scalar white = new Scalar(255, 255, 255);
            
            // CMY 이미지 생성 (white - BGR_img)
            Mat CMY_img = white - BGR_img;
            
             // CMY 채널 분리
            Mat[] CMY_arr = Cv2.Split(CMY_img);

						// black 채널 계산
            Mat black = new Mat();
            Cv2.Min(CMY_arr[0], CMY_arr[1], black);
            Cv2.Min(black, CMY_arr[2], black);

						// CMY에서 black을 뺀 값 계산
            CMY_arr[0] = CMY_arr[0] - black;
            CMY_arr[1] = CMY_arr[1] - black;
            CMY_arr[2] = CMY_arr[2] - black;

            Cv2.ImShow("black", black);
            Cv2.ImShow("Yellow", CMY_arr[0]);
            Cv2.ImShow("Magenta", CMY_arr[1]);
            Cv2.ImShow("Cyan", CMY_arr[2]);

            Cv2.WaitKey(0);
        }
    }
}

==========================================================
using OpenCvSharp;

namespace conver_HSV
{
    class CVUtils
    {
        public void Bgr2Hsi(Mat img, out Mat hsv)
        {
            hsv = new Mat(img.Size(), MatType.CV_32FC3);

            for (int i = 0; i < img.Rows; i++)
            {
                for (int j = 0; j < img.Cols; j++)
                {
                    float B = img.At<Vec3b>(i, j)[0];
                    float G = img.At<Vec3b>(i, j)[1];
                    float R = img.At<Vec3b>(i, j)[2];

                    float s = 1 - 3 * Math.Min(R, Math.Min(G, B)) / (R + B + G);
                    float v = (R + G + B) / 3.0f;

                    float tmp1 = ((R - G) + (R - B)) * 0.5f;
                    float tmp2 = (float)Math.Sqrt((R - G) * (R - B) + (G - B) * (G - B));
                    //float angle = (float)Math.Acos(tmp1 / tmp2) * (180f / Math.PI);
                    float angle = (float)(Math.Acos(tmp1 / tmp2) * (180.0 / Math.PI));
                    float h = (B <= G) ? angle : 360 - angle;

                    hsv.Set(i, j, new Vec3f(h / 2, s * 255, v));
                }
            }
            hsv.ConvertTo(hsv, MatType.CV_8U);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat BGR_img = Cv2.ImRead("c:/Temp/opencv/color_space.jpg", ImreadModes.Color);
            if (BGR_img.Empty())
            {
                throw new Exception("이미지를 읽을 수 없습니다.");
            }

            Mat HSI_img = new Mat();
            Mat HSV_img = new Mat();
            Mat[] hsi = new Mat[3];
            Mat[] hsv = new Mat[3];

            CVUtils cvUtils = new CVUtils();
            cvUtils.Bgr2Hsi(BGR_img, out HSI_img);

            // OpenCV 함수를 이용하여 HSV로 변환
            Cv2.CvtColor(BGR_img, HSV_img, ColorConversionCodes.BGR2HSV);

            // HSI 및 HSV 채널 분리
            Cv2.Split(HSI_img, out hsi);
            Cv2.Split(HSV_img, out hsv);

            // 이미지 출력
            Cv2.ImShow("BGR_img", BGR_img);
            Cv2.ImShow("Hue", hsi[0]);
            Cv2.ImShow("Saturation", hsi[1]);
            Cv2.ImShow("Intensity", hsi[2]);
            Cv2.ImShow("OpenCV_Hue", hsv[0]);               // OpenCV 제공 함수 이용
            Cv2.ImShow("OpenCV_Saturation", hsv[1]);
            Cv2.ImShow("OpenCV_Value", hsv[2]);

            Cv2.WaitKey();
        }
    }
}
=============================================
using OpenCvSharp;

namespace convert_others
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 이미지 파일 로드
            Mat BGR_img = new Mat(@"C:\Temp\opencv\color_space.jpg", ImreadModes.Color);

            // 컬러 공간 변환
            Mat Gray_img = new Mat();
            Mat YCC_img = new Mat();
            Mat YUV_img = new Mat();
            Mat Lab_img = new Mat();

            Cv2.CvtColor(BGR_img, Gray_img, ColorConversionCodes.BGR2GRAY);  // 명암도 영상 변환
            Cv2.CvtColor(BGR_img, YCC_img, ColorConversionCodes.BGR2YCrCb); // YCrCb 컬러 공간 변환
            Cv2.CvtColor(BGR_img, YUV_img, ColorConversionCodes.BGR2YUV);   // YUV 컬러 공간 변환
            Cv2.CvtColor(BGR_img, Lab_img, ColorConversionCodes.BGR2Lab);   // Lab 컬러 공간 변환

            // 채널 분리
            Mat[] YCC_arr = Cv2.Split(YCC_img);
            Mat[] YUV_arr = Cv2.Split(YUV_img);
            Mat[] Lab_arr = Cv2.Split(Lab_img);

            // 이미지 출력
            Cv2.ImShow("BGR Image", BGR_img);
            Cv2.ImShow("Gray Image", Gray_img);
            Cv2.ImShow("YCC Image", YCC_img);
            Cv2.ImShow("YUV Image", YUV_img);
            Cv2.ImShow("Lab Image", Lab_img);

            // 각 채널의 이미지 출력
            Cv2.ImShow("YCC Y", YCC_arr[0]);
            Cv2.ImShow("YCC Cr", YCC_arr[1]);
            Cv2.ImShow("YCC Cb", YCC_arr[2]);

            Cv2.ImShow("YUV Y", YUV_arr[0]);
            Cv2.ImShow("YUV U", YUV_arr[1]);
            Cv2.ImShow("YUV V", YUV_arr[2]);

            Cv2.ImShow("Lab L", Lab_arr[0]);
            Cv2.ImShow("Lab a", Lab_arr[1]);
            Cv2.ImShow("Lab b", Lab_arr[2]);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
===================================
using OpenCvSharp;

namespace hue_threshold01
{
    public partial class Form1 : Form
    {
        private int hueMin = 0;
        private int hueMax = 255;

        // 원본 이미지 저장용 변수
        private Mat originalImage;

        public Form1()
        {
            InitializeComponent();

            // 트랙바 초기화 및 이벤트 연결
            trackBar1.Maximum = 255;
            trackBar2.Maximum = 255;
            trackBar1.Scroll += trackBar1_Scroll;
            trackBar2.Scroll += trackBar2_Scroll;

            // 예시 이미지 로드
            LoadOriginalImage();
        }

        private void LoadOriginalImage()
        {
            originalImage = Cv2.ImRead("c:\\Temp\\opencv\\color_space.jpg", ImreadModes.Color);  // 원본 이미지 파일 경로
            pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(originalImage);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            hueMin = trackBar1.Value;
            textBox1.Text = $"trackBar1 : {hueMin}";
            ApplyHueFilter();  // 필터 적용
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            hueMax = trackBar2.Value;
            textBox2.Text = $"trackBar2 : {hueMax}";
            ApplyHueFilter();  // 필터 적용
        }

        // 트랙바로 선택한 Hue 범위를 적용한 필터링
        private void ApplyHueFilter()
        {
            if (originalImage == null) return;

            // 이미지를 HSV 색상으로 변환
            Mat hsvImage = new Mat();
            Cv2.CvtColor(originalImage, hsvImage, ColorConversionCodes.BGR2HSV);

            // Hue 범위에 따라 필터링
            Mat mask = new Mat();
            Cv2.InRange(hsvImage, new Scalar(hueMin, 50, 50), new Scalar(hueMax, 255, 255), mask);

            // 결과를 picHue에 표시
            pictureBox2.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mask);
        }
    }
}
