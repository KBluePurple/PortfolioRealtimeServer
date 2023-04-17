using PortfolioRealtime.FlatBuffers;

[AttributeUsage(AttributeTargets.Class)]
internal class PacketTypeAttribute : Attribute
{
    public PacketTypeAttribute(PacketType packetType)
    {
        PacketType = packetType;
    }

    public PacketType PacketType { get; }
}