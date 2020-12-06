using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_UI : MonoBehaviour
{
    public Scheduler _scheduler;
    
    public Text timeText;
    public Text system;

    public void Awake()
    {
        _scheduler.Time_Event += Update_Text;
    }

    private void Update_Text(Infra.Time time)
    {
        timeText.text = string.Format("{0}:{1}", time.Hour < 10 ? $"0{time.Hour}" : $"{time.Hour}", time.Minute < 10 ? $"0{time.Minute}" : $"{time.Minute}");
    }
    
}
