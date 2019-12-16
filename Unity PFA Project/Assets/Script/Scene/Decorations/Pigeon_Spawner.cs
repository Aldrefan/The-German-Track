using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeon_Spawner : MonoBehaviour
{
    public Vector2Int nbrInterval;
    void OnEnable()
    {
        int nbrOfPigeons = Random.Range(nbrInterval.x, nbrInterval.y);
        Vector3 minValue = GetComponent<BoxCollider2D>().bounds.min;
        Vector3 maxValue = GetComponent<BoxCollider2D>().bounds.max;

        for(int i = 0; i <= nbrOfPigeons; i++)
        {
            float xValue = Random.Range(minValue.x, maxValue.x);
            float yValue = Random.Range(minValue.y, maxValue.y);
            Vector3 pigeonPosition = new Vector3(xValue, yValue, transform.localPosition.z);
            bool direction  = (Random.value > 0.5f);
            GameObject pigeon = Instantiate(Resources.Load("GameObject/Pigeon"), pigeonPosition, Quaternion.Euler(0,0,0), transform) as GameObject;
            if(direction)
            {
                pigeon.GetComponent<SpriteRenderer>().flipX = false;
            }
            else pigeon.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void OnDisable()
    {
        if(transform.childCount > 0)
        {
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
