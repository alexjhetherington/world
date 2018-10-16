// ===========
// DO NOT EDIT - this file is automatically regenerated.
// =========== 
using System;
using Improbable.Unity.Core;
using Improbable.Worker;
using Improbable;
using Improbable.Character;
using Improbable.Core;
using Improbable.Player;
using Improbable.Words;

namespace Improbable.GeneratedCode
{
    public static class SpatialOsCommanderExtensions
    {
        public static void CreatePlayer(this IComponentCommander commander, 
            EntityId entityId, global::Improbable.Core.CreatePlayerRequest request, 
            CommandCallback<global::Improbable.Core.CreatePlayerResponse> callback,
            TimeSpan? timeout = null, CommandDelivery commandDelivery = CommandDelivery.RoundTrip)
        {
            var rawRequest = new Improbable.Core.PlayerCreation.Commands.CreatePlayer.Request(request);
            commander.SendCommand<Improbable.Core.PlayerCreation.Commands.CreatePlayer,
                global::Improbable.Core.CreatePlayerResponse>(entityId, rawRequest, ExtractResponse_CreatePlayer, callback, timeout, commandDelivery);
        }
        
        public static ICommandResponseHandler<global::Improbable.Core.CreatePlayerResponse> CreatePlayer(this IComponentCommander commander, 
            EntityId entityId, global::Improbable.Core.CreatePlayerRequest request, 
            TimeSpan? timeout = null, CommandDelivery commandDelivery = CommandDelivery.RoundTrip)
        {
            var rawRequest = new Improbable.Core.PlayerCreation.Commands.CreatePlayer.Request(request);
            var resultHandler = new CommandResponseHandler<global::Improbable.Core.CreatePlayerResponse>();
            commander.SendCommand<Improbable.Core.PlayerCreation.Commands.CreatePlayer,
                global::Improbable.Core.CreatePlayerResponse>(entityId, rawRequest, ExtractResponse_CreatePlayer, resultHandler.Trigger, timeout, commandDelivery);
            return resultHandler;
        }

        private static global::Improbable.Core.CreatePlayerResponse ExtractResponse_CreatePlayer(
            ICommandResponse<Improbable.Core.PlayerCreation.Commands.CreatePlayer> rawResponse)
        {
            return rawResponse.Get().Value;
        }

        public static void Heartbeat(this IComponentCommander commander, 
            EntityId entityId, global::Improbable.Player.HeartbeatRequest request, 
            CommandCallback<global::Improbable.Player.HeartbeatResponse> callback,
            TimeSpan? timeout = null, CommandDelivery commandDelivery = CommandDelivery.RoundTrip)
        {
            var rawRequest = new Improbable.Player.ClientConnection.Commands.Heartbeat.Request(request);
            commander.SendCommand<Improbable.Player.ClientConnection.Commands.Heartbeat,
                global::Improbable.Player.HeartbeatResponse>(entityId, rawRequest, ExtractResponse_Heartbeat, callback, timeout, commandDelivery);
        }
        
        public static ICommandResponseHandler<global::Improbable.Player.HeartbeatResponse> Heartbeat(this IComponentCommander commander, 
            EntityId entityId, global::Improbable.Player.HeartbeatRequest request, 
            TimeSpan? timeout = null, CommandDelivery commandDelivery = CommandDelivery.RoundTrip)
        {
            var rawRequest = new Improbable.Player.ClientConnection.Commands.Heartbeat.Request(request);
            var resultHandler = new CommandResponseHandler<global::Improbable.Player.HeartbeatResponse>();
            commander.SendCommand<Improbable.Player.ClientConnection.Commands.Heartbeat,
                global::Improbable.Player.HeartbeatResponse>(entityId, rawRequest, ExtractResponse_Heartbeat, resultHandler.Trigger, timeout, commandDelivery);
            return resultHandler;
        }

