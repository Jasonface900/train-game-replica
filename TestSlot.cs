using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSlot : MonoBehaviour {
    private Item item;
    public Vector3 offset;

    public Color hoverColor;
    private MeshRenderer rend;
    private Color StartColor;

    private void OnMouseDown() {
        Debug.Log("pointer down");

        if(BuildSystem.instance.held_Object != null && item == null) {
            Debug.Log("reached the instanci");
            Instantiate(BuildSystem.instance.held_Object.cross, offset + gameObject.transform.position, Quaternion.identity);
        }
    }

    public void Start() {
        rend = GetComponent<MeshRenderer>();
        StartColor = rend.material.color;
        Debug.Break();
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
