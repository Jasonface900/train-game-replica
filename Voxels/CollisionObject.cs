using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(BoxCollider))]
public class CollisionObject : MonoBehaviour {

    Vector3Int index;

    public void SetIndex(int x, int y, int z){
        SetIndex(new Vector3Int(x,y,z));
    }

    public void SetIndex(Vector3Int index){
        this.index = index;
    }

    public Vector3Int GetIndex(Vector3Int index){
        return index;
    }
    public void Awake(){

    }

    public void Start(){

    }

    public void Update(){

    }

    public void OnHit(){
        this.GetComponentInParent<VoxelRender>().SendMessage("ColliderHit", index);
    }
}