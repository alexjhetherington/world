// Copyright (c) Improbable Worlds Ltd, All Rights Reserved

using System;
using System.Diagnostics;
using Improbable.Unity.Editor.Addons;

namespace Improbable.Unity.MinimalBuildSystem
{
    internal static class SpatialCommandRunner
    {
        internal static void RunSpatialCommand(string args, string description)
        {
            var process = SpatialCommand.RunCommandWithSpatialInThePath(SpatialCommand.SpatialPath, new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                FileName = SpatialCommand.SpatialPath,
                Arguments = args,
                CreateNoWindow = true
            });

            var output = process.StandardOutput.ReadToEnd();
            var errOut = process.StandardError.ReadToEnd();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                throw new Exception(string.Format(
                                                  "Could not {2}. The following error occurred: {0}, {1}\n", output,
                                                  errOut, description));
            }
        }
    }
}
