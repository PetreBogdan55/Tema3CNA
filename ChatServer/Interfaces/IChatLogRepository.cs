using ChatCommon;
using System.Collections.Generic;

namespace ChatServer.Interfaces
{

    public interface IChatLogRepository
    {
        void Add(ChatLog chatLog);
        IEnumerable<ChatLog> GetAll();
    }
}
