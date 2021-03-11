using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace YHVegeterianFoodOrderingSystem.Controllers
{
    public class BlobsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private CloudBlobContainer getBlobStorageInformation()
        {
            //step 1: read json
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            IConfigurationRoot configure = builder.Build();
            //to get key access
            //once link, time to read the content to get the connectionstring
            CloudStorageAccount objectaccount = CloudStorageAccount.Parse(configure["ConnectionStrings:yhblob"]);
            CloudBlobClient blobclient = objectaccount.CreateCloudBlobClient();

            //step 2: how to create a new container in the blob storage account.
            CloudBlobContainer container = blobclient.GetContainerReference("yhblob");
            return container;
        }

        //PAGE TO SEE WHETHER CONTAINER IS CREATED OR NOT
        public ActionResult CreateBlobsContainer()
        {
            //refer to the container
            CloudBlobContainer container = getBlobStorageInformation(); //LINK ACCOUNT
            ViewBag.Success = container.CreateIfNotExistsAsync().Result;//CREATE CONTAINER IF NAME IS NOT EXIST
            ViewBag.BlobContainerName = container.Name; //COLLECT NAME TO DISPLAY
            return View(); 
        }

        public string UploadBlob()
        {
            CloudBlobContainer container = getBlobStorageInformation(); //LINK ACC TO GET BLOB CONTAINER
            string uploadedfile=""; string name = "";
            try
            {
                CloudBlockBlob blob = container.GetBlockBlobReference("my bae.jpg"); //FILE NAME PUT HERE
                using (var fileStream = System.IO.File.OpenRead(@"C:\\Users\\ASUS\\Pictures\\Saved Pictures\\my bae.jpg"))
                {
                    name = fileStream.Name;
                    blob.UploadFromStreamAsync(fileStream).Wait();
                }
            }
            catch(Exception ex)
            {
                return "Something is wrong...";
            }
            uploadedfile = uploadedfile + name + " is uploaded successfully!";
            return uploadedfile;
        }

        public string UploadMultipleImages()
        {
            CloudBlobContainer container = getBlobStorageInformation(); //LINK ACC TO GET BLOB CONTAINER

            string uploadedfile = ""; string name = "";
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    CloudBlockBlob blob = container.GetBlockBlobReference("yhblob");
                    using (var fileStream = System.IO.File.OpenRead(@"C:\\Users\\ASUS\\Pictures\\Saved Pictures\\my bae.jpg"))
                    {
                        name = fileStream.Name;
                        blob.UploadFromStreamAsync(fileStream).Wait();
                    }
                }
                catch (Exception ex)
                {
                    return "Something is wrong...";
                }
                uploadedfile = uploadedfile + name + " is uploaded successfully!";
            }
            return uploadedfile;
        }
    }
}