        private static global::Improbable.Player.HeartbeatResponse ExtractResponse_Heartbeat(
            ICommandResponse<Improbable.Player.ClientConnection.Commands.Heartbeat> rawResponse)
        {
            return rawResponse.Get().Value;
        }

        public static void DisconnectClient(this IComponentCommander commander, 
            EntityId entityId, global::Improbable.Player.ClientDisconnectRequest request, 
            CommandCallback<global::Improbable.Player.ClientDisconnectResponse> callback,
            TimeSpan? timeout = null, CommandDelivery commandDelivery = CommandDelivery.RoundTrip)
        {
            var rawRequest = new Improbable.Player.ClientConnection.Commands.DisconnectClient.Request(request);
            commander.SendCommand<Improbable.Player.ClientConnection.Commands.DisconnectClient,
                global::Improbable.Player.ClientDisconnectResponse>(entityId, rawRequest, ExtractResponse_DisconnectClient, callback, timeout, commandDelivery);
        }
        
        public static ICommandResponseHandler<global::Improbable.Player.ClientDisconnectResponse> DisconnectClient(this IComponentCommander commander, 
            EntityId entityId, global::Improbable.Player.ClientDisconnectRequest request, 
            TimeSpan? timeout = null, CommandDelivery commandDelivery = CommandDelivery.RoundTrip)
        {
            var rawRequest = new Improbable.Player.ClientConnection.Commands.DisconnectClient.Request(request);
            var resultHandler = new CommandResponseHandler<global::Improbable.Player.ClientDisconnectResponse>();
            commander.SendCommand<Improbable.Player.ClientConnection.Commands.DisconnectClient,
                global::Improbable.Player.ClientDisconnectResponse>(entityId, rawRequest, ExtractResponse_DisconnectClient, resultHandler.Trigger, timeout, commandDelivery);
            return resultHandler;
        }

        private static global::Improbable.Player.ClientDisconnectResponse ExtractResponse_DisconnectClient(
            ICommandResponse<Improbable.Player.ClientConnection.Commands.DisconnectClient> rawResponse)
        {
            return rawResponse.Get().Value;
        }

        public static void Spawn(this IComponentCommander commander, 
            EntityId entityId, global::Improbable.Words.MessageSpawnRequest request, 
            CommandCallback<global::Improbable.Words.MessageSpawnResponse> callback,
            TimeSpan? timeout = null, CommandDelivery commandDelivery = CommandDelivery.RoundTrip)
        {
            var rawRequest = new Improbable.Words.MessageSpawner.Commands.Spawn.Request(request);
            commander.SendCommand<Improbable.Words.MessageSpawner.Commands.Spawn,
                global::Improbable.Words.MessageSpawnResponse>(entityId, rawRequest, ExtractResponse_Spawn, callback, timeout, commandDelivery);
        }
        
        public static ICommandResponseHandler<global::Improbable.Words.MessageSpawnResponse> Spawn(this IComponentCommander commander, 
            EntityId entityId, global::Improbable.Words.MessageSpawnRequest request, 
            TimeSpan? timeout = null, CommandDelivery commandDelivery = CommandDelivery.RoundTrip)
        {
            var rawRequest = new Improbable.Words.MessageSpawner.Commands.Spawn.Request(request);
            var resultHandler = new CommandResponseHandler<global::Improbable.Words.MessageSpawnResponse>();
            commander.SendCommand<Improbable.Words.MessageSpawner.Commands.Spawn,
                global::Improbable.Words.MessageSpawnResponse>(entityId, rawRequest, ExtractResponse_Spawn, resultHandler.Trigger, timeout, commandDelivery);
            return resultHandler;
        }

        private static global::Improbable.Words.MessageSpawnResponse ExtractResponse_Spawn(
            ICommandResponse<Improbable.Words.MessageSpawner.Commands.Spawn> rawResponse)
        {
            return rawResponse.Get().Value;
        }

    }
}