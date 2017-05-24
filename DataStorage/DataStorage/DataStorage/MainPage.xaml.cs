using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Settings;
using Xamarin.Forms;

namespace DataStorage
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            string name = CrossSettings.Current.GetValueOrDefault<string>("name", "отсутствует");
            nameBox.Text = name;
            base.OnAppearing();
        }

        private void OnClick(object sender, EventArgs e)
        {
            string value = nameBox.Text;
            CrossSettings.Current.AddOrUpdateValue<string>("name", value);
        }
    }
}
