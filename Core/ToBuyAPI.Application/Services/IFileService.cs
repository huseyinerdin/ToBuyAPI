using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToBuyAPI.Application.Services
{
    public interface IFileService
    {
         Task<List<(string fileName, string path)>> UploadAsync(string filePath, IFormFileCollection files);
         Task<bool> FileCopyAsync(string filePath,IFormFile file);
    }
}
