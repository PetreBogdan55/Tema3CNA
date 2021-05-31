using ChatApp.Common;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Core.Interceptors;
using GrpcWpfSample.Server.Infrastructure;
using GrpcWpfSample.Server.Model;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace GrpcWpfSample.Server.Rpc
{
    [Export(typeof(IService))]
    public class ChatServiceGrpcServer : Chat.ChatBase, IService
    {
        [Import]
        private Logger m_logger = null;

        [Import]
        private ChatService m_chatService = null;
        private readonly Empty m_empty = new Empty();

        private const int Port = 50052;
        private readonly Grpc.Core.Server m_server;

        public ChatServiceGrpcServer()
        {
            m_server = new Grpc.Core.Server
            {
                Services =
                    {
                        Chat.BindService(this)
                            .Intercept(new IpAddressAuthenticator())
                    },
                Ports =
                    {
                        new ServerPort("localhost", Port, ServerCredentials.Insecure)
                    }
            };

        }

        public void Start()
        {
            m_server.Start();

            m_logger.Info("Started.");
        }

        public override async Task Subscribe(Empty request, IServerStreamWriter<ChatLog> responseStream, ServerCallContext context)
        {
            var peer = context.Peer; // keep peer information because it is not available after disconnection
            m_logger.Info($"{peer} subscribes.");

            context.CancellationToken.Register(() => m_logger.Info($"{peer} cancels subscription."));

            try
            {
                await m_chatService.GetChatLogsAsObservable()
                    .ToAsyncEnumerable()
                    .ForEachAwaitAsync(async (x) => await responseStream.WriteAsync(x), context.CancellationToken)
                    .ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
                m_logger.Info($"{peer} unsubscribed.");
            }
        }

        public override async Task GetUsers(Empty request, IServerStreamWriter<User> responseStream, ServerCallContext context)
        {
            var peer = context.Peer; // keep peer information because it is not available after disconnection
            m_logger.Info($"{peer} gets the users.");

            context.CancellationToken.Register(() => m_logger.Info($"{peer} cancels subscription to the user list."));

            try
            {
                await m_chatService.GetChatLogsUsersAsObservable()
                    .ToAsyncEnumerable()
                    .ForEachAwaitAsync(async (x) => await responseStream.WriteAsync(x), context.CancellationToken)
                    .ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
                m_logger.Info($"{peer} unsubscribed from getting the user list.");
            }
        }

        public override Task<Empty> Write(ChatLog request, ServerCallContext context)
        {
            m_logger.Info($"{context.Peer} {request}");

            m_chatService.Add(request);
            m_chatService.AddUser(new User { Name = request.Name });

            return Task.FromResult(m_empty);
        }
    }
}