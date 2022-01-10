using MyLibrary.Utilities;
using StoreManager.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace StoreManager.DTO
{
    public class Category : ICategory, INotifyPropertyChanged
    {
        private string name;

        public int Id { get; set; }
        public string Name
        {
            get => name;

            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Name;
        }

        public static explicit operator Category(Model.Category category)
        {
            return new Category { Id = category.Id, Name = category.Name };
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
