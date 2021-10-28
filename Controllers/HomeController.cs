using EssentialToolsMVC.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EssentialToolsMVC.Controllers
{
    public class HomeController : Controller
    {
        private IValueCalculator _calc;
        private Product[] products = {
             new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
             new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
             new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
             new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
         };

        public HomeController(IValueCalculator calcParam)
        {
            _calc = calcParam;
        }
        public ActionResult Index()
        {
            //// instance that controls all the interfaces an thrie corresponding clasess
            //IKernel ninjectKernel = new StandardKernel();

            //// telling ninject wich interface is gointo to use what class implementation
            //ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();

            //// getting the interface implementation previously registered
            //IValueCalculator calc = ninjectKernel.Get<IValueCalculator>();

            ShoppingCart cart = new ShoppingCart(_calc)
            {
                Products = products
            };

            decimal totalValue = cart.CalculateProductTotal();
            return View(totalValue);
        }
    }
}

