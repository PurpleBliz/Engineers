using System;
using Engineers.IService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using Engineers.Models;
using System.Linq;
using Newtonsoft.Json;

namespace Engineers.Service
{
    public class FileService : IFileService
    {
        public readonly IWebHostEnvironment _env;

        public string _path = "";

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public bool Delete(string Path)
        {
            if (Path != Properties.PathToDefaultUserImage)
                return DeleteFile(Path);

            return false;
        }

        public string Upload(IFormFile File)
        {
            if (File != null)
                return SaveFile(File);

            return Properties.PathToDefaultUserImage;
        }

        public List<string> UploadArray(IFormFileCollection Files)
        {
            List<string> result = new(); 

            if (Files.Count > 0 && Files != null)
            {
                foreach (var item in Files)
                    result.Add(SaveFile(item));

                return result;
            }

            result.Add(Properties.PathToDefaultOrderImage);

            return result;
        }

        private string SaveFile(IFormFile file)
        {
            _path = Path.Combine(_env.WebRootPath, Properties.PathToFiles);

            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            var uniqueFileName = GetUniqueFileName(file.FileName);
            var filePath = Path.Combine(_path, uniqueFileName);

            file.CopyTo(new FileStream(filePath, FileMode.Create));

            return Path.Combine(Properties.PathToURL, uniqueFileName);
        }

        private bool DeleteFile(string LocalPath)
        {
            try
            {
                _path = Path.Combine(_env.WebRootPath, LocalPath.Replace(Properties.PathToURL, ""));

                if (File.Exists(_path))
                    File.Delete(_path);
                else
                    return true;

                return !File.Exists(_path);
            }
            catch
            {
                return false;
            }
        }

        public List<Blueprint> GetBlueprints()
        {
            _path = Path.Combine(_env.WebRootPath, Properties.PathToBlueprins);

            List<Blueprint> blueprints = new();

            Directory.GetFiles(_path, "*", SearchOption.AllDirectories).ToList()
                .ForEach(file =>
                {
                    var result = JsonConvert.DeserializeObject<Blueprint>(File.ReadAllText(file));

                    blueprints.Add(result);
                });

            return blueprints;
        }

        private static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}
