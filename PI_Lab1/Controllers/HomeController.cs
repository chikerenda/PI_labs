using System;
using System.Web.Mvc;
using PI_Lab1.Models;

namespace PI_Lab1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(CalculatorModel cm)
        {
            double firstNumber = 0;
            double secondNumber = 0;

            try
            {
                firstNumber = Convert.ToDouble(cm.FirstNumber);
                secondNumber = Convert.ToDouble(cm.SecondNumber);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Could not convert entered data to double.");
                return View(cm);
            }
            double result = 0;
            switch (cm.Symbol)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
                case "*":
                    result = firstNumber*secondNumber;
                    break;
                case "/":
                    if (secondNumber.Equals(0))
                    {
                        ModelState.AddModelError("", "Division by zero.");
                        return View(cm);
                    }
                    result = firstNumber / secondNumber;
                    break;
            }

            return RedirectToAction("Result", "Home", new {result});
        }

        [HttpGet]
        public ActionResult Result(double result)
        {
            ViewBag.Result = result;
            return View();
        }
    }
}