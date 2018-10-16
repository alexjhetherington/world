// Generated by SpatialOS codegen. DO NOT EDIT!
// source: PlayerCreation in improbable/core/PlayerCreation.schema.

namespace Improbable.Core
{

public static class PlayerCreation_Extensions
{
  public static PlayerCreation.Data Get(this global::Improbable.Worker.IComponentData<PlayerCreation> data)
  {
    return (PlayerCreation.Data) data;
  }

  public static PlayerCreation.Update Get(this global::Improbable.Worker.IComponentUpdate<PlayerCreation> update)
  {
    return (PlayerCreation.Update) update;
  }

  public static PlayerCreation.Commands.CreatePlayer.Request Get(this global::Improbable.Worker.ICommandRequest<PlayerCreation.Commands.CreatePlayer> request)
  {
    return (PlayerCreation.Commands.CreatePlayer.Request) request;
  }

  public static PlayerCreation.Commands.CreatePlayer.Response Get(this global::Improbable.Worker.ICommandResponse<PlayerCreation.Commands.CreatePlayer> response)
  {
    return (PlayerCreation.Commands.CreatePlayer.Response) response;
  }
}

public partial class PlayerCreation : global::Improbable.Worker.IComponentMetaclass
{
  public const uint ComponentId = 1001;

  uint global::Improbable.Worker.IComponentMetaclass.ComponentId
  {
    get { return ComponentId; }
  }

  /// <summary>
  /// Concrete data type for the PlayerCreation component.
  /// </summary>
  public class Data : global::Improbable.Worker.IComponentData<PlayerCreation>, global::Improbable.Collections.IDeepCopyable<Data>
  {
    public global::Improbable.Core.PlayerCreationData Value;

    public Data(global::Improbable.Core.PlayerCreationData value)
    {
      Value = value;
    }

    public Data()
    {
      Value = new global::Improbable.Core.PlayerCreationData();
    }

    public Data DeepCopy()
    {
      return new Data(Value.DeepCopy());
    }

    public global::Improbable.Worker.IComponentUpdate<PlayerCreation> ToUpdate()
    {
      var update = new Update();
      return update;
    }
  }

  /// <summary>
  /// Concrete update type for the PlayerCreation component.
  /// </summary>
  public class Update : global::Improbable.Worker.IComponentUpdate<PlayerCreation>, global::Improbable.Collections.IDeepCopyable<Update>
  {
    public Update DeepCopy()
    {
      var _result = new Update();
      return _result;
    }

    public global::Improbable.Worker.IComponentData<PlayerCreation> ToInitialData()
    {
      return new Data(new global::Improbable.Core.PlayerCreationData());
    }

    public void ApplyTo(global::Improbable.Worker.IComponentData<PlayerCreation> _data)
    {
    }
  }

  public partial class Commands
  {
    /// <summary>
    /// Command create_player.
    /// </summary>
    public partial class CreatePlayer : global::Improbable.Worker.ICommandMetaclass
    {
      public uint ComponentId { get { return 1001; } }
      public uint CommandId { get { return 1; } }

      public class Request : global::Improbable.Worker.ICommandRequest<CreatePlayer>, global::Improbable.Collections.IDeepCopyable<Request>
      {
        public global::Improbable.Core.CreatePlayerRequest Value;

        public Request(global::Improbable.Core.CreatePlayerRequest value)
        {
          Value = value;
        }

        public Request()
        {
          Value = new global::Improbable.Core.CreatePlayerRequest();
        }

        public Request DeepCopy()
        {
          return new Request(Value.DeepCopy());
        }

        public global::Improbable.Worker.Internal.GenericCommandObject ToGenericObject()
        {
          return new global::Improbable.Worker.Internal.GenericCommandObject(1, this);
        }
      }

      public class Response : global::Improbable.Worker.ICommandResponse<CreatePlayer>, global::Improbable.Collections.IDeepCopyable<Response>
      {
        public global::Improbable.Core.CreatePlayerResponse Value;

        public Response(global::Improbable.Core.CreatePlayerResponse value)
        {
          Value = value;
        }

        public Response()
        {
          Value = new global::Improbable.Core.CreatePlayerResponse();
        }

        public Response DeepCopy()
        {
          return new Response(Value.DeepCopy());
        }

        public global::Improbable.Worker.Internal.GenericCommandObject ToGenericObject()
        {
          return new global::Improbable.Worker.Internal.GenericCommandObject(1, this);
        }
      }
    }
  }

  // Implementation details below here.
  //----------------------------------------------------------------

  public global::Improbable.Worker.Internal.ComponentProtocol.ComponentVtable Vtable {
    get {
      global::Improbable.Worker.Internal.ComponentProtocol.ComponentVtable vtable;
      vtable.ComponentId = ComponentId;
      unsafe { vtable.UserData = null; }
      vtable.Free = global::System.Runtime.InteropServices.Marshal
          .GetFunctionPointerForDelegate(global::Improbable.Worker.Internal.ClientHandles.ClientFree);
      vtable.Copy = global::System.Runtime.InteropServices.Marshal
          .GetFunctionPointerForDelegate(global::Improbable.Worker.Internal.ClientHandles.ClientCopy);
      vtable.Deserialize = global::System.Runtime.InteropServices.Marshal
          .GetFunctionPointerForDelegate(clientDeserialize);
      vtable.Serialize = global::System.Runtime.InteropServices.Marshal
          .GetFunctionPointerForDelegate(clientSerialize);
      return vtable;
    }
  }

  public void InvokeHandler(global::Improbable.Worker.Dynamic.Handler handler)
  {
    handler.Accept<PlayerCreation>(this);
  }

