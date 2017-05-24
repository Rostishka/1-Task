using GreenChat.Xamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenChat.Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsListPageView : ContentPage
    {
        public FriendsListPageView()
        {
            InitializeComponent();
            BindingContext = new FriendsListViewModel() { Navigation = this.Navigation };
        }
    }
}