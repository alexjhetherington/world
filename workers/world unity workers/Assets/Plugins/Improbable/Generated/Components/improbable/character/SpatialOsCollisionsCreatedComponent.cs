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

    }
}
