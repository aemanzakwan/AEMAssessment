using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PlatformWell_AEM.Model;

namespace APISyncApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Set up Entity Framework and database context
            var serviceProvider = new ServiceCollection()
                .AddDbContext<TestAemContext>(options =>
                    options.UseSqlServer("Data Source=(localdb)\\Local;Initial Catalog=TestAEM;Integrated Security=True"))
                .BuildServiceProvider();

            using (var context = serviceProvider.GetService<TestAemContext>())
            {
                context.Database.EnsureCreated();

                // Initialize HttpClient
                using (var httpClient = new HttpClient())
                {
                    // Authenticate with the API and get a bearer token
                    var token = await AuthenticateAsync(httpClient);
                    if (token != null)
                    {
                        // Use the token to call the GetPlatformWellActual API
                        var actualData = await GetPlatformWellActualAsync(httpClient, token);

                        // Sync data into the database
                        SyncDataIntoDatabase(context, actualData);
                    }
                }
            }
        }

        static async Task<string> AuthenticateAsync(HttpClient httpClient)
        {
            var loginRequest = new
            {
                username = "user@aemenersol.com",
                password = "Test@123"
            };

            var response = await httpClient.PostAsJsonAsync("http://test-demo.aemenersol.com/api/Account/Login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<string>(responseData);
                return token;
            }

            return null;
        }

        static async Task<List<PlatformTable>> GetPlatformWellActualAsync(HttpClient httpClient, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("http://test-demo.aemenersol.com/api/PlatformWell/GetPlatformWellActual");

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                List<PlatformTable> actualData = JsonConvert.DeserializeObject<List<PlatformTable>>(responseData);
                return actualData;
            }

            return null;
        }

        static void SyncDataIntoDatabase(TestAemContext context, List<PlatformTable> data)
        {
            if (data.Count != 0)
            {
                foreach (var platform in data)
                {
                    var platformModel = context.PlatformTables.FirstOrDefault(instance => instance.Id == platform.Id);
                    if (platformModel == null)
                    {
                        context.Add(platform);
                    }
                    else
                    {
                        platformModel.Id = platform.Id;
                        platformModel.UniqueName = platform.UniqueName;
                        platformModel.UpdatedAt = platform.UpdatedAt;
                        platformModel.CreatedAt = platform.CreatedAt;
                        platformModel.Latitude = platform.Latitude;
                        platformModel.Longitude = platform.Longitude;
                        context.Update(platformModel);
                    }

                    if (platform.Well.Count != 0)
                    {
                        foreach (var well in platform.Well)
                        {
                            var wellModel = context.PlatformWells.FirstOrDefault(instance => instance.Id == well.Id);
                            if (wellModel == null)
                            {
                                context.Add(well);
                            }
                            else
                            {
                                wellModel.Id = well.Id;
                                wellModel.PlatformId = well.PlatformId;
                                wellModel.UniqueName = well.UniqueName;
                                wellModel.UpdatedAt = well.UpdatedAt;
                                wellModel.CreatedAt = well.CreatedAt;
                                wellModel.Latitude = well.Latitude;
                                wellModel.Longitude = well.Longitude;
                                context.Update(wellModel);
                            }
                        }
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
