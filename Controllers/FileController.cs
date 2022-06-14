﻿using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace TaskManager.Controllers
{
    [Route("file")]
    [Authorize]
    public class FileController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetFile(string name)
        {
            var rootFolder = Directory.GetCurrentDirectory();
            var fileFullPath = rootFolder + "/PrivateAssets/" + name;

            if (System.IO.File.Exists(fileFullPath))
            {
                return NotFound();
            }

            var file = System.IO.File.ReadAllBytes(fileFullPath);
            var fileProvider = new FileExtensionContentTypeProvider();

            fileProvider.TryGetContentType(fileFullPath, out var contentType);

            return File(file, contentType, name);
        }
    }
}
