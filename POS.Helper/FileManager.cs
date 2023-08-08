using System;
using System.IO;
using System.Threading.Tasks;

namespace POS.Helper
{
    public static class FileManager
    {
        public static async Task SaveFile(string file, string fileData, string path)
        {
            if (!string.IsNullOrWhiteSpace(fileData))
            {
                var pathToSave = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), path);
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                await FileData.SaveFile(Path.Combine(pathToSave, file), fileData);
            }
        }

        public static string GetUpdateFile(bool isUpdated, string oldFile, string newFile, string format)
        {
            if (isUpdated)
            {
                if (!string.IsNullOrEmpty(newFile))
                {
                    return $"{Guid.NewGuid()}.{format}";
                }
                else
                {
                    return "";
                }
            }
            return oldFile;
        }

        /*
         "" "asdasd.png"
         "asdasd.png" ""
         */

        public static string GetUpdateFile(bool isUpdated, string oldFile, string format)
        {
            if (isUpdated)
            {
                if (!string.IsNullOrEmpty(oldFile))
                {
                    return $"{Guid.NewGuid()}.{format}";
                }
                else
                {
                    return "";
                }
            }
            return oldFile;
        }

        public static void DeleteFile(string path, string fileName)
        {
            string contentRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"); //_webHostEnvironment.WebRootPath;

            if (!string.IsNullOrWhiteSpace(fileName) && File.Exists(Path.Combine(contentRootPath, path, fileName)))
            {
                var pathToDelete = Path.Combine(Path.Combine(contentRootPath, path));
                FileData.DeleteFile(Path.Combine(pathToDelete, fileName));
            }
        }

        public static async Task UpdateFile(bool isChangeFile, string path, string newFile, string newFileData, string oldFile)
        {
            if (isChangeFile)
            {
                string contentRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"); //_webHostEnvironment.WebRootPath;
                // delete old file
                if (!string.IsNullOrWhiteSpace(oldFile)
                    && File.Exists(Path.Combine(contentRootPath, path, oldFile)))
                {
                    FileData.DeleteFile(Path.Combine(contentRootPath, path, oldFile));
                }

                // save new file
                if (!string.IsNullOrWhiteSpace(newFileData))
                {
                    var pathToSave = Path.Combine(contentRootPath, path);
                    if (!Directory.Exists(pathToSave))
                    {
                        Directory.CreateDirectory(pathToSave);
                    }
                    await FileData.SaveFile(Path.Combine(pathToSave, newFile), newFileData);
                }
            }
        }

        public static string GetPathFile(string file, string path)
        {
            return !string.IsNullOrWhiteSpace(file) ? Path.Combine(path, file) : "";
        }

        public static string GetNewPathFile(string file, string format)
        {
            return string.IsNullOrWhiteSpace(file) ? "" : Guid.NewGuid().ToString() + "." + format;
        }
    }
}
