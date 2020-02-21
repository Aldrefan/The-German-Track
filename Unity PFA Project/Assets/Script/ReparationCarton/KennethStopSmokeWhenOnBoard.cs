using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KennethStopSmokeWhenOnBoard : MonoBehaviour
{

    public GameObject Board;

    // Start is called before the first frame update
    void Start()
    {
        Board = GameObject.Find("BoardCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (Board != null)
        {

            if (Board.activeSelf)
            {
                this.GetComponent<ParticleSystem>().Stop();
            }
            else if (!this.GetComponent<ParticleSystem>().isEmitting)
            {
                this.GetComponent<ParticleSystem>().Play();

            }
        }
        else
        {
            Board = GameObject.Find("BoardCanvas");

        }
    }
}
