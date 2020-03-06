using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KenMovementsLvl1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTimerBeforeCheckActivation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartTimerBeforeCheckActivation()
    {
        if(GetComponent<Interactions>().PnjMet.Contains("Clara"))
        {
            GetComponent<MovementsPlayer>().canMove = true;
        }
        else 
        {
            yield return new WaitForSeconds(1);
            GetComponent<MovementsPlayer>().canMove = true;
        }
    }
}
