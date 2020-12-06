using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjEvent : MonoBehaviour
{
    private MapManager _manager;
    private string _name;
    public string Name
    {
        get{return _name;}
        set
        {
            _name = value;
            objText.text = _name;
        }
    }
    public Text objText;
    
    private void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.7f;
    }

    public void SetText(MapManager obj, string name)
    {
        _manager = obj;
        Name = name;
    }   

    public void SendCommand()
    {
        _manager.GetCommand(Name);
    }
}
