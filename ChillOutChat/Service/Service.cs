using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using chatpkg;

namespace Service
{
    class Service : IService
    {
        private string chatRoomsDB = @"..\..\chatRooms.xml";
        private string usersDB = @"..\..\users.xml";


        public List<ChatRoom> GetChatRooms()
        {
            List<ChatRoom> chatRooms = new List<ChatRoom>();
            if (File.Exists(chatRoomsDB))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ChatRoom>));
                using (FileStream str = File.OpenRead(chatRoomsDB))
                {
                    try
                    {
                        chatRooms = (List<ChatRoom>)xmlSerializer.Deserialize(str);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            return chatRooms;
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            if (File.Exists(usersDB))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<User>));
                using (FileStream str = File.OpenRead(usersDB))
                {
                    try
                    {
                        users = (List<User>)xmlSerializer.Deserialize(str);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            return users;
        }

        public void SetChatRooms(List<ChatRoom> chatRooms)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ChatRoom>));
            using (var str = File.Create(chatRoomsDB))
            {
                xmlSerializer.Serialize(str, chatRooms);
            }
        }

        public void SetUsers(List<User> users)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<User>));
            using (var str = File.Create(usersDB))
            {
                xmlSerializer.Serialize(str, users);
            }
        }
    }
}
