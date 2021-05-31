using ChatApp.Common;
using System.Collections.Generic;

namespace GrpcWpfSample.Server.Model
{
    public interface IChatLogRepository
    {
        void Add(ChatLog chatLog);
        void AddUser(User chatLog);
        IEnumerable<ChatLog> GetAll();
        IEnumerable<User> GetAllUsers();
    }
}