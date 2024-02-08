using BusinessObjects.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using RentingCarAPI.ViewModel;
using RentingCarServices.ServiceInterface;

namespace RentingCarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly IReviewService _reviewService;
        private readonly Cloudinary _cloudinary;

        public ReviewController(ILogger<ReviewController> logger, IReviewService reviewService,
            Cloudinary cloudinary)
        {
            _logger = logger;
            _reviewService = reviewService;
            _cloudinary = cloudinary;
        }

        [HttpGet("images/get", Name = "Get All Review Images")]
        public IActionResult GetAllReviewImages([FromQuery] int page, [FromQuery] int quantity)
        {
            try
            {
                var reviewImageList = _reviewService.GetReviewImages(page, quantity);
                return Ok(reviewImageList);
            }
            catch (Exception)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot get review images list"
                });
            }
        }
        [HttpPost("image/add", Name = "Upload Review Image")]
        public IActionResult AddReviewImage(IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0)
                {
                    return BadRequest(new ResponseVM
                    {
                        Message = $"Cannot Upload Image",
                        Errors = new string[] { "File is null" }
                    });
                }

                //upload file to cloudinary
                var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "exe201/Review"
                };
                var uploadResult = _cloudinary.Upload(uploadParams);


                //add image to database
                ReviewImage uploadImage = new ReviewImage
                {
                    ImagesLink = uploadResult.Url.ToString(),
                };
                var check = _reviewService.AddReviewImage(uploadImage);
                if (!check)
                {
                    return BadRequest(new ResponseVM
                    {
                        Message = $"Cannot Upload Image {file.FileName}",
                        Errors = new string[] { "Cannot Upload Image" }
                    });
                }
                return Ok(new ResponseVMWithEntity<ReviewImage>
                {
                    Message = $"Successfully Upload Image {file.FileName}",
                    Entity = uploadImage
                });
            }
            catch (Exception)
            {
                return BadRequest(new ResponseVM
                {
                    Message = $"Cannot Upload Image {file.FileName}",
                    Errors = new string[] { "Cannot Upload Image" }
                });
            }
        }
    }
}
