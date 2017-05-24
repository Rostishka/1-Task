using System.Collections.Generic;
using GreenChat.Xamarin.Views;
using GreenChat.Xamarin.Models;
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
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Story",
                IconSource = "reminders.png",
                TargetType = typeof(MainPageView)
            });
			masterPageItems.Add (new MasterPageItem {
				Title = "Friends",
				IconSource = "contacts.png",
				TargetType = typeof(FriendsListPageView)
			});
			masterPageItems.Add (new MasterPageItem {
				Title = "Settings",
				IconSource = "todo.png",
				TargetType = typeof(SettingsPageView)
			});

			listView.ItemsSource = masterPageItems;
		}
	}
}
