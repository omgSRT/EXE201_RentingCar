using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Payment.Models;
using Payment.Services;
using System.Collections.Specialized;

namespace Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private static string vnPayReturnUrl;
        private static PaymentResponseModel objectFlag;

        public PaymentsController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            return Ok(url);
        }

        /*       
               [HttpPost("GetPaymentUrl")]
               public  string GetPaymentUrl([FromBody] PaymentInformationModel model)
               {

                   var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
                   return url;   

               }
        */

        [HttpGet("PaymentCallBack")]
        public IActionResult PaymentCallBack()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            vnPayReturnUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}{Request.Path}{Request.QueryString}";
            objectFlag = response;
            return Ok(response);
        }


        [HttpGet("GetPaymentData")]
        public async Task<IActionResult> GetPaymentData()
        {

            var url = vnPayReturnUrl;
            if (url == null)
            {
                return BadRequest();
            }
            Console.Write(url.ToString());
            NameValueCollection queryParameters = System.Web.HttpUtility.ParseQueryString(new Uri(url).Query);

            // Map the parsed values to the PaymentResponseModel properties
            PaymentResponseModel responseModel = new PaymentResponseModel
            {
                OrderDescription = queryParameters["vnp_OrderInfo"]!,
                TransactionId = queryParameters["vnp_TransactionNo"]!,
                OrderId = queryParameters["vnp_TxnRef"]!,
                PaymentMethod = "VnPay",
                PaymentId = queryParameters["vnp_TransactionNo"]!,
                Success = objectFlag.Success,
                Token = queryParameters["vnp_SecureHash"]!,
                VnPayResponseCode = queryParameters["vnp_ResponseCode"]!
            };
            if (responseModel != null)
            {
                return Ok(responseModel);
            }
            return BadRequest();
        }



    }
}
