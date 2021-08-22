using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engineers.Models;
using Microsoft.AspNetCore.Http;

namespace Engineers.IService
{
    public interface IFileService
    {
        public string Upload(IFormFile file);
        public List<string> UploadArray(IFormFileCollection files);
        public List<Blueprint> GetBlueprints();
        public bool Delete(string Path);
    }
}
