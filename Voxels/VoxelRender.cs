using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class VoxelRender : MonoBehaviour {

    Mesh mesh;
    Transform root;
    List<Vector3> vertices;
    List<int> triangles;
    GameObject[,,] colliders;
    List<Vector3Int> collidersHit;
    VoxelData lastData;

    public float scale = 1f;
    public float octave = 2f;
    float oldOctave;
    float adjScale;
    float oldScale;

    public Vector3Int Size;
    Vector3Int oldSize;
    
    void Awake(){
        mesh = GetComponent<MeshFilter>().mesh;
        root = GetComponent<Transform>();
        colliders = new GameObject[Size.x, Size.y, Size.z];
        collidersHit = new List<Vector3Int>();
        adjScale = scale * 0.5f;

        oldOctave = octave;
        oldSize = Size;
        oldScale = scale;
    }

    void Start(){
        VoxelData data = new ProceduralTerrain(Size, octave).GenerateTerrain();
        lastData = data;
        //GenerateVoxelMesh(data);
        //Debug.Log("Generated Voxel Mesh");
        UpdateColliderObjects(data);
        Debug.Log("Updated Collider Objects");
        //UpdateMesh();
        //Debug.Log("Updated Mesh");
    }

    void Update(){
        if(octave != oldOctave || Size != oldSize || scale != oldScale){
            adjScale = scale * 0.5f;

            VoxelData data = new ProceduralTerrain(Size, octave).GenerateTerrain();
            colliders = new GameObject[Size.x, Size.y, Size.z];

            //GenerateVoxelMesh(data);
            //UpdateMesh();
            destroyOldColliderObjects();
            UpdateColliderObjects(data);

            oldOctave = octave;
            oldSize = Size;
            oldScale = scale;
            
            lastData = data;
        }

        if(collidersHit.Count > 0){
            foreach(Vector3Int voxel in collidersHit){
                lastData.SetCell(voxel.x, voxel.y, voxel.z);
            }
            //GenerateVoxelMesh(lastData);
            //UpdateMesh();
            UpdateColliderObjects(lastData);
            collidersHit = new List<Vector3Int>();
        }
    }

    public void ColliderHit(Vector3Int index){
        collidersHit.Add(index);
        Debug.Log("Collider at index " + index + " was just hit!");
    }

    void UpdateColliderObjects(VoxelData data){
        for(int z = 0; z < data.Depth; z++){
            for(int y = 0; y < data.Height; y++){
                for(int x = 0; x < data.Width; x++){
                    if(data.GetCell(x,y,z).Equals(new Voxel(BlockType.Air))){
                        if(colliders[x,y,z])
                            GameObject.Destroy(colliders[x,y,z]);
                        continue;
                    }
                    if(data.CheckIfNoNeighbors(x,y,z) &&  !colliders[x,y,z]){
                        GameObject collider;
                        collider = MakeColliderObject(new Vector3((float)x * scale, (float)y * scale, (float)z * scale), scale);
                        collider.GetComponent<CollisionObject>().SetIndex(x,y,z);
                        collider.AddComponent<MeshFilter>();
                        collider.GetComponent<MeshFilter>().mesh = data.GetCell(x,y,z).GetMesh();
                        collider.AddComponent<MeshRenderer>();
                        collider.GetComponent<MeshRenderer>().material = data.GetCell(x,y,z).GetMaterial();
                        //Debug.Log(data.GetCell(x,y,z).GetMaterial());
                        colliders[x,y,z] = collider;
                    }
                }
            }
        }
    }

    GameObject MakeColliderObject(Vector3 colliderPos, float colliderScale){
        GameObject collider = new GameObject("Collider ("+(int)colliderPos.x+", "+(int)colliderPos.y+", "+(int)colliderPos.z+")", typeof(BoxCollider));
        collider.AddComponent<CollisionObject>();
        collider.GetComponent<BoxCollider>().center = new Vector3(0,.5f, 0);
        Transform transform = collider.GetComponent<Transform>();
        transform.SetParent(root);
        transform.localPosition = new Vector3(0,0,0);
        transform.Translate(colliderPos);
        transform.localScale = new Vector3(colliderScale, colliderScale, colliderScale);
        return collider;
    }

    void GenerateVoxelMesh(VoxelData data){
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for(int z = 0; z < data.Depth; z++){
            for(int y = 0; y < data.Height; y++){
                for(int x = 0; x < data.Width; x++){
                    if(data.GetCell(x,y,z).Equals(new Voxel(BlockType.Air)))
                        continue;
                    MakeCube(adjScale, new Vector3((float)x * scale, (float)y * scale, (float)z * scale), x, y, z, data);
                }
            }
        }
    }

    void MakeCube(float cubeScale, Vector3 cubePos, int x, int y, int z, VoxelData data){
        for(int i = 0; i < 6; i++){
            if(data.GetNeighbor(x,y,z,(Direction)i).Equals(new Voxel(BlockType.Air))){
                MakeFace((Direction)i, cubeScale, cubePos);
            }
        }
    }

    void MakeFace(Direction dir, float faceScale, Vector3 facePos){
        vertices.AddRange(CubeMeshData.faceVertices(dir, faceScale, facePos));
        int vCount = vertices.Count;

        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 1);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4 + 3);
    }

    void UpdateMesh(){
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

    void destroyOldColliderObjects(){
        foreach(Transform child in this.GetComponentsInChildren<Transform>()){
            if(child.Equals(root))
                continue;
            else
                GameObject.Destroy(child.gameObject);
        }
    }   
}