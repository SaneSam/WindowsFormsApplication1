using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class PlayerQuest : INotifyPropertyChanged
    {
        private Quest _details;
        private bool _isCompleted;
        public Quest Details
        {
            get { return _details; }
            set
            {
                _details = value;
                OnpropertyChanged("Details");
            }
        }
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                _isCompleted = value;
                OnpropertyChanged("IsCompleted");
                OnpropertyChanged("Name");
            }
        }
        public string Name
        {
            get { return Details.Name; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnpropertyChanged(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public PlayerQuest(Quest details)
        {
            Details = details;
            IsCompleted = false;
        }
    }
}
