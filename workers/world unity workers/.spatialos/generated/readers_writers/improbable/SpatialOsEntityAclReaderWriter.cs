// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

namespace Improbable
{

/// <summary>
/// EntityAclWriter is deprecated and will be removed in a future SpatialOS version. Please migrate to the simplified
/// <c>EntityAcl.Writer</c> interface (but see its documentation for semantic differences).
/// </summary>
[global::Improbable.Entity.Component.WriterInterface]
[global::Improbable.Entity.Component.ComponentId(50)]
[global::System.Obsolete("Please use EntityAcl.Writer (but see its documentation for semantic differences).")]
public interface EntityAclWriter : EntityAclReader
{
  EntityAcl.Updater Update { get; }
}

/// <summary>
/// EntityAclReader is deprecated and will be removed in a future SpatialOS version. Please migrate to the simplified
/// <c>EntityAcl.Reader</c> interface (but see its documentation for semantic differences).
/// </summary>
[global::Improbable.Entity.Component.ReaderInterface]
[global::Improbable.Entity.Component.ComponentId(50)]
[global::System.Obsolete("Please use EntityAcl.Reader (but see its documentation for semantic differences).")]
public interface EntityAclReader
{
  [global::System.Obsolete("Please use \"Authority == Improbable.Worker.Authority.Authoritative || Authority == Improbable.Worker.Authority.AuthorityLossImminent\".")]
  bool IsAuthoritativeHere { get; }
  global::Improbable.Worker.Authority Authority { get; }
  event global::System.Action PropertyUpdated;
  event global::System.Action<global::Improbable.Worker.Authority> AuthorityChanged;
  // Field readAcl = 1.
  global::Improbable.WorkerRequirementSet ReadAcl { get; }
  event global::System.Action<global::Improbable.WorkerRequirementSet> ReadAclUpdated;
  // Field componentWriteAcl = 2.
  global::Improbable.Collections.Map<uint, global::Improbable.WorkerRequirementSet> ComponentWriteAcl { get; }
  event global::System.Action<global::Improbable.Collections.Map<uint, global::Improbable.WorkerRequirementSet>> ComponentWriteAclUpdated;
}

public partial class EntityAcl : global::Improbable.Entity.Component.IComponentFactory
{
  [global::Improbable.Entity.Component.WriterInterface]
  [global::Improbable.Entity.Component.ComponentId(50)]
  public interface Writer : Reader, global::Improbable.Entity.Component.IComponentWriter
  {
    /// <summary>
    /// Sends a component update.
    /// </summary>
    /// <remarks>
    /// Unlike the deprecated <c>EntityAclWriter</c> interface, changes made by the update are not
    /// applied to the local copy of the component (returned by the <c>Data</c> property) until the
    /// update is processed by the connection. Therefore, the <c>ComponentUpdated</c> event is not
    /// triggered immediately, but queued to be triggered at a later time. Additionally, the
    /// update will be sent even if it would have no effect on the current local copy of the
    /// component data; discarding of no-op updates should be done manually. The behaviour is
    /// undefined if the update is mutated after it is sent; use <c>Send(update.DeepCopy())</c> if
    /// you intend to hold on to the update and modify it later. <seealso cref="CopyAndSend"/>
    /// </remarks>
    void Send(Update update);
	/// <summary>
	/// Sends an authority loss imminent ack for this component.
	/// </summary>
	void SendAuthorityLossImminentAcknowledgement();
    /// <summary>
    /// Performs a deep copy and sends the copy as a component update.
    /// </summary>
    /// <remarks>
    /// Works exactly like <c>Send(update)</c>, but performs a deep copy of the update before sending
    /// it. Use this method if you intend to hold on to the update and modify it later. <see cref="Send"/>
    /// </remarks>
    void CopyAndSend(Update update);
    /// <summary>
    /// Returns the CommandReceiver for this component.
    /// </summary>
    ICommandReceiver CommandReceiver { get; }
  }

  [global::Improbable.Entity.Component.ReaderInterface]
  [global::Improbable.Entity.Component.ComponentId(50)]
  public interface Reader
  {
    /// <summary>
    /// Whether the local worker currently has authority over this component.
    /// </summary>

    [global::System.Obsolete("Please use \"Authority == Improbable.Worker.Authority.Authoritative || Authority == Improbable.Worker.Authority.AuthorityLossImminent\".")]
    bool HasAuthority { get; }

