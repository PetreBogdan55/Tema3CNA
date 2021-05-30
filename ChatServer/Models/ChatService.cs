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
        private Logger logger = null;

        [Import]
        private IChatLogRepository repository = null;
        private event Action<ChatLog> Added;

        public void Add(ChatLog chatLog)
        {
            logger.Info($"{chatLog}");
            repository.Add(chatLog);
            Added?.Invoke(chatLog);
        }
        public IObservable<ChatLog> GetChatLogsAsObservable()
        {
            var oldLogs = repository.GetAll().ToObservable();
            var newLogs = Observable.FromEvent<ChatLog>((x) => Added += x, (x) => Added -= x);

            return oldLogs.Concat(newLogs);
        }
    }
}