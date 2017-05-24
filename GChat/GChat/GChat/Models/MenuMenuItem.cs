using System;
using GChat.Views;

namespace GChat.Models
{

    public class MenuMenuItem
    {
        public MenuMenuItem()
        {
            TargetType = typeof(MenuDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}
