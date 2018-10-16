using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSprite : MonoBehaviour {

    public SpriteRenderer characterSprite;
    public SpriteRenderer spiritSprite;

    private void OnEnable()
    {
        spiritSprite.enabled = false;
    }

    // Update is called once per frame
    void Update () {
        if(gameObject.layer == 11)
        {
            if (characterSprite.enabled)
            {
                characterSprite.enabled = false;
                spiritSprite.enabled = true;
            }
        }
        else
        {
            if (!characterSprite.enabled)
            {
                characterSprite.enabled = true;
                spiritSprite.enabled = false;
            }
        }
	}
}
