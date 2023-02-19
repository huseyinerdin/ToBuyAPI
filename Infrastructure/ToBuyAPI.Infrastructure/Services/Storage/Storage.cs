using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyAPI.Infrastructure.Operations;

namespace ToBuyAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string path, string fileName);
        protected string FileRename(string path, string fileName, HasFile hasFileMethod)
        {
            string extension = Path.GetExtension(fileName);
            string oldFileName = Path.GetFileNameWithoutExtension(fileName);
            string newFileName = NameOperation.CharacterRegulatory(oldFileName);
            return FileNameRegulatory(newFileName, extension, path, hasFileMethod);
        }

        private string FileNameRegulatory(string fileName, string extension, string path, HasFile hasFileMethod, int index = 2)
        {
            if (hasFileMethod(path,$"{fileName}{extension}"))
            {
                return FileNameRegulatory($"{fileName}-{index}", extension, path, hasFileMethod, index++);
            }
            return fileName + extension;
        }
    }
}
