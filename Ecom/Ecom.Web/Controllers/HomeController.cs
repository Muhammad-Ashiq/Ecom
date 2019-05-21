﻿using Ecom.Service;
using Ecom.Web.ViewModels;
using System.Web.Mvc;

namespace Ecom.Web.Controllers
{
    public class HomeController : Controller
    {
        CategoriesService categoryService = new CategoriesService();

        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.FeaturedCategories = categoryService.GetFeaturedCategories();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}