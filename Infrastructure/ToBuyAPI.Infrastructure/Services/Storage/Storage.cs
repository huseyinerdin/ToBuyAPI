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

        private string FileNameRegulatory(string fileName, string extension, string path, HasFile hasFileMethod, int index = 1)
        {
			string newFileName = $"{fileName}{extension}";
			if (index > 1)
			{
				newFileName = $"{fileName}-{index}{extension}";
			}
			if (hasFileMethod(path,$"{newFileName}"))
            {
                return FileNameRegulatory(fileName, extension, path, hasFileMethod, ++index);
            }
            return newFileName;
        }
    }
}
