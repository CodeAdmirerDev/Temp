
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Database Context
        services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AzureSQLConnection")));

        // Azure Blob Storage Configuration
        services.AddSingleton(new AzureBlobService(
            Configuration["AzureStorage:BlobConnectionString"],
            Configuration["AzureStorage:ContainerName"]
        ));

        // Register the Data Service
        services.AddScoped<IDataService, DataService>();

        // Add Controllers
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
