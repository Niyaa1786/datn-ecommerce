using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Common.DTOs
{
    public class FileUploadResult
    {
        public required string ImgUrl { get; set; }
        public required string PublicId { get; set; }
    }
}
