using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electronics_store_db_wpf.Helper
{
    public static class FilePathHelper
    {

        public static string _baseDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));


        public static string GetRelativePathForSaving(string imagePath, string relativePath)
        {
            // Проверяем, является ли путь абсолютным
            bool isAbsolutePath = System.IO.Path.IsPathRooted(imagePath);

            if (isAbsolutePath)
            {

                string fullRelativePath = System.IO.Path.Combine(_baseDirectory, relativePath);

                if (imagePath.StartsWith(fullRelativePath))
                {
                    // Файл уже находится в относительном пути, возвращаем его относительный путь
                    imagePath = imagePath.Substring(_baseDirectory.Length)
                                         .TrimStart(System.IO.Path.DirectorySeparatorChar);
                }
                else
                {
                    // Файл находится за пределами относительного пути, копируем его в папку и возвращаем новый относительный путь
                    string newFileName = System.IO.Path.GetFileName(imagePath);
                    string newFilePath = System.IO.Path.Combine(fullRelativePath, newFileName);

                    System.IO.Directory.CreateDirectory(fullRelativePath);
                    System.IO.File.Copy(imagePath, newFilePath, true);

                    imagePath = System.IO.Path.Combine(relativePath, newFileName);
                }
            }

            return imagePath;
        }
        public static string GetRelativePathForRetrieval(string imagePath)
        {

            if (System.IO.Path.IsPathRooted(imagePath))
            {
                return imagePath;
            }

            return System.IO.Path.Combine(_baseDirectory, imagePath);
        }
    }
}
