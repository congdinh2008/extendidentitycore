using System;
using ExtendIdentity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ExtendIdentity.Areas.Identity.IdentityHostingStartup))]
namespace ExtendIdentity.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ExtendIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ExtendIdentityContextConnection")));

                services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<ExtendIdentityContext>();
            });
        }
    }
}