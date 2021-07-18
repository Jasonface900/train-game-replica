using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {
    #region Singleton
    public static Pathfinding instance;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("There is more than one Pathfinding in scene\nSelf Destruct activated");

            Destroy(this);
        }
        else {
            instance = this;
        }
    }
    #endregion
    public List<GameObject> Rails;
    //path list
    //train list with ids

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
