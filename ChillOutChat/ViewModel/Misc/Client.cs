using chatpkg;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Misc
{
    public class Client : ChannelFactory<IService>, IService
    {
        IService factory;

        public Client(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public List<ChatRoom> GetChatRooms()
        {
            return factory.GetChatRooms();
        }

        public List<User> GetUsers()
        {
            return factory.GetUsers();
        }

        public void SetChatRooms(List<ChatRoom> chatRooms)
        {
            factory.SetChatRooms(chatRooms);
        }

        public void SetUsers(List<User> users)
        {
            factory.SetUsers(users);
        }
    }
}
