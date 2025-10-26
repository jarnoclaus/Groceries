using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Groceries.Models
{
    public class GroceryItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string name;
        public string Name
        {
            get { return name; }
            set 
            { 
                name = value;
                OnPropertyChanged();
            }
        }
        private int priority;
        public int Priority
        {
            get { return priority; }
            set 
            { 
                priority = value; 
                OnPropertyChanged();
            }
        }

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
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DisplayAmount));
                }
            }
        }
        public decimal Price { get; set; }

        public string DisplayAmount => Amount > 1 ? Amount.ToString() : string.Empty;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
