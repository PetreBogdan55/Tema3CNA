using ChatCommon;
using ChatServer.Interfaces;
using ChatServer;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using ChatServer.Logging;

namespace ChatServer.Models
{
    [Export]
    public class ChatService
    {
        [Import]
        private Logger m_logger = null;

        [Import]
        private IChatRepository m_repository = null;
        private event Action<ChatLog> Added;

        public void Add(ChatLog chatLog)
        {
            m_logger.Info($"{chatLog}");

            m_repository.Add(chatLog);
            Added?.Invoke(chatLog);
        }

    }
}