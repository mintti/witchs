using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    #region System
    #endregion
    #region PlayerData
    
    private int day;
    public int Day{get{return day;} set{day = value;}}
    private int gold;
    public int Gold{get{return gold;}set{gold = value;}}


    //Skill
    public float Interval;

    #endregion

    
    public void SetInitial()
    {
        Interval = 0.2f;
        day = 1;
        gold = 0;
    }


    private void Awake() {
        gameObject.SendMessage("TimeSetting", Interval);
    }
}
