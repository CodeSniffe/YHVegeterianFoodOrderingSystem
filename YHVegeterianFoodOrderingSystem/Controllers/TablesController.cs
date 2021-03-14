using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YHVegeterianFoodOrderingSystem.Models;

namespace YHVegeterianFoodOrderingSystem.Controllers
{
    public class TablesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private CloudTable getTableStorageInformation()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfigurationRoot configure = builder.Build();

            CloudStorageAccount storageAccount =
            CloudStorageAccount.Parse(configure["ConnectionStrings:yhblob"]);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("YHTable");
            return table;
        }

        public ActionResult CreateTables() //PAGE TO SHOW SUCCESSFUL STEP 
        {
            CloudTable table = getTableStorageInformation(); //GET ACC LINK
            ViewBag.Success = table.CreateIfNotExistsAsync().Result;
            ViewBag.TableName = table.Name;
            return View();
        }

        //ADD ENTITY
        public ActionResult AddEntity()
        {
            CloudTable table = getTableStorageInformation();
            CustomerEntity customers = new CustomerEntity("Les", "Lee");
            customers.Email = "lee@mail.com";
            DateTime thisDate = new DateTime(2000, 6, 10);
            customers.BirthDate = thisDate;
            TableOperation InsertOperation = TableOperation.Insert(customers);
            TableResult result = table.ExecuteAsync(InsertOperation).Result;
            ViewBag.Tablename = table.Name;
            ViewBag.Result = result.HttpStatusCode;

            return View();
        }


    }
}
