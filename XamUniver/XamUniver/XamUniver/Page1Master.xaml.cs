using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamUniver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1Master : ContentPage
    {
        public ListView ListView => ListViewMenuItems;

        public Page1Master()
        {
            InitializeComponent();
            BindingContext = new Page1MasterViewModel();
        }



        class Page1MasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<Page1MenuItem> MenuItems { get; }
            public Page1MasterViewModel()
            {
                MenuItems = new ObservableCollection<Page1MenuItem>(new[]
                {
                    new Page1MenuItem { Id = 0, Title = "Page 1" },
                    new Page1MenuItem { Id = 1, Title = "Page 2" },
                    new Page1MenuItem { Id = 2, Title = "Page 3" },
                    new Page1MenuItem { Id = 3, Title = "Page 4" },
                    new Page1MenuItem { Id = 4, Title = "Page 5" },
                });
            }
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        }
    }
}
