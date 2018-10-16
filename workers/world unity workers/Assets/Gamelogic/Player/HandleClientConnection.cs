using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable.Entity.Component;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityWorker)]
public class HandleClientConnection : MonoBehaviour {

    [Require] private ClientConnection.Writer ClientConnectionWriter;

    private Coroutine heartbeatCoroutine;

    private void OnEnable()
    {
        //TODO Why is this async??? Didn't seem to cause any errors when i added a response
        ClientConnectionWriter.CommandReceiver.OnDisconnectClient.RegisterAsyncResponse(OnDisconnectClient);

        ClientConnectionWriter.CommandReceiver.OnHeartbeat.RegisterResponse(OnHeartbeat);
        heartbeatCoroutine = StartCoroutine(TimerUtils.CallRepeatedly(SimulationSettings.HeartbeatCheckIntervalSecs, CheckHeartbeat));
    }

    private void OnDisable()
    {
        ClientConnectionWriter.CommandReceiver.OnDisconnectClient.DeregisterResponse();
        ClientConnectionWriter.CommandReceiver.OnHeartbeat.DeregisterResponse();
        StopCoroutine(heartbeatCoroutine);
    }

    private void OnDisconnectClient(ResponseHandle<ClientConnection.Commands.DisconnectClient,
                                        ClientDisconnectRequest,
                                        ClientDisconnectResponse> handle)
    {
        DeletePlayerEntity();
        handle.Respond(new ClientDisconnectResponse()); //Response after this entity has ostensibly been deleted seems to cause no issue
    }

    private void DeletePlayerEntity()
    {
        SpatialOS.Commands.DeleteEntity(ClientConnectionWriter, gameObject.EntityId());
    }

    private void SetHeartbeat(uint beats)
    {
        var update = new ClientConnection.Update();
        update.SetTimeoutBeatsRemaining(beats);
        ClientConnectionWriter.Send(update);
    }

    private void CheckHeartbeat()
    {
        var heartbeatsRemainingBeforeTimeout = ClientConnectionWriter.Data.timeoutBeatsRemaining;
        if (heartbeatsRemainingBeforeTimeout == 0)
        {
            StopCoroutine(heartbeatCoroutine);
            DeletePlayerEntity();
            return;
        }
        SetHeartbeat(heartbeatsRemainingBeforeTimeout - 1);
    }

    private HeartbeatResponse OnHeartbeat(HeartbeatRequest request, ICommandCallerInfo callerinfo)
    {
        SetHeartbeat(SimulationSettings.TotalHeartbeatsBeforeTimeout);
        return new HeartbeatResponse();
    }
}
