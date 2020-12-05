using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model.Cook;

public class Cooking : MonoBehaviour
{

    private List<Recipe> Recipes;
    // Start is called before the first frame update
    void Start()
    {
        SetRecipe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRecipe()
    {
        Recipes.Add(new Recipe(new List<int>(){1, 2, 3}, 10));
    }

    public int? Cook(List<int> ele)
    {
        ele.Sort();
        int? result = Recipes.Find(e => e.Ingredients.Equals(ele)).Result;

        if(result == null)
            return -1;
        return result;
    }
}