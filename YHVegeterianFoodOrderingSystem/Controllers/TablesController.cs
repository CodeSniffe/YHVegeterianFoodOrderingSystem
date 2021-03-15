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

        private CloudTable getStorageInformation()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfigurationRoot configure = builder.Build();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(configure["ConnectionStrings:yhblob"]);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("YHTable");
            return table;
        }

        public ActionResult CreateTables() //PAGE TO SHOW SUCCESSFUL STEP 
        {
            CloudTable table = getStorageInformation(); //GET ACC LINK
            ViewBag.Success = table.CreateIfNotExistsAsync().Result;
            ViewBag.TableName = table.Name;
            return View();
        }

        //ADD ENTITY
        public ActionResult AddEntity()
        {
            CloudTable table = getStorageInformation();
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

        //ADD MULTIPLE ENTITIES
        public ActionResult AddMultipleEntity()
        {
            CloudTable tableinfo = getStorageInformation();
            TableBatchOperation batch = new TableBatchOperation();
            IList<TableResult> results;

            string[,] customer =
            {
                {"Clark","Joe","JadenSmith","JadenSmith@mail.com","1999-01-01" },
                {"Clark","Josh","JadenQuest","JadenQuest@mail.com","1999-01-01" },
            };

            for (int i = 0; i < 2; i++)
            {
                //ROWKEY IS THE PRIMARY KEY TO AVOID DUPLICATION
                CustomerEntity cus = new CustomerEntity(customer[i, 0], customer[i, 1]);
                cus.Email = customer[i, 3];
                DateTime cusBDate = DateTime.Parse(customer[i, 4]);
                cus.BirthDate = cusBDate;
                batch.Insert(cus);//COLLECT AND STORE IN BATCH
            }

            try
            {
                //SEND BATCH INTO TABLE
                results = tableinfo.ExecuteBatchAsync(batch).Result;
                ViewBag.msg = "Sucess";
                //SHOW RESULT IF NO PROBLEM
                return View(results); }
            catch (Exception ex)
            {
                ViewBag.msg = "Error:" + ex.ToString();
            }
            return View();
        }

        //SEARCH 
        public ActionResult Search(string dialogmsg=null)
        {
            ViewBag.msg = dialogmsg;
            return View();
        }

        //GET ONE ENTITY
        [HttpPost]
        public ActionResult GetEntity(string PartitionName, string RowName)
        {
            CloudTable tableinfo = getStorageInformation();
            string msg ="";
            try
            {
                //SEARCH INFO
                TableOperation retrieve = TableOperation.Retrieve<CustomerEntity>(PartitionName, RowName);
                TableResult result = tableinfo.ExecuteAsync(retrieve).Result;
                if (result.Etag != null)
                {
                    return View(result);
                }
                else
                {
                    msg = "Data not exist...";
                }
            }
            catch(Exception ex)
            {
                msg = "Error" + ex.ToString();
            }
            return RedirectToAction("Search","Tables",new { dialogmsg = msg });
        }

        //DELETE ENTITY
        public ActionResult DeleteEntity(string PartitionKey, string RowKey)
        {
            CloudTable tableinfo = getStorageInformation();
            string msg = null;

            try
            {
                TableOperation delete = TableOperation.Delete(new CustomerEntity(PartitionKey, RowKey) {ETag="*"});
                tableinfo.ExecuteAsync(delete);
                msg = "Data for customer" + PartitionKey + " " + RowKey + "'is deleted from table...";
            }
            catch (Exception ex)
            {
                msg = "Unable to delete data... Error: " + ex.ToString();
            }
            return RedirectToAction("Search", "Tables",new { dialogmsg = msg });
        }
    }
}
