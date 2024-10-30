using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResourceBehaviour : MonoBehaviour
{

    public bool _IsAlive = true;

    [SerializeField]
    private int _MAXRESOURCES;

    private int _Resources;

 

    // Start is called before the first frame update
    void Start()
    {
        _Resources = _MAXRESOURCES;
    }

    // Update is called once per frame
    void Update()
    {
        if (_IsAlive) 
        { 
            if(_Resources < 0)
            {
                _IsAlive = false;
                _Resources = 0;
                Debug.Log("boss dead");
            }
        }
    }

    public int GetResources()
    { return _Resources; }

    public void DoDamage(int damage)
    {
        _Resources -= damage;
    }

    private void Reset()
    {
        _IsAlive = true;
        _Resources = _MAXRESOURCES;
    }

    public void AddResource(int resource)
    {
        _Resources += resource;
    }
}
