// websocket client for testing

using System.Net.WebSockets;
using Google.FlatBuffers;
using PortfolioRealtime.FlatBuffers;

var client = new ClientWebSocket();
await client.ConnectAsync(new Uri("ws://localhost:6132"), CancellationToken.None);

Console.WriteLine("Connected to server.");

async Task Send(Packet packet)
{
    var buffer = packet.ByteBuffer;
    await client.SendAsync(buffer.ToArraySegment(buffer.Position, buffer.Length - buffer.Position), WebSocketMessageType.Binary, true,
        CancellationToken.None);
    Console.WriteLine($"Sent packet of type {packet.DataType}.");
}

var buffer = new byte[1024 * 4];
async Task Receive()
{
    while (client.State == WebSocketState.Open)
    {
        var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        if (result.MessageType == WebSocketMessageType.Close)
        {
            await client.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            Console.WriteLine("Connection closed.");
        }
        else
        {
            if (result.Count == 0) continue;

            var bb = new ByteBuffer(buffer);
            var packet = Packet.GetRootAsPacket(bb);

            Console.WriteLine($"Received packet of type {packet.DataType}.");

            switch (packet.DataType)
            {
                case PacketType.Heartbeat:
                    var builder = new FlatBufferBuilder(1);
                    HeartbeatPacket.StartHeartbeatPacket(builder);
                    var heartbeatPacket = HeartbeatPacket.EndHeartbeatPacket(builder);
                    Packet.StartPacket(builder);
                    Packet.AddDataType(builder, PacketType.Heartbeat);
                    Packet.AddData(builder, heartbeatPacket.Value);
                    var packet2 = Packet.EndPacket(builder);
                    builder.Finish(packet2.Value);
                    await Send(Packet.GetRootAsPacket(builder.DataBuffer));
                    Console.WriteLine("Sent heartbeat.");
                    break;
                case PacketType.NONE:
                    break;
                case PacketType.ChatMessage:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

while (true) await Receive();