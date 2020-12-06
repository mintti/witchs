using System.Collections;
using System.Collections.Generic;

namespace Infra
{
    public abstract class Monster
    {
        //Default State
        private int num {get; set;}
        public string name{get;set;}
        
        public int hp;
        public int power;

        public void Attack()
        {
            
        }
    }
}