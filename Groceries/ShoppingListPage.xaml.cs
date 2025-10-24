using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Groceries.Models;

namespace Groceries
{
    public partial class ShoppingListPage : ContentPage
    {
        public ObservableCollection<GroceryItem> list { get; set; } = GroceryData.ShoppingList;
        public ICommand OnItemLongPressed { get; }
        public ShoppingListPage()
        {
            InitializeComponent();
            OnItemLongPressed = new Command<GroceryItem>(HandleLongPress);
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var sorted = list.OrderBy(x => x.Priority).ToList();
            GroceryData.ShoppingList.Clear();
            foreach(var item in sorted)
                GroceryData.ShoppingList.Add(item);
        }

        public void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = e.CurrentSelection.FirstOrDefault() as GroceryItem;

            if (selectedItem == null)
                return;

            var collectionView = (CollectionView)sender;

            collectionView.SelectedItem = null;

            var itemIndex = GroceryData.ShoppingList.IndexOf(selectedItem);
            selectedItem.IsPickedUp = !selectedItem.IsPickedUp;
            GroceryData.ShoppingList[itemIndex] = selectedItem;
        }

        public async void OnCatalogusClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///MainPage");
        }

        public void OnResetClicked(object sender, EventArgs e)
        {
            GroceryData.ShoppingList.Clear();
        }
        public void HandleLongPress(GroceryItem item)
        {
            if (item.Amount > 1)
                item.Amount--;
            else
                GroceryData.ShoppingList.Remove(item);
        }
    }
}