    /// <summary>
    /// The authority state of this component.
    /// </summary>
    global::Improbable.Worker.Authority Authority { get; }
    /// <summary>
    /// Retrieves the local copy of the component data.
    /// </summary>
    global::Improbable.EntityAclData Data { get; }

    /// <summary>
    /// Triggered whenever an update is received for this component.
    /// </summary>
    global::Improbable.Entity.Component.EventCallbackHandler<Update> ComponentUpdated { get; set; }
    /// <summary>
    /// Triggered whenever the local worker's authority over this component changes.
    /// </summary>
    global::Improbable.Entity.Component.EventCallbackHandler<global::Improbable.Worker.Authority> AuthorityChanged { get; set; }
    /// <summary>
    /// Register callbacks here to be invoked whenever readAcl field value is changed.
    /// </summary>
    global::Improbable.Entity.Component.PropertyCallbackHandler<global::Improbable.WorkerRequirementSet> ReadAclUpdated { get; set; }
    /// <summary>
    /// Register callbacks here to be invoked whenever componentWriteAcl field value is changed.
    /// </summary>
    global::Improbable.Entity.Component.PropertyCallbackHandler<global::Improbable.Collections.Map<uint, global::Improbable.WorkerRequirementSet>> ComponentWriteAclUpdated { get; set; }
  }

  /// <summary>
  /// EntityAcl.Updater is deprecated and will be removed in a future SpatialOS version.
  /// Please use EntityAcl.Writer.Send() instead (see the documentation for semantic differences).
  /// </summary>
  [global::System.Obsolete("Please use EntityAcl.Writer.Send() instead (see the documentation for semantic differences).")]
  public interface Updater
  {
    void FinishAndSend();
	void SendAuthorityLossImminentAcknowledgement();
    // Field readAcl = 1.
    #pragma warning disable 0618
    Updater ReadAcl(global::Improbable.WorkerRequirementSet newValue);
    #pragma warning restore 0618
    // Field componentWriteAcl = 2.
    #pragma warning disable 0618
    Updater ComponentWriteAcl(global::System.Collections.Generic.IDictionary<uint, global::Improbable.WorkerRequirementSet> newValue);
    #pragma warning restore 0618
  }

  public interface ICommandReceiver
  {
  }

  public partial class Commands
  {

  }
  // Implementation details below here.
  //-----------------------------------

  private readonly global::System.Collections.Generic.Dictionary<global::Improbable.EntityId, Impl> implMap =
      new global::System.Collections.Generic.Dictionary<global::Improbable.EntityId, Impl>();
  private bool loggedInvalidAddComponent = false;
  private bool loggedInvalidRemoveComponent = false;

  public void UnregisterWithConnection(global::Improbable.Worker.Connection connection, global::Improbable.Worker.Dispatcher dispatcher) {
    loggedInvalidAddComponent = false;
    loggedInvalidRemoveComponent = false;
    implMap.Clear();
  }

  public void RegisterWithConnection(global::Improbable.Worker.Connection connection, global::Improbable.Worker.Dispatcher dispatcher,
                                     global::Improbable.Entity.Component.ComponentFactoryCallbacks callbacks)
  {
    dispatcher.OnAddComponent<EntityAcl>((op) => {
      if (!implMap.ContainsKey(op.EntityId))
      {
        var impl = new Impl(connection, op.EntityId, op.Data.Get());
        implMap.Add(op.EntityId, impl);
        if (callbacks.OnComponentAdded != null)
        {
          callbacks.OnComponentAdded(op.EntityId, this, impl);
        }
      }
      else if (!loggedInvalidAddComponent)
      {
        global::Improbable.Worker.ClientError.LogClientException(new global::System.InvalidOperationException(
            "Component EntityAcl added to entity " + op.EntityId + ", but it already exists." +
            "This error will be reported once only for each component."));
        loggedInvalidAddComponent = true;
      }
    });

    dispatcher.OnRemoveComponent<EntityAcl>((op) => {
      Impl impl;
      if (implMap.TryGetValue(op.EntityId, out impl))
      {
        if (callbacks.OnComponentRemoved != null)
        {
          callbacks.OnComponentRemoved(op.EntityId, this, impl);
        }
        implMap.Remove(op.EntityId);
      }
      else if (!loggedInvalidRemoveComponent)
      {
        global::Improbable.Worker.ClientError.LogClientException(new global::System.InvalidOperationException(
            "Component EntityAcl removed from entity " + op.EntityId + ", but it does not exist." +
            "This error will be reported once only for each component."));
        loggedInvalidRemoveComponent = true;
      }
    });

    dispatcher.OnAuthorityChange<EntityAcl>((op) => {
      Impl impl;
      if (implMap.TryGetValue(op.EntityId, out impl))
      {
        impl.On_AuthorityChange(op.Authority);
        if (callbacks.OnAuthorityChanged != null)
        {
          callbacks.OnAuthorityChanged(op.EntityId, this, op.Authority, impl);
        }
      }
    });

    dispatcher.OnComponentUpdate<EntityAcl>((op) => {
      Impl impl;
      if (implMap.TryGetValue(op.EntityId, out impl))
      {
        impl.On_ComponentUpdate(op.Update.Get());
        if (callbacks.OnComponentUpdated != null)
        {
          callbacks.OnComponentUpdated(op.EntityId, this, op.Update.Get());
        }
      }
    });
  }

