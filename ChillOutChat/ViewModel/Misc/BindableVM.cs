using chatpkg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Misc
{
    public class BindableVM : INotifyPropertyChanged
    {
        public static List<User> Users { get; set; }
        public static BindingList<User> UsersFront { get; set; }
        public static BindingList<ChatRoom> ChatRoomsFront { get; set; }

        protected virtual void SetProperty<T>(ref T member, T val,
              [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;

            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
