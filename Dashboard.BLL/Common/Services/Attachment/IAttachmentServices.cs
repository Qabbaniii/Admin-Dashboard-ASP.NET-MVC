using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.BLL.Common.Services.Attachment
{
    public interface IAttachmentServices
    {
        public string UploadImage(IFormFile file, string folderName);
        public bool DeleteImage(string filePath);
    }
}
