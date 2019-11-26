using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFigurant : MonoBehaviour
{
    public List<Sprite> pilots;
    public Vector2 size;
    public int layer;
    public float direction;

    void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       // if(col)
    }
}
