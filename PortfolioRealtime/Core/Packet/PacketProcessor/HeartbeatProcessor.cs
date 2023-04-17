using Core.Service;
using Google.FlatBuffers;
using PortfolioRealtime.FlatBuffers;
using PortfolioRealtime.Logger;

[PacketType(PacketType.Heartbeat)]
internal class HeartbeatProcessor : IPacketProcessor
{
    public async Task Process(Session sender, Packet packet)
    {
        HeartbeatService.SetSessionAlive(sender, true);
        Logger.LogDebug($"Heartbeat received from {sender}.");
    }
}