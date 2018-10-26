using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable.Unity.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Splash : MonoBehaviour {

    public Button connectButton;
    public GameObject notReadyWarning;
    public GameObject loading;

    public void AttemptToConnect()
    {
        // Disable connect button
        connectButton.interactable = false;

        // Hide warning if already shown
        notReadyWarning.SetActive(false);

        // Start loading spinner
        loading.SetActive(true);

        AttemptConnection();
    }

    private void AttemptConnection()
    {
        Bootstrap bootstrap = FindObjectOfType<Bootstrap>();
        if (!bootstrap)
        {
            throw new System.Exception("Couldn't find Bootstrap script on GameEntry in UnityScene");
        }
        bootstrap.ConnectToClient();

        // In case the client connection is successful this coroutine is destroyed as part of unloading
        // the splash screen so ConnectionTimeout won't be called
        StartCoroutine(TimerUtils.WaitAndPerform(SimulationSettings.ClientConnectionTimeoutSecs, ConnectionTimeout));
    }

    private void ConnectionTimeout()
    {
        if (SpatialOS.IsConnected)
        {
            SpatialOS.Disconnect();
        }

        loading.SetActive(false);
        notReadyWarning.SetActive(true);
        connectButton.interactable = true;
    }
}
