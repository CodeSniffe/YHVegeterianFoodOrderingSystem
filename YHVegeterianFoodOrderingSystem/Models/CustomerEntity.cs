using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YHVegeterianFoodOrderingSystem.Models
{
    public class CustomerEntity : TableEntity
    {
        public CustomerEntity(string Lastname, string Firstname)
        {
            this.PartitionKey = Lastname;
            this.RowKey = Firstname;
        }

        public CustomerEntity() { }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

    }
}
