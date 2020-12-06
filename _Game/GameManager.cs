using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DataController _data;
    public int Test_Index;
    
    private void Start()
    {
        _data = gameObject.GetComponent<DataController>();
       Test_Game(); 
    }

    private void Test_Game()
    {  
        switch(Test_Index)
        {
            case 1 :
                _data.SetInitial();
                Start_Day();
                break; 
            default :
                break;
        }
    }

    public void Start_Day()
    {
        gameObject.SendMessage("Start_Scheduler");
        Debug.Log("시작");
    }

    public void End_Day()
    {
        Debug.Log("하루종료");
    }
    
}
