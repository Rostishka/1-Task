using MVVMFriendsPage.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVMFriendsPage.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsListPage : ContentPage
    {
        public FriendsListPage()
        {
            InitializeComponent();
            BindingContext = new FriendsListViewModel() { Navigation = this.Navigation };
        }
    }
}
