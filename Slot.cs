using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {
    private Item item;
    public Vector3 offset;

    public Color hoverColor;
    private Renderer rend;
    private Color StartColor;

    public void OnMouseDown() {
        Debug.Log("pointer down");

        if(BuildSystem.instance.held_Object != null && item == null && BuildSystem.instance.held_Object.name != "") {
            Debug.Log("reached the instanci");
            Instantiate(BuildSystem.instance.held_Object.cross, offset + gameObject.transform.position, Quaternion.identity);
        }
    }

    public void Start() {
        rend = GetComponent<Renderer>();
        StartColor = rend.material.color;
    }
    //public something golbal
    private void OnMouseEnter() {
        //make it glow white
        rend.material.color = hoverColor;
    }
    private void OnMouseExit() {
        rend.material.color = StartColor;
    }
}
