using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float time;
    public float direction;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LifeTime");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x - direction, transform.position.y);
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
