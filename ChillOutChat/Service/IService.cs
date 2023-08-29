using chatpkg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        void SetUsers(List<User> users);
        [OperationContract]
        void SetChatRooms(List<ChatRoom> chatRooms);

        [OperationContract]
        List<User> GetUsers();
        [OperationContract]
        List<ChatRoom> GetChatRooms();
    }
}
