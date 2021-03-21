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

        //UPLOAD FILE
        public string UploadMultipleImages()
        {
            CloudBlobContainer container = getBlobStorageInformation(); //LINK ACC TO GET BLOB CONTAINER

            string uploadedfile = ""; string name = "";
            for (int i = 1; i < 6; i++)
            {
                try
                {
                    CloudBlockBlob blob = container.GetBlockBlobReference("ImageMenu"+ i +".jpg");
                    using (var fileStream = System.IO.File.OpenRead(@"C:\\Users\\ASUS\\Desktop\\ImageMenu" + i + ".jpg"))
                    {
                        name = fileStream.Name;
                        blob.UploadFromStreamAsync(fileStream).Wait();
                    }
                }
                catch (Exception ex)
                {
                    return "Something is wrong..."+ex.ToString();
                }
                uploadedfile = uploadedfile + name + " is uploaded successfully!";
            }
            return uploadedfile;
        }

        //DISPLAY PICTURE FROM BLOB
        public IActionResult ListItemsAsGallery()
        {
            CloudBlobContainer container = getBlobStorageInformation();
            List<string> blobs = new List<string>(); //NEW LIST TO STORE BLOB INFO

            //GET LISTING RECORD FROM BLOB STORAGE
            BlobResultSegment result = container.ListBlobsSegmentedAsync(null).Result;

            //READ BLOB FROM STORAGE
            foreach (IListBlobItem item in result.Results)
            {
                //CHECK TYPE OF BLOB
                if(item.GetType()==typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    blobs.Add(blob.Name + "#" + blob.Uri.ToString());
                }
                else if(item.GetType()==typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory blob = (CloudBlobDirectory)item;
                    blobs.Add(blob.Uri.ToString());
                }
            }
            return View(blobs);
        }

        //DOWNLOAD IMAGE
        public string DownloadBlob(string area)
        {
            CloudBlobContainer container = getBlobStorageInformation();
            CloudBlockBlob downloadedblob = container.GetBlockBlobReference(area);
            try
            {
                using (var output = System.IO.File.OpenWrite(@"C:\\Users\\ASUS\\Downloads\\" + downloadedblob.Name))
                {
                    downloadedblob.DownloadToStreamAsync(output).Wait();
                }                    
            }
            catch(Exception ex)
            {
                return "Error" + ex.ToString();
            }
            return downloadedblob.Name + "is already downloaded";
        }

    }
}
