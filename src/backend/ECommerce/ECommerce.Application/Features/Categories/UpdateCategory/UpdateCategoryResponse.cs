using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Categories.UpdateCategory
{
    public class UpdateCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
