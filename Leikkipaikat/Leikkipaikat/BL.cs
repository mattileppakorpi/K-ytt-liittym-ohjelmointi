using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Leikkipaikat
{
    public class Playground
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Info { get; set; }
        public List<Equipment> Equipment { get; set; }

    }

    public class Equipment : INotifyPropertyChanged
    {
        private string name;
        private string brand;
        private ObservableCollection<Fault> faults;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }
        public string Brand
        {
            get { return brand; }
            set
            {
                if (brand != value)
                {
                    brand = value;
                    RaisePropertyChanged("Brand");
                }
            }
        }
        public ObservableCollection<Fault> Faults
        {
            get { return faults; }
            set
            {
                if (Equals(faults, value)) return;
                {
                    faults = value;
                    RaisePropertyChanged("Faults");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));

            }
        }

    }
    public class Fault : INotifyPropertyChanged
    {
        private string faultName;
        private char category;
        public string FaultName
        {
            get { return faultName; }
            set
            {
                if (faultName != value)
                {
                    faultName = value;
                    RaisePropertyChanged("FaultName");
                }
            }
        }

        public char Category
        {
            get { return category; }
            set
            {
                if (category != value)
                {
                    category = value;
                    RaisePropertyChanged("Category");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));

            }

        }
    }
}
