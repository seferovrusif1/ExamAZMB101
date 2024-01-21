﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.Admin.ViewModels.ItemVMs
{
    public class ListItemVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Category { get; set; }
        public bool IsDeleted { get; set; }

    }
}
