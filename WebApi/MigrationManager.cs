using Microsoft.EntityFrameworkCore;
using Persistence;

namespace WebApi;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase(this WebApplication webApp)
    {
        using var scope = webApp.Services.CreateScope();
        using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        try
        {
            appContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            //Log errors or do anything you think it's needed
            throw new Exception("An error occurred while app initialization");
        }
        
        return webApp;
    }
}