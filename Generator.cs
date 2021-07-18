using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour{
    public float timer;
    public int time_in_Seconds;
    public int stored;
    //type of obejct class 
    void Start() {
        timer = time_in_Seconds;
    }
    void Update() {
        if(timer>0) {
            timer -= (1*Time.deltaTime);
        }
        else {
            stored++;
            timer = time_in_Seconds;
        }
    }
    //public in
    //public int Give_ITems(Storage storage) {
        
    //}
    //public Ressources Give
}
