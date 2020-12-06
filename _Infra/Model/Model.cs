using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infra
{
    public struct Time
    {
        private int hour;
        public int Hour{get{return hour;}}
        private int minute;
        public int Minute
        {
            get{return minute;}
            set{
                minute = value;
                if(minute % 60 == 0)
                {
                    hour ++;
                    minute = 0;
                }
            }
        }
        public void Reset()
        {
            hour = 0;
            minute = 0;
        }
        
        public void TickTock()
        {
            Minute += 5;
        }

    }
}