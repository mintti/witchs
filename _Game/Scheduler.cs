using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Infra;

public class Scheduler : MonoBehaviour
{
    public delegate void Time(Infra.Time time);
    public event Time Time_Event;

    private Infra.Time _time;
    private float _interval;
    private int _augmenter;
    

    public void TimeSetting(float interval)
    {
        
        _interval = interval;
    }

    public void Start_Scheduler()
    {
        _time.Reset();
        StartCoroutine("Timer");
    }

    public void Continue_Scheduler()
    {
        StartCoroutine("Timer");
    }

    public void End_Scheduler()
    {
        StopCoroutine("Timer");
        this.gameObject.SendMessage("End_Day");
    }
    
    IEnumerator Timer()
    {
        while(true)
        {
            _time.TickTock();
            Time_Event(_time);
            yield return new WaitForSeconds(_interval);
        }
    }
}
