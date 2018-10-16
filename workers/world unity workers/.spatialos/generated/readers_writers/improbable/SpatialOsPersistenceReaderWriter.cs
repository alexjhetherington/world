// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

namespace Improbable
{

/// <summary>
/// PersistenceWriter is deprecated and will be removed in a future SpatialOS version. Please migrate to the simplified
/// <c>Persistence.Writer</c> interface (but see its documentation for semantic differences).
/// </summary>
[global::Improbable.Entity.Component.WriterInterface]
[global::Improbable.Entity.Component.ComponentId(55)]
[global::System.Obsolete("Please use Persistence.Writer (but see its documentation for semantic differences).")]
public interface PersistenceWriter : PersistenceReader
{
  Persistence.Updater Update { get; }
}

/// <summary>
/// PersistenceReader is deprecated and will be removed in a future SpatialOS version. Please migrate to the simplified
/// <c>Persistence.Reader</c> interface (but see its documentation for semantic differences).
/// </summary>
[global::Improbable.Entity.Component.ReaderInterface]
[global::Improbable.Entity.Component.ComponentId(55)]
[global::System.Obsolete("Please use Persistence.Reader (but see its documentation for semantic differences).")]
public interface PersistenceReader
{
  [global::System.Obsolete("Please use \"Authority == Improbable.Worker.Authority.Authoritative || Authority == Improbable.Worker.Authority.AuthorityLossImminent\".")]
  bool IsAuthoritativeHere { get; }
  global::Improbable.Worker.Authority Authority { get; }
  event global::System.Action PropertyUpdated;
  event global::System.Action<global::Improbable.Worker.Authority> AuthorityChanged;
}

public partial class Persistence : global::Improbable.Entity.Component.IComponentFactory
{
  [global::Improbable.Entity.Component.WriterInterface]
  [global::Improbable.Entity.Component.ComponentId(55)]
  public interface Writer : Reader, global::Improbable.Entity.Component.IComponentWriter
  {
    /// <summary>
    /// Sends a component update.
    /// </summary>
    /// <remarks>
    /// Unlike the deprecated <c>PersistenceWriter</c> interface, changes made by the update are not
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
  [global::Improbable.Entity.Component.ComponentId(55)]
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
    global::Improbable.PersistenceData Data { get; }

    /// <summary>
    /// Triggered whenever an update is received for this component.
    /// </summary>
    global::Improbable.Entity.Component.EventCallbackHandler<Update> ComponentUpdated { get; set; }
    /// <summary>
    /// Triggered whenever the local worker's authority over this component changes.
    /// </summary>
    global::Improbable.Entity.Component.EventCallbackHandler<global::Improbable.Worker.Authority> AuthorityChanged { get; set; }
  }

  /// <summary>
  /// Persistence.Updater is deprecated and will be removed in a future SpatialOS version.
  /// Please use Persistence.Writer.Send() instead (see the documentation for semantic differences).
  /// </summary>
  [global::System.Obsolete("Please use Persistence.Writer.Send() instead (see the documentation for semantic differences).")]
  public interface Updater
  {
    void FinishAndSend();
	void SendAuthorityLossImminentAcknowledgement();
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
    dispatcher.OnAddComponent<Persistence>((op) => {
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
            "Component Persistence added to entity " + op.EntityId + ", but it already exists." +
            "This error will be reported once only for each component."));
        loggedInvalidAddComponent = true;
      }
    });

    dispatcher.OnRemoveComponent<Persistence>((op) => {
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
            "Component Persistence removed from entity " + op.EntityId + ", but it does not exist." +
            "This error will be reported once only for each component."));
        loggedInvalidRemoveComponent = true;
      }
    });

    dispatcher.OnAuthorityChange<Persistence>((op) => {
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

    dispatcher.OnComponentUpdate<Persistence>((op) => {
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
  public class Impl : PersistenceWriter, Writer, Updater
  #pragma warning restore 0618
  {
    private readonly global::Improbable.Worker.Connection connection;
    private readonly global::Improbable.EntityId entityId;
    private readonly CommandReceiverImpl commandReceiver;

    private Data data;
    private global::Improbable.Worker.Authority authority = global::Improbable.Worker.Authority.NotAuthoritative;

    public uint ComponentId { get { return 55; } }

    public Impl(global::Improbable.Worker.Connection connection, global::Improbable.EntityId entityId, Data initialData)
    {
      this.connection = connection;
      this.entityId = entityId;
      this.commandReceiver = new CommandReceiverImpl();
      data = initialData.DeepCopy();

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

    public global::Improbable.PersistenceData Data
    {
      get { return data.Value; }
    }

    public void Send(Update update)
    {
      connection.SendComponentUpdate(entityId, update);
    }

	public void SendAuthorityLossImminentAcknowledgement()
	{
	  connection.SendAuthorityLossImminentAcknowledgement<Persistence>(entityId);
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


    private void TriggerCallbacks(Update update)
    {
    }

    // Implementation of deprecated interface.
    //----------------------------------------

    private Persistence.Update currentUpdate = null;

    #pragma warning disable 0618
    public Updater Update
    #pragma warning restore 0618
    {
      get
      {
        if (currentUpdate == null)
        {
          currentUpdate = new Persistence.Update();
        }
        else
        {
          global::Improbable.Worker.ClientError.LogClientException(new global::System.InvalidOperationException(
              "Update for component Persistence called with an update already in-flight! " +
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
    event global::System.Action<global::Improbable.Worker.Authority> PersistenceReader.AuthorityChanged
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

    private bool FinishAndSend_ResolveDiff(Update update)
    {
      bool isNoOp = true;
      return !isNoOp;
    }

    internal class CommandReceiverImpl : ICommandReceiver
    {
    }
  }
}
}