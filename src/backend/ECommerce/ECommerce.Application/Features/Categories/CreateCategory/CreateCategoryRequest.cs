using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Categories.CreateCategory
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
