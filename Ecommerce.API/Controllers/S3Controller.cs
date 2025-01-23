using Ecommerce.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class S3Controller : Controller
    {
        private readonly IS3Service _S3Service;

        public S3Controller(IS3Service s3Service)
        {
            _S3Service = s3Service;
        }

        [HttpPost]
        public async Task<string> UploadFileAsync(IFormFile file)
        {

            var response = await _S3Service.UploadFileAsync(file);

            return response.ToString();
        }
    }
}
