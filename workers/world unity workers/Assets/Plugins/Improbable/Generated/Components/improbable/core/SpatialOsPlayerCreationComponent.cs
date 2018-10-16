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

namespace Improbable.Core
{
    public class SpatialOsPlayerCreationComponent: SpatialOsComponentBase
    {
        /// <inheritdoc />
        public override uint ComponentId { get { return 1001; } }


        /// <inheritdoc />
        public override bool Init(ISpatialCommunicator communicator, IEntityObject entityObject)
        {
            if (!base.Init(communicator, entityObject))
            {
                return false;
            }

            DispatcherCallbackKeys.Add(
                communicator.RegisterCommandRequest<global::Improbable.Core.PlayerCreation.Commands.CreatePlayer>(OnCreatePlayerCommandRequestDispatcherCallback));
            return true;
        }

        /// <inheritdoc />
        public override void OnAddComponentPipelineOp(AddComponentPipelineOp op)
        {
            OnAddComponentDispatcherCallback(new AddComponentOp<global::Improbable.Core.PlayerCreation> { EntityId = entityId, Data = new global::Improbable.Core.PlayerCreation.Data(((global::Improbable.Core.PlayerCreation.Impl)op.ComponentObject).Data) });
        }

        /// <inheritdoc />
        public override void OnComponentUpdatePipelineOp(UpdateComponentPipelineOp op)
        {
            OnComponentUpdateDispatcherCallback(new ComponentUpdateOp<global::Improbable.Core.PlayerCreation> { EntityId = entityId, Update = (global::Improbable.Core.PlayerCreation.Update)op.UpdateObject });
        }

        protected void OnAddComponentDispatcherCallback(AddComponentOp<global::Improbable.Core.PlayerCreation> op)
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
        public virtual void SendComponentUpdate(global::Improbable.Core.PlayerCreation.Update update)
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
		    communicator.SendAuthorityLossImminentAcknowledgement<global::Improbable.Core.PlayerCreation>(entityId);
		}

        /// <summary>
        ///     The type of callback to listen for component updates.
        /// </summary>
        public delegate void OnComponentUpdateCallback(global::Improbable.Core.PlayerCreation.Update update);

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
            global::Improbable.Worker.ComponentUpdateOp<global::Improbable.Core.PlayerCreation> op)
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

        protected void OnComponentUpdateDispatcherCallback(global::Improbable.Core.PlayerCreation.Update update) {

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


        private CreatePlayerCommandRequestCallbackWrapper createPlayerCommandRequestCallbackWrapper;

        /// <summary>
        ///     Invoked when a 'CreatePlayer' request is received.
        /// </summary>
        public CreatePlayerCommandRequestCallbackWrapper OnCreatePlayerCommandRequest
        {
            get
            {
                if (createPlayerCommandRequestCallbackWrapper == null)
                {
                    createPlayerCommandRequestCallbackWrapper = new CreatePlayerCommandRequestCallbackWrapper();
                }
                return createPlayerCommandRequestCallbackWrapper;
            }
            set { createPlayerCommandRequestCallbackWrapper = value; }
        }
        /// <summary>
        ///     The type of callback to pass to listen for incoming 'CreatePlayer' command requests and respond asynchronously.
        /// </summary>
        public delegate void OnCreatePlayerCommandRequestAsyncCallback(CreatePlayerCommandResponseHandle responseHandle);
        /// <summary>
        ///     The type of callback to pass to listen for incoming 'CreatePlayer' command requests and respond synchronously.
        /// </summary>
        public delegate global::Improbable.Core.CreatePlayerResponse OnCreatePlayerCommandRequestSyncCallback(global::Improbable.Core.CreatePlayerRequest request, ICommandCallerInfo commandCallerInfo);
        /// <summary>
        ///     Wraps a synchronous or asynchronous callback to be invoked when a command response is received for the CreatePlayer command.
        /// </summary>
        public class CreatePlayerCommandRequestCallbackWrapper
        {
            private OnCreatePlayerCommandRequestSyncCallback syncCallback;
            private OnCreatePlayerCommandRequestAsyncCallback asyncCallback;
            /// <summary>
            ///     Registers a synchronous callback to be invoked immediately upon receiving a command request.
            /// </summary>
            public void RegisterResponse(OnCreatePlayerCommandRequestSyncCallback callback)
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
            public void RegisterAsyncResponse(OnCreatePlayerCommandRequestAsyncCallback callback)
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
                    throw new InvalidOperationException("Attempted to deregister a command response when none is registered for command CreatePlayer");
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
                throw new InvalidOperationException("Attempted to register a command response when one has already been registered for command CreatePlayer.");
            }
            /// <summary>
            ///     Invokes the registered callback. This is an implementation detail; it should not be called from user code.
            /// </summary>
            public void InvokeCallback(CreatePlayerCommandResponseHandle responseHandle)
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
        ///     A response handle for the 'CreatePlayer' command.
        /// </summary>
        public class CreatePlayerCommandResponseHandle
        {
            private readonly
                global::Improbable.Worker.CommandRequestOp<global::Improbable.Core.PlayerCreation.Commands.CreatePlayer>
                commandRequestOp;
            private readonly CommandCallerInfo commandCallerInfo;
            private readonly ISpatialCommunicator communicator;

            /// <summary>
            ///     Creates a new response handle. This is an implementation detail; it should not be called from user code.
            /// </summary>
            public CreatePlayerCommandResponseHandle(
                global::Improbable.Worker.CommandRequestOp<global::Improbable.Core.PlayerCreation.Commands.CreatePlayer>
                    commandRequestOp, ISpatialCommunicator communicator)
            {
                this.commandRequestOp = commandRequestOp;
                this.commandCallerInfo = new CommandCallerInfo(commandRequestOp.CallerWorkerId, commandRequestOp.CallerAttributeSet);
                this.communicator = communicator;
            }

            /// <summary>
            ///     Returns the request object.
            /// </summary>
            public global::Improbable.Core.CreatePlayerRequest Request { get { return commandRequestOp.Request.Get().Value; } }

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
            public void Respond(global::Improbable.Core.CreatePlayerResponse response)
            {
                var commandResponse = new global::Improbable.Core.PlayerCreation.Commands.CreatePlayer.Response(response);
                communicator.SendCommandResponse(commandRequestOp.RequestId, commandResponse);
            }
        }

        protected void OnCreatePlayerCommandRequestDispatcherCallback(
            global::Improbable.Worker.CommandRequestOp<global::Improbable.Core.PlayerCreation.Commands.CreatePlayer> op)
        {
            if (op.EntityId != entityId || createPlayerCommandRequestCallbackWrapper == null || !createPlayerCommandRequestCallbackWrapper.IsCallbackRegistered())
            {
                return;
            }
            var responseHandle = new CreatePlayerCommandResponseHandle(op, communicator);
            createPlayerCommandRequestCallbackWrapper.InvokeCallback(responseHandle);

#if UNITY_EDITOR
            LogCommandRequest(DateTime.Now, "CreatePlayer", op.Request.Get().Value);
#endif
        }

    }
}
