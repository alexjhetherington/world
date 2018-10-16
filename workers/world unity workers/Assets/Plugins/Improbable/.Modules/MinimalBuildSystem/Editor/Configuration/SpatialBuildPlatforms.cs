// Copyright (c) Improbable Worlds Ltd, All Rights Reserved

namespace Improbable.Unity.MinimalBuildSystem.Configuration
{
    [System.Flags]
    public enum SpatialBuildPlatforms
    {
        Current = 1 << 0,
        Windows32 = 1 << 1,
        Windows64 = 1 << 2,
        Linux = 1 << 3,
        OSX = 1 << 4
    }
}
