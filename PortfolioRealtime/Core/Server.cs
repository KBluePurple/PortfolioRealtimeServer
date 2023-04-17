using System.Net;
using PortfolioRealtime.Logger;

namespace Core;

internal class Server
{
    private readonly CancellationToken _cancellationToken;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly HttpListener _listener;
    private readonly int _port;
    private bool _isListening;

    public Server(int port)
    {
        _cancellationToken = _cancellationTokenSource.Token;
        _listener = new HttpListener();
        _port = port;
        
        Logger.LogInfo("Initializing server...");
        PacketManager.Initialize();
        Logger.LogInfo("Server initialized.");
    }

    public Task Start()
    {
        Logger.LogInfo("Starting server...");
        _listener.Prefixes.Add($"http://localhost:{_port}/");
        _listener.Start();
        _isListening = true;
        Logger.LogInfo("Server started.");

        while (_isListening)
        {
            var context = Task.Run(() => _listener.GetContextAsync(), _cancellationToken).Result;
            Logger.LogDebug($"Received request from {context.Request.RemoteEndPoint}.");
            if (context.Request.IsWebSocketRequest)
            {
                Logger.LogDebug("Request is a WebSocket request.");
                Task.Run(() => ProcessWebSocketRequest(context), _cancellationToken);
            }
            else
            {
                Logger.LogDebug("Request is not a WebSocket request.");
                context.Response.StatusCode = 400;
                context.Response.Close();
            }
        }

        return Task.CompletedTask;
    }

    private static async Task ProcessWebSocketRequest(HttpListenerContext context)
    {
        var webSocketContext = await context.AcceptWebSocketAsync(null);
        var webSocket = webSocketContext.WebSocket;
        var buffer = new byte[1024 * 4];
        var session = new Session(webSocket);

        await session.Receive(buffer);
    }

    public void Stop()
    {
        Logger.LogInfo("Stopping server...");
        _isListening = false;
        _listener.Stop();
        _cancellationTokenSource.Cancel();
        Logger.LogInfo("Server stopped.");
    }
}