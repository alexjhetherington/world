// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========
using Improbable.Entity.Component;
using Improbable.Unity.CodeGeneration;
using Improbable.Unity.Core;
using Improbable.Unity.Entity;
using Improbable.Worker;
using UnityEngine;
using System;

namespace Improbable.Player
{
    public class SpatialOsClientConnectionComponent: SpatialOsComponentBase
    {
        /// <inheritdoc />
        public override uint ComponentId { get { return 1003; } }

        protected uint timeoutBeatsRemainingValue;
        /// <summary>
        ///     Returns the value of the field 'TimeoutBeatsRemaining'.
        /// </summary>
        public uint TimeoutBeatsRemaining { get { return timeoutBeatsRemainingValue; } }


        /// <inheritdoc />
        public override bool Init(ISpatialCommunicator communicator, IEntityObject entityObject)
        {
            if (!base.Init(communicator, entityObject))
            {
                return false;
            }

            DispatcherCallbackKeys.Add(
                communicator.RegisterCommandRequest<global::Improbable.Player.ClientConnection.Commands.Heartbeat>(OnHeartbeatCommandRequestDispatcherCallback));
            DispatcherCallbackKeys.Add(
                communicator.RegisterCommandRequest<global::Improbable.Player.ClientConnection.Commands.DisconnectClient>(OnDisconnectClientCommandRequestDispatcherCallback));
            return true;
        }

        /// <inheritdoc />
        public override void OnAddComponentPipelineOp(AddComponentPipelineOp op)
        {
            OnAddComponentDispatcherCallback(new AddComponentOp<global::Improbable.Player.ClientConnection> { EntityId = entityId, Data = new global::Improbable.Player.ClientConnection.Data(((global::Improbable.Player.ClientConnection.Impl)op.ComponentObject).Data) });
        }

        /// <inheritdoc />
        public override void OnComponentUpdatePipelineOp(UpdateComponentPipelineOp op)
        {
            OnComponentUpdateDispatcherCallback(new ComponentUpdateOp<global::Improbable.Player.ClientConnection> { EntityId = entityId, Update = (global::Improbable.Player.ClientConnection.Update)op.UpdateObject });
        }

        protected void OnAddComponentDispatcherCallback(AddComponentOp<global::Improbable.Player.ClientConnection> op)
        {
            if (op.EntityId != entityId)
            {
                return;
            }
            var update = op.Data.ToUpdate();
            OnComponentUpdateDispatcherCallback(update.Get());
        }

        /// <summary>
        ///     Send a component update.
        /// </summary>
        public virtual void SendComponentUpdate(global::Improbable.Player.ClientConnection.Update update)
        {
            if (Authority == global::Improbable.Worker.Authority.NotAuthoritative)
            {
                Debug.LogError(string.Format("Component {0}: Attempted to send a component update without write authority. The component update was discarded. Please make sure you only send component updates when component.Authority is Authoritative or AuthorityLossImminent.", this.GetType()));
                return;
            }
            communicator.SendComponentUpdate(entityId, update);
        }

		/// <summary>
		///     Notify the runtime that authority loss imminent was received and that the worker is now ready to lose authority.
		/// </summary>
		public virtual void SendAuthorityLossImminentAcknowledgement()
		{
		    communicator.SendAuthorityLossImminentAcknowledgement<global::Improbable.Player.ClientConnection>(entityId);
		}

        /// <summary>
        ///     The type of callback to listen for component updates.
        /// </summary>
        public delegate void OnComponentUpdateCallback(global::Improbable.Player.ClientConnection.Update update);

        protected System.Collections.Generic.List<OnComponentUpdateCallback> onComponentUpdateCallbacks;

