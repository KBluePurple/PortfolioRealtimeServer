using PortfolioRealtime.FlatBuffers;

internal interface IPacketProcessor
{
    public Task Process(Session sender, Packet packet);
}