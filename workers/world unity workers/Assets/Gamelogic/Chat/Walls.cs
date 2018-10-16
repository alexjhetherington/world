using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Walls : MonoBehaviour {

    public TMP_Text text;
    private SpriteRenderer spriteRenderer;
    private BoxCollider boxCollider;
    public BoxCollider triggerBoxCollider;

    //TODO factor out extra size properly
    public void SizeWalls()
    {
        //Debug.Log(text.bounds.max - text.bounds.min);
        Vector3 size = text.bounds.max - text.bounds.min;

        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.size = new Vector2(size.x + 0.64f, size.y + 0.64f);

        if(boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        boxCollider.size = new Vector3(size.x + 0.64f, size.y + 0.64f, 1);
        boxCollider.center = new Vector3(0, 0, -transform.position.y);

        if(triggerBoxCollider != null)
        {
            triggerBoxCollider.size = new Vector3(size.x + 0.64f, size.y + 0.64f, 1);
            triggerBoxCollider.center = new Vector3(0, 0, -transform.position.y);
        }
    }
}
