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
            Mat CMY_img = white - BGR_img;

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
            Mat CMY_img = white - BGR_img;
            Mat[] CMY_arr = Cv2.Split(CMY_img);

            Mat black = new Mat();
            Cv2.Min(CMY_arr[0], CMY_arr[1], black);
            Cv2.Min(black, CMY_arr[2], black);

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