  public object GetComponentForEntity(global::Improbable.EntityId entityId)
  {
    Impl impl;
    implMap.TryGetValue(entityId, out impl);
    return impl;
  }

  [global::System.Obsolete("As of SpatialOS 10.4.3, this method no longer does anything. Please disconnect events manually.")]
  public void DisconnectEventHandlersWithTarget(global::Improbable.EntityId entityId, object actionTarget)
  {
  }

  #pragma warning disable 0618
  public class Impl : EntityAclWriter, Writer, Updater
  #pragma warning restore 0618
  {
    private readonly global::Improbable.Worker.Connection connection;
    private readonly global::Improbable.EntityId entityId;
    private readonly CommandReceiverImpl commandReceiver;

    private Data data;
    private global::Improbable.Worker.Authority authority = global::Improbable.Worker.Authority.NotAuthoritative;

    public uint ComponentId { get { return 50; } }

    public Impl(global::Improbable.Worker.Connection connection, global::Improbable.EntityId entityId, Data initialData)
    {
      this.connection = connection;
      this.entityId = entityId;
      this.commandReceiver = new CommandReceiverImpl();
      data = initialData.DeepCopy();

      readAclUpdateHandler =
        new global::Improbable.Entity.Component.PropertyCallbackHandler<global::Improbable.WorkerRequirementSet>(() => data.Value.readAcl);
      componentWriteAclUpdateHandler =
        new global::Improbable.Entity.Component.PropertyCallbackHandler<global::Improbable.Collections.Map<uint, global::Improbable.WorkerRequirementSet>>(() => data.Value.componentWriteAcl);
    }

    // Constructor for tests.
    internal Impl()
    {
    }

    // Constructor for tests
    internal Impl(global::Improbable.EntityId entityId, Data initialData)
    {
        this.entityId = entityId;
        data = initialData.DeepCopy();
    }

    public ICommandReceiver CommandReceiver
    {
      get { return commandReceiver; }
    }

    internal CommandReceiverImpl CommandReceiverInternal
    {
      get { return commandReceiver; }
    }

    private global::Improbable.Entity.Component.EventCallbackHandler<Update> componentUpdatedHandler
      = new global::Improbable.Entity.Component.EventCallbackHandler<Update>();

    public global::Improbable.Entity.Component.EventCallbackHandler<Update> ComponentUpdated
    {
      get { return componentUpdatedHandler; }
	  set { componentUpdatedHandler = value; }
    }

    private global::Improbable.Entity.Component.EventCallbackHandler<global::Improbable.Worker.Authority> authorityChangedHandler
      = new global::Improbable.Entity.Component.EventCallbackHandler<global::Improbable.Worker.Authority>();

    public global::Improbable.Entity.Component.EventCallbackHandler<global::Improbable.Worker.Authority> AuthorityChanged
    {
      get { return authorityChangedHandler; }
	  set { authorityChangedHandler = value; }
    }

	[global::System.Obsolete("Please use \"Authority == Improbable.Worker.Authority.Authoritative || Authority == Improbable.Worker.Authority.AuthorityLossImminent\".")]
    public bool HasAuthority
    {
      get { return Authority == Improbable.Worker.Authority.Authoritative || Authority == Improbable.Worker.Authority.AuthorityLossImminent; }
    }

    public global::Improbable.Worker.Authority Authority
    {
      get { return authority; }
    }

    public global::Improbable.EntityAclData Data
    {
      get { return data.Value; }
    }

    public void Send(Update update)
    {
      connection.SendComponentUpdate(entityId, update);
    }

	public void SendAuthorityLossImminentAcknowledgement()
	{
	  connection.SendAuthorityLossImminentAcknowledgement<EntityAcl>(entityId);
	}

    public void CopyAndSend(Update update)
    {
      Send(update.DeepCopy());
    }

    internal void On_AuthorityChange(global::Improbable.Worker.Authority newAuthority)
    {
      authority = newAuthority;
      authorityChangedHandler.InvokeCallbacks(newAuthority);
    }

    internal void On_ComponentUpdate(Update update)
    {
      update.ApplyTo(data);
      componentUpdatedHandler.InvokeCallbacks(update);
      TriggerCallbacks(update);
    }

    public global::Improbable.EntityId EntityId
    {
      get { return entityId; }
    }

    private global::Improbable.Entity.Component.PropertyCallbackHandler<global::Improbable.WorkerRequirementSet> readAclUpdateHandler;

    public global::Improbable.Entity.Component.PropertyCallbackHandler<global::Improbable.WorkerRequirementSet> ReadAclUpdated
    {
      get { return readAclUpdateHandler; }
	    set { readAclUpdateHandler = value; }
    }
    private global::Improbable.Entity.Component.PropertyCallbackHandler<global::Improbable.Collections.Map<uint, global::Improbable.WorkerRequirementSet>> componentWriteAclUpdateHandler;

    public global::Improbable.Entity.Component.PropertyCallbackHandler<global::Improbable.Collections.Map<uint, global::Improbable.WorkerRequirementSet>> ComponentWriteAclUpdated
    {
      get { return componentWriteAclUpdateHandler; }
	    set { componentWriteAclUpdateHandler = value; }
    }

    private void TriggerCallbacks(Update update)
    {
      if (update.readAcl.HasValue)
      {
        readAclUpdateHandler.InvokeCallbacks(update.readAcl.Value);
      }
      if (update.componentWriteAcl.HasValue)
      {
        componentWriteAclUpdateHandler.InvokeCallbacks(update.componentWriteAcl.Value);
      }
    }

    // Implementation of deprecated interface.
    //----------------------------------------

    private EntityAcl.Update currentUpdate = null;

    #pragma warning disable 0618
    public Updater Update
    #pragma warning restore 0618
    {
      get
      {
        if (currentUpdate == null)
        {
          currentUpdate = new EntityAcl.Update();
        }
        else
        {
          global::Improbable.Worker.ClientError.LogClientException(new global::System.InvalidOperationException(
              "Update for component EntityAcl called with an update already in-flight! " +
              "Each Update must be followed by a FinishAndSend() before another update is made."));
        }
        return this;
      }
    }

    public void FinishAndSend()
    {
      if (currentUpdate != null)
      {
        if (FinishAndSend_ResolveDiff(currentUpdate)) {
          On_ComponentUpdate(currentUpdate);
          connection.SendComponentUpdate(entityId, currentUpdate, /* legacy semantics */ true);
        }
        currentUpdate = null;
      }
      else
      {
        global::Improbable.Worker.ClientError.LogClientException(new global::System.InvalidOperationException(
            "FinishAndSend() for component Everything called with no update in-flight!"));
      }
    }

    private global::Improbable.Entity.Component.ReferenceCountedMemoiser _callbackConversionMemoiser = null;

    private global::Improbable.Entity.Component.ReferenceCountedMemoiser CallbackConversionMemoiser {
      get
      {
        return _callbackConversionMemoiser ?? (_callbackConversionMemoiser = new global::Improbable.Entity.Component.ReferenceCountedMemoiser());
      }
    }

    [global::System.Obsolete("Please use \"Authority == Improbable.Worker.Authority.Authoritative || Authority == Improbable.Worker.Authority.AuthorityLossImminent\".")]
    public bool IsAuthoritativeHere
    {
      get { return Authority == Improbable.Worker.Authority.Authoritative || Authority == Improbable.Worker.Authority.AuthorityLossImminent; }
    }

    #pragma warning disable 0618
    event global::System.Action<global::Improbable.Worker.Authority> EntityAclReader.AuthorityChanged
    #pragma warning restore 0618
    {
      add { ((Reader) this).AuthorityChanged.Add(value); value(Authority); }
      remove { ((Reader) this).AuthorityChanged.Remove(value); }
    }

    private static global::System.Action<Update> PropertyUpdated_Wrap(global::System.Action action)
    {
      return (update) => { action(); };
    }

    public event global::System.Action PropertyUpdated
    {
      add { ComponentUpdated.Add(CallbackConversionMemoiser.Add<global::System.Action, global::System.Action<Update>>(PropertyUpdated_Wrap, value)); value(); }
      remove { ComponentUpdated.Remove(CallbackConversionMemoiser.Remove<global::System.Action, global::System.Action<Update>>(PropertyUpdated_Wrap, value)); }
    }

    public global::Improbable.WorkerRequirementSet ReadAcl
    {
      get { return Data.readAcl; }
    }

    private static global::System.Action<Update> ReadAclUpdated_Wrap(global::System.Action<global::Improbable.WorkerRequirementSet> action)
    {
      return (update) => { if (update.readAcl.HasValue) { action(update.readAcl.Value); } };
    }

    #pragma warning disable 0618
    event global::System.Action<global::Improbable.WorkerRequirementSet> EntityAclReader.ReadAclUpdated
    #pragma warning restore 0618
    {
      add { ComponentUpdated.Add(CallbackConversionMemoiser.Add<global::System.Action<global::Improbable.WorkerRequirementSet>, global::System.Action<Update>>(ReadAclUpdated_Wrap, value)); value(Data.readAcl); }
      remove { ComponentUpdated.Remove(CallbackConversionMemoiser.Remove<global::System.Action<global::Improbable.WorkerRequirementSet>, global::System.Action<Update>>(ReadAclUpdated_Wrap, value)); }
    }

    #pragma warning disable 0618
    Updater Updater.ReadAcl(global::Improbable.WorkerRequirementSet newValue)
    #pragma warning restore 0618
    {
      currentUpdate.SetReadAcl(newValue);
      return this;
    }
    public global::Improbable.Collections.Map<uint, global::Improbable.WorkerRequirementSet> ComponentWriteAcl
    {
      get { return Data.componentWriteAcl; }
    }

    private static global::System.Action<Update> ComponentWriteAclUpdated_Wrap(global::System.Action<global::Improbable.Collections.Map<uint, global::Improbable.WorkerRequirementSet>> action)
    {
      return (update) => { if (update.componentWriteAcl.HasValue) { action(update.componentWriteAcl.Value); } };
    }

    #pragma warning disable 0618
    event global::System.Action<global::Improbable.Collections.Map<uint, global::Improbable.WorkerRequirementSet>> EntityAclReader.ComponentWriteAclUpdated
    #pragma warning restore 0618
    {
      add { ComponentUpdated.Add(CallbackConversionMemoiser.Add<global::System.Action<global::Improbable.Collections.Map<uint, global::Improbable.WorkerRequirementSet>>, global::System.Action<Update>>(ComponentWriteAclUpdated_Wrap, value)); value(Data.componentWriteAcl); }
      remove { ComponentUpdated.Remove(CallbackConversionMemoiser.Remove<global::System.Action<global::Improbable.Collections.Map<uint, global::Improbable.WorkerRequirementSet>>, global::System.Action<Update>>(ComponentWriteAclUpdated_Wrap, value)); }
    }

    #pragma warning disable 0618
    Updater Updater.ComponentWriteAcl(global::System.Collections.Generic.IDictionary<uint, global::Improbable.WorkerRequirementSet> newValue)
    #pragma warning restore 0618
    {
      currentUpdate.SetComponentWriteAcl(new global::Improbable.Collections.Map<uint, global::Improbable.WorkerRequirementSet>(newValue));
      return this;
    }
    private bool FinishAndSend_ResolveDiff(Update update)
    {
      bool isNoOp = true;
      if (update.readAcl.HasValue)
      {
        if (update.readAcl.Value == Data.readAcl)
        {
          update.readAcl.Clear();
        }
        else
        {
          isNoOp = false;
        }
      }
      if (update.componentWriteAcl.HasValue)
      {
        if (update.componentWriteAcl.Value == Data.componentWriteAcl)
        {
          update.componentWriteAcl.Clear();
        }
        else
        {
          isNoOp = false;
        }
      }
      return !isNoOp;
    }

    internal class CommandReceiverImpl : ICommandReceiver
    {
    }
  }
}
}
