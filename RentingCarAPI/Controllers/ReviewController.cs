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

        [HttpGet("Images/GetImages", Name = "Get All Review Images")]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<ReviewImage>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        public IActionResult GetAllReviewImages([FromQuery] int page, [FromQuery] int quantity)
        {
            try
            {
                var reviewImageList = _reviewService.GetReviewImages(page, quantity);
                if (!reviewImageList.Any())
                {
                    return NotFound(new ResponseVM
                    {
                        Message = "Review Image List is Empty",
                        Errors = new string[] { "There's No Data in Database" }
                    });
                }
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
        [HttpPost("Image/AddImages", Name = "Demo Upload Review Image")]
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
        [HttpGet("GetReviews", Name = "Get All Reviews")]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<Review>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        public IActionResult GetAllReviews([FromQuery] int page, [FromQuery] int quantity, [FromQuery] int? filterPoint)
        {
            try
            {
                var reviewList = _reviewService.GetReviews(page, quantity, filterPoint);
                if (!reviewList.Any())
                {
                    return NotFound(new ResponseVM
                    {
                        Message = "Review List is Empty",
                        Errors = new string[] { "There's No Data in Database" }
                    });
                }
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
        [HttpPost("CreateReview", Name = "Create New Review")]
        [ProducesResponseType(typeof(ResponseVMWithEntity<ReviewRequestVM>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        public IActionResult CreateReview([FromForm] ReviewRequestVM reviewRequest)
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

                Review newReview = new Review
                {
                    Description = reviewRequest.Description,
                    Point = reviewRequest.Point,
                    AccountId = reviewRequest.AccountId,
                    VehicleId = reviewRequest.VehicleId,
                    StatusId = reviewRequest.StatusId,
                };

                var checkReview = _reviewService.AddReview(newReview);
                if (!checkReview)
                {
                    return BadRequest(new ResponseVM
                    {
                        Message = "Cannot Create Review",
                        Errors = new string[] { "Invalid Data to Database", "Invalid Input" }
                    });
                }
                long insertedReviewId = newReview.ReviewId;

                if (reviewRequest.Files != null)
                {
                    foreach (var file in reviewRequest.Files)
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
                                return BadRequest(new ResponseVM
                                {
                                    Message = "Cannot Upload Images",
                                    Errors = new string[] { "Error While Inserting Data" }
                                });
                            }
                        }
                    }
                }

                return Ok(new ResponseVMWithEntity<ReviewRequestVM>
                {
                    Message = "Add Successfully",
                    Entity = reviewRequest,
                });
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

        [HttpPut("DeleteReview/{id}", Name = "Delete A Review")]
        [ProducesResponseType(typeof(ResponseVMWithEntity<Review>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVMWithEntity<Review>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseVM), StatusCodes.Status404NotFound)]
        public IActionResult DeleteReview([FromRoute] long id)
        {
            try
            {
                var review = _reviewService.GetReviewById(id);
                if (review == null)
                {
                    return NotFound(new ResponseVM
                    {
                        Message = "There's No Review With ID " + id,
                        Errors = new string[] { "No Review Data With ID " + id }
                    });
                }
                review.StatusId = 2;

                var check = _reviewService.UpdateReview(review);
                if (!check)
                {
                    return BadRequest(new ResponseVMWithEntity<Review>
                    {
                        Message = "Cannot Delete Review",
                        Errors = new string[] { "Error Handling Delete From Database" },
                        Entity = review
                    });
                }

                return Ok(new ResponseVMWithEntity<Review>
                {
                    Message = "Delete Successfully",
                    Entity = review
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseVM
                {
                    Message = "Cannot Delete Review",
                    Errors = new string[] { ex.Message }
                });
            }
        }
    }
}
