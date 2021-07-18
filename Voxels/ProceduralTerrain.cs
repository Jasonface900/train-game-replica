using UnityEngine;

public class ProceduralTerrain{
    Vector3Int Size;    
    float noisescale;
    int Width;
    int Height;
    int Depth;

    public ProceduralTerrain(Vector3Int size, float noisescale){
        Size = size;
        this.noisescale = noisescale;
        Width = size.x;
        Height = size.y;
        Depth = size.z;
    }
    float lerp(float t, float a, float b){
        return a + t * (b - a);
    }

    float[] GenerateOctave(){
        // Width and height of the texture in pixels.
        int pixWidth  = Size.x;
        int pixHeight = Size.z;

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
                float x1 = xOrg + x / pixWidth * noisescale;
                float y1 = yOrg + y / pixHeight * noisescale;

                float s1 = Mathf.PerlinNoise(x1, y1);

                float sample = s1;

                pix[(int)y * pixWidth + (int)x] = sample;

                x++;
            }
            y++;
        }
        return pix;
    }

    public VoxelData GenerateTerrain(){
        float[] noise = GenerateOctave();
        Voxel[,,] voxels = new Voxel[Width,Height,Depth];

        for(int z = 0; z < Depth; z++){
            for(int x = 0; x < Width; x++){
                float noiseval = noise[z*Width + x];
                int y = (int)(noiseval * Height);
                voxels[x, y, z] = new Voxel(BlockType.Grass);
                for(int y1 = 0; y1 < y; y1++)
                    voxels[x, y1, z] = new Voxel(BlockType.Grass);
                for(int y2 = y; y2 < Height; y2++)
                    voxels[x, y2, z] = new Voxel(BlockType.Air);
            }
        }
        return new VoxelData(voxels);
    }
}
