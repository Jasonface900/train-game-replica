//this class should only ever hold and return data
//if any data needs to be created or modified, it should not be in here
public class VoxelData{
    Voxel[,,] voxels;

    public VoxelData(){
        voxels = new Voxel[,,]{
            {{new Voxel(BlockType.Air),new Voxel(BlockType.Grass),new Voxel(BlockType.Air)},{new Voxel(BlockType.Grass),new Voxel(BlockType.Grass),new Voxel(BlockType.Grass)},{new Voxel(BlockType.Grass),new Voxel(BlockType.Air),new Voxel(BlockType.Grass)}},
            {{new Voxel(BlockType.Grass),new Voxel(BlockType.Grass),new Voxel(BlockType.Grass)},{new Voxel(BlockType.Grass),new Voxel(BlockType.Grass),new Voxel(BlockType.Grass)},{new Voxel(BlockType.Grass),new Voxel(BlockType.Air),new Voxel(BlockType.Grass)}},
            {{new Voxel(BlockType.Air),new Voxel(BlockType.Air),new Voxel(BlockType.Air)},{new Voxel(BlockType.Grass),new Voxel(BlockType.Air),new Voxel(BlockType.Grass)},{new Voxel(BlockType.Grass),new Voxel(BlockType.Grass),new Voxel(BlockType.Grass)}}
        };
    }

    public VoxelData(Voxel[,,] voxels){
        this.voxels = voxels;
    }

    public int Width{
        get{
            return voxels.GetLength(0);
        }
    }

    public int Height{
        get{
            return voxels.GetLength(1);
        }
    }

    public int Depth{
        get{
            return voxels.GetLength(2);
        }
    }

    public Voxel SetCell(int x, int y, int z){
        Voxel oldCell = GetCell(x,y,z);
        voxels[x,y,z] = new Voxel(BlockType.Air);
        return oldCell;
    }

    public Voxel GetCell(int x, int y, int z){
        return voxels[x, y, z];
    }

    public Voxel GetNeighbor(int x, int y, int z, Direction dir){
        DataCoordinate offsetToCheck = offsets[(int)dir];
        DataCoordinate neighborCoord = new DataCoordinate(x+offsetToCheck.x, y+offsetToCheck.y, z+offsetToCheck.z);

        if(neighborCoord.x < 0 || neighborCoord.x >= Width || 
           neighborCoord.y < 0 || neighborCoord.y >= Height|| 
           neighborCoord.z < 0 || neighborCoord.z >= Depth  )
            return new Voxel(BlockType.Air);
        else
            return GetCell(neighborCoord.x, neighborCoord.y, neighborCoord.z);
    }

    public bool CheckIfNoNeighbors(int x, int y, int z){
        for(int i = 0; i < 6; i++){
            if(GetNeighbor(x,y,z,(Direction)i).Equals(new Voxel(BlockType.Air)))
                return true;
        }
        return false;
    }

    struct DataCoordinate{
        public int x;
        public int y;
        public int z;

        public DataCoordinate(int x, int y, int z){
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    DataCoordinate[] offsets = {
        new DataCoordinate(0, 0, 1),
        new DataCoordinate(1, 0, 0),
        new DataCoordinate(0, 0, -1),
        new DataCoordinate(-1, 0, 0),
        new DataCoordinate(0, 1, 0),
        new DataCoordinate(0,-1, 0)
    };
}

public enum Direction{
    North,
    East,
    South,
    West,
    Up,
    Down
}

public enum BlockType{
    Air,
    Grass,
    Stone,
    Dirt,
    Sand,
    Water
}