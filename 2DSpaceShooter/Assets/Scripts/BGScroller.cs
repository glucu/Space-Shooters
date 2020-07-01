using System.Collections;
using UnityEngine;

/* Haven't decided to use this yet */

public class BGScroller : MonoBehaviour
{

    public float scrollSpeed;
    public float tileSizeY;
    public Vector2 startPos;

    private void Start()
    {

    }

    private void Update()
    {
        float newPos = Mathf.Repeat(Time.time * scrollSpeed, tileSizeY);
        transform.position = startPos + Vector2.up * newPos;

    }
}
