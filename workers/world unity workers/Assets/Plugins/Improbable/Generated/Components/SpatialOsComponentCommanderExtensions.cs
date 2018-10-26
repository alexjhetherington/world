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
        public static void ServerCollisionCreated(this IComponentCommander commander, 
            EntityId entityId, global::Improbable.Character.ServerCollisionCreatedRequest request, 
            CommandCallback<global::Improbable.Character.ServerCollisionCreatedResponse> callback,
            TimeSpan? timeout = null, CommandDelivery commandDelivery = CommandDelivery.RoundTrip)
        {
            var rawRequest = new Improbable.Character.CollisionsCreated.Commands.ServerCollisionCreated.Request(request);
            commander.SendCommand<Improbable.Character.CollisionsCreated.Commands.ServerCollisionCreated,
                global::Improbable.Character.ServerCollisionCreatedResponse>(entityId, rawRequest, ExtractResponse_ServerCollisionCreated, callback, timeout, commandDelivery);
        }
        
        public static ICommandResponseHandler<global::Improbable.Character.ServerCollisionCreatedResponse> ServerCollisionCreated(this IComponentCommander commander, 
            EntityId entityId, global::Improbable.Character.ServerCollisionCreatedRequest request, 
            TimeSpan? timeout = null, CommandDelivery commandDelivery = CommandDelivery.RoundTrip)
        {
            var rawRequest = new Improbable.Character.CollisionsCreated.Commands.ServerCollisionCreated.Request(request);
            var resultHandler = new CommandResponseHandler<global::Improbable.Character.ServerCollisionCreatedResponse>();
            commander.SendCommand<Improbable.Character.CollisionsCreated.Commands.ServerCollisionCreated,
                global::Improbable.Character.ServerCollisionCreatedResponse>(entityId, rawRequest, ExtractResponse_ServerCollisionCreated, resultHandler.Trigger, timeout, commandDelivery);
            return resultHandler;
        }

        private static global::Improbable.Character.ServerCollisionCreatedResponse ExtractResponse_ServerCollisionCreated(
            ICommandResponse<Improbable.Character.CollisionsCreated.Commands.ServerCollisionCreated> rawResponse)
        {
            return rawResponse.Get().Value;
        }

        public static void ClientCollisionCreated(this IComponentCommander commander, 
            EntityId entityId, global::Improbable.Character.ClientCollisionCreatedRequest request, 
            CommandCallback<global::Improbable.Character.ClientCollisionCreatedResponse> callback,
            TimeSpan? timeout = null, CommandDelivery commandDelivery = CommandDelivery.RoundTrip)
        {
            var rawRequest = new Improbable.Character.CollisionsCreated.Commands.ClientCollisionCreated.Request(request);
            commander.SendCommand<Improbable.Character.CollisionsCreated.Commands.ClientCollisionCreated,
                global::Improbable.Character.ClientCollisionCreatedResponse>(entityId, rawRequest, ExtractResponse_ClientCollisionCreated, callback, timeout, commandDelivery);
        }
        
        public static ICommandResponseHandler<global::Improbable.Character.ClientCollisionCreatedResponse> ClientCollisionCreated(this IComponentCommander commander, 
            EntityId entityId, global::Improbable.Character.ClientCollisionCreatedRequest request, 
            TimeSpan? timeout = null, CommandDelivery commandDelivery = CommandDelivery.RoundTrip)
        {
            var rawRequest = new Improbable.Character.CollisionsCreated.Commands.ClientCollisionCreated.Request(request);
            var resultHandler = new CommandResponseHandler<global::Improbable.Character.ClientCollisionCreatedResponse>();
            commander.SendCommand<Improbable.Character.CollisionsCreated.Commands.ClientCollisionCreated,
                global::Improbable.Character.ClientCollisionCreatedResponse>(entityId, rawRequest, ExtractResponse_ClientCollisionCreated, resultHandler.Trigger, timeout, commandDelivery);
            return resultHandler;
        }

        private static global::Improbable.Character.ClientCollisionCreatedResponse ExtractResponse_ClientCollisionCreated(
            ICommandResponse<Improbable.Character.CollisionsCreated.Commands.ClientCollisionCreated> rawResponse)
        {
            return rawResponse.Get().Value;
        }

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