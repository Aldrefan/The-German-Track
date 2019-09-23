using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightLight : MonoBehaviour
{
    public enum timeEnum
    {
        Day,
        Night
    }
    public timeEnum time;

    public List<GameObject> backgroundsInAWindowDay;
    public List<GameObject> backgroundsInAWindowNight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DayTime()
    {
        time = timeEnum.Day;
        for(int i = 0; i < backgroundsInAWindowDay.Count; i++)
        {
            backgroundsInAWindowDay[i].SetActive(true);
        }
        for(int i = 0; i < backgroundsInAWindowNight.Count; i++)
        {
            backgroundsInAWindowNight[i].SetActive(false);
        }
    }

    public void NightTime()
    {
        time = timeEnum.Day;
        for(int i = 0; i < backgroundsInAWindowNight.Count; i++)
        {
            backgroundsInAWindowNight[i].SetActive(true);
        }
        for(int i = 0; i < backgroundsInAWindowDay.Count; i++)
        {
            backgroundsInAWindowDay[i].SetActive(false);
        }
    }
}
