using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CarRental.API.Helpers.FileUpload
{
    public static class FileUploadHelper
    {
        // FileName Updates
        public static string UploadFile(IFormFile file, string folder, string subFolder)
        {
            var folderName = Path.Combine(folder, subFolder);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(subFolder, fileName).Replace("\\","/");

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return dbPath;
            }

            return null;
        }

        public static void RemoveFile(string FileName)
        {
            if (System.IO.File.Exists(FileName))
            {
                System.IO.File.Delete(FileName);
            }
        }

    }
}
