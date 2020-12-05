using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Cooking
{
    public class Recipe
    {
        public List<Recipe> Tail;
        public int _value;
        public int _result;

        public Recipe(int value)
        {
            _value = value;
        }

        public void Add(List<int> list, int res)
        {
            _value = list[0];
            list.RemoveAt(0);

            if(list.Count == 0)
                _result = res;
            else
            {
                if(Tail.Find(e => e._value.Equals(list[0])).Equals(null))
                {
                    Tail.Add(new Recipe(list[0]));
                }
                Tail.Find(e => e._value.Equals(list[0])).Add(list, res);
            }
        }
        
        public int Get(List<int> list)
        {
            Recipe node = Tail.Find(e => e._value.Equals(list[0]));
            list.RemoveAt(0);

            if(node.Equals(null)) return -1;

            if(list.Count > 1)
                return node.Get(list);
            else return node._result;
        }
    }
}