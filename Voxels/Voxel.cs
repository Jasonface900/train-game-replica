using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Voxel{
    
    BlockType type;
    Mesh mesh;

    public Voxel(BlockType type){
        mesh = null;
        if(type != BlockType.Air)
            mesh = Resources.Load("Objects/TestVoxel") as Mesh;
        this.type = type;
    }

    public override string ToString(){
        return type.ToString() + " type block.";
    }

    public override bool Equals(object obj){
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        } else {
            Voxel voxel = (Voxel)obj;
            return voxel.type == type;
        }
    }
}