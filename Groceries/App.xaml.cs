using Groceries.Models;

namespace Groceries
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Task.Run(async () => {
                await FileService.LoadCatalogueList();
                await FileService.LoadShoppingList();

                try
                {
                    await SyncService.PullFromCloudAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Cloud pull failed: {ex}");
                }                
            });

            MainPage = new AppShell();
        }

        protected override async void OnSleep()
        {
            try
            {
                await SyncService.PushToCloudAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cloud push failed: {ex}");
            }            

            await FileService.SaveCatalogueList();
            await FileService.SaveShoppingList();
        }
    }
}