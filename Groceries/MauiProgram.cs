using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Microsoft.Maui.LifecycleEvents;
using Groceries.Models;

namespace Groceries
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.ConfigureLifecycleEvents(events =>
        {
#if ANDROID
            events.AddAndroid(android => android
            .OnStop(async (activity) => 
            {
                try
                {
                    await SyncService.PushToCloudAsync();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
                await FileService.SaveCatalogueList();
                await FileService.SaveShoppingList();
            })
            .OnPause( async(activity) =>
            {
                try
                {
                    await SyncService.PushToCloudAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                await FileService.SaveCatalogueList();
                await FileService.SaveShoppingList();
            }));

#endif
        });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
