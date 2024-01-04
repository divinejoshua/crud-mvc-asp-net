using System;
using Azure.Storage.Blobs;

namespace RunGroopWebApp.Interfaces
{
	public interface IFileService
	{
        Task<BlobClient> UploadFileAsync(IFormFile file);
    }
}

