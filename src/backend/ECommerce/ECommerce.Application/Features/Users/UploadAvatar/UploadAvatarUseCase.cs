using FluentValidation;
using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Users.UploadAvatar
{
    public class UploadAvatarUseCase : IUseCase<UploadAvatarRequest, UploadAvatarResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileUploadService _fileUploadService;

        public UploadAvatarUseCase(IUnitOfWork unitOfWork, IFileUploadService fileUploadService)
        {
            _unitOfWork = unitOfWork;
            _fileUploadService = fileUploadService;
        }

        public async Task<UploadAvatarResponse> ExecuteAsync(UploadAvatarRequest request, CancellationToken ct = default)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, ct);
            if (user == null)
                throw new NotFoundException("User not found.");

            var folderName = "avatars";
            var publicId = request.UserId.ToString();
            var overWrite = true;

            var avatarUrl = await _fileUploadService.UploadFileAsync(request.file, folderName, publicId, overWrite, ct);

            user.UpdateAvatar(avatarUrl.ImgUrl);
            await _unitOfWork.SaveChangesAsync(ct);

            return new UploadAvatarResponse
            {
                AvatarUrl = avatarUrl.ImgUrl
            };
        }
    }
}
