// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========
using Improbable.Unity.CodeGeneration;
using Improbable.Unity.EditorTools.Util;
using UnityEditor;
using UnityEngine;

namespace Improbable.Character
{
    [CustomEditor(typeof(SpatialOsCharacterControlsComponent))]
    public class SpatialOsCharacterControlsComponentEditor : UnityEditor.Editor
    {
        private SpatialOsCharacterControlsComponent editedComponent;
        private SpatialOsCharacterControlsComponentEditorData componentData;

        private class SpatialOsCharacterControlsComponentEditorData : SpatialOsComponentEditorBase<SpatialOsCharacterControlsComponent>
        {        }

        protected virtual void OnEnable()
        {
            editedComponent = (SpatialOsCharacterControlsComponent) target;

            var hashCode = editedComponent.GetHashCode();
            componentData = (SpatialOsCharacterControlsComponentEditorData) EditorObjectStateManager.GetComponentEditorData(hashCode);
            if(componentData == null)
            {
                componentData = new SpatialOsCharacterControlsComponentEditorData();
                EditorObjectStateManager.SetComponentEditorData(hashCode, componentData);
            }

            componentData.AttachComponent(editedComponent);
        }

        protected virtual void OnDisable()
        {
            componentData.DetachComponent();
        }

        private Vector2 scrollPos;

        public override void OnInspectorGUI()
        {
            componentData.UpdateEditorLogs();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("EntityId", componentData.EntityId.IsValid() ? componentData.EntityId.Id.ToString() : "n/a");
            if (componentData.IsComponentReady)
            {
                EditorGUILayout.LabelField("Component Initialised", GuiUtil.GreenTextStyle);
            }
            else
            {
                EditorGUILayout.LabelField("Component Not Initialised", GuiUtil.RedTextStyle);
            }

            if (componentData.Authority == global::Improbable.Worker.Authority.Authoritative)
            {
                EditorGUILayout.LabelField("Authoritative", GuiUtil.GreenTextStyle);
            }
            else if (componentData.Authority == global::Improbable.Worker.Authority.NotAuthoritative)
            {
                EditorGUILayout.LabelField("Not Authoritative", GuiUtil.RedTextStyle);
            }
            else if (componentData.Authority == global::Improbable.Worker.Authority.AuthorityLossImminent)
            {
                EditorGUILayout.LabelField("Authority Loss Imminent", GuiUtil.YellowTextStyle);
            }

            EditorGUILayout.Space();

            EditorGUILayout.Space();
            componentData.ShowUpdates = EditorGUILayout.Foldout(componentData.ShowUpdates, string.Format("Component Updates: ({0}/s)",
                System.Math.Round(componentData.AverageComponentUpdatesPerSecond, 2)), /* toggleOnLabelClick: */ true );

            if (componentData.ShowUpdates)
            {
                scrollPos = EditorGUILayout.BeginScrollView(
                    scrollPos, /* always horizontal scroll bar */ false, /* always vertical scroll bar */ false,
                    GUILayout.Width(100), GUILayout.Height(100));
                if (componentData.ComponentUpdateLogArray != null)
                {
                    for (var i = 0; i < componentData.ComponentUpdateLogArray.Length; i++)
                    {
                        GUILayout.Label(componentData.ComponentUpdateLogArray[i]);
                    }
                }

                EditorGUILayout.EndScrollView();
                EditorGUILayout.Space();
            }

            componentData.ShowCommands = EditorGUILayout.Foldout(componentData.ShowCommands, string.Format("Command Requests: ({0}/s)",
                System.Math.Round(componentData.AverageCommandRequestsPerSecond, 2)), /* toggleOnLabelClick: */ true);

            if (componentData.ShowCommands)
            {
                scrollPos = EditorGUILayout.BeginScrollView(
                    scrollPos, /* always horizontal scroll bar */ false, /* always vertical scroll bar */ false,
                    GUILayout.Width(100), GUILayout.Height(100));

                if (componentData.CommandRequestLogArray != null)
                {
                    for (var i = 0; i < componentData.CommandRequestLogArray.Length; i++)
                    {
                        GUILayout.Label(componentData.CommandRequestLogArray[i]);
                    }
                }
                EditorGUILayout.EndScrollView();
            }
        }
    }
}

