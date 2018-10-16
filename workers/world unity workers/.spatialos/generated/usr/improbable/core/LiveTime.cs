// Generated by SpatialOS codegen. DO NOT EDIT!
// source: LiveTime in improbable/core/LiveTime.schema.

namespace Improbable.Core
{

public static class LiveTime_Extensions
{
  public static LiveTime.Data Get(this global::Improbable.Worker.IComponentData<LiveTime> data)
  {
    return (LiveTime.Data) data;
  }

  public static LiveTime.Update Get(this global::Improbable.Worker.IComponentUpdate<LiveTime> update)
  {
    return (LiveTime.Update) update;
  }
}

public partial class LiveTime : global::Improbable.Worker.IComponentMetaclass
{
  public const uint ComponentId = 1006;

  uint global::Improbable.Worker.IComponentMetaclass.ComponentId
  {
    get { return ComponentId; }
  }

  /// <summary>
  /// Concrete data type for the LiveTime component.
  /// </summary>
  public class Data : global::Improbable.Worker.IComponentData<LiveTime>, global::Improbable.Collections.IDeepCopyable<Data>
  {
    public global::Improbable.Core.LiveTimeData Value;

    public Data(global::Improbable.Core.LiveTimeData value)
    {
      Value = value;
    }

    public Data(float timestamp)
    {
      Value = new global::Improbable.Core.LiveTimeData(timestamp);
    }

    public Data DeepCopy()
    {
      return new Data(Value.DeepCopy());
    }

    public global::Improbable.Worker.IComponentUpdate<LiveTime> ToUpdate()
    {
      var update = new Update();
      update.SetTimestamp(Value.timestamp);
      return update;
    }
  }

  /// <summary>
  /// Concrete update type for the LiveTime component.
  /// </summary>
  public class Update : global::Improbable.Worker.IComponentUpdate<LiveTime>, global::Improbable.Collections.IDeepCopyable<Update>
  {
    /// <summary>
    /// Field timestamp = 1.
    /// </summary>
    public global::Improbable.Collections.Option<float> timestamp;
    public Update SetTimestamp(float _value)
    {
      timestamp.Set(_value);
      return this;
    }

    public Update DeepCopy()
    {
      var _result = new Update();
      if (timestamp.HasValue)
      {
        float field;
        field = timestamp.Value;
        _result.timestamp.Set(field);
      }
      return _result;
    }

    public global::Improbable.Worker.IComponentData<LiveTime> ToInitialData()
    {
      return new Data(new global::Improbable.Core.LiveTimeData(timestamp.Value));
    }

    public void ApplyTo(global::Improbable.Worker.IComponentData<LiveTime> _data)
    {
      var _concrete = _data.Get();
      if (timestamp.HasValue)
      {
        _concrete.Value.timestamp = timestamp.Value;
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
    handler.Accept<LiveTime>(this);
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
            global::Improbable.Worker.Internal.Pbio.GetObject(root, /* entity_state */ 2), 1006);
        if (global::Improbable.Worker.Internal.Pbio.GetFloatCount(stateObject, 1) > 0)
        {
          float field;
          {
            field = global::Improbable.Worker.Internal.Pbio.GetFloat(stateObject, 1);
          }
          data.timestamp.Set(field);
        }
        **handleOut = global::Improbable.Worker.Internal.ClientHandles.Instance.CreateHandle(data);
      }
      else if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Snapshot)
      {
        var data = new Data(global::Improbable.Core.LiveTimeData_Internal.Read(
            global::Improbable.Worker.Internal.Pbio.GetObject(root, 1006)));
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
            global::Improbable.Worker.Internal.Pbio.AddObject(root, /* entity_state */ 2), 1006);
        if (data.timestamp.HasValue)
        {
          {
            global::Improbable.Worker.Internal.Pbio.AddFloat(stateObject, 1, data.timestamp.Value);
          }
        }
      }
      else if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Snapshot) {
        var data = (Data) global::Improbable.Worker.Internal.ClientHandles.Instance.Dereference(*handle);
        global::Improbable.Core.LiveTimeData_Internal.Write(_pool, data.Value, global::Improbable.Worker.Internal.Pbio.AddObject(root, 1006));
      }
      else if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Request)
      {
        global::Improbable.Worker.Internal.Pbio.AddObject(root, 1006);
      }
      else if (handleType == (byte) global::Improbable.Worker.Internal.ComponentProtocol.ClientHandleType.Response)
      {
        global::Improbable.Worker.Internal.Pbio.AddObject(root, 1006);
      }
    }
    catch (global::System.Exception e)
    {
      global::Improbable.Worker.ClientError.LogClientException(e);
    }
  }
}

}