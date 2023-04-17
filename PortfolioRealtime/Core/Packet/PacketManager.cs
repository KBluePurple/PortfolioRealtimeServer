using Google.FlatBuffers;
using PortfolioRealtime.FlatBuffers;
using PortfolioRealtime.Logger;

internal static class PacketManager
{
    private static readonly Dictionary<PacketType, IPacketProcessor> PacketProcessors =
        new();

    public static void Initialize()
    {
        var types = typeof(PacketManager).Assembly.GetTypes();
        foreach (var type in types)
        {
            var attributes = type.GetCustomAttributes(typeof(PacketTypeAttribute), false);
            if (attributes.Length == 0) continue;
            if (Activator.CreateInstance(type) is not IPacketProcessor processor) continue;
            var attribute = (PacketTypeAttribute) attributes[0];
            PacketProcessors.Add(attribute.PacketType, processor);
            
            Logger.LogDebug($"Packet processor {type.Name} initialized.");
        }
    }

    public static async Task Process(Session session, byte[] buffer, int count)
    {
        var bb = new ByteBuffer(buffer);
        var packet = Packet.GetRootAsPacket(bb);
        await Process(session, packet);
    }

    private static async Task Process(Session session, Packet packet)
    {
        var packetType = packet.DataType;
        if (!PacketProcessors.TryGetValue(packetType, out var processor))
        {
            Logger.LogError($"Packet type {packetType} is not supported. Packet will be ignored.");
            return;
        }

        await processor.Process(session, packet);
    }
}