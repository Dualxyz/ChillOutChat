using chatpkg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Misc;

namespace ViewModel.ViewModels
{
    public class MainWindowVM : BindableVM
    {
        private BindableVM currentVM;
        private ModeratorVM moderatorVM;
        private ChatVM chatVM;

        public MyICommand<string> NavCommand { get; set; }

        public MainWindowVM()
        {
            Users = new List<User>();
            UsersFront = new BindingList<User>();
            ChatRoomsFront = new BindingList<ChatRoom>();

            moderatorVM = new ModeratorVM();
            moderatorVM.LoadDatabase();

            chatVM = new ChatVM();

            NavCommand = new MyICommand<string>(NavMethod);

            CurrentVM = chatVM;
        }

        private void NavMethod(string obj)
        {
            if (obj.Equals("mod"))
                CurrentVM = moderatorVM;
            else if (obj.Equals("chat"))
                CurrentVM = chatVM;
        }

        public BindableVM CurrentVM
        {
            get { return currentVM; }
            set
            {
                SetProperty(ref currentVM, value);
            }
        }
    }
}
