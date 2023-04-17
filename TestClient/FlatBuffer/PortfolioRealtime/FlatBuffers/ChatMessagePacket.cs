// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace PortfolioRealtime.FlatBuffers
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct ChatMessagePacket : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_3_3(); }
  public static ChatMessagePacket GetRootAsChatMessagePacket(ByteBuffer _bb) { return GetRootAsChatMessagePacket(_bb, new ChatMessagePacket()); }
  public static ChatMessagePacket GetRootAsChatMessagePacket(ByteBuffer _bb, ChatMessagePacket obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public ChatMessagePacket __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public long Timestamp { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetLong(o + __p.bb_pos) : (long)0; } }
  public string Message { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMessageBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetMessageBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetMessageArray() { return __p.__vector_as_array<byte>(6); }
  public string To { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetToBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetToBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetToArray() { return __p.__vector_as_array<byte>(8); }

  public static Offset<PortfolioRealtime.FlatBuffers.ChatMessagePacket> CreateChatMessagePacket(FlatBufferBuilder builder,
      long timestamp = 0,
      StringOffset messageOffset = default(StringOffset),
      StringOffset toOffset = default(StringOffset)) {
    builder.StartTable(3);
    ChatMessagePacket.AddTimestamp(builder, timestamp);
    ChatMessagePacket.AddTo(builder, toOffset);
    ChatMessagePacket.AddMessage(builder, messageOffset);
    return ChatMessagePacket.EndChatMessagePacket(builder);
  }

  public static void StartChatMessagePacket(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddTimestamp(FlatBufferBuilder builder, long timestamp) { builder.AddLong(0, timestamp, 0); }
  public static void AddMessage(FlatBufferBuilder builder, StringOffset messageOffset) { builder.AddOffset(1, messageOffset.Value, 0); }
  public static void AddTo(FlatBufferBuilder builder, StringOffset toOffset) { builder.AddOffset(2, toOffset.Value, 0); }
  public static Offset<PortfolioRealtime.FlatBuffers.ChatMessagePacket> EndChatMessagePacket(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<PortfolioRealtime.FlatBuffers.ChatMessagePacket>(o);
  }
}


}
