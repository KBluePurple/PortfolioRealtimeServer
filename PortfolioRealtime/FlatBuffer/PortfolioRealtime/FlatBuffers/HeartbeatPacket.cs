// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace PortfolioRealtime.FlatBuffers
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct HeartbeatPacket : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_3_3(); }
  public static HeartbeatPacket GetRootAsHeartbeatPacket(ByteBuffer _bb) { return GetRootAsHeartbeatPacket(_bb, new HeartbeatPacket()); }
  public static HeartbeatPacket GetRootAsHeartbeatPacket(ByteBuffer _bb, HeartbeatPacket obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public HeartbeatPacket __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartHeartbeatPacket(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<PortfolioRealtime.FlatBuffers.HeartbeatPacket> EndHeartbeatPacket(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<PortfolioRealtime.FlatBuffers.HeartbeatPacket>(o);
  }
}


}