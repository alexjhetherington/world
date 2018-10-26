using Improbable.Player;
using UnityEngine;

namespace Assets.Gamelogic.Core
{
    public static class SimulationSettings
    {
        //EntityPrefab Names
        public static readonly string PlayerCreatingInstancePrefabName = "PlayerCreatingInstance";
        public static readonly string PlayerCharacterPrefabName = "PlayerCharacter";
        public static readonly string MessageOnGroundPrefabName = "MessageOnGround";
        public static readonly string WalledMessageOnGroundPrefabName = "WalledMessageOnGround";

        public static readonly int TargetClientFramerate = 60;
        public static readonly int TargetServerFramerate = 60;
        public static readonly int FixedFramerate = 20;

        // Snapshot
        public static readonly string DefaultRelativeSnapshotPath = "/../../../snapshots/default.snapshot";

        public static readonly uint TotalHeartbeatsBeforeTimeout = 10;
        public static readonly float HeartbeatCheckIntervalSecs = 5;
        public static readonly float HeartbeatSendingIntervalSecs = 5;

        public static readonly float ClientConnectionTimeoutSecs = 10;
    }
}
