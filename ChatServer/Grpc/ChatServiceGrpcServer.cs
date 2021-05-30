using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Core.Interceptors;
using ChatCommon;
using ChatServer.Logging;
using ChatServer.Services;
using System.ComponentModel.Composition;
using ChatServer;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ChatServer.Interfaces;

namespace ChatServer.Grpc
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
        private readonly Server m_server;

        public ChatServiceGrpcServer()
        {

            m_server = new Server
            {
                Services =
                    {
                        Chat.BindService(this)

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

        public override Task<Empty> Write(ChatLog request, ServerCallContext context)
        {
            m_logger.Info($"{context.Peer} {request}");

            m_chatService.Add(request);

            return Task.FromResult(m_empty);
        }
    }
}
