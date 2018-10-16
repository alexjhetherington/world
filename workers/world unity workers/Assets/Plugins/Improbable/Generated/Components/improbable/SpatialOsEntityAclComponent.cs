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

namespace Improbable
{
    public class SpatialOsEntityAclComponent: SpatialOsComponentBase
    {
        /// <inheritdoc />
        public override uint ComponentId { get { return 50; } }

        protected global::Improbable.WorkerRequirementSet readAclValue;
        /// <summary>
        ///     Returns the value of the field 'ReadAcl'.
        /// </summary>
        public global::Improbable.WorkerRequirementSet ReadAcl { get { return readAclValue; } }

        protected global::Improbable.Collections.Map<uint, global::Improbable.WorkerRequirementSet> componentWriteAclValue;
        /// <summary>
        ///     Returns the value of the field 'ComponentWriteAcl'.
        /// </summary>
        public global::Improbable.Collections.Map<uint, global::Improbable.WorkerRequirementSet> ComponentWriteAcl { get { return componentWriteAclValue; } }


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
            OnAddComponentDispatcherCallback(new AddComponentOp<global::Improbable.EntityAcl> { EntityId = entityId, Data = new global::Improbable.EntityAcl.Data(((global::Improbable.EntityAcl.Impl)op.ComponentObject).Data) });
        }

        /// <inheritdoc />
        public override void OnComponentUpdatePipelineOp(UpdateComponentPipelineOp op)
        {
            OnComponentUpdateDispatcherCallback(new ComponentUpdateOp<global::Improbable.EntityAcl> { EntityId = entityId, Update = (global::Improbable.EntityAcl.Update)op.UpdateObject });
        }

        protected void OnAddComponentDispatcherCallback(AddComponentOp<global::Improbable.EntityAcl> op)
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
        public virtual void SendComponentUpdate(global::Improbable.EntityAcl.Update update)
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
		    communicator.SendAuthorityLossImminentAcknowledgement<global::Improbable.EntityAcl>(entityId);
		}

        /// <summary>
        ///     The type of callback to listen for component updates.
        /// </summary>
        public delegate void OnComponentUpdateCallback(global::Improbable.EntityAcl.Update update);

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
            global::Improbable.Worker.ComponentUpdateOp<global::Improbable.EntityAcl> op)
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

        protected void OnComponentUpdateDispatcherCallback(global::Improbable.EntityAcl.Update update) {
            if (update.readAcl.HasValue) {
                readAclValue = update.readAcl.Value;
            }
            if (update.componentWriteAcl.HasValue) {
                componentWriteAclValue = update.componentWriteAcl.Value;
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
            if (update.readAcl.HasValue) {
#if UNITY_EDITOR
                LogComponentUpdate("readAcl", readAclValue);
#endif
                if (onReadAclUpdateCallbacks != null)
                {
                    for (var i = 0; i < onReadAclUpdateCallbacks.Count; i++)
                    {
                        try
                        {
                            onReadAclUpdateCallbacks[i](update.readAcl.Value);
                        }
                        catch (Exception e)
                        {
                            Debug.LogException(e);
                        }
                    }
                }
            }
            if (update.componentWriteAcl.HasValue) {
#if UNITY_EDITOR
                LogComponentUpdate("componentWriteAcl", componentWriteAclValue);
#endif
                if (onComponentWriteAclUpdateCallbacks != null)
                {
                    for (var i = 0; i < onComponentWriteAclUpdateCallbacks.Count; i++)
                    {
                        try
                        {
                            onComponentWriteAclUpdateCallbacks[i](update.componentWriteAcl.Value);
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
        ///     The type of callback to listen for updates to field 'ReadAcl'.
        /// </summary>
        public delegate void OnReadAclUpdateCallback(global::Improbable.WorkerRequirementSet newReadAcl);

        protected System.Collections.Generic.List<OnReadAclUpdateCallback> onReadAclUpdateCallbacks;

        /// <summary>
        ///     Invoked when the field 'ReadAcl' is updated.
        /// </summary>
        public event OnReadAclUpdateCallback OnReadAclUpdate
        {
            add
            {
                if (onReadAclUpdateCallbacks == null)
                {
                    onReadAclUpdateCallbacks = new System.Collections.Generic.List<OnReadAclUpdateCallback>();
                }
                onReadAclUpdateCallbacks.Add(value);
            }
            remove
            {
                if (onReadAclUpdateCallbacks != null)
                {
                    onReadAclUpdateCallbacks.Remove(value);
                }
            }
        }

        /// <summary>
        ///     The type of callback to listen for updates to field 'ComponentWriteAcl'.
        /// </summary>
        public delegate void OnComponentWriteAclUpdateCallback(global::Improbable.Collections.Map<uint, global::Improbable.WorkerRequirementSet> newComponentWriteAcl);

        protected System.Collections.Generic.List<OnComponentWriteAclUpdateCallback> onComponentWriteAclUpdateCallbacks;

        /// <summary>
        ///     Invoked when the field 'ComponentWriteAcl' is updated.
        /// </summary>
        public event OnComponentWriteAclUpdateCallback OnComponentWriteAclUpdate
        {
            add
            {
                if (onComponentWriteAclUpdateCallbacks == null)
                {
                    onComponentWriteAclUpdateCallbacks = new System.Collections.Generic.List<OnComponentWriteAclUpdateCallback>();
                }
                onComponentWriteAclUpdateCallbacks.Add(value);
            }
            remove
            {
                if (onComponentWriteAclUpdateCallbacks != null)
                {
                    onComponentWriteAclUpdateCallbacks.Remove(value);
                }
            }
        }

    }
}
