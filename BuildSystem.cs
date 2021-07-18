using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildSystem : MonoBehaviour{

    #region Singleton
    public static BuildSystem instance;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("There is more than one StoryManager in scene\nSelf Destruct activated");

            Destroy(this);
        }
        else {
            instance = this;
        }
    }
    #endregion

    public Item held_Object;

    private Shop shop;
    private void Start() {
        shop = GetComponent<Shop>();
    }
    public void Rail() {
        //buy rail
        //addrail to hand
        held_Object=shop.Buy("Rails");
    }
}

