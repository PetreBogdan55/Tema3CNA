using ChatCommon;
using System.Collections.Generic;

namespace ChatServer.Interfaces
{

    public interface IChatRepository
    {
        void Add(ChatLog chatLog);
        IEnumerable<ChatLog> GetAll();
    }
}
