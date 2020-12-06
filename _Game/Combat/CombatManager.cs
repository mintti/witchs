using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private int _tick;
    public int Tick
    {
        get{return _tick;}
        set{_tick = value;}
    }
    public void NextTick()
    {
        Tick++;
        TickEvent();
    }

    private int _timer;
    public int Timer
    {
        get {return _timer;}
        set
        {
            if(value % Interval == 0)
            {
                _timer = 0;
                NextTick();
                return;
            }
            _timer = value;
        }
    }    

    private int _interval;
    public int Interval
    {
        get{return _interval;}
        set{_interval = value;}
    }

    public void TickEvent()
    {

    }

    public void Stop()
    {
        
    }

    public void Start()
    {
        Tick = 0;
        Interval = 0;
        Timer = 0;

        
    }


    
}
