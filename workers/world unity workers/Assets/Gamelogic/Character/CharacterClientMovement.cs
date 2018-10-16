using Improbable;
using Improbable.Character;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityClient)]
public class CharacterClientMovement : SelfClientMovement<CharacterInputs>
{
    [Require] private CharacterControls.Writer characterControlsWriter;
    [Require] private Position.Reader positionReader;
    [Require] private PositionSetTimestamp.Reader timestampReader;
    [Require] private LiveTime.Reader liveTimeReader;

    private ChatPrep chatPrep;

    private void Awake()
    {
        movementCalculation = new MoveCalc(GetComponent<CharacterController>());
        chatPrep = GetComponent<ChatPrep>();
    }

    protected override CharacterInputs GetMovementInput()
    {
        if (!chatPrep.IsTyping())
        {
            return new CharacterInputs(timestamp, Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), Input.GetKey(KeyCode.LeftShift));
        }
        else
        {
            return new CharacterInputs(timestamp, 0, 0, false);
        }
    }

    protected override void SendMovementInputToServer(List<CharacterInputs> movementInput)
    {
        /*List<CharacterInputs> cloneList = new List<CharacterInputs>();
        foreach(CharacterInputs charIn in movementInput)
        {
            cloneList.Add(charIn);
        }
        FakeLatency(cloneList);*/
        ActuallySendToServer(movementInput);
    }

    private void ActuallySendToServer(List<CharacterInputs> movementInput)
    {
        /*Debug.Log("This input package is");
        foreach (CharacterInputs debugInput in movementInput){
            Debug.Log(debugInput.ToString());
        }*/

        Improbable.Collections.List<CharacterControlsUpdate> characterControls = new Improbable.Collections.List<CharacterControlsUpdate>();
        for (int i = 0; i < movementInput.Count; i++)
        {
            CharacterInputs input = movementInput[i];

            CharacterControlsUpdate update = new CharacterControlsUpdate();
            update.horizontalAxis = input.horizontalAxis;
            update.verticalAxis = input.verticalAxis;
            update.spiritMode = input.spiritMode;
            update.timestamp = input.timestamp;

            characterControls.Add(update);
        }
        characterControlsWriter.Send(new CharacterControls.Update().SetCharacterControls(characterControls));
    }

    protected override AuthoritativeTransform GetAuthoritativeTransform()
    {
        Vector3 position = positionReader.Data.coords.ToUnityVector();
        Quaternion rotation = Quaternion.Euler(0, 0,0);
        float timestamp = timestampReader.Data.timestamp;
        AuthoritativeTransform at = new AuthoritativeTransform(timestamp, position, rotation);
        return at;
    }

    private new void Update()
    {
        /*latencyArray.Tick(Time.deltaTime);
        List<CharacterInputs> delayed = latencyArray.GetFirstIfTimedOut();
        if (delayed != null)
        {
            ActuallySendToServer(delayed);
        }*/

        base.Update();
    }


    /*private TimedArray<List<CharacterInputs>> latencyArray = new TimedArray<List<CharacterInputs>>();
    private float latency = 1;
    private void FakeLatency(List<CharacterInputs> movementInput)
    {
        latencyArray.Insert(latency, movementInput);
    }*/

    protected override float GetLiveTime()
    {
        return liveTimeReader.Data.timestamp;
    }
}



public class TimedArray<T>
{
    private List<TimedItem<T>> _timedArray = new List<TimedItem<T>>();

    public void Insert(float time, T item)
    {
        _timedArray.Add(new TimedItem<T>(time, item));
    }

    public void Tick(float deltaTime)
    {
        for (int i = _timedArray.Count - 1; i >= 0; i--)
        {
            TimedItem<T> currentItem = _timedArray[i];
            float newTime = currentItem.timeLeft - deltaTime;
            if (newTime > 0)
            {
                _timedArray[i] = new TimedItem<T>(newTime, currentItem.item);
            }
            else
            {
                _timedArray[i] = new TimedItem<T>(0, currentItem.item);
            }

        }
    }

    public T GetFirstIfTimedOut()
    {
        if (_timedArray.Count > 0 && _timedArray[0].timeLeft <= 0)
        {
            T item = _timedArray[0].item;
            _timedArray.RemoveAt(0);
            return item;
        }
        return default(T);
    }
}

public class TimedItem<T>
{
    public float timeLeft;
    public T item;

    public TimedItem(float timeLeft, T item)
    {
        this.timeLeft = timeLeft;
        this.item = item;
    }
}