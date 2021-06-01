using ChatApp.Common;
using GrpcWpfSample.Server.Infrastructure;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;

namespace GrpcWpfSample.Server.Model
{
    [Export]
    public class ChatService
    {
        [Import]
        private Logger m_logger = null;

        [Import]
        private IChatLogRepository m_repository = null;
        private event Action<ChatLog> Added;
        private event Action<User> AddedUser;

        public void Add(ChatLog chatLog)
        {
            m_logger.Info($"{chatLog}");

            m_repository.Add(chatLog);
            Added?.Invoke(chatLog);
        }

        public void AddUser(User user)
        {
            m_repository.AddUser(user);
            AddedUser?.Invoke(user);
        }

        public IObservable<ChatLog> GetChatLogsAsObservable()
        {
            var oldLogs = m_repository.GetAll().ToObservable();
            var newLogs = Observable.FromEvent<ChatLog>((x) => Added += x, (x) => Added -= x);

            return oldLogs.Concat(newLogs);
        }

        public IObservable<User> GetChatLogsUsersAsObservable()
        {
            var oldUsers = m_repository.GetAllUsers().ToObservable();
            var newUsers = Observable.FromEvent<User>((x) => AddedUser += x, (x) => AddedUser -= x);

            return oldUsers.Concat(newUsers);
        }
    }
}