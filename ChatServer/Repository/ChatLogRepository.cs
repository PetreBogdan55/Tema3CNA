using ChatServer.Interfaces;
using ChatCommon;
using ChatServer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;

namespace ChatServer.Repository
{
    [Export(typeof(IChatLogRepository))]
    public class ChatLogRepository : IChatLogRepository
    {
        private readonly List<ChatLog> storage = new List<ChatLog>(); // dummy on memory storage

        public void Add(ChatLog chatLog)
        {
            storage.Add(chatLog);
        }

        public IEnumerable<ChatLog> GetAll()
        {
            return storage.AsReadOnly();
        }
    }
}