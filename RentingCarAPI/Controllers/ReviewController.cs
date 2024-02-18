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
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<ReviewImage>), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<ReviewImage>), StatusCodes.Status200OK)]
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
        [HttpGet("get", Name = "Get All Reviews")]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<Review>), StatusCodes.Status200OK)]
        public IActionResult GetAllReviews([FromQuery] int page, [FromQuery] int quantity)
        {
            try
            {
                var reviewList = _reviewService.GetReviews(page, quantity);
                return Ok(reviewList);
            }
            catch (Exception)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot get review list"
                });
            }
        }
        [HttpPost("create", Name = "Create New Review")]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        public IActionResult CreateReview(Review review,
            IFormFile file1, IFormFile file2, IFormFile file3)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseVM
                    {
                        Message = "Cannot Create Review",
                        Errors = new string[] { "Invalid Data to Database", "Invalid Input" }
                    });
                }

                // Check if any property in the review object is null
                var properties = review.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property.GetValue(review) == null)
                    {
                        return BadRequest(new ResponseVM
                        {
                            Message = "Cannot Create Review",
                            Errors = new string[] { "One or more properties in the review object are null" }
                        });
                    }
                }
                var checkReview = _reviewService.AddReview(review);
                if (!checkReview)
                {
                    return BadRequest(new ResponseVM
                    {
                        Message = "Cannot Create Review",
                        Errors = new string[] { "Invalid Data to Database", "Invalid Input" }
                    });
                }
                int insertedReviewId = _reviewService.GetLastInsertedReviewId();

                if ((file1 == null || file1.Length <= 0)
                    && (file2 == null || file2.Length <= 0)
                    && (file3 == null || file3.Length <= 0))
                {
                    return BadRequest(new ResponseVM
                    {
                        Message = $"Cannot Upload Image",
                        Errors = new string[] { "File is null" }
                    });
                }

                UploadImage(file1, insertedReviewId);
                UploadImage(file2, insertedReviewId);
                UploadImage(file3, insertedReviewId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Create Review",
                    Errors = new string[] { ex.Message }
                });
            }
        }

        public void UploadImage(IFormFile file, int insertedReviewId)
        {
            if (file != null && file.Length != 0)
            {
                var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "exe201/Review"
                };
                var uploadResult = _cloudinary.Upload(uploadParams);
                ReviewImage uploadImage = new ReviewImage
                {
                    ImagesLink = uploadResult.Url.ToString(),
                    ReviewId = insertedReviewId,
                };
                var check = _reviewService.AddReviewImage(uploadImage);
                if (!check)
                {
                    throw new Exception($"Cannot Upload Image {file.FileName}");
                }
            }
        }
    }
}
