// Generated by SpatialOS codegen. DO NOT EDIT!
// source: Message in improbable/words/Message.schema.

namespace Improbable.Words
{

public static class Message_Extensions
{
  public static Message.Data Get(this global::Improbable.Worker.IComponentData<Message> data)
  {
    return (Message.Data) data;
  }

  public static Message.Update Get(this global::Improbable.Worker.IComponentUpdate<Message> update)
  {
    return (Message.Update) update;
  }
}

public partial class Message : global::Improbable.Worker.IComponentMetaclass
{
  public const uint ComponentId = 1007;

  uint global::Improbable.Worker.IComponentMetaclass.ComponentId
  {
    get { return ComponentId; }
  }

  /// <summary>
  /// Concrete data type for the Message component.
  /// </summary>
  public class Data : global::Improbable.Worker.IComponentData<Message>, global::Improbable.Collections.IDeepCopyable<Data>
  {
    public global::Improbable.Words.MessageData Value;

    public Data(global::Improbable.Words.MessageData value)
    {
      Value = value;
    }

    public Data(string message)
    {
      Value = new global::Improbable.Words.MessageData(message);
    }

    public Data DeepCopy()
    {
      return new Data(Value.DeepCopy());
    }

    public global::Improbable.Worker.IComponentUpdate<Message> ToUpdate()
    {
      var update = new Update();
      update.SetMessage(Value.message);
      return update;
    }
  }

  /// <summary>
  /// Concrete update type for the Message component.
  /// </summary>
  public class Update : global::Improbable.Worker.IComponentUpdate<Message>, global::Improbable.Collections.IDeepCopyable<Update>
  {
    /// <summary>
    /// Field message = 1.
    /// </summary>
    public global::Improbable.Collections.Option<string> message;
    public Update SetMessage(string _value)
    {
      message.Set(_value);
      return this;
    }

    public Update DeepCopy()
    {
      var _result = new Update();
      if (message.HasValue)
      {
        string field;
        field = message.Value;
        _result.message.Set(field);
      }
      return _result;
    }

    public global::Improbable.Worker.IComponentData<Message> ToInitialData()
    {
      return new Data(new global::Improbable.Words.MessageData(message.Value));
    }

    public void ApplyTo(global::Improbable.Worker.IComponentData<Message> _data)
    {
      var _concrete = _data.Get();
      if (message.HasValue)
      {
        _concrete.Value.message = message.Value;
      }
    }
  }

  public partial class Commands
  {
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
    handler.Accept<Message>(this);
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
        var fieldsToClear = new global::System.Collections.Generic.HashSet<uint>();
        var fieldsToClearCount = global::Improbable.Worker.Internal.Pbio.GetUint32Count(root, /* fields to clear */ 1);
        for (uint i = 0; i < fieldsToClearCount; ++i)
        {
           fieldsToClear.Add(global::Improbable.Worker.Internal.Pbio.IndexUint32(root, /* fields to clear */ 1, i));
        }
        var stateObject = global::Improbable.Worker.Internal.Pbio.GetObject(
            global::Improbable.Worker.Internal.Pbio.GetObject(root, /* entity_state */ 2), 1007);
        if (global::Improbable.Worker.Internal.Pbio.GetBytesCount(stateObject, 1) > 0)
        {
          string field;
          {
            field = global::System.Text.Encoding.UTF8.GetString(global::Improbable.Worker.Bytes.CopyOf(global::Improbable.Worker.Internal.Pbio.GetBytes(stateObject, 1), global::Improbable.Worker.Internal.Pbio.GetBytesLength(stateObject, 1)).BackingArray);
          }
          data.message.Set(field);
        }
        **handleOut = global::Improbable.Worker.Internal.ClientHandles.Instance.CreateHandle(data);
      }
      else if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Snapshot)
      {
        var data = new Data(global::Improbable.Words.MessageData_Internal.Read(
            global::Improbable.Worker.Internal.Pbio.GetObject(root, 1007)));
        **handleOut = global::Improbable.Worker.Internal.ClientHandles.Instance.CreateHandle(data);
      }
      else if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Request)
      {
        var data = new global::Improbable.Worker.Internal.GenericCommandObject();
        **handleOut = global::Improbable.Worker.Internal.ClientHandles.Instance.CreateHandle(data);
        return 0;
      }
      else if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Response)
      {
        var data = new global::Improbable.Worker.Internal.GenericCommandObject();
        **handleOut = global::Improbable.Worker.Internal.ClientHandles.Instance.CreateHandle(data);
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
        var data = (Update) global::Improbable.Worker.Internal.ClientHandles.Instance.Dereference(*handle);
        var stateObject = global::Improbable.Worker.Internal.Pbio.AddObject(
            global::Improbable.Worker.Internal.Pbio.AddObject(root, /* entity_state */ 2), 1007);
        if (data.message.HasValue)
        {
          {
            if (data.message.Value != null)
            {
              var _buffer = global::System.Text.Encoding.UTF8.GetBytes(data.message.Value);
              global::Improbable.Worker.Internal.Pbio.AddBytes(stateObject, 1, (byte*) _pool.Pin(_buffer), (uint) _buffer.Length);
            }
            else
            {
              global::Improbable.Worker.Internal.Pbio.AddBytes(stateObject, 1, null, 0);
            }
          }
        }
      }
      else if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Snapshot) {
        var data = (Data) global::Improbable.Worker.Internal.ClientHandles.Instance.Dereference(*handle);
        global::Improbable.Words.MessageData_Internal.Write(_pool, data.Value, global::Improbable.Worker.Internal.Pbio.AddObject(root, 1007));
      }
      else if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Request)
      {
        global::Improbable.Worker.Internal.Pbio.AddObject(root, 1007);
      }
      else if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Response)
      {
        global::Improbable.Worker.Internal.Pbio.AddObject(root, 1007);
      }
    }
    catch (global::System.Exception e)
    {
      global::Improbable.Worker.ClientError.LogClientException(e);
    }
  }
}

}
