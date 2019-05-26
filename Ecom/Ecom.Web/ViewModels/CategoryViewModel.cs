﻿using Ecom.Models;
using System.Collections.Generic;

namespace Ecom.Web.ViewModels
{

    public class CategorySearchViewModel
    {
        public List<Category> Categories { get; set; }
        public string SearchTerm { get; set; }
    }

    public class NewCategoryViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool IsFeatured { get; set; }

     
    }


    public class EditCategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool IsFeatured { get; set; }
    }
}