using GChat.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GChat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuMaster : ContentPage
    {
        public ListView ListView => ListViewMenuItems;

        public MenuMaster()
        {
            InitializeComponent();
            BindingContext = new MenuMasterViewModel();
        }
    }
}
