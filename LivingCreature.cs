using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Engine
{
    public class LivingCreature : INotifyPropertyChanged
    {
        private int _HP;
        private int _currentStamina;
        public int HP
        {
            get { return _HP; }
            set
            {
                _HP = value;
                OnPropertyChanged("HP");
            }
        }

        public int Stamina
        {
            get { return _currentStamina; }
            set
            {
                _currentStamina = value;
                OnPropertyChanged("Stamina");
            }
        }
        public int MaxHP { get; set; }
        public bool IsDead { get { return HP <= 0; } }
        public LivingCreature(int hp, int Maxhp, int stamina)
        {
            HP = hp;
            MaxHP = Maxhp;
            Stamina = stamina;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        { 
            if(name == "HP")
            {
                if(HP<0)
                {
                    HP = 0;
                }
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
