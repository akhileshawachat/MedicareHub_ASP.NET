using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Razorpay.Api;
using WebAppDemoStudent.Models;

namespace WebAppDemoStudent.Controllers
{
    public class OrderController : Controller
    {

        [BindProperty]
        public OrderEntity _OrderDetails { get; set; }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InitiateOrder()
        {
            string key = "rzp_test_HhgkYLDKc9OTjS";
            string secret = "d64JmG8AgZMgOEI2jZow1Ykd";

            Random _random = new Random();
            string TransactionId = _random.Next(0, 1000).ToString();



            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", Convert.ToDecimal(_OrderDetails.TotalAmount) * 100); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", "TransactionId");

            

            RazorpayClient client = new RazorpayClient(key, secret);

            Razorpay.Api.Order order = client.Order.Create(input);
            ViewBag.orderid = order["id"].ToString();


            return View("Payment", _OrderDetails);
        }

        public IActionResult Payment(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature)
        {
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            attributes.Add("razorpay_payment_id", razorpay_payment_id);
            attributes.Add( "razorpay_signature", razorpay_signature);
            attributes.Add("razorpay_order_id", razorpay_order_id);

            Utils.verifyPaymentSignature(attributes);

            OrderEntity orderdetails = new OrderEntity();
            orderdetails.TransactionId = razorpay_order_id;
            //orderdetails.OrderId = razorpay_order_id;

            return View("PaymentSuccess", orderdetails);

        }



        
    }
}
