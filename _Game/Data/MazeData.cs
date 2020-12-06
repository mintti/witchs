using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RandomMapMaker;

public class MazeData : MonoBehaviour
{
    private Map _map;
    public Map Map
    {
        get{return _map;}
    }
    private void Start()
    {

    }

    public void SetMap()
    {
        _map = Program.MapMaker(20, 50);
    }

    public void Reset()
    {
        _map = null;
    }
}
