using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public List<GameObject> cars;
    public List<Sprite> pilots;
    public Vector2 size;
    public int layer;
    public float direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        OpenScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenScene()
    {
        float distance = Random.Range(80, 150);
        int car = Random.Range(0, cars.Count - 1);
        int pilot = Random.Range(0, pilots.Count);
        float speed = Random.Range(0.8f, 1.2f);

        GameObject newcar = Instantiate(cars[car], new Vector2(transform.position.x, transform.position.y), transform.rotation, transform);
        newcar.transform.localPosition = new Vector2(distance * -direction, 0);
        newcar.GetComponent<SpriteRenderer>().sortingOrder = layer;
        newcar.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = pilots[pilot];
        newcar.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = layer - 1;
        newcar.transform.localScale = size;
        newcar.GetComponent<Car>().direction = direction * speed;
        RandomNumber();
    }

    void RandomNumber()
    {
        float delay = Random.Range(1f, 3f);
        IEnumerator coroutine = TimeBetweenCars(delay);
        StartCoroutine(coroutine);
    }

    IEnumerator TimeBetweenCars(float time)
    {
        yield return new WaitForSeconds(time);
        int car = Random.Range(0, cars.Count - 1);
        int pilot = Random.Range(0, pilots.Count);
        float speed = Random.Range(0.8f, 1.2f);

        GameObject newcar = Instantiate(cars[car], transform.position, transform.rotation, transform);
        newcar.GetComponent<SpriteRenderer>().sortingOrder = layer;
        newcar.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = pilots[pilot];
        newcar.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = layer - 1;
        newcar.transform.localScale = size;
        newcar.GetComponent<Car>().direction = direction * speed;
        RandomNumber();
    }
}
