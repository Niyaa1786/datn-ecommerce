using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ECommerce.Application.Common.DTOs;
using ECommerce.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ECommerce.Infrastructure.Services
{
    public class CloudinaryService : IFileUploadService
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryService(IConfiguration configuration)
        {

            var cloudinarySettings = configuration.GetSection("CloudinarySettings");
            var cloudName = cloudinarySettings["CloudName"];
            var apiKey = cloudinarySettings["ApiKey"];
            var apiSecret = cloudinarySettings["ApiSecret"];

            if (string.IsNullOrEmpty(cloudName) ||
                string.IsNullOrEmpty(apiKey) ||
                string.IsNullOrEmpty(apiSecret))
                throw new Exception("Cloudinary configuration is missing.");

            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);

        }

        public async Task<FileUploadResult> UploadFileAsync(IFormFile file, string folder, string publicId, bool overwrite = false, CancellationToken ct = default)
        {
            await using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = folder,
                PublicId = publicId,
                Overwrite = overwrite,
                Invalidate = overwrite,
                Transformation = new Transformation()
                    .Width(1200).Crop("limit")
                    .Quality("auto:good")
                    .FetchFormat("auto")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams, ct);

            if (uploadResult.Error is not null)
                throw new Exception($"Cloudinary upload failed: {uploadResult.Error.Message}");

            return new FileUploadResult
            {
                PublicId = uploadResult.PublicId,
                ImgUrl = uploadResult.SecureUrl.ToString(),
            };
        }

        public async Task<IEnumerable<FileUploadResult>> UploadFilesAsync(IEnumerable<IFormFile> files, string folder, CancellationToken ct = default)
        {
            var uploadTasks = files.Select(async (file, index) =>
            {
                await using var stream = file.OpenReadStream();

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = folder,
                    UniqueFilename = true,
                    Transformation = new Transformation()
                    .Width(1200).Crop("limit")
                    .Quality("auto:good")
                    .FetchFormat("auto")
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams, ct);

                return new
                {
                    Index = index,
                    Result = new FileUploadResult
                    {
                        PublicId = uploadResult.PublicId,
                        ImgUrl = uploadResult.SecureUrl.ToString()
                    }
                };
            });

            var resultsWithIndex = await Task.WhenAll(uploadTasks);

            return resultsWithIndex
                .OrderBy(x => x.Index)
                .Select(r => r.Result)
                .ToList();
        }

        public async Task<bool> DeleteFileAsync(string publicId, CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(publicId))
                return false;

            var deletionParams = new DeletionParams(publicId);

            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

            return deletionResult.Result == "ok";
        }

        public async Task<bool> DeleteFilesAsync(IEnumerable<string> publicIds, CancellationToken ct = default)
        {
            var idsList = publicIds.Where(id => !string.IsNullOrEmpty(id)).Distinct().ToList();
            if (!idsList.Any()) return true;

            var delResParams = new DelResParams
            {
                PublicIds = idsList,
                ResourceType = ResourceType.Image,
                Invalidate = true
            };
            var deletionResult = await _cloudinary.DeleteResourcesAsync(delResParams, ct);

            return deletionResult.StatusCode == HttpStatusCode.OK;
        }
    }
}
