using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YHVegeterianFoodOrderingSystem.Areas.Identity.Data;
using YHVegeterianFoodOrderingSystem.Data;

[assembly: HostingStartup(typeof(YHVegeterianFoodOrderingSystem.Areas.Identity.IdentityHostingStartup))]
namespace YHVegeterianFoodOrderingSystem.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<YHVegeterianFoodOrderingSystemContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("YHVegeterianFoodOrderingSystemContextConnection")));

                services.AddDefaultIdentity<YHVegeterianFoodOrderingSystemUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<YHVegeterianFoodOrderingSystemContext>();
            });
        }
    }
}