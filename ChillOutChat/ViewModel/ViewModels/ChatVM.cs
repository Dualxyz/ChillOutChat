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
    public class ChatVM : BindableVM
    {
        private string searchString;

        private User selectedUser;

        private string chatRoomName;
        public MyICommand CreateChatRoomCommand { get; set; }

        private ChatRoom selectedChatRoom;
        public MyICommand ChatRoomSelectionChangedCommand { get; set; }

        private string content;

        public MyICommand SendMessageCommand { get; set; }

        public MyICommand HelloCommand { get; set; }
        public MyICommand GoodbyeCommand { get; set; }
        public MyICommand HowCommand { get; set; }

        public BindingList<ChatLine> ChatRoomChatLines { get; set; }

        public ChatVM()
        {
            ChatRoomChatLines = new BindingList<ChatLine>();

            ChatRoomSelectionChangedCommand = new MyICommand(ChatRoomSelectionChangedMethod);

            CreateChatRoomCommand = new MyICommand(CreateChatRoomMethod);

            SendMessageCommand = new MyICommand(SendMessageMethod);

            HelloCommand = new MyICommand(HelloMethod);
            GoodbyeCommand = new MyICommand(GoodbyeMethod);
            HowCommand = new MyICommand(HowMethod);
        }

        private void CreateChatRoomMethod()
        {
            ChatRoom chatRoom = new ChatRoom(ChatRoomName, new List<ChatLine>());
            ChatRoomsFront.Add(chatRoom);
        }

        private void AddChatLine(ChatLine cl)
        {
            SelectedChatRoom.chatLines.Add(cl);
            ChatRoomChatLines.Add(cl);
        }

        private void SendMessageMethod()
        {
            if (SelectedUser == null)
                return;
            ChatLine cl = new ChatLine(Content, SelectedUser);
            AddChatLine(cl);
        }

        private void GenerateMessage(string v)
        {
            if (SelectedUser == null)
                return;
            ChatLine cl = SelectedUser.concreteMessageFactory.GetAutoMessage(v, SelectedUser);
            AddChatLine(cl);
        }
        
        private void HelloMethod()
        {
            GenerateMessage("1");
        }
        
        private void GoodbyeMethod()
        {
            GenerateMessage("2");
        }

        private void HowMethod()
        {
            GenerateMessage("3");
        }

        private void ChatRoomSelectionChangedMethod()
        {
            if (SelectedChatRoom == null)
                return;
            ChatRoomChatLines.Clear();
            foreach (ChatLine c in SelectedChatRoom.chatLines)
            {
                ChatRoomChatLines.Add(c);
            }
        }

        public void Search()
        {
            ChatRoomChatLines.Clear();
            foreach(ChatLine cl in SelectedChatRoom.chatLines)
                if (cl.Username.Contains(SearchString) || SearchString.Equals(""))
                    ChatRoomChatLines.Add(cl);
        }

        public string ChatRoomName
        {
            get { return chatRoomName; }
            set
            {
                if (value != chatRoomName)
                {
                    chatRoomName = value;
                    OnPropertyChanged("ChatRoomName");
                }
            }
        }

        public string SearchString
        {
            get { return searchString; }
            set
            {
                if (value != searchString)
                {
                    searchString = value;
                    Search();
                    OnPropertyChanged("SearchString");
                }
            }
        }

        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (value != selectedUser)
                {
                    selectedUser = value;
                    OnPropertyChanged("SelectedUser");
                }
            }
        }

        public ChatRoom SelectedChatRoom
        {
            get { return selectedChatRoom; }
            set
            {
                if (value != selectedChatRoom)
                {
                    selectedChatRoom = value;
                    OnPropertyChanged("SelectedChatRoom");
                }
            }
        }

        public string Content
        {
            get { return content; }
            set
            {
                if (value != content)
                {
                    content = value;
                    OnPropertyChanged("Content");
                }
            }
        }
    }
}
