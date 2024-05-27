using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var cosmos = builder
    .AddAzureCosmosDB("cosmosdb")
    .AddDatabase("astrometrics");

if (builder.Environment.IsDevelopment())
{
    cosmos.RunAsEmulator();
}

var apiService = builder.AddProject<Projects.Ejafi_Astrometrics_ApiService>("apiservice")
    .WithReference(cosmos);

builder.AddProject<Projects.Ejafi_Astrometrics_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
