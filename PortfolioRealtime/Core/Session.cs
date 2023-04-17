using System.Net.WebSockets;
using Core.Service;
using PortfolioRealtime.FlatBuffers;
using PortfolioRealtime.Logger;

internal class Session
{
    private readonly CancellationToken _cancellationToken;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly Guid _id = Guid.NewGuid();
    private bool _isAlive;

    public Session(WebSocket socket)
    {
        Socket = socket;
        _cancellationToken = _cancellationTokenSource.Token;
        _isAlive = true;
        HeartbeatService.AddSession(this);
        PacketSenderService.AddSession(this);
        Logger.LogDebug($"Session {_id} created.");
    }

    public WebSocket Socket { get; }

    public void Send(Packet packet)
    {
        try
        {
            PacketSenderService.EnqueuePacket(this, packet);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Receive(byte[] buffer)
    {
        while (Socket.State == WebSocketState.Open)
        {
            var result = await Socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Close)
                await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            else
                await PacketManager.Process(this, buffer, result.Count);
        }

        await Close();
    }

    public async Task Close()
    {
        if (!_isAlive) return;

        _cancellationTokenSource.Cancel();
        await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
        _isAlive = false;

        HeartbeatService.RemoveSession(this);
        PacketSenderService.RemoveSession(this);

        Logger.LogDebug($"Session {_id} closed.");
    }

    public override string ToString()
    {
        return _id.ToString();
    }
}