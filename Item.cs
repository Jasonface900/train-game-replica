using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]public class Item {
    public string name;
    public Image image;
    //public fahrzeuge train,car,etc
    public GameObject Straight, Curved, combined, cross, all;//somehow procedual?
    public int y_offset;
}
