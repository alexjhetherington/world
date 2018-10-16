using Assets.Gamelogic.Core;
using Improbable;
using Improbable.Worker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class SnapshotMenu : MonoBehaviour
    {
        [MenuItem("Improbable/Snapshots/Generate Default Snapshot")]
        private static void GenerateDefaultSnapshot()
        {
            var snapshotEntities = new Dictionary<EntityId, Entity>();
            var currentEntityId = 1;
            snapshotEntities.Add(new EntityId(currentEntityId++), EntityTemplateFactory.CreatePlayerCreatingInstanceTemplate());

            SaveSnapshot(snapshotEntities);
        }

        private static void SaveSnapshot(Dictionary<EntityId, Entity> snapshotEntities)
        {
            var snapshotPath = Application.dataPath + SimulationSettings.DefaultRelativeSnapshotPath;
            File.Delete(snapshotPath);
            using (SnapshotOutputStream stream = new SnapshotOutputStream(snapshotPath))
            {
                foreach (var kvp in snapshotEntities)
                {
                    var error = stream.WriteEntity(kvp.Key, kvp.Value);
                    if (error.HasValue)
                    {
                        Debug.LogErrorFormat("Failed to generate initial world snapshot: {0}", error.Value);
                        return;
                    }
                }
            }
            Debug.LogFormat("Successfully generated initial world snapshot at {0}", snapshotPath);
        }
    }
}
