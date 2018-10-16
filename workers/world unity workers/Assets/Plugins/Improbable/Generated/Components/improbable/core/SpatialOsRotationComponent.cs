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
    public class SpatialOsRotationComponent: SpatialOsComponentBase
    {
        /// <inheritdoc />
        public override uint ComponentId { get { return 1004; } }

        protected uint rotationValue;
        /// <summary>
        ///     Returns the value of the field 'Rotation'.
        /// </summary>
        public uint Rotation { get { return rotationValue; } }


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
            OnAddComponentDispatcherCallback(new AddComponentOp<global::Improbable.Core.Rotation> { EntityId = entityId, Data = new global::Improbable.Core.Rotation.Data(((global::Improbable.Core.Rotation.Impl)op.ComponentObject).Data) });
        }

        /// <inheritdoc />
        public override void OnComponentUpdatePipelineOp(UpdateComponentPipelineOp op)
        {
            OnComponentUpdateDispatcherCallback(new ComponentUpdateOp<global::Improbable.Core.Rotation> { EntityId = entityId, Update = (global::Improbable.Core.Rotation.Update)op.UpdateObject });
        }

        protected void OnAddComponentDispatcherCallback(AddComponentOp<global::Improbable.Core.Rotation> op)
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
        public virtual void SendComponentUpdate(global::Improbable.Core.Rotation.Update update)
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
		    communicator.SendAuthorityLossImminentAcknowledgement<global::Improbable.Core.Rotation>(entityId);
		}

        /// <summary>
        ///     The type of callback to listen for component updates.
        /// </summary>
        public delegate void OnComponentUpdateCallback(global::Improbable.Core.Rotation.Update update);

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
            global::Improbable.Worker.ComponentUpdateOp<global::Improbable.Core.Rotation> op)
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

        protected void OnComponentUpdateDispatcherCallback(global::Improbable.Core.Rotation.Update update) {
            if (update.rotation.HasValue) {
                rotationValue = update.rotation.Value;
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
            if (update.rotation.HasValue) {
#if UNITY_EDITOR
                LogComponentUpdate("rotation", rotationValue);
#endif
                if (onRotationUpdateCallbacks != null)
                {
                    for (var i = 0; i < onRotationUpdateCallbacks.Count; i++)
                    {
                        try
                        {
                            onRotationUpdateCallbacks[i](update.rotation.Value);
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
        ///     The type of callback to listen for updates to field 'Rotation'.
        /// </summary>
        public delegate void OnRotationUpdateCallback(uint newRotation);

        protected System.Collections.Generic.List<OnRotationUpdateCallback> onRotationUpdateCallbacks;

        /// <summary>
        ///     Invoked when the field 'Rotation' is updated.
        /// </summary>
        public event OnRotationUpdateCallback OnRotationUpdate
        {
            add
            {
                if (onRotationUpdateCallbacks == null)
                {
                    onRotationUpdateCallbacks = new System.Collections.Generic.List<OnRotationUpdateCallback>();
                }
                onRotationUpdateCallbacks.Add(value);
            }
            remove
            {
                if (onRotationUpdateCallbacks != null)
                {
                    onRotationUpdateCallbacks.Remove(value);
                }
            }
        }

    }
}
