// Generated by SpatialOS codegen. DO NOT EDIT!
// source: improbable.player.HeartbeatResponse in improbable/player/ClientConnection.schema.

namespace Improbable.Player
{

public partial struct HeartbeatResponse : global::System.IEquatable<HeartbeatResponse>, global::Improbable.Collections.IDeepCopyable<HeartbeatResponse>
{
  public static HeartbeatResponse Create()
  {
    var _result = new HeartbeatResponse();
    return _result;
  }

  public HeartbeatResponse DeepCopy()
  {
    var _result = new HeartbeatResponse();
    return _result;

  }

  public override bool Equals(object _obj)
  {
    return _obj is HeartbeatResponse && Equals((HeartbeatResponse) _obj);
  }

  public static bool operator==(HeartbeatResponse a, HeartbeatResponse b)
  {
    return a.Equals(b);
  }

  public static bool operator!=(HeartbeatResponse a, HeartbeatResponse b)
  {
    return !a.Equals(b);
  }

  public bool Equals(HeartbeatResponse _obj)
  {
    return true;
  }

  public override int GetHashCode()
  {
    int _result = 1327;
    return _result;
  }
}

public static class HeartbeatResponse_Internal
{
  public static unsafe void Write(global::Improbable.Worker.Internal.GcHandlePool _pool,
                                  HeartbeatResponse _data, global::Improbable.Worker.Internal.Pbio.Object* _obj)
  {
  }

  public static unsafe HeartbeatResponse Read(global::Improbable.Worker.Internal.Pbio.Object* _obj)
  {
    HeartbeatResponse _data;
    return _data;
  }
}

}
