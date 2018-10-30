using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Walls : MonoBehaviour {

    public TMP_Text text;
    private SpriteRenderer spriteRenderer;
    private BoxCollider boxCollider;

    [SerializeField] private float wallExtraSize = 0.64f;
    
    public void SizeWalls()
    {
        Vector3 size = text.bounds.max - text.bounds.min;

        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.size = new Vector2(size.x + wallExtraSize, size.y + wallExtraSize);

        if(boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        boxCollider.size = new Vector3(size.x + wallExtraSize, size.y + wallExtraSize, 1);
        boxCollider.center = new Vector3(0, 0, transform.position.y);
    }
}
