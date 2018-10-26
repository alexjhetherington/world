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

namespace Improbable.Character
{
    public class SpatialOsCollisionsCreatedComponent: SpatialOsComponentBase
    {
        /// <inheritdoc />
        public override uint ComponentId { get { return 1009; } }

        protected global::Improbable.Collections.List<global::Improbable.Character.NewCollision> newCollisionsValue;
        /// <summary>
        ///     Returns the value of the field 'NewCollisions'.
        /// </summary>
        public global::Improbable.Collections.List<global::Improbable.Character.NewCollision> NewCollisions { get { return newCollisionsValue; } }


        /// <inheritdoc />
        public override bool Init(ISpatialCommunicator communicator, IEntityObject entityObject)
        {
            if (!base.Init(communicator, entityObject))
            {
                return false;
            }

            DispatcherCallbackKeys.Add(
                communicator.RegisterCommandRequest<global::Improbable.Character.CollisionsCreated.Commands.ServerCollisionCreated>(OnServerCollisionCreatedCommandRequestDispatcherCallback));
            DispatcherCallbackKeys.Add(
                communicator.RegisterCommandRequest<global::Improbable.Character.CollisionsCreated.Commands.ClientCollisionCreated>(OnClientCollisionCreatedCommandRequestDispatcherCallback));
            return true;
        }

        /// <inheritdoc />
        public override void OnAddComponentPipelineOp(AddComponentPipelineOp op)
        {
            OnAddComponentDispatcherCallback(new AddComponentOp<global::Improbable.Character.CollisionsCreated> { EntityId = entityId, Data = new global::Improbable.Character.CollisionsCreated.Data(((global::Improbable.Character.CollisionsCreated.Impl)op.ComponentObject).Data) });
        }

        /// <inheritdoc />
        public override void OnComponentUpdatePipelineOp(UpdateComponentPipelineOp op)
        {
            OnComponentUpdateDispatcherCallback(new ComponentUpdateOp<global::Improbable.Character.CollisionsCreated> { EntityId = entityId, Update = (global::Improbable.Character.CollisionsCreated.Update)op.UpdateObject });
        }

        protected void OnAddComponentDispatcherCallback(AddComponentOp<global::Improbable.Character.CollisionsCreated> op)
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
        public virtual void SendComponentUpdate(global::Improbable.Character.CollisionsCreated.Update update)
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
		    communicator.SendAuthorityLossImminentAcknowledgement<global::Improbable.Character.CollisionsCreated>(entityId);
		}

