using System;

namespace GameBox.Services.Models.View.Categories
{
    public class ListCategoriesViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Games { get; set; }
    }
}