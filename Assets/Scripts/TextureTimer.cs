using UnityEngine;
using System.Collections;

public class TextureTimer : MonoBehaviour
{
    public float scrollSpeed = 0.5F;
    public Renderer rend;
    public float timer = 0;
    public Vector2 offset;
    void Start()
    {
        rend = GetComponent<Renderer>();
        offset = Vector2.zero;
    }
    void Update()
    {
        timer++;
        if (timer >= 50)
        {
            offset.x = Random.Range(0.1f, 0.9f);
            offset.y = Random.Range(0.1f, 0.9f);
            rend.material.mainTextureOffset = new Vector2(offset.x, offset.y);
            timer = 0;
        }
    }
}
