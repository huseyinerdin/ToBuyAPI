using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ToBuyAPI.Application.Abstractions.Storage.Local;
using ToBuyAPI.Infrastructure.Operations;

namespace ToBuyAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : ILocalStorage
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task DeleteAsync(string path, string fileName) => File.Delete($"{path}\\{fileName}");


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

            List<(string fileName, string path)> datas = new();
            foreach (IFormFile file in files)
            {
                string newFileName = await FileRenameAsync(uploadPath, file.FileName);
                string fullPath = Path.Combine(uploadPath, newFileName);
                bool result = await FileCopyAsync(fullPath, file);
                datas.Add((newFileName, fullPath));
            }
            return datas;
        }
        public async Task<bool> FileCopyAsync(string filePath, IFormFile file)
        {
            try
            {
                using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> FileRenameAsync(string path, string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string oldFileName = Path.GetFileNameWithoutExtension(fileName);
            string newFileName = NameOperation.CharacterRegulatory(oldFileName);
            return FileNameRegulatory(newFileName, extension, path);
        }

        /// <summary>
        /// Girilen path de benzer isimde dosya var ise dosya adının sonuna numerik rakamlar koyarak geri döndürür.
        /// Eğer yok ise aynı ismi geri döndürür.(fileName-index.extension gibi)
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="extension"></param>
        /// <param name="path"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private string FileNameRegulatory(string fileName, string extension, string path, int index = 2)
        {
            if (File.Exists($"{path}\\{fileName}{extension}"))
            {
                return FileNameRegulatory($"{fileName}-{index}", extension, path, index++);
            }
            return fileName + extension;
        }
    }
}
