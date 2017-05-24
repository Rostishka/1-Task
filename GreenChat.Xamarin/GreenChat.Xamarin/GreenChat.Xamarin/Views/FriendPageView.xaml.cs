using GreenChatXamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenChat.Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendPageView : ContentPage
    {
        public FriendViewModel ViewModel { get; private set; }

        public FriendPageView(FriendViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            this.BindingContext = ViewModel;
        }
    }
}