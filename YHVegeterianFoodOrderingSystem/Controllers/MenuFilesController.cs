using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

namespace YHVegeterianFoodOrderingSystem.Controllers
{
    public class MenuFilesController : Controller
    {
        public IActionResult Index(string contents)
        {
            ViewBag.content = contents;
            return View();
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            var filePath = ""; string fileContents = null;
            int i = 1;
            foreach (var formFile in files)
            {
                if (formFile.ContentType.ToLower() != "text/plain") //not text file..
                {
                    return BadRequest("The " + formFile.FileName + " unable to upload because uploaded file must be a text file");
                }
                if (formFile.Length == 0)
                {
                    return BadRequest("The " + formFile.FileName + "file is empty content!");
                }
                else if (formFile.Length > 1048576)
                {
                    return BadRequest("The " + formFile.FileName + "file is exceed 1 MB !");
                }
                else
                {
                    if (formFile.Length > 0)
                    {
                        filePath = "C:\\Users\\ASUS\\Desktop\\Uploadtest" + i + ".txt"; //UPLOAD TO A LOCATION
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                    using (var reader = new StreamReader(formFile.OpenReadStream(),
                    new UTF8Encoding(encoderShouldEmitUTF8Identifier: false,
                    throwOnInvalidBytes: true), detectEncodingFromByteOrderMarks: true))
                    {
                        //fileContents = fileContents + await reader.ReadToEndAsync();
                        fileContents = "File has been uploaded!";
                    }
                }
                i++;
            }
            return RedirectToAction("Index", "MenuFiles", new { @contents = fileContents });
        }

    }
}
