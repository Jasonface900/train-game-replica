using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Voxel{
    
    BlockType type;
    Mesh mesh;
    Material material;

    public Voxel(BlockType type){
        this.type = type;
        if(type != BlockType.Air){
            mesh = Resources.Load<Mesh>("TestVoxel");
            material = Resources.Load<Material>("Palette");
            //Debug.Log(material);
        }
        else{
            mesh = null;
            material = null;
        }
    }

    public Mesh GetMesh(){
        return mesh;
    }

    public Material GetMaterial(){
        return material;
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