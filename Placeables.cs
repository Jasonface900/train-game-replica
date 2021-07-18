using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeables : MonoBehaviour {
    void Start() {
        //if name is rail add to rail
        Pathfinding.instance.Add((transform.position.x), (transform.position.y), (transform.position.z), gameObject, "Rails");
    }
}
