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
            });

            MainPage = new AppShell();            
        }

        protected override async void OnSleep()
        {
            await FileService.SaveCatalogueList();
            await FileService.SaveShoppingList();
        }
    }
}