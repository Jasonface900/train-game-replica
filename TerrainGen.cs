using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class TerrainGen : MonoBehaviour {
    public Vector3 Size;
    public GameObject collider;
    public Transform transform;
    public float scale1;
    public float scale2;
    public float scale3;

    public float strength2;
    public float strength3;

    private Vector3[] verts;
    private int[] tris;

    private MeshRenderer mrend;
    private MeshFilter mfilt;
    private Mesh mesh;

    private void Start() {
        mrend = GetComponent<MeshRenderer>();
        mfilt = gameObject.AddComponent<MeshFilter>();
        mesh = new Mesh();
        MeshData meshdata = MeshGenerator.GenerateTerrainMesh(Perlinate(), Size);
        DrawMesh(meshdata);
        CreateColliders(meshdata);
    }

    public void DrawMesh(MeshData meshData){
        mfilt.sharedMesh = meshData.CreateMesh();
        mrend.sharedMaterial = mrend.material;
    }

    public void CreateColliders(MeshData meshData){
        Vector3[] verts = meshData.verts;

        int w = (int)Size.x*3;
        int h = (int)Size.z*3;

        /*foreach(Vector3 vert in verts){
            float x = vert.x + .5f;
            float y = vert.y + .5f;
            float z = vert.z + .5f;

            if (x < w - 1 && y < h - 1){
                    GameObject I = Instantiate(collider, new Vector3(x,y,z), Quaternion.identity, transform);
                }
            //GameObject I = Instantiate(collider, vert, Quaternion.identity, transform);
        }
        */
        for(int y = 0; y < h; y+=3){
            for(int x = 0; x < w; x+=3){
                if (x < w*4 - 1 && y < h*4 - 1){
                    GameObject I = Instantiate(collider, verts[y * h + x], Quaternion.identity, transform);
                }
            }
        }
        
    }

    float lerp(float t, float a, float b){
        return a + t * (b - a);
    }

    float[] Perlinate(){
        // Width and height of the texture in pixels.
        int pixWidth  = (int)Size.x;
        int pixHeight = (int)Size.z;

        // The origin of the sampled area in the plane.
        float xOrg = 0;
        float yOrg = 0;

        // The number of cycles of the basic noise pattern that are repeated
        // over the width and height of the texture.
        //float scale1 = .2F;
        //float scale2 = .5F;
        //float scale3 = .8F;

        //float strength1 = .8F;
        //float strength2 = .4F;
        //float strength3 = .1F;

        float[] pix;
        pix = new float[pixWidth * pixHeight];

        // For each pixel in the texture...
        float y = 0.0F;

        while (y < pixHeight)
        {
            float x = 0.0F;
            while (x < pixWidth)
            {
                float x1 = xOrg + x / pixWidth * scale1;
                float x2 = xOrg + x / pixWidth * scale2;
                float x3 = xOrg + x / pixWidth * scale3;

                float y1 = yOrg + y / pixHeight * scale1;
                float y2 = yOrg + y / pixHeight * scale2;
                float y3 = yOrg + y / pixHeight * scale3;

                float s1 = Mathf.PerlinNoise(x1, y1);
                float s2 = Mathf.PerlinNoise(x2, y2);
                float s3 = Mathf.PerlinNoise(x3, y3);

                s2 = lerp(strength2, s2, s1);
                float sample = lerp(strength3, s3, s2);

                pix[(int)y * pixWidth + (int)x] = sample;

                x++;
            }
            y++;
        }
        return pix;
    }
}

public static class MeshGenerator {
    public static MeshData GenerateTerrainMesh(float[] heightmap, Vector3 size){
        int w = (int)size.x;
        int h = (int)size.z;
        float topLeftX = (w-1) / -2f;
        float topLeftZ = (h-1) /  2f;

        MeshData meshdata = new MeshData(w, h);
        int vertIndex = 0;

        for(int y = 0; y < h; y++){
            for(int x = 0; x < w; x++){
                meshdata.verts[vertIndex] = new Vector3(topLeftX + x, (int)(heightmap[x * w + y]*100)-30, topLeftZ - y);
                meshdata.uvs[vertIndex] = new Vector2(x / (float)w,y / (float)h);

                if (x < w - 1 && y < h - 1){
                    meshdata.AddTris(vertIndex, vertIndex+w+1, vertIndex + w);
                    meshdata.AddTris(vertIndex+w+1, vertIndex, vertIndex + 1);
                }

                vertIndex++;
            }
        }

        //possibly sharpens stuff????
        
        
        
        return meshdata;

    }

}

public class MeshData {
    public Vector3[] verts;
    public int[] tris;
    public Vector2[] uvs;

    int trisIndex;

    public MeshData(int meshW, int meshH){
        verts = new Vector3[(meshW-1) * (meshH-1) * 6];
        uvs = new Vector2[(meshW-1) * (meshH-1) * 6];
        tris = new int[(meshW-1) * (meshH-1) * 6];
        trisIndex = 0;
    }

    public void AddTris(int a, int b, int c){
        tris[trisIndex]   = a;
        tris[trisIndex+1] = b;
        tris[trisIndex+2] = c;
        trisIndex += 3;
    }

    public void SharpenMesh(){
        Vector3[] sharpverts = new Vector3[tris.Length];
        int[] sharptris = new int[tris.Length];

        for(int i = 0; i < tris.Length; i++){
            sharpverts[i] = verts[tris[i]];
            sharptris[i] = i;
        }

        verts = sharpverts;
        tris = sharptris;
    }

    public Mesh CreateMesh(){
        Mesh mesh = new Mesh();
        SharpenMesh();
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
