using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public List<GameObject> cars;
    public Vector2 size;
    public int layer;
    public float direction;
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        RandomNumber();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomNumber()
    {
        float delay = Random.Range(1f, 2f);
        IEnumerator coroutine = TimeBetweenCars(delay);
        StartCoroutine(coroutine);
    }

    IEnumerator TimeBetweenCars(float time)
    {
        yield return new WaitForSeconds(time);
        int car = Random.Range(0, cars.Count - 1);
        GameObject newcar = Instantiate(cars[car], transform.position, transform.rotation);
        newcar.GetComponent<SpriteRenderer>().sortingOrder = layer;
        newcar.transform.localScale = size;
        newcar.GetComponent<Car>().time = lifeTime;
        newcar.GetComponent<Car>().direction = direction;
        //newcar.transform.localScale = new Vector3();
        RandomNumber();
    }
}
