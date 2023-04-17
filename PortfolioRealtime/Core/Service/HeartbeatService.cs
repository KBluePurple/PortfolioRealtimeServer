using System.Net.WebSockets;
using Google.FlatBuffers;
using PortfolioRealtime.FlatBuffers;
using PortfolioRealtime.Logger;

namespace Core.Service;

internal static class HeartbeatService
{
    private const int HeartbeatDelay = 1000;

    private static readonly CancellationToken CancellationToken;
    private static readonly CancellationTokenSource CancellationTokenSource = new();
    private static readonly object Lock = new();
    private static readonly List<SessionAlive> Sessions = new();
    private static readonly List<Session> SessionsToRemove = new();

    static HeartbeatService()
    {
        CancellationToken = CancellationTokenSource.Token;
        Task.Run(Heartbeat, CancellationToken);
    }

    private static async Task Heartbeat()
    {
        while (true)
            try
            {
                foreach (var tuple in Sessions)
                {
                    if (tuple.Session.Socket.State != WebSocketState.Open)
                    {
                        SessionsToRemove.Add(tuple.Session);
                        continue;
                    }

                    if (tuple.IsAlive == false)
                    {
                        SessionsToRemove.Add(tuple.Session);
                        continue;
                    }

                    var builder = new FlatBufferBuilder(1);

                    HeartbeatPacket.StartHeartbeatPacket(builder);
                    var heartbeatPacket = HeartbeatPacket.EndHeartbeatPacket(builder);
                    Packet.StartPacket(builder);
                    Packet.AddDataType(builder, PacketType.Heartbeat);
                    Packet.AddData(builder, heartbeatPacket.Value);
                    var packet = Packet.EndPacket(builder);
                    builder.Finish(packet.Value);

                    tuple.Session.Send(Packet.GetRootAsPacket(builder.DataBuffer));
                    SetSessionAlive(tuple.Session, false);
                    Logger.LogDebug($"Heartbeat sent to {tuple.Session}.");
                }

                await Task.Delay(HeartbeatDelay, CancellationToken);
                CleanupSessions();
            }
            catch
            {
                // ignored
            }
    }

    private static void CleanupSessions()
    {
        lock (Lock)
        {
            foreach (var item in SessionsToRemove.Select(session => Sessions.First(x => x.Session == session)))
            {
                _ = item.Session.Close();
                Sessions.Remove(item);
                Logger.LogDebug($"Session {item.Session} removed from heartbeat service.");
            }

            SessionsToRemove.Clear();
        }
    }

    public static void AddSession(Session session)
    {
        lock (Lock)
        {
            Sessions.Add(new SessionAlive(session, true));
            Logger.LogDebug($"Session {session} added to heartbeat service.");
        }
    }

    public static void SetSessionAlive(Session session, bool isAlive)
    {
        lock (Lock)
        {
            Sessions.First(x => x.Session == session).IsAlive = isAlive;
        }
    }

    private class SessionAlive
    {
        public SessionAlive(Session session, bool isAlive)
        {
            Session = session;
            IsAlive = isAlive;
        }

        public readonly Session Session;
        public bool IsAlive;
    }

    public static void RemoveSession(Session session)
    {
        lock (Lock)
        {
            Sessions.Remove(Sessions.First(x => x.Session == session));
        }
    }
}