  private static unsafe readonly global::Improbable.Worker.Internal.ComponentProtocol.ClientDeserialize
      clientDeserialize = ClientDeserialize;
  private static unsafe readonly global::Improbable.Worker.Internal.ComponentProtocol.ClientSerialize
      clientSerialize = ClientSerialize;

  [global::Improbable.Worker.Internal.MonoPInvokeCallback(typeof(global::Improbable.Worker.Internal.ComponentProtocol.ClientDeserialize))]
  private static unsafe global::System.Byte
  ClientDeserialize(global::System.UInt32 componentId,
                    void* userData,
                    global::System.Byte handleType,
                    global::Improbable.Worker.Internal.Pbio.Object* root,
                    global::Improbable.Worker.Internal.ComponentProtocol.ClientHandle** handleOut)
  {
    *handleOut = null;
    try
    {
      *handleOut = global::Improbable.Worker.Internal.ClientHandles.HandleAlloc();
      if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Update) {
        var data = new Update();
        **handleOut = global::Improbable.Worker.Internal.ClientHandles.Instance.CreateHandle(data);
      }
      else if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Snapshot)
      {
        var data = new Data(global::Improbable.Core.PlayerCreationData_Internal.Read(
            global::Improbable.Worker.Internal.Pbio.GetObject(root, 1001)));
        **handleOut = global::Improbable.Worker.Internal.ClientHandles.Instance.CreateHandle(data);
      }
      else if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Request)
      {
        var data = new global::Improbable.Worker.Internal.GenericCommandObject();
        **handleOut = global::Improbable.Worker.Internal.ClientHandles.Instance.CreateHandle(data);
        var commandObject = global::Improbable.Worker.Internal.Pbio.GetObject(root, 1001);
        if (global::Improbable.Worker.Internal.Pbio.GetObjectCount(commandObject, 1) != 0) {
          data.CommandId = 1;
          data.CommandObject = new Commands.CreatePlayer.Request(global::Improbable.Core.CreatePlayerRequest_Internal.Read(global::Improbable.Worker.Internal.Pbio.GetObject(commandObject, 1)));
          return 1;
        }
        return 0;
      }
      else if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Response)
      {
        var data = new global::Improbable.Worker.Internal.GenericCommandObject();
        **handleOut = global::Improbable.Worker.Internal.ClientHandles.Instance.CreateHandle(data);
        var commandObject = global::Improbable.Worker.Internal.Pbio.GetObject(root, 1001);
        if (global::Improbable.Worker.Internal.Pbio.GetObjectCount(commandObject, 2) != 0) {
          data.CommandId = 1;
          data.CommandObject = new Commands.CreatePlayer.Response(global::Improbable.Core.CreatePlayerResponse_Internal.Read(global::Improbable.Worker.Internal.Pbio.GetObject(commandObject, 2)));
          return 1;
        }
        return 0;
      }
    }
    catch (global::System.Exception e)
    {
      global::Improbable.Worker.ClientError.LogClientException(e);
      return 0;
    }
    return 1;
  }

  [global::Improbable.Worker.Internal.MonoPInvokeCallback(typeof(global::Improbable.Worker.Internal.ComponentProtocol.ClientSerialize))]
  private static unsafe void
  ClientSerialize(global::System.UInt32 componentId,
                  void* userData,
                  global::System.Byte handleType,
                  global::Improbable.Worker.Internal.ComponentProtocol.ClientHandle* handle,
                  global::Improbable.Worker.Internal.Pbio.Object* root)
  {
    try
    {
      var _pool = global::Improbable.Worker.Internal.ClientHandles.Instance.GetGcHandlePool(*handle);
      if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Update) {
        global::Improbable.Worker.Internal.Pbio.AddObject(
            global::Improbable.Worker.Internal.Pbio.AddObject(root, /* entity_state */ 2), 1001);
      }
      else if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Snapshot) {
        var data = (Data) global::Improbable.Worker.Internal.ClientHandles.Instance.Dereference(*handle);
        global::Improbable.Core.PlayerCreationData_Internal.Write(_pool, data.Value, global::Improbable.Worker.Internal.Pbio.AddObject(root, 1001));
      }
      else if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Request)
      {
        var data = (global::Improbable.Worker.Internal.GenericCommandObject)
            global::Improbable.Worker.Internal.ClientHandles.Instance.Dereference(*handle);
        var commandObject = global::Improbable.Worker.Internal.Pbio.AddObject(root, 1001);
        if (data.CommandId == 1)
        {
          var requestObject = (Commands.CreatePlayer.Request) data.CommandObject;
          {
            global::Improbable.Core.CreatePlayerRequest_Internal.Write(_pool, requestObject.Value, global::Improbable.Worker.Internal.Pbio.AddObject(commandObject, 1));
          }
        }
      }
      else if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Response)
      {
        var data = (global::Improbable.Worker.Internal.GenericCommandObject)
            global::Improbable.Worker.Internal.ClientHandles.Instance.Dereference(*handle);
        var commandObject = global::Improbable.Worker.Internal.Pbio.AddObject(root, 1001);
        if (data.CommandId == 1)
        {
          var responseObject = (Commands.CreatePlayer.Response) data.CommandObject;
          {
            global::Improbable.Core.CreatePlayerResponse_Internal.Write(_pool, responseObject.Value, global::Improbable.Worker.Internal.Pbio.AddObject(commandObject, 2));
          }
        }
      }
    }
    catch (global::System.Exception e)
    {
      global::Improbable.Worker.ClientError.LogClientException(e);
    }
  }
}

}