using CommunityToolkit.Maui.Views;
using Groceries.Models;

namespace Groceries;

public partial class EditItemPopup : Popup
{
	private GroceryItem _item;
	public EditItemPopup(GroceryItem item)
	{
		InitializeComponent();
		_item = item;

		NameEntry.Text = _item.Name;
		PriorityEntry.Text = _item.Priority.ToString();
	}

	private void OnSaveClicked(object sender, EventArgs e)
	{
		if(!string.IsNullOrWhiteSpace(NameEntry.Text) && int.TryParse(PriorityEntry.Text, out int priority))
		{
			_item.Name = NameEntry.Text;
			_item.Priority = priority;
			CloseAsync();
		}
		else
		{
			Application.Current.MainPage.DisplayAlert("Error", "Invalid input", "OK");
		}
	}
}