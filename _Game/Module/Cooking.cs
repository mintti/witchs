using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model.Cooking;

public class Cooking : MonoBehaviour
{

    private Recipe Recipes;
    // Start is called before the first frame update
    void Start()
    {
        SetRecipe();
    }

    public void SetRecipe()
    {
        Recipes.Add(new List<int>(){1, 2, 3},10);
    }

    public int Cook(List<int> ele)
    {
        ele.Sort();

        return Recipes.Get(ele);
    }
}