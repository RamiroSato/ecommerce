using Amazon;
using Amazon.Internal;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Ecommerce.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class S3Service : IS3Service
    {
        private readonly AmazonS3Client _s3Client;
        private readonly string _bucketName;

        public S3Service(IConfiguration configuration)
        {
            _s3Client = new AmazonS3Client(
                new BasicAWSCredentials(configuration["AccessKeyId"], configuration["SecretAccessKey"]),
                RegionEndpoint.GetBySystemName(configuration["Region"])
                );
            _bucketName = configuration["AWS_BucketName"];
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var contentType = file.ContentType;

            Stream fileStream = file.OpenReadStream();
            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                InputStream = fileStream,
                ContentType = contentType
            };

            var response = await _s3Client.PutObjectAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
            }
            else
            {
                throw new Exception("No se pudo subir el archivo");
            }
        }
    }
}
