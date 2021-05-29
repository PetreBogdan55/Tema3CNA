using ChatCommon;
using System.Collections.Generic;

namespace chatserver.models
{

    public interface IChatRepository
    {
        void Add(ChatLog chatLog);
        IEnumerable<ChatLog> GetAll();
    }
}
