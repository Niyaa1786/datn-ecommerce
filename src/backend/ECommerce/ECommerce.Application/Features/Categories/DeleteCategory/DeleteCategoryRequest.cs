using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerce.Application.Features.Categories.DeleteCategory
{
    public class DeleteCategoryRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}
