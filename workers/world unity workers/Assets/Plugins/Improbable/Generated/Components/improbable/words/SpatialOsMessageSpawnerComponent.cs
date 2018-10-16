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

namespace Improbable.Words
{
    public class SpatialOsMessageSpawnerComponent: SpatialOsComponentBase
    {
        /// <inheritdoc />
        public override uint ComponentId { get { return 1008; } }


        /// <inheritdoc />
        public override bool Init(ISpatialCommunicator communicator, IEntityObject entityObject)
        {
            if (!base.Init(communicator, entityObject))
            {
                return false;
            }

            DispatcherCallbackKeys.Add(
                communicator.RegisterCommandRequest<global::Improbable.Words.MessageSpawner.Commands.Spawn>(OnSpawnCommandRequestDispatcherCallback));
            return true;
        }

        /// <inheritdoc />
        public override void OnAddComponentPipelineOp(AddComponentPipelineOp op)
        {
            OnAddComponentDispatcherCallback(new AddComponentOp<global::Improbable.Words.MessageSpawner> { EntityId = entityId, Data = new global::Improbable.Words.MessageSpawner.Data(((global::Improbable.Words.MessageSpawner.Impl)op.ComponentObject).Data) });
        }

        /// <inheritdoc />
        public override void OnComponentUpdatePipelineOp(UpdateComponentPipelineOp op)
        {
            OnComponentUpdateDispatcherCallback(new ComponentUpdateOp<global::Improbable.Words.MessageSpawner> { EntityId = entityId, Update = (global::Improbable.Words.MessageSpawner.Update)op.UpdateObject });
        }

        protected void OnAddComponentDispatcherCallback(AddComponentOp<global::Improbable.Words.MessageSpawner> op)
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
        public virtual void SendComponentUpdate(global::Improbable.Words.MessageSpawner.Update update)
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
		    communicator.SendAuthorityLossImminentAcknowledgement<global::Improbable.Words.MessageSpawner>(entityId);
		}

        /// <summary>
        ///     The type of callback to listen for component updates.
        /// </summary>
        public delegate void OnComponentUpdateCallback(global::Improbable.Words.MessageSpawner.Update update);

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
            global::Improbable.Worker.ComponentUpdateOp<global::Improbable.Words.MessageSpawner> op)
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

        protected void OnComponentUpdateDispatcherCallback(global::Improbable.Words.MessageSpawner.Update update) {

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


        private SpawnCommandRequestCallbackWrapper spawnCommandRequestCallbackWrapper;

        /// <summary>
        ///     Invoked when a 'Spawn' request is received.
        /// </summary>
        public SpawnCommandRequestCallbackWrapper OnSpawnCommandRequest
        {
            get
            {
                if (spawnCommandRequestCallbackWrapper == null)
                {
                    spawnCommandRequestCallbackWrapper = new SpawnCommandRequestCallbackWrapper();
                }
                return spawnCommandRequestCallbackWrapper;
            }
            set { spawnCommandRequestCallbackWrapper = value; }
        }
        /// <summary>
        ///     The type of callback to pass to listen for incoming 'Spawn' command requests and respond asynchronously.
        /// </summary>
        public delegate void OnSpawnCommandRequestAsyncCallback(SpawnCommandResponseHandle responseHandle);
        /// <summary>
        ///     The type of callback to pass to listen for incoming 'Spawn' command requests and respond synchronously.
        /// </summary>
        public delegate global::Improbable.Words.MessageSpawnResponse OnSpawnCommandRequestSyncCallback(global::Improbable.Words.MessageSpawnRequest request, ICommandCallerInfo commandCallerInfo);
        /// <summary>
        ///     Wraps a synchronous or asynchronous callback to be invoked when a command response is received for the Spawn command.
        /// </summary>
        public class SpawnCommandRequestCallbackWrapper
        {
            private OnSpawnCommandRequestSyncCallback syncCallback;
            private OnSpawnCommandRequestAsyncCallback asyncCallback;
            /// <summary>
            ///     Registers a synchronous callback to be invoked immediately upon receiving a command request.
            /// </summary>
            public void RegisterResponse(OnSpawnCommandRequestSyncCallback callback)
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
            public void RegisterAsyncResponse(OnSpawnCommandRequestAsyncCallback callback)
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
                    throw new InvalidOperationException("Attempted to deregister a command response when none is registered for command Spawn");
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
                throw new InvalidOperationException("Attempted to register a command response when one has already been registered for command Spawn.");
            }
            /// <summary>
            ///     Invokes the registered callback. This is an implementation detail; it should not be called from user code.
            /// </summary>
            public void InvokeCallback(SpawnCommandResponseHandle responseHandle)
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
        ///     A response handle for the 'Spawn' command.
        /// </summary>
        public class SpawnCommandResponseHandle
        {
            private readonly
                global::Improbable.Worker.CommandRequestOp<global::Improbable.Words.MessageSpawner.Commands.Spawn>
                commandRequestOp;
            private readonly CommandCallerInfo commandCallerInfo;
            private readonly ISpatialCommunicator communicator;

            /// <summary>
            ///     Creates a new response handle. This is an implementation detail; it should not be called from user code.
            /// </summary>
            public SpawnCommandResponseHandle(
                global::Improbable.Worker.CommandRequestOp<global::Improbable.Words.MessageSpawner.Commands.Spawn>
                    commandRequestOp, ISpatialCommunicator communicator)
            {
                this.commandRequestOp = commandRequestOp;
                this.commandCallerInfo = new CommandCallerInfo(commandRequestOp.CallerWorkerId, commandRequestOp.CallerAttributeSet);
                this.communicator = communicator;
            }

            /// <summary>
            ///     Returns the request object.
            /// </summary>
            public global::Improbable.Words.MessageSpawnRequest Request { get { return commandRequestOp.Request.Get().Value; } }

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
            public void Respond(global::Improbable.Words.MessageSpawnResponse response)
            {
                var commandResponse = new global::Improbable.Words.MessageSpawner.Commands.Spawn.Response(response);
                communicator.SendCommandResponse(commandRequestOp.RequestId, commandResponse);
            }
        }

        protected void OnSpawnCommandRequestDispatcherCallback(
            global::Improbable.Worker.CommandRequestOp<global::Improbable.Words.MessageSpawner.Commands.Spawn> op)
        {
            if (op.EntityId != entityId || spawnCommandRequestCallbackWrapper == null || !spawnCommandRequestCallbackWrapper.IsCallbackRegistered())
            {
                return;
            }
            var responseHandle = new SpawnCommandResponseHandle(op, communicator);
            spawnCommandRequestCallbackWrapper.InvokeCallback(responseHandle);

#if UNITY_EDITOR
            LogCommandRequest(DateTime.Now, "Spawn", op.Request.Get().Value);
#endif
        }

    }
}
