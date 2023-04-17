using System.Net.WebSockets;
using PortfolioRealtime.FlatBuffers;
using PortfolioRealtime.Logger;

namespace Core.Service;

internal static class PacketSenderService
{
    private static readonly object Lock = new();
    private static readonly List<(Session session, Queue<Packet> sendQueue)> Sessions = new();

    static PacketSenderService()
    {
        Task.Run(SendLoop);
    }

    private static async Task SendLoop()
    {
        while (true)
            try
            {
                foreach (var tuple in Sessions)
                {
                    if (tuple.sendQueue.Count == 0)
                        continue;

                    var packet = tuple.sendQueue.Dequeue();
                    var buffer = packet.ByteBuffer;
                    await tuple.session.Socket.SendAsync(
                        buffer.ToArraySegment(buffer.Position, buffer.Length - buffer.Position),
                        WebSocketMessageType.Binary, true,
                        CancellationToken.None);
                }
            }
            catch
            {
                // ignored
            }
    }

    public static void AddSession(Session session)
    {
        lock (Lock)
        {
            Sessions.Add((session, new Queue<Packet>()));
        }
    }

    public static void RemoveSession(Session session)
    {
        lock (Lock)
        {
            Sessions.RemoveAll(tuple => tuple.session == session);
        }
    }

    public static void EnqueuePacket(Session session, Packet packet)
    {
        lock (Lock)
        {
            Sessions.Find(tuple => tuple.session == session).sendQueue.Enqueue(packet);
        }
    }
}