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
    public class SpatialOsPositionSetTimestampComponent: SpatialOsComponentBase
    {
        /// <inheritdoc />
        public override uint ComponentId { get { return 1005; } }

        protected float timestampValue;
        /// <summary>
        ///     Returns the value of the field 'Timestamp'.
        /// </summary>
        public float Timestamp { get { return timestampValue; } }


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
            OnAddComponentDispatcherCallback(new AddComponentOp<global::Improbable.Core.PositionSetTimestamp> { EntityId = entityId, Data = new global::Improbable.Core.PositionSetTimestamp.Data(((global::Improbable.Core.PositionSetTimestamp.Impl)op.ComponentObject).Data) });
        }

        /// <inheritdoc />
        public override void OnComponentUpdatePipelineOp(UpdateComponentPipelineOp op)
        {
            OnComponentUpdateDispatcherCallback(new ComponentUpdateOp<global::Improbable.Core.PositionSetTimestamp> { EntityId = entityId, Update = (global::Improbable.Core.PositionSetTimestamp.Update)op.UpdateObject });
        }

        protected void OnAddComponentDispatcherCallback(AddComponentOp<global::Improbable.Core.PositionSetTimestamp> op)
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
        public virtual void SendComponentUpdate(global::Improbable.Core.PositionSetTimestamp.Update update)
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
		    communicator.SendAuthorityLossImminentAcknowledgement<global::Improbable.Core.PositionSetTimestamp>(entityId);
		}

        /// <summary>
        ///     The type of callback to listen for component updates.
        /// </summary>
        public delegate void OnComponentUpdateCallback(global::Improbable.Core.PositionSetTimestamp.Update update);

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
            global::Improbable.Worker.ComponentUpdateOp<global::Improbable.Core.PositionSetTimestamp> op)
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

        protected void OnComponentUpdateDispatcherCallback(global::Improbable.Core.PositionSetTimestamp.Update update) {
            if (update.timestamp.HasValue) {
                timestampValue = update.timestamp.Value;
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
            if (update.timestamp.HasValue) {
#if UNITY_EDITOR
                LogComponentUpdate("timestamp", timestampValue);
#endif
                if (onTimestampUpdateCallbacks != null)
                {
                    for (var i = 0; i < onTimestampUpdateCallbacks.Count; i++)
                    {
                        try
                        {
                            onTimestampUpdateCallbacks[i](update.timestamp.Value);
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
        ///     The type of callback to listen for updates to field 'Timestamp'.
        /// </summary>
        public delegate void OnTimestampUpdateCallback(float newTimestamp);

        protected System.Collections.Generic.List<OnTimestampUpdateCallback> onTimestampUpdateCallbacks;

        /// <summary>
        ///     Invoked when the field 'Timestamp' is updated.
        /// </summary>
        public event OnTimestampUpdateCallback OnTimestampUpdate
        {
            add
            {
                if (onTimestampUpdateCallbacks == null)
                {
                    onTimestampUpdateCallbacks = new System.Collections.Generic.List<OnTimestampUpdateCallback>();
                }
                onTimestampUpdateCallbacks.Add(value);
            }
            remove
            {
                if (onTimestampUpdateCallbacks != null)
                {
                    onTimestampUpdateCallbacks.Remove(value);
                }
            }
        }

    }
}
