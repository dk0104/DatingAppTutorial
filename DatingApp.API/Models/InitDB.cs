using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DatingApp.API.Models
{
    public class InitDB
    {
       public static void Populate(IApplicationBuilder app) {
           using (var serviceScope = app.ApplicationServices.CreateScope())
           {
              SeedData(serviceScope.ServiceProvider.GetService<DataContext>());
           }
       }

       public static void SeedData(DataContext context){
           System.Console.WriteLine("Appling Migrations");
           context.Database.Migrate();
           if (!context.ValueItems.Any())
           {
               System.Console.WriteLine("Adding data - seeding ...");
               context.ValueItems.AddRange(
                   new Values(){Value= "ValueOne"},
                   new Values(){Value= "ValueTwo"},
                   new Values(){Value= "ValueTree"}
               );
               context.SaveChanges();
               
           }
           else{
            System.Console.WriteLine("Already have some data seeding not ness");
           }

       }
    }
}