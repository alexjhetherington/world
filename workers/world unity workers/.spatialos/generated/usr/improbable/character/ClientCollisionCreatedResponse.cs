// Generated by SpatialOS codegen. DO NOT EDIT!
// source: improbable.character.ClientCollisionCreatedResponse in improbable/character/CollisionsCreated.schema.

namespace Improbable.Character
{

public partial struct ClientCollisionCreatedResponse : global::System.IEquatable<ClientCollisionCreatedResponse>, global::Improbable.Collections.IDeepCopyable<ClientCollisionCreatedResponse>
{
  public static ClientCollisionCreatedResponse Create()
  {
    var _result = new ClientCollisionCreatedResponse();
    return _result;
  }

  public ClientCollisionCreatedResponse DeepCopy()
  {
    var _result = new ClientCollisionCreatedResponse();
    return _result;

  }

  public override bool Equals(object _obj)
  {
    return _obj is ClientCollisionCreatedResponse && Equals((ClientCollisionCreatedResponse) _obj);
  }

  public static bool operator==(ClientCollisionCreatedResponse a, ClientCollisionCreatedResponse b)
  {
    return a.Equals(b);
  }

  public static bool operator!=(ClientCollisionCreatedResponse a, ClientCollisionCreatedResponse b)
  {
    return !a.Equals(b);
  }

  public bool Equals(ClientCollisionCreatedResponse _obj)
  {
    return true;
  }

  public override int GetHashCode()
  {
    int _result = 1327;
    return _result;
  }
}

public static class ClientCollisionCreatedResponse_Internal
{
  public static unsafe void Write(global::Improbable.Worker.Internal.GcHandlePool _pool,
                                  ClientCollisionCreatedResponse _data, global::Improbable.Worker.Internal.Pbio.Object* _obj)
  {
  }

  public static unsafe ClientCollisionCreatedResponse Read(global::Improbable.Worker.Internal.Pbio.Object* _obj)
  {
    ClientCollisionCreatedResponse _data;
    return _data;
  }
}

}
