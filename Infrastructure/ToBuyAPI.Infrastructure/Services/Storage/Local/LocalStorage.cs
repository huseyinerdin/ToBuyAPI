using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ToBuyAPI.Application.Abstractions.Storage.Local;

namespace ToBuyAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : Storage, ILocalStorage
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task DeleteAsync(string path, string fileName) =>File.Delete($"{path}\\{fileName}");

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string path, string fileName) => File.Exists($"{path}\\{fileName}");

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            try
            {
				List<(string fileName, string path)> datas = new();
				foreach (IFormFile file in files)
				{
					string newFileName = FileRename(uploadPath, file.FileName, HasFile);
					string fullPath = Path.Combine(uploadPath, newFileName);
					bool result = await FileCopyAsync(fullPath, file);
					datas.Add((newFileName, fullPath));
				}
				return datas;
			}
            catch (Exception)
            {
                //TODO: Error durumları için Error nesneleri ayarlanmalı.
                return null;
            }
        }
        private async Task<bool> FileCopyAsync(string path, IFormFile file)
        {
            try
            {
                using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
