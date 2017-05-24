using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContextMenu
{
    class PhonesListViewModel
    {
        public ObservableCollection<PhoneViewModel> Phones { get; set; }
        public ICommand MoveToTopCommand { protected set; get; }
        public ICommand MoveToBottomCommand { protected set; get; }
        public ICommand RemoveCommand { protected set; get; }
        public PhonesListViewModel()
        {
            Phones = new ObservableCollection<PhoneViewModel>
            {
                new PhoneViewModel { Title="HP Elite z3", Price=55000, Company="HP", ListViewModel=this},
                new PhoneViewModel {Title="Honor 8", Price= 28000, Company="Huawei", ListViewModel=this},
                new PhoneViewModel {Title="iPhone SE", Price=30000, Company="Apple", ListViewModel=this },
                new PhoneViewModel {Title="Galaxy Note 7", Price=60000, Company="Samsung", ListViewModel=this },
                new PhoneViewModel {Title="Lumia 950 XL", Price=36000, Company="Microsoft", ListViewModel=this }
            };

            MoveToTopCommand = new Command(MoveToTop);
            MoveToBottomCommand = new Command(MoveToBottom);
            RemoveCommand = new Command(Remove);
        }

        private void MoveToTop(object phoneObj)
        {
            PhoneViewModel phone = phoneObj as PhoneViewModel;
            if (phone == null) return;
            int oldIndex = Phones.IndexOf(phone);
            if (oldIndex > 0)
                Phones.Move(oldIndex, oldIndex - 1);
        }
        private void MoveToBottom(object phoneObj)
        {
            PhoneViewModel phone = phoneObj as PhoneViewModel;
            if (phone == null) return;
            int oldIndex = Phones.IndexOf(phone);
            if (oldIndex < Phones.Count - 1)
                Phones.Move(oldIndex, oldIndex + 1);
        }
        private void Remove(object phoneObj)
        {
            PhoneViewModel phone = phoneObj as PhoneViewModel;
            if (phone == null) return;

            Phones.Remove(phone);
        }
    }
}
