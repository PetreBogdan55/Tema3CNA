﻿using ChatCommon;
using ChatServer.Interfaces;
using ChatServer;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using ChatServer.Logging;

namespace ChatServer.Services
{
    [Export]
    public class ChatService
    {
        [Import]
        private Logger m_logger = null;

        [Import]
        private IChatLogRepository m_repository = null;
        private event Action<ChatLog> Added;

        public void Add(ChatLog chatLog)
        {
            m_logger.Info($"{chatLog}");
            m_repository.Add(chatLog);
            Added?.Invoke(chatLog);
        }
        public IObservable<ChatLog> GetChatLogsAsObservable()
        {
            var oldLogs = m_repository.GetAll().ToObservable();
            var newLogs = Observable.FromEvent<ChatLog>((x) => Added += x, (x) => Added -= x);

            return oldLogs.Concat(newLogs);
        }
    }
}