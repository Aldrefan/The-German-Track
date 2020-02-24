using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigurantSpawner : MonoBehaviour
{
    [SerializeField]
    private bool spawnAtStart;
    [SerializeField]
    private List<GameObject> figurants;
    [SerializeField]
    private Vector2 size;
    [SerializeField]
    private int layer;
    [SerializeField]
    private Vector2 spawnOffset;
    [SerializeField]
    private bool hasAShadow;
    [SerializeField]
    private GameObject shadow;

    void OnEnable()
    {
        if(spawnAtStart)
        {
            SpawnFigurant();
        }
    }
    
    void OnDisable()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    void SpawnFigurant()
    {
        int newFigurant = Random.Range(0, figurants.Count);

        GameObject spawnedFigurant = Instantiate(figurants[newFigurant], new Vector2(transform.position.x + spawnOffset.x, transform.position.y + spawnOffset.y), transform.rotation, transform);
        spawnedFigurant.GetComponent<FigurantMovement>().enabled = false;
        if(hasAShadow)
        {
            GameObject spawnedShadow = Instantiate(shadow, new Vector2(transform.position.x + spawnOffset.x, transform.position.y + spawnOffset.y), transform.rotation, transform);
            spawnedShadow.GetComponent<SpriteRenderer>().sprite = spawnedFigurant.GetComponent<SpriteRenderer>().sprite;
            spawnedShadow.GetComponent<Shadow>().owner = spawnedFigurant;
            spawnedShadow.GetComponent<Shadow>().SetOwner();
            spawnedShadow.GetComponent<Shadow>().size = new Vector2(Mathf.Abs(size.x), Mathf.Abs(size.y));
        }
        spawnedFigurant.GetComponent<SpriteRenderer>().sortingOrder = layer;
        spawnedFigurant.GetComponent<SpriteRenderer>().color = Color.white;
        spawnedFigurant.GetComponent<FigurantMovement>().isJustMoving = false;
        spawnedFigurant.transform.localScale = new Vector2(size.x, size.y);
        spawnedFigurant.GetComponent<FigurantMovement>().canDespawn = true;
        spawnedFigurant.GetComponent<FigurantMovement>().enabled = true;
    }

    IEnumerator NewFigurantCreationTime()
    {
        yield return new WaitForSeconds(0.001f);

    }

    /*void OnDisable()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }*/

    public IEnumerator NewFigurantTimer(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnFigurant();
    }
}
