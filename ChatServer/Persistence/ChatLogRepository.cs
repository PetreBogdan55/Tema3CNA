using ChatApp.Common;
using GrpcWpfSample.Server.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;

namespace GrpcWpfSample.Server.Persistence
{
    [Export(typeof(IChatLogRepository))]
    public class ChatLogRepository : IChatLogRepository
    {
        private readonly List<ChatLog> m_storage = new List<ChatLog>();
        private readonly List<User> m_storage_users = new List<User>();

        public void Add(ChatLog chatLog)
        {
            m_storage.Add(chatLog);
        }

        public IEnumerable<ChatLog> GetAll()
        {
            return m_storage.AsReadOnly();
        }

        public void AddUser(User user)
        {
            if (!m_storage_users.Exists((x) => x.Name.Equals(user.Name)))
                m_storage_users.Add(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return m_storage_users.AsReadOnly();
        }
    }
}