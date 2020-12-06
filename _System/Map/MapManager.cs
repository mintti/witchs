using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] Objects;

    private void Start()
    {
        Objects[0].GetComponent<ObjEvent>().SetText(this, "Craft Shop");
        Objects[1].GetComponent<ObjEvent>().SetText(this, "Feild");
        Objects[2].GetComponent<ObjEvent>().SetText(this, "Virtual Forest");
        Objects[3].GetComponent<ObjEvent>().SetText(this, "Maze");
    }

    public void GetCommand(string name)
    {
        Debug.Log(name);
    }
}
