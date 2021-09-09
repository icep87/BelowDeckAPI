using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BelowDeckAPI.Controllers
{

    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FilesController(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}


        // POST api/values
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            //TODO: Add check for extensions
            //TODO: Add hash check if file is already uploaded

            long size = files.Sum(f => f.Length);
            string webRootPath = _webHostEnvironment.ContentRootPath;
            var fileData = new ArrayList();


            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var ext = Path.GetExtension(formFile.FileName).ToLowerInvariant();
                    var fileName = string.Concat(Guid.NewGuid().ToString(), ext);
                    var filePath = Path.Combine(Path.Combine(_webHostEnvironment.ContentRootPath, "FileStorage"), fileName);
                    var request = _httpContextAccessor.HttpContext.Request;
                    var _baseURL = $"{request.Scheme}://{request.Host}/Filestorage/";

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    var fileUrl = string.Concat(_baseURL, fileName);
                    var fileInfo = new { url = fileUrl, name = fileName};
                    fileData.Add(fileInfo);
                }
            }

            return Ok(fileData);
        }
    }
}
