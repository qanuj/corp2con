using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using e10.Shared.Util;

namespace Talent21.Web.Models
{
    public class FilesStatus
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }
        public string Progress { get; set; }
        public string Url { get; set; }
        public string Error { get; set; }
        public string Original { get; set; }

        public FilesStatus() { }
        public FilesStatus(FileInfo fileInfo, bool isPicture = false) { SetValues(fileInfo.Name, fileInfo.Extension, isPicture); }

        public FilesStatus(FileInfo fileInfo, string fileError, bool isPicture = false)
        {
            SetValues(fileInfo.Name, fileInfo.Extension, isPicture, (int)fileInfo.Length);
            Error = fileError;
        }
        public FilesStatus(string fileName, string ext = "", bool isPicture = false, int fileLength = 0) { SetValues(fileName, ext, isPicture, fileLength); }
        private void SetValues(string fileName, string ext, bool isPicture = false, int fileLength = 0)
        {
            Original = fileName;
            Name = Path.GetFileNameWithoutExtension(fileName) + "-" + Guid.NewGuid() + Path.GetExtension(fileName);
            Type = MimeTypeMap.GetMimeType(ext);
            Size = fileLength;
            Progress = "1.0";
            Url = "/" + (isPicture ? "picture" : "files") + "/" + Name;
        }
    }
}