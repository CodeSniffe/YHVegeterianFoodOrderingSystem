using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;

namespace YHVegeterianFoodOrderingSystem.Controllers
{
    public class ServiceBusController : Controller
    {
        //PASSWORD FOR CONNECTING THE QUEUE
        const string ServiceBusConnectionString = "Endpoint=sb://yhvegeterianfoodorderingsystem.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=2HAGU3DXdsOyWfkjiFu7eLKWViRm9W1d//ducCWAfZ4=";
        const string QueueName = "YhQueue";

        public async Task<ActionResult> Index()
        {   
            //TO ALLOW USER TO VIEW MESSAGE OF QUEUE
            var managementClient = new ManagementClient(ServiceBusConnectionString);
            var queue = await managementClient.GetQueueRuntimeInfoAsync(QueueName);
            ViewBag.MessageCount = queue.MessageCount;
            return View();
        }

        private static async Task CreateQueue()
        {
            var managementClient = new ManagementClient(ServiceBusConnectionString);
            bool queueExists = await managementClient.QueueExistsAsync(QueueName);
            if (!queueExists)
            {
                QueueDescription qd = new QueueDescription(QueueName);
                qd.MaxSizeInMB = 1024;
                qd.MaxDeliveryCount = 3;
                await managementClient.CreateQueueAsync(qd);
            }
        }

        public static void Initialize() 
        {
            CreateQueue().GetAwaiter().GetResult();
        }
    }
}
