using ECommerce.Application.Common.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Common.Interfaces
{
    public interface IFileUploadService
    {
        Task<FileUploadResult> UploadFileAsync(IFormFile file, string folder, string publicId, bool overwrite = false, CancellationToken ct = default);
        Task<IEnumerable<FileUploadResult>> UploadFilesAsync(IEnumerable<IFormFile> files, string folder, CancellationToken ct = default);
        Task<bool> DeleteFileAsync(string publicId, CancellationToken ct = default);
        Task<bool> DeleteFilesAsync(IEnumerable<string> publicIds, CancellationToken ct = default);
    }
}
