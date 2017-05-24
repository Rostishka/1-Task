using System.Collections.Generic;
using GreenChat.Xamarin.Views;
using GreenChatXamarin.Models;
using Xamarin.Forms;

namespace GreenChat.Xamarin
{
	public partial class MasterPage : ContentPage
	{
		public ListView ListView { get { return listView; } }

		public MasterPage ()
		{
			InitializeComponent ();

			var masterPageItems = new List<MasterPageItem> ();
			masterPageItems.Add (new MasterPageItem {
				Title = "Contacts",
				IconSource = "contacts.png",
				TargetType = typeof(SettingsPageView)
			});
			masterPageItems.Add (new MasterPageItem {
				Title = "TodoList",
				IconSource = "todo.png",
				TargetType = typeof(FriendsListPageView)
			});

			listView.ItemsSource = masterPageItems;
		}
	}
}
