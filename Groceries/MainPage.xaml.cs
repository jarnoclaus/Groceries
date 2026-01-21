using CommunityToolkit.Maui.Extensions;
using Groceries.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
namespace Groceries
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<GroceryItem> catalogue { get; set; } = GroceryData.Catalogue;
        public ICommand OnItemLongPressed { get; }
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            OnItemLongPressed = new Command<GroceryItem>(HandleLongPress);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SortList();
        }
        public void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = e.CurrentSelection.FirstOrDefault() as GroceryItem;

            if (selectedItem == null)
                return;

            var collectionView = (CollectionView)sender;

            collectionView.SelectedItem = null;

            if (GroceryData.ShoppingList.Any(i => i.Name == selectedItem.Name))
            {
                var existing = GroceryData.ShoppingList.First(i => i.Name == selectedItem.Name);
                existing.Amount++;
            }
            else
            {
                GroceryData.ShoppingList.Add(new GroceryItem
                {
                    Name = selectedItem.Name,
                    Priority = selectedItem.Priority
                });
            }
        }
        public void OnAddItemClicked(object sender, EventArgs e)
        {
            string name = NewItemName.Text.Trim();
            var priority = NewItemPriority.Text;

            if (!string.IsNullOrEmpty(name) && int.TryParse(priority, out int prior))
            {
                name = char.ToUpper(name[0]) + name.Substring(1).ToLower();
                GroceryData.Catalogue.Add(new GroceryItem() { Name = name, Priority = prior });
                SortList();
            }                
            else
                return;

            NewItemName.Text = string.Empty;
            NewItemPriority.Text = string.Empty;
        }
        public async void OnToonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///ShoppingListPage");
        }
        public async void HandleLongPress(GroceryItem item)
        {
            string action = await Application.Current.MainPage.DisplayActionSheet("Choose action", "Cancel", null, "Edit", "Delete");
            if (action == "Edit")
            {
                var popup = new EditItemPopup(item);
                await this.ShowPopupAsync(popup);
            }
            else if (action == "Delete")
            {
                GroceryData.Catalogue.Remove(item);
            }
        }
        public void SortList()
        {
            var sorted = GroceryData.Catalogue.OrderBy(x => x.Name).ToList();
            GroceryData.Catalogue.Clear();
            foreach(var item in sorted)
            {
                GroceryData.Catalogue.Add(item);
            }
        }
    }
}