        /// <summary>
        ///     The type of callback to listen for component updates.
        /// </summary>
        public delegate void OnComponentUpdateCallback(global::Improbable.Character.CollisionsCreated.Update update);

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
            global::Improbable.Worker.ComponentUpdateOp<global::Improbable.Character.CollisionsCreated> op)
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

        protected void OnComponentUpdateDispatcherCallback(global::Improbable.Character.CollisionsCreated.Update update) {
            if (update.newCollisions.HasValue) {
                newCollisionsValue = update.newCollisions.Value;
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
            if (update.newCollisions.HasValue) {
#if UNITY_EDITOR
                LogComponentUpdate("newCollisions", newCollisionsValue);
#endif
                if (onNewCollisionsUpdateCallbacks != null)
                {
                    for (var i = 0; i < onNewCollisionsUpdateCallbacks.Count; i++)
                    {
                        try
                        {
                            onNewCollisionsUpdateCallbacks[i](update.newCollisions.Value);
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
        ///     The type of callback to listen for updates to field 'NewCollisions'.
        /// </summary>
        public delegate void OnNewCollisionsUpdateCallback(global::Improbable.Collections.List<global::Improbable.Character.NewCollision> newNewCollisions);

        protected System.Collections.Generic.List<OnNewCollisionsUpdateCallback> onNewCollisionsUpdateCallbacks;

        /// <summary>
        ///     Invoked when the field 'NewCollisions' is updated.
        /// </summary>
        public event OnNewCollisionsUpdateCallback OnNewCollisionsUpdate
        {
            add
            {
                if (onNewCollisionsUpdateCallbacks == null)
                {
                    onNewCollisionsUpdateCallbacks = new System.Collections.Generic.List<OnNewCollisionsUpdateCallback>();
                }
                onNewCollisionsUpdateCallbacks.Add(value);
            }
            remove
            {
                if (onNewCollisionsUpdateCallbacks != null)
                {
                    onNewCollisionsUpdateCallbacks.Remove(value);
                }
            }
        }


        private ServerCollisionCreatedCommandRequestCallbackWrapper serverCollisionCreatedCommandRequestCallbackWrapper;

        /// <summary>
        ///     Invoked when a 'ServerCollisionCreated' request is received.
        /// </summary>
        public ServerCollisionCreatedCommandRequestCallbackWrapper OnServerCollisionCreatedCommandRequest
        {
            get
            {
                if (serverCollisionCreatedCommandRequestCallbackWrapper == null)
                {
                    serverCollisionCreatedCommandRequestCallbackWrapper = new ServerCollisionCreatedCommandRequestCallbackWrapper();
                }
                return serverCollisionCreatedCommandRequestCallbackWrapper;
            }
            set { serverCollisionCreatedCommandRequestCallbackWrapper = value; }
        }
        /// <summary>
        ///     The type of callback to pass to listen for incoming 'ServerCollisionCreated' command requests and respond asynchronously.
        /// </summary>
        public delegate void OnServerCollisionCreatedCommandRequestAsyncCallback(ServerCollisionCreatedCommandResponseHandle responseHandle);
        /// <summary>
        ///     The type of callback to pass to listen for incoming 'ServerCollisionCreated' command requests and respond synchronously.
        /// </summary>
        public delegate global::Improbable.Character.ServerCollisionCreatedResponse OnServerCollisionCreatedCommandRequestSyncCallback(global::Improbable.Character.ServerCollisionCreatedRequest request, ICommandCallerInfo commandCallerInfo);
        /// <summary>
        ///     Wraps a synchronous or asynchronous callback to be invoked when a command response is received for the ServerCollisionCreated command.
        /// </summary>
        public class ServerCollisionCreatedCommandRequestCallbackWrapper
        {
            private OnServerCollisionCreatedCommandRequestSyncCallback syncCallback;
            private OnServerCollisionCreatedCommandRequestAsyncCallback asyncCallback;
            /// <summary>
            ///     Registers a synchronous callback to be invoked immediately upon receiving a command request.
            /// </summary>
            public void RegisterResponse(OnServerCollisionCreatedCommandRequestSyncCallback callback)
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
            public void RegisterAsyncResponse(OnServerCollisionCreatedCommandRequestAsyncCallback callback)
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
                    throw new InvalidOperationException("Attempted to deregister a command response when none is registered for command ServerCollisionCreated");
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
                throw new InvalidOperationException("Attempted to register a command response when one has already been registered for command ServerCollisionCreated.");
            }
            /// <summary>
            ///     Invokes the registered callback. This is an implementation detail; it should not be called from user code.
            /// </summary>
            public void InvokeCallback(ServerCollisionCreatedCommandResponseHandle responseHandle)
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
        ///     A response handle for the 'ServerCollisionCreated' command.
        /// </summary>
        public class ServerCollisionCreatedCommandResponseHandle
        {
            private readonly
                global::Improbable.Worker.CommandRequestOp<global::Improbable.Character.CollisionsCreated.Commands.ServerCollisionCreated>
                commandRequestOp;
            private readonly CommandCallerInfo commandCallerInfo;
            private readonly ISpatialCommunicator communicator;

            /// <summary>
            ///     Creates a new response handle. This is an implementation detail; it should not be called from user code.
            /// </summary>
            public ServerCollisionCreatedCommandResponseHandle(
                global::Improbable.Worker.CommandRequestOp<global::Improbable.Character.CollisionsCreated.Commands.ServerCollisionCreated>
                    commandRequestOp, ISpatialCommunicator communicator)
            {
                this.commandRequestOp = commandRequestOp;
                this.commandCallerInfo = new CommandCallerInfo(commandRequestOp.CallerWorkerId, commandRequestOp.CallerAttributeSet);
                this.communicator = communicator;
            }

            /// <summary>
            ///     Returns the request object.
            /// </summary>
            public global::Improbable.Character.ServerCollisionCreatedRequest Request { get { return commandRequestOp.Request.Get().Value; } }

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
            public void Respond(global::Improbable.Character.ServerCollisionCreatedResponse response)
            {
                var commandResponse = new global::Improbable.Character.CollisionsCreated.Commands.ServerCollisionCreated.Response(response);
                communicator.SendCommandResponse(commandRequestOp.RequestId, commandResponse);
            }
        }

        protected void OnServerCollisionCreatedCommandRequestDispatcherCallback(
            global::Improbable.Worker.CommandRequestOp<global::Improbable.Character.CollisionsCreated.Commands.ServerCollisionCreated> op)
        {
            if (op.EntityId != entityId || serverCollisionCreatedCommandRequestCallbackWrapper == null || !serverCollisionCreatedCommandRequestCallbackWrapper.IsCallbackRegistered())
            {
                return;
            }
            var responseHandle = new ServerCollisionCreatedCommandResponseHandle(op, communicator);
            serverCollisionCreatedCommandRequestCallbackWrapper.InvokeCallback(responseHandle);

#if UNITY_EDITOR
            LogCommandRequest(DateTime.Now, "ServerCollisionCreated", op.Request.Get().Value);
#endif
        }


        private ClientCollisionCreatedCommandRequestCallbackWrapper clientCollisionCreatedCommandRequestCallbackWrapper;

        /// <summary>
        ///     Invoked when a 'ClientCollisionCreated' request is received.
        /// </summary>
        public ClientCollisionCreatedCommandRequestCallbackWrapper OnClientCollisionCreatedCommandRequest
        {
            get
            {
                if (clientCollisionCreatedCommandRequestCallbackWrapper == null)
                {
                    clientCollisionCreatedCommandRequestCallbackWrapper = new ClientCollisionCreatedCommandRequestCallbackWrapper();
                }
                return clientCollisionCreatedCommandRequestCallbackWrapper;
            }
            set { clientCollisionCreatedCommandRequestCallbackWrapper = value; }
        }
        /// <summary>
        ///     The type of callback to pass to listen for incoming 'ClientCollisionCreated' command requests and respond asynchronously.
        /// </summary>
        public delegate void OnClientCollisionCreatedCommandRequestAsyncCallback(ClientCollisionCreatedCommandResponseHandle responseHandle);
        /// <summary>
        ///     The type of callback to pass to listen for incoming 'ClientCollisionCreated' command requests and respond synchronously.
        /// </summary>
        public delegate global::Improbable.Character.ClientCollisionCreatedResponse OnClientCollisionCreatedCommandRequestSyncCallback(global::Improbable.Character.ClientCollisionCreatedRequest request, ICommandCallerInfo commandCallerInfo);
        /// <summary>
        ///     Wraps a synchronous or asynchronous callback to be invoked when a command response is received for the ClientCollisionCreated command.
        /// </summary>
        public class ClientCollisionCreatedCommandRequestCallbackWrapper
        {
            private OnClientCollisionCreatedCommandRequestSyncCallback syncCallback;
            private OnClientCollisionCreatedCommandRequestAsyncCallback asyncCallback;
            /// <summary>
            ///     Registers a synchronous callback to be invoked immediately upon receiving a command request.
            /// </summary>
            public void RegisterResponse(OnClientCollisionCreatedCommandRequestSyncCallback callback)
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
            public void RegisterAsyncResponse(OnClientCollisionCreatedCommandRequestAsyncCallback callback)
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
                    throw new InvalidOperationException("Attempted to deregister a command response when none is registered for command ClientCollisionCreated");
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
                throw new InvalidOperationException("Attempted to register a command response when one has already been registered for command ClientCollisionCreated.");
            }
            /// <summary>
            ///     Invokes the registered callback. This is an implementation detail; it should not be called from user code.
            /// </summary>
            public void InvokeCallback(ClientCollisionCreatedCommandResponseHandle responseHandle)
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
        ///     A response handle for the 'ClientCollisionCreated' command.
        /// </summary>
        public class ClientCollisionCreatedCommandResponseHandle
        {
            private readonly
                global::Improbable.Worker.CommandRequestOp<global::Improbable.Character.CollisionsCreated.Commands.ClientCollisionCreated>
                commandRequestOp;
            private readonly CommandCallerInfo commandCallerInfo;
            private readonly ISpatialCommunicator communicator;

            /// <summary>
            ///     Creates a new response handle. This is an implementation detail; it should not be called from user code.
            /// </summary>
            public ClientCollisionCreatedCommandResponseHandle(
                global::Improbable.Worker.CommandRequestOp<global::Improbable.Character.CollisionsCreated.Commands.ClientCollisionCreated>
                    commandRequestOp, ISpatialCommunicator communicator)
            {
                this.commandRequestOp = commandRequestOp;
                this.commandCallerInfo = new CommandCallerInfo(commandRequestOp.CallerWorkerId, commandRequestOp.CallerAttributeSet);
                this.communicator = communicator;
            }

            /// <summary>
            ///     Returns the request object.
            /// </summary>
            public global::Improbable.Character.ClientCollisionCreatedRequest Request { get { return commandRequestOp.Request.Get().Value; } }

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
            public void Respond(global::Improbable.Character.ClientCollisionCreatedResponse response)
            {
                var commandResponse = new global::Improbable.Character.CollisionsCreated.Commands.ClientCollisionCreated.Response(response);
                communicator.SendCommandResponse(commandRequestOp.RequestId, commandResponse);
            }
        }

        protected void OnClientCollisionCreatedCommandRequestDispatcherCallback(
            global::Improbable.Worker.CommandRequestOp<global::Improbable.Character.CollisionsCreated.Commands.ClientCollisionCreated> op)
        {
            if (op.EntityId != entityId || clientCollisionCreatedCommandRequestCallbackWrapper == null || !clientCollisionCreatedCommandRequestCallbackWrapper.IsCallbackRegistered())
            {
                return;
            }
            var responseHandle = new ClientCollisionCreatedCommandResponseHandle(op, communicator);
            clientCollisionCreatedCommandRequestCallbackWrapper.InvokeCallback(responseHandle);

#if UNITY_EDITOR
            LogCommandRequest(DateTime.Now, "ClientCollisionCreated", op.Request.Get().Value);
#endif
        }

    }
}
