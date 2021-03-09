using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YHVegeterianFoodOrderingSystem.Models;

namespace YHVegeterianFoodOrderingSystem.Data
{
    public class YHVegeterianFoodOrderingSystemContextNew : DbContext
    {
        public YHVegeterianFoodOrderingSystemContextNew (DbContextOptions<YHVegeterianFoodOrderingSystemContextNew> options)
            : base(options)
        {
        }

        public DbSet<YHVegeterianFoodOrderingSystem.Models.Menu> Menu { get; set; }
    }
}
