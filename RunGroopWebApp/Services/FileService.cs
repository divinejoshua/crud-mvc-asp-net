using System;
using Azure;
using System.Reflection.Metadata;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using RunGroopWebApp.Interfaces;

namespace RunGroopWebApp.Services
{
	public class FileService : IFileService
	{
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;

        //Constructor
        public FileService(IConfiguration configuration)
		{
            _storageConnectionString = configuration.GetValue<string>("BlobConnectionString");
            _storageContainerName = configuration.GetValue<string>("BlobContainerName");
        }

        public async Task<BlobClient> UploadFileAsync(IFormFile file)
        {
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            // Get a reference to the blob just uploaded from the API in a container from configuration settings
            BlobClient client = container.GetBlobClient(file.FileName);

            //checked if file exist
            if (file == null || file.Length <= 0)
            {
                return client;
            }

            // Open a stream for the file we want to upload
            await using (Stream? data = file.OpenReadStream())
            {
                // Upload the file async
                await client.UploadAsync(data);
            }
            return client;


        }
    }
}

