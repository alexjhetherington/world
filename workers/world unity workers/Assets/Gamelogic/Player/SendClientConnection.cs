using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[WorkerType(WorkerPlatform.UnityClient)]
public class SendClientConnection : MonoBehaviour {

    [Require] private ClientAuthorityCheck.Writer ClientAuthorityCheckWriter;

    private Coroutine heartbeatCoroutine;

    private void OnEnable()
    {
        heartbeatCoroutine = StartCoroutine(TimerUtils.CallRepeatedly(SimulationSettings.HeartbeatSendingIntervalSecs, SendHeartbeat));
        SceneManager.UnloadSceneAsync(BuildSettings.SplashScreenScene);
    }

    private void OnDisable()
    {
        StopCoroutine(heartbeatCoroutine);
        SceneManager.LoadScene(BuildSettings.SplashScreenScene, LoadSceneMode.Additive);
    }

    private void SendHeartbeat()
    {
        SpatialOS.Commands.SendCommand(ClientAuthorityCheckWriter, ClientConnection.Commands.Heartbeat.Descriptor, new HeartbeatRequest(), gameObject.EntityId());
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Application Quit");
        if (SpatialOS.IsConnected)
        {
            SpatialOS.Commands.SendCommand(ClientAuthorityCheckWriter, ClientConnection.Commands.DisconnectClient.Descriptor, new ClientDisconnectRequest(), gameObject.EntityId());
        }
    }
}
