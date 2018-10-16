using Prime31.MessageKit;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chat : MonoBehaviour {

    public TMP_InputField inputField;

    //private EventSystem eventSystem;

    private const string CONTROL_NAME = "chatBox";

    private void Awake()
    {
        //eventSystem = FindObjectOfType<EventSystem>();
    }

    // Use this for initialization
    void Start () {
        MessageKit<bool>.addObserver(MessageKitIds.TYPING_STATE, EnableDisableChat);
	}

    private void OnDestroy()
    {
        MessageKit<bool>.removeObserver(MessageKitIds.TYPING_STATE, EnableDisableChat);
    }

    private void EnableDisableChat(bool typing)
    {
        if (typing)
        {
            inputField.gameObject.SetActive(true);
            inputField.text = "";
            
            //Issue where holding down buttons as you activated chat box would stop it from gaining focus until mouse click
            //For some reason this fixes it. Literally trawled through the API and guessed this :l not entirely sure how 
            //the input system works.
            EventSystem.current.currentInputModule.Process();
            EventSystem.current.SetSelectedGameObject(inputField.gameObject);
        }
        else
        {
            string text = inputField.text;
            inputField.gameObject.SetActive(false);

            if (text.Equals(""))
            {
                Debug.Log("No text");
            }
            else
            {
                Debug.Log(text);
                MessageKit<string>.post(MessageKitIds.SEND_CHAT, text);
            }
            
        }
    }
}
