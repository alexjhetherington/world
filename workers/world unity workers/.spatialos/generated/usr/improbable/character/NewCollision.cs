// Generated by SpatialOS codegen. DO NOT EDIT!
// source: improbable.character.NewCollision in improbable/character/CollisionsCreated.schema.

namespace Improbable.Character
{

public partial struct NewCollision : global::System.IEquatable<NewCollision>, global::Improbable.Collections.IDeepCopyable<NewCollision>
{
  /// <summary>
  /// Field entity_id = 1.
  /// </summary>
  public long entityId;
  /// <summary>
  /// Field timestamp = 2.
  /// </summary>
  public float timestamp;

  public NewCollision(
      long entityId,
      float timestamp)
  {
    this.entityId = entityId;
    this.timestamp = timestamp;
  }

  public static NewCollision Create()
  {
    var _result = new NewCollision();
    return _result;
  }

  public NewCollision DeepCopy()
  {
    var _result = new NewCollision();
    _result.entityId = entityId;
    _result.timestamp = timestamp;
    return _result;

  }

  public override bool Equals(object _obj)
  {
    return _obj is NewCollision && Equals((NewCollision) _obj);
  }

  public static bool operator==(NewCollision a, NewCollision b)
  {
    return a.Equals(b);
  }

  public static bool operator!=(NewCollision a, NewCollision b)
  {
    return !a.Equals(b);
  }

  public bool Equals(NewCollision _obj)
  {
    return
        entityId == _obj.entityId &&
        timestamp == _obj.timestamp;
  }

  public override int GetHashCode()
  {
    int _result = 1327;
    _result = (_result * 977) + entityId.GetHashCode();
    _result = (_result * 977) + timestamp.GetHashCode();
    return _result;
  }
}

public static class NewCollision_Internal
{
  public static unsafe void Write(global::Improbable.Worker.Internal.GcHandlePool _pool,
                                  NewCollision _data, global::Improbable.Worker.Internal.Pbio.Object* _obj)
  {
    {
      global::Improbable.Worker.Internal.Pbio.AddInt64(_obj, 1, _data.entityId);
    }
    {
      global::Improbable.Worker.Internal.Pbio.AddFloat(_obj, 2, _data.timestamp);
    }
  }

  public static unsafe NewCollision Read(global::Improbable.Worker.Internal.Pbio.Object* _obj)
  {
    NewCollision _data;
    {
      _data.entityId = global::Improbable.Worker.Internal.Pbio.GetInt64(_obj, 1);
    }
    {
      _data.timestamp = global::Improbable.Worker.Internal.Pbio.GetFloat(_obj, 2);
    }
    return _data;
  }
}

}