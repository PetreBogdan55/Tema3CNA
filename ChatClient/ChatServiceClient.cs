using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Common;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace ChatClient
{
    public class ChatServiceClient
    {
        private readonly Chat.Common.Chat.ChatClient m_client;

        public ChatServiceClient()
        {
            m_client = new Chat.Common.Chat.ChatClient(new Channel("localhost", 50052, ChannelCredentials.Insecure));
        }

        public async Task Write(ChatLog chatLog)
        {
            await m_client.WriteAsync(chatLog);
        }

        public IAsyncEnumerable<ChatLog> ChatLogs()
        {
            var call = m_client.Subscribe(new Empty());

            return call.ResponseStream
                .ToAsyncEnumerable()
                .Finally(() => call.Dispose());
        }
    }
}
