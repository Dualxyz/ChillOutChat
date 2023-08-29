using chatpkg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Misc;

namespace ViewModel.ViewModels
{
    public class ModeratorVM : BindableVM
    {
        private Moderator moderator;
        private Client proxy;

        private User selectedUser;
        public MyICommand UserSelectionChangedCommand { get; set; }

        public BindingList<ChatLine> ChatRoomChatLines { get; set; }
        private ChatLine selectedLine;

        public MyICommand AddCommand { get; set; }
        public MyICommand AlterCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }

        private string firstName;
        private string lastName;

        public MyICommand RefreshCommand { get; set; }
        public MyICommand InitializationCommand { get; set; }

        private ChatRoom selectedChatRoom;
        public MyICommand ChatRoomSelectionChangedCommand { get; set; }
        public MyICommand RemoveMessageCommand { get; set; }

        public MyICommand UndoCommand { get; set; }
        public MyICommand RedoCommand { get; set; }

        public ModeratorVM()
        {
            moderator = new Moderator(0, new List<Command>());
            proxy = new Client(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10100/Service"));
            ChatRoomChatLines = new BindingList<ChatLine>();

            UserSelectionChangedCommand = new MyICommand(UserSelectionChangedMethod);
            
            AddCommand = new MyICommand(AddMethod);
            AlterCommand = new MyICommand(AlterMethod);
            DeleteCommand = new MyICommand(DeleteMethod);

            RefreshCommand = new MyICommand(RefreshMethod);
            InitializationCommand = new MyICommand(InitializationMethod);

            ChatRoomSelectionChangedCommand = new MyICommand(ChatRoomSelectionChangedMethod);
            RemoveMessageCommand = new MyICommand(RemoveMessageMethod);

            UndoCommand = new MyICommand(UndoMethod);
            RedoCommand = new MyICommand(RedoMethod);
        }

        private void UserSelectionChangedMethod()
        {
            if (SelectedUser == null)
                return;
            FirstName = SelectedUser.FirstName;
            LastName = SelectedUser.LastName;
        }
        
        private void AddMethod()
        {
            User u = new User(FirstName, LastName, new ConcreteMessageFactory());
            Users.Add(u);
            UsersFront.Add(u);
        }

        private void AlterMethod()
        {
            AddMethod();
            DeleteMethod();
        }

        private void DeleteMethod()
        {
            Users.Remove(SelectedUser);
            UsersFront.Remove(SelectedUser);
        }

        private void RefreshMethod()
        {
            proxy.SetUsers(Users);
            proxy.SetChatRooms(ChatRoomsFront.ToList());
            LoadDatabase();
        }

        private void ChatRoomSelectionChangedMethod()
        {
            if (SelectedChatRoom == null)
                return;
            ChatRoomChatLines.Clear();
            foreach(ChatLine c in SelectedChatRoom.chatLines)
            {
                ChatRoomChatLines.Add(c);
            }
        }

        private void RemoveMessageMethod()
        {
            moderator.HideLine(SelectedChatRoom, SelectedLine);
            ChatRoomSelectionChangedMethod();
        }

        private void UndoMethod()
        {
            moderator.Undo();
            ChatRoomSelectionChangedMethod();
        }

        private void RedoMethod()
        {
            moderator.Redo();
            ChatRoomSelectionChangedMethod();
        }

        internal void LoadDatabase()
        {
            List<User> users = proxy.GetUsers();
            if (users == null)
                users = new List<User>();
            List<ChatRoom> chatRooms = proxy.GetChatRooms();
            if (chatRooms == null)
                chatRooms = new List<ChatRoom>();
            
            Users.Clear();
            UsersFront.Clear();
            foreach (User u in users)
            {
                UsersFront.Add(u);
                Users.Add(u);
            }
            ChatRoomsFront.Clear();
            foreach (ChatRoom c in chatRooms)
                ChatRoomsFront.Add(c);
        }

        private void InitializationMethod()
        {
            User u1 = new User("Ranko", "Rakocevic", new ConcreteMessageFactory());
            User u2 = new User("John", "Johnson", new ConcreteMessageFactory());
            User u3 = new User("Gabriel", "Hernandez", new ConcreteMessageFactory());

            ChatRoom c1 = new ChatRoom("Football", new List<ChatLine>());
            ChatRoom c2 = new ChatRoom("Cars", new List<ChatLine>());

            Users.Add(u1);
            Users.Add(u2);
            Users.Add(u3);
            UsersFront.Add(u1);
            UsersFront.Add(u2);
            UsersFront.Add(u3);

            ChatLine cl1 = new ChatLine("Ronaldo is a very good player!", Users[0]);
            ChatLine cl2 = new ChatLine("But Messi is better", Users[1]);

            ChatLine cl3 = Users[2].concreteMessageFactory.GetAutoMessage("1", Users[2]);

            c1.AddChatLine(cl1);
            c1.AddChatLine(cl2);

            c2.AddChatLine(cl3);

            ChatRoomsFront.Add(c1);
            ChatRoomsFront.Add(c2);
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
        public ChatLine SelectedLine
        {
            get { return selectedLine; }
            set
            {
                if (value != selectedLine)
                {
                    selectedLine = value;
                    OnPropertyChanged("SelectedLine");
                }
            }
        }
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (value != firstName)
                {
                    firstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (value != lastName)
                {
                    lastName = value;
                    OnPropertyChanged("LastName");
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
    }
}
