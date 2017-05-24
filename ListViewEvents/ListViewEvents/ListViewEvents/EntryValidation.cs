using System;
using Xamarin.Forms;

namespace ListViewEvents
{
    public class EntryValidation : TriggerAction<Entry>
    {
        protected override void Invoke(Entry sender)
        {
            Int32 parsed;
            Boolean valid = Int32.TryParse(sender.Text, out parsed);

            if (!valid) sender.TextColor= Color.Red;
            else sender.TextColor = Color.Blue;
        }
    }
}