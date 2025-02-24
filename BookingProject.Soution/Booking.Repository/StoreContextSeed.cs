using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Booking.Core.Entities;
using Microsoft.Extensions.Logging;

namespace Booking.Repository
{
   public class StoreContextSeed
    {
        public static async Task SeedAsync (StoreContext context,ILoggerFactory loggerFactory)
        {


            try
            {
                if(!context.Residentials.Any())
                {
                    var ResidentialUnits = File.ReadAllText("../Booking.Repository/Data/DataSeeding/Residentials.json");
                    var residentials = JsonSerializer.Deserialize<List<Residential>>(ResidentialUnits);

                    foreach (var residential in residentials)
                    {
                        context.Set<Residential>().Add(residential);
                    }
                  
                }
                await context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, ex.Message);
               
            }
           
        }
    }
}
