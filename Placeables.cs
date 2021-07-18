using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeables : MonoBehaviour
{
    [Tooltip("find something better")]
    public string name;
    public float x,y;
    // Start is called before the first frame update
    void Start()
    {
        
        name = gameObject.name;//find something better
        x = transform.position.x;
        y = transform.position.y;
        Pathfinding.instance.Rails.Add(this.gameObject);
    }
}
