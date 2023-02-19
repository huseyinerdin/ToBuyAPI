using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyAPI.Application.Abstractions.Storage;

namespace ToBuyAPI.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;
        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName { get => _storage.GetType().Name;}

        public async Task DeleteAsync(string path, string fileName) =>await _storage.DeleteAsync(path, fileName);

        public List<string> GetFiles(string path) => _storage.GetFiles(path);

        public bool HasFile(string path, string fileName) => _storage.HasFile(path, fileName);

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files) =>await _storage.UploadAsync(path, files);
    }
}
