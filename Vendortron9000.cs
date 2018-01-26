using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Engine
{
    public class Vendortron9000 : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public BindingList<Inventory> Inventory { get;  set; }
        public Vendortron9000(string name)
        {
            Name = name;
            Inventory = new BindingList<Inventory>();
        }
        public void Additemtoinventory(_items itemToadd, int quantity = 1)
        {
            Inventory item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemToadd.ID);
                if(item == null)
            {
                Inventory.Add(new Inventory(itemToadd, quantity));
            }
                else
            {
                item.Quantity += quantity;
            }
            OnPropertyChanged("inventory");
        }
        public void removeAItemFromInventory(_items itemtoRemove, int quantity = 1)
        {
            Inventory item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemtoRemove.ID);
            if(item==null)
            {

            }
            else
            {
                item.Quantity -= quantity;
                if(item.Quantity < 0 )
                {
                    item.Quantity = 0;
                }
                if ( item.Quantity == 0)
                {
                    Inventory.Remove(item);
                }
                OnPropertyChanged("Inventory");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
