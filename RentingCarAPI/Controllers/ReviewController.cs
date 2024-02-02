using BusinessObjects.Models;
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

        public ReviewController(ILogger<ReviewController> logger, IReviewService reviewService)
        {
            _logger = logger;
            _reviewService = reviewService;
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
                return BadRequest(new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Cannot get review images list"
                });
            }
        }
    }
}
