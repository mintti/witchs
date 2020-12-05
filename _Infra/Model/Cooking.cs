using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Cook
{
    public class Recipe
    {
        public Node<int> Ingredients;
        public int Result;

        public Recipe(List<int> ing, int res)
        {
            Ingredients = new Node<int>();
            Ingredients.Add(ing);

            Result = res;
        }
    }
    
    public class Node<T>
    {
        public Node<T> Tail;
        public T _value;
        
        public void Add(List<T> value)
        {
            _value = value[0];
            value.RemoveAt(0);

            if(value.Count == 0)
                return;
            else
            {
                Tail = new Node<T>();
                Tail.Add(value);
            }
            
        }
    }
}