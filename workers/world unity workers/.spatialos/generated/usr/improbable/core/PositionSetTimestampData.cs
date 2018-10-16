// Generated by SpatialOS codegen. DO NOT EDIT!
// source: improbable.core.PositionSetTimestampData in improbable/core/PositionSetTimestamp.schema.

namespace Improbable.Core
{

public partial struct PositionSetTimestampData : global::System.IEquatable<PositionSetTimestampData>, global::Improbable.Collections.IDeepCopyable<PositionSetTimestampData>
{
  /// <summary>
  /// Field timestamp = 1.
  /// </summary>
  public float timestamp;

  public PositionSetTimestampData(float timestamp)
  {
    this.timestamp = timestamp;
  }

  public static PositionSetTimestampData Create()
  {
    var _result = new PositionSetTimestampData();
    return _result;
  }

  public PositionSetTimestampData DeepCopy()
  {
    var _result = new PositionSetTimestampData();
    _result.timestamp = timestamp;
    return _result;

  }

  public override bool Equals(object _obj)
  {
    return _obj is PositionSetTimestampData && Equals((PositionSetTimestampData) _obj);
  }

  public static bool operator==(PositionSetTimestampData a, PositionSetTimestampData b)
  {
    return a.Equals(b);
  }

  public static bool operator!=(PositionSetTimestampData a, PositionSetTimestampData b)
  {
    return !a.Equals(b);
  }

  public bool Equals(PositionSetTimestampData _obj)
  {
    return
        timestamp == _obj.timestamp;
  }

  public override int GetHashCode()
  {
    int _result = 1327;
    _result = (_result * 977) + timestamp.GetHashCode();
    return _result;
  }
}

public static class PositionSetTimestampData_Internal
{
  public static unsafe void Write(global::Improbable.Worker.Internal.GcHandlePool _pool,
                                  PositionSetTimestampData _data, global::Improbable.Worker.Internal.Pbio.Object* _obj)
  {
    {
      global::Improbable.Worker.Internal.Pbio.AddFloat(_obj, 1, _data.timestamp);
    }
  }

  public static unsafe PositionSetTimestampData Read(global::Improbable.Worker.Internal.Pbio.Object* _obj)
  {
    PositionSetTimestampData _data;
    {
      _data.timestamp = global::Improbable.Worker.Internal.Pbio.GetFloat(_obj, 1);
    }
    return _data;
  }
}

}