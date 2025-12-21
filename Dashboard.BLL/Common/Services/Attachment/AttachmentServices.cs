using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.BLL.Common.Services.Attachment
{
    public class AttachmentServices : IAttachmentServices
    {
        private readonly List<string> AllowedExtentions = new List<string> { ".png", ".jpeg",".jpg" };
        private readonly int FileMaximumSize = 2_097_152;
        public string UploadImage(IFormFile file, string folderName)
        {
            var fileExtention = Path.GetExtension(file.FileName);
            if (!AllowedExtentions.Contains(fileExtention))
                throw new Exception("Invalid File Extention!!");
            if (file.Length > FileMaximumSize)
                throw new Exception("Invalid File Size!!");

            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folderName);
            
            if(!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            var FileName = $"{Guid.NewGuid()}_{file.FileName}";
            var FilePath = Path.Combine(FolderPath, FileName);

            using var FileStream = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(FileStream);

            return FileName;

        }

        public bool DeleteImage(string FilePath)
        {
            if(File.Exists(FilePath))
            { 
                File.Delete(FilePath); 
                return true;
            }
            else 
            { 
                return false; 
            }
        }

        
    }
}