        /// <summary>
        ///     Invoked when authority changes for this component.
        /// </summary>
        public event OnComponentUpdateCallback OnComponentUpdate
        {
            add
            {
                if (onComponentUpdateCallbacks == null)
                {
                    onComponentUpdateCallbacks = new System.Collections.Generic.List<OnComponentUpdateCallback>();
                }
                onComponentUpdateCallbacks.Add(value);
            }
            remove
            {
                if (onComponentUpdateCallbacks != null)
                {
                    onComponentUpdateCallbacks.Remove(value);
                }
            }
        }

        protected void OnComponentUpdateDispatcherCallback(
            global::Improbable.Worker.ComponentUpdateOp<global::Improbable.Player.ClientConnection> op)
        {
            if (op.EntityId != entityId)
            {
                return;
            }
            var update = op.Update.Get();
            OnComponentUpdateDispatcherCallback(update);
#if UNITY_EDITOR
            FinalizeComponentUpdateLog();
#endif
        }

        protected void OnComponentUpdateDispatcherCallback(global::Improbable.Player.ClientConnection.Update update) {
            if (update.timeoutBeatsRemaining.HasValue) {
                timeoutBeatsRemainingValue = update.timeoutBeatsRemaining.Value;
            }

            if (onComponentUpdateCallbacks != null)
            {
                for (var i = 0; i < onComponentUpdateCallbacks.Count; i++)
                {
                    try
                    {
                        onComponentUpdateCallbacks[i](update);
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
            }
            if (update.timeoutBeatsRemaining.HasValue) {
#if UNITY_EDITOR
                LogComponentUpdate("timeoutBeatsRemaining", timeoutBeatsRemainingValue);
#endif
                if (onTimeoutBeatsRemainingUpdateCallbacks != null)
                {
                    for (var i = 0; i < onTimeoutBeatsRemainingUpdateCallbacks.Count; i++)
                    {
                        try
                        {
                            onTimeoutBeatsRemainingUpdateCallbacks[i](update.timeoutBeatsRemaining.Value);
                        }
                        catch (Exception e)
                        {
                            Debug.LogException(e);
                        }
                    }
                }
            }

            if (!isComponentReady)
            {
                isComponentReady = true;
                if (onComponentReadyCallbacks != null)
                {
                    for (var i = 0; i < onComponentReadyCallbacks.Count; i++)
                    {
                        try
                        {
                            onComponentReadyCallbacks[i]();
                        }
                        catch (Exception e)
                        {
                            Debug.LogException(e);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     The type of callback to listen for updates to field 'TimeoutBeatsRemaining'.
        /// </summary>
        public delegate void OnTimeoutBeatsRemainingUpdateCallback(uint newTimeoutBeatsRemaining);

        protected System.Collections.Generic.List<OnTimeoutBeatsRemainingUpdateCallback> onTimeoutBeatsRemainingUpdateCallbacks;

        /// <summary>
        ///     Invoked when the field 'TimeoutBeatsRemaining' is updated.
        /// </summary>
        public event OnTimeoutBeatsRemainingUpdateCallback OnTimeoutBeatsRemainingUpdate
        {
            add
            {
                if (onTimeoutBeatsRemainingUpdateCallbacks == null)
                {
                    onTimeoutBeatsRemainingUpdateCallbacks = new System.Collections.Generic.List<OnTimeoutBeatsRemainingUpdateCallback>();
                }
                onTimeoutBeatsRemainingUpdateCallbacks.Add(value);
            }
            remove
            {
                if (onTimeoutBeatsRemainingUpdateCallbacks != null)
                {
                    onTimeoutBeatsRemainingUpdateCallbacks.Remove(value);
                }
            }
        }


        private HeartbeatCommandRequestCallbackWrapper heartbeatCommandRequestCallbackWrapper;

        /// <summary>
        ///     Invoked when a 'Heartbeat' request is received.
        /// </summary>
        public HeartbeatCommandRequestCallbackWrapper OnHeartbeatCommandRequest
        {
            get
            {
                if (heartbeatCommandRequestCallbackWrapper == null)
                {
                    heartbeatCommandRequestCallbackWrapper = new HeartbeatCommandRequestCallbackWrapper();
                }
                return heartbeatCommandRequestCallbackWrapper;
            }
            set { heartbeatCommandRequestCallbackWrapper = value; }
        }
        /// <summary>
        ///     The type of callback to pass to listen for incoming 'Heartbeat' command requests and respond asynchronously.
        /// </summary>
        public delegate void OnHeartbeatCommandRequestAsyncCallback(HeartbeatCommandResponseHandle responseHandle);
        /// <summary>
        ///     The type of callback to pass to listen for incoming 'Heartbeat' command requests and respond synchronously.
        /// </summary>
        public delegate global::Improbable.Player.HeartbeatResponse OnHeartbeatCommandRequestSyncCallback(global::Improbable.Player.HeartbeatRequest request, ICommandCallerInfo commandCallerInfo);
        /// <summary>
        ///     Wraps a synchronous or asynchronous callback to be invoked when a command response is received for the Heartbeat command.
        /// </summary>
        public class HeartbeatCommandRequestCallbackWrapper
        {
            private OnHeartbeatCommandRequestSyncCallback syncCallback;
            private OnHeartbeatCommandRequestAsyncCallback asyncCallback;
            /// <summary>
            ///     Registers a synchronous callback to be invoked immediately upon receiving a command request.
            /// </summary>
            public void RegisterResponse(OnHeartbeatCommandRequestSyncCallback callback)
            {
                if (IsCallbackRegistered())
                {
                    ThrowCallbackAlreadyRegisteredException();
                }
                syncCallback = callback;
            }
            /// <summary>
            ///     Registers an asynchronous callback to be invoked with a response handle upon receiving a command request.
            /// </summary>
            public void RegisterAsyncResponse(OnHeartbeatCommandRequestAsyncCallback callback)
            {
                if (IsCallbackRegistered())
                {
                    ThrowCallbackAlreadyRegisteredException();
                }
                asyncCallback = callback;
            }
            /// <summary>
            ///     Deregisters a previously registered command response.
            /// </summary>
            public void DeregisterResponse()
            {
                if (!IsCallbackRegistered())
                {
                    throw new InvalidOperationException("Attempted to deregister a command response when none is registered for command Heartbeat");
                }
                syncCallback = null;
                asyncCallback = null;
            }
            /// <summary>
            ///     Returns whether or not a callback is currently registered.
            /// </summary>
            public bool IsCallbackRegistered()
            {
                return syncCallback != null || asyncCallback != null;
            }
            private void ThrowCallbackAlreadyRegisteredException()
            {
                throw new InvalidOperationException("Attempted to register a command response when one has already been registered for command Heartbeat.");
            }
            /// <summary>
            ///     Invokes the registered callback. This is an implementation detail; it should not be called from user code.
            /// </summary>
            public void InvokeCallback(HeartbeatCommandResponseHandle responseHandle)
            {
                if (syncCallback != null)
                {
                    responseHandle.Respond(syncCallback(responseHandle.Request, responseHandle.CallerInfo));
                }
                if (asyncCallback != null)
                {
                    asyncCallback(responseHandle);
                }
            }
        }

        /// <summary>
        ///     A response handle for the 'Heartbeat' command.
        /// </summary>
        public class HeartbeatCommandResponseHandle
        {
            private readonly
                global::Improbable.Worker.CommandRequestOp<global::Improbable.Player.ClientConnection.Commands.Heartbeat>
                commandRequestOp;
            private readonly CommandCallerInfo commandCallerInfo;
            private readonly ISpatialCommunicator communicator;

            /// <summary>
            ///     Creates a new response handle. This is an implementation detail; it should not be called from user code.
            /// </summary>
            public HeartbeatCommandResponseHandle(
                global::Improbable.Worker.CommandRequestOp<global::Improbable.Player.ClientConnection.Commands.Heartbeat>
                    commandRequestOp, ISpatialCommunicator communicator)
            {
                this.commandRequestOp = commandRequestOp;
                this.commandCallerInfo = new CommandCallerInfo(commandRequestOp.CallerWorkerId, commandRequestOp.CallerAttributeSet);
                this.communicator = communicator;
            }

            /// <summary>
            ///     Returns the request object.
            /// </summary>
            public global::Improbable.Player.HeartbeatRequest Request { get { return commandRequestOp.Request.Get().Value; } }

            /// <summary>
            /// Metadata for this command request.
            /// </summary>
            public ICommandCallerInfo CallerInfo
            {
                get { return commandCallerInfo; }
            }

            /// <summary>
            ///     Sends the command response.
            /// </summary>
            public void Respond(global::Improbable.Player.HeartbeatResponse response)
            {
                var commandResponse = new global::Improbable.Player.ClientConnection.Commands.Heartbeat.Response(response);
                communicator.SendCommandResponse(commandRequestOp.RequestId, commandResponse);
            }
        }

        protected void OnHeartbeatCommandRequestDispatcherCallback(
            global::Improbable.Worker.CommandRequestOp<global::Improbable.Player.ClientConnection.Commands.Heartbeat> op)
        {
            if (op.EntityId != entityId || heartbeatCommandRequestCallbackWrapper == null || !heartbeatCommandRequestCallbackWrapper.IsCallbackRegistered())
            {
                return;
            }
            var responseHandle = new HeartbeatCommandResponseHandle(op, communicator);
            heartbeatCommandRequestCallbackWrapper.InvokeCallback(responseHandle);

#if UNITY_EDITOR
            LogCommandRequest(DateTime.Now, "Heartbeat", op.Request.Get().Value);
#endif
        }


        private DisconnectClientCommandRequestCallbackWrapper disconnectClientCommandRequestCallbackWrapper;

        /// <summary>
        ///     Invoked when a 'DisconnectClient' request is received.
        /// </summary>
        public DisconnectClientCommandRequestCallbackWrapper OnDisconnectClientCommandRequest
        {
            get
            {
                if (disconnectClientCommandRequestCallbackWrapper == null)
                {
                    disconnectClientCommandRequestCallbackWrapper = new DisconnectClientCommandRequestCallbackWrapper();
                }
                return disconnectClientCommandRequestCallbackWrapper;
            }
            set { disconnectClientCommandRequestCallbackWrapper = value; }
        }
        /// <summary>
        ///     The type of callback to pass to listen for incoming 'DisconnectClient' command requests and respond asynchronously.
        /// </summary>
        public delegate void OnDisconnectClientCommandRequestAsyncCallback(DisconnectClientCommandResponseHandle responseHandle);
        /// <summary>
        ///     The type of callback to pass to listen for incoming 'DisconnectClient' command requests and respond synchronously.
        /// </summary>
        public delegate global::Improbable.Player.ClientDisconnectResponse OnDisconnectClientCommandRequestSyncCallback(global::Improbable.Player.ClientDisconnectRequest request, ICommandCallerInfo commandCallerInfo);
        /// <summary>
        ///     Wraps a synchronous or asynchronous callback to be invoked when a command response is received for the DisconnectClient command.
        /// </summary>
        public class DisconnectClientCommandRequestCallbackWrapper
        {
            private OnDisconnectClientCommandRequestSyncCallback syncCallback;
            private OnDisconnectClientCommandRequestAsyncCallback asyncCallback;
            /// <summary>
            ///     Registers a synchronous callback to be invoked immediately upon receiving a command request.
            /// </summary>
            public void RegisterResponse(OnDisconnectClientCommandRequestSyncCallback callback)
            {
                if (IsCallbackRegistered())
                {
                    ThrowCallbackAlreadyRegisteredException();
                }
                syncCallback = callback;
            }
            /// <summary>
            ///     Registers an asynchronous callback to be invoked with a response handle upon receiving a command request.
            /// </summary>
            public void RegisterAsyncResponse(OnDisconnectClientCommandRequestAsyncCallback callback)
            {
                if (IsCallbackRegistered())
                {
                    ThrowCallbackAlreadyRegisteredException();
                }
                asyncCallback = callback;
            }
            /// <summary>
            ///     Deregisters a previously registered command response.
            /// </summary>
            public void DeregisterResponse()
            {
                if (!IsCallbackRegistered())
                {
                    throw new InvalidOperationException("Attempted to deregister a command response when none is registered for command DisconnectClient");
                }
                syncCallback = null;
                asyncCallback = null;
            }
            /// <summary>
            ///     Returns whether or not a callback is currently registered.
            /// </summary>
            public bool IsCallbackRegistered()
            {
                return syncCallback != null || asyncCallback != null;
            }
            private void ThrowCallbackAlreadyRegisteredException()
            {
                throw new InvalidOperationException("Attempted to register a command response when one has already been registered for command DisconnectClient.");
            }
            /// <summary>
            ///     Invokes the registered callback. This is an implementation detail; it should not be called from user code.
            /// </summary>
            public void InvokeCallback(DisconnectClientCommandResponseHandle responseHandle)
            {
                if (syncCallback != null)
                {
                    responseHandle.Respond(syncCallback(responseHandle.Request, responseHandle.CallerInfo));
                }
                if (asyncCallback != null)
                {
                    asyncCallback(responseHandle);
                }
            }
        }

        /// <summary>
        ///     A response handle for the 'DisconnectClient' command.
        /// </summary>
        public class DisconnectClientCommandResponseHandle
        {
            private readonly
                global::Improbable.Worker.CommandRequestOp<global::Improbable.Player.ClientConnection.Commands.DisconnectClient>
                commandRequestOp;
            private readonly CommandCallerInfo commandCallerInfo;
            private readonly ISpatialCommunicator communicator;

            /// <summary>
            ///     Creates a new response handle. This is an implementation detail; it should not be called from user code.
            /// </summary>
            public DisconnectClientCommandResponseHandle(
                global::Improbable.Worker.CommandRequestOp<global::Improbable.Player.ClientConnection.Commands.DisconnectClient>
                    commandRequestOp, ISpatialCommunicator communicator)
            {
                this.commandRequestOp = commandRequestOp;
                this.commandCallerInfo = new CommandCallerInfo(commandRequestOp.CallerWorkerId, commandRequestOp.CallerAttributeSet);
                this.communicator = communicator;
            }

            /// <summary>
            ///     Returns the request object.
            /// </summary>
            public global::Improbable.Player.ClientDisconnectRequest Request { get { return commandRequestOp.Request.Get().Value; } }

            /// <summary>
            /// Metadata for this command request.
            /// </summary>
            public ICommandCallerInfo CallerInfo
            {
                get { return commandCallerInfo; }
            }

            /// <summary>
            ///     Sends the command response.
            /// </summary>
            public void Respond(global::Improbable.Player.ClientDisconnectResponse response)
            {
                var commandResponse = new global::Improbable.Player.ClientConnection.Commands.DisconnectClient.Response(response);
                communicator.SendCommandResponse(commandRequestOp.RequestId, commandResponse);
            }
        }

        protected void OnDisconnectClientCommandRequestDispatcherCallback(
            global::Improbable.Worker.CommandRequestOp<global::Improbable.Player.ClientConnection.Commands.DisconnectClient> op)
        {
            if (op.EntityId != entityId || disconnectClientCommandRequestCallbackWrapper == null || !disconnectClientCommandRequestCallbackWrapper.IsCallbackRegistered())
            {
                return;
            }
            var responseHandle = new DisconnectClientCommandResponseHandle(op, communicator);
            disconnectClientCommandRequestCallbackWrapper.InvokeCallback(responseHandle);

#if UNITY_EDITOR
            LogCommandRequest(DateTime.Now, "DisconnectClient", op.Request.Get().Value);
#endif
        }

    }
}
