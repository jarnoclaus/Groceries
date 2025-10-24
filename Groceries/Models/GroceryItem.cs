using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groceries.Models
{
    public class GroceryItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public string Name { get; set; }
        public int Priority { get; set; }
        public bool IsPickedUp { get; set; } = false;
        private int amount = 1;
        public int Amount
        {
            get { return amount; }
            set 
            { 
                if(amount != value)
                {
                    amount = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Amount)));

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayAmount)));
                }
            }
        }
        public string DisplayAmount => Amount > 1 ? Amount.ToString() : string.Empty;

    }
}
