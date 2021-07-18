using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorldGen : MonoBehaviour {
    public Vector3 Size;
    public GameObject ground;
    public GameObject stone;
    public Transform transform;
    public float offset;

    public float scale1;
    public float scale2;
    public float scale3;

    public float strength2;
    public float strength3;

    private void Start() {
        Pathfinding.instance.SetARRAY(Size);
        Pathfinding.instance.WorldPosition = Vector3.zero;
        Generate_World(Size);
    }

    float lerp(float t, float a, float b){
        return a + t * (b - a);
    }

    float[] Perlinate() {
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

        while(y < pixHeight) {
            float x = 0.0F;
            while(x < pixWidth) {
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
    void Generate_World(Vector3 Size) {
        float[] noise = Perlinate();
        for(int x = 0; x < Size.x; x++) {
            for(int y = 0; y < Size.y; y++) {
                for(int z = 0; z < Size.z; z++) {
                    if(y==0) {
                         

                        GameObject I = Instantiate(ground, new Vector3(x * offset, (int)(noise[x * (int)Size.x + z]*100)-30 * offset, z * offset), Quaternion.identity, transform);
                        I.GetComponentInChildren<Renderer>().material.color = new Color(.15f, noise[x * (int)Size.x + z], .1f);
                        //grid = new GridXZ<int>(1, 1, 1f, I.transform.position, ((GridXZ<int> g, int x, int z) => new int()));
                    }
                    else
                        Instantiate(stone, new Vector3(x * offset, (int)(noise[x * (int)Size.x + z]*100)-(30+y) * offset, z * offset), Quaternion.identity, transform);
                }
                

            }
        }
    }
}
