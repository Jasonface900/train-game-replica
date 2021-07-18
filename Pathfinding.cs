using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {
    #region Singleton
    public static Pathfinding instance;

    void Awake() {
        if(instance != null) {
            Debug.LogWarning("There is more than one Pathfinding in scene\nSelf Destruct activated");

            Destroy(this);
        }
        else {
            instance = this;
        }
    }
    #endregion
    public GameObject[,,] Rails;
    public Vector3 WorldPosition;
    public Vector3 StartPosition;
    public Vector3 EndPosition;
    //path list
    //train list with ids

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    public void SetARRAY(Vector3 vector) {
        Rails = new GameObject[(int)(vector.x), (int)(vector.y), (int)(vector.z)];
    }
    public void Add(float x,float y,float z, GameObject game,string name) {
        if(name.ToLower()=="rails") {
            Rails[(int)x, (int)y, (int)z] = game;
        }
    }
    public bool check_Postition(Vector3 vector) {
        if(Rails[(int)(vector.x), (int)(vector.y), (int)(vector.z)] != null) {
            return true;
        }
        else
            return false;
    }
}