using List.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace List.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Item selectedLeftItem;
        public Item SelectedLeftItem
        {
            get { return selectedLeftItem; }
            set
            {
                if (selectedLeftItem != value)
                {
                    selectedLeftItem = value;
                    OnPropertyChanged(nameof(SelectedLeftItem));
                }
            }
        }

        private Item selectedRightItem;
        public Item SelectedRightItem
        {
            get { return selectedRightItem; }
            set
            {
                if (selectedRightItem != value)
                {
                    selectedRightItem = value;
                    OnPropertyChanged(nameof(SelectedRightItem));
                }
            }
        }
        public ObservableCollection<Item> LeftItems { get; set; }
        public ObservableCollection<Item> RightItems { get; set; }
        public ICommand MoveToRightCommand { get; private set; }
        public ICommand MoveToLeftCommand { get; private set; }
        public ICommand MoveAllToRightCommand { get; private set; }
        public ICommand MoveAllToLeftCommand { get; private set; }
        public MainViewModel()
        {
            LeftItems = new ObservableCollection<Item>()
            {
                new Item { ID = 1, Nombre = "I0001" },
                new Item { ID = 2, Nombre = "I0002" },
                new Item { ID = 3, Nombre = "I0003" }
            };

            RightItems = new ObservableCollection<Item>()
            {
                new Item { ID = 4, Nombre = "I0004" },
                new Item { ID = 5, Nombre = "I0005" },
                new Item { ID = 6, Nombre = "I0006" }
            };

            MoveToRightCommand = new RelayCommand(MoveToRight);
            MoveToLeftCommand = new RelayCommand(MoveToLeft);
            MoveAllToRightCommand = new RelayCommand(MoveAllToRight);
            MoveAllToLeftCommand = new RelayCommand(MoveAllToLeft);

        }



        private void MoveToRight(object parameter)
        {
            if (SelectedLeftItem != null)
            {
                RightItems.Add(SelectedLeftItem);
                LeftItems.Remove(SelectedLeftItem);
            }
            else {
                MessageBox.Show("Debes seleccionar un elemento de la Izquierda.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        private void MoveToLeft(object parameter)
        {
            if (SelectedRightItem != null)
            {
                LeftItems.Add(SelectedRightItem);
                RightItems.Remove(SelectedRightItem);
            }
            else
            {
                MessageBox.Show("Debes seleccionar un elemento de la derecha.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        private void MoveAllToRight(object parameter)
        {
            if (LeftItems.Count == 0)
            {
                MessageBox.Show("No hay elementos en la lista izquierda para mover.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            LeftItems.ToList().ForEach(item =>
            {
                RightItems.Add(item);
                LeftItems.Remove(item);
            });
        }
        private void MoveAllToLeft(object parameter)
        {
            if (RightItems.Count == 0)
            {
                MessageBox.Show("No hay elementos en la lista derecha para mover.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            RightItems.ToList().ForEach(item =>
            {
                LeftItems.Add(item);
                RightItems.Remove(item);
            });
        }

    }


}
