using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Interfaces
{
    public interface IS3Service
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}
