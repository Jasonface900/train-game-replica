using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class texting : MonoBehaviour
{
    private Grid<StringObject> Stringgrid;
    private GridXZ<int> GridXZ;
    // Start is called before the first frame update
    void Start()
    {
        //GridXZ = new GridXZ<int>(20,20,4f,Vector3.zero,((GridXZ<int> g,int x, int z)=> new int()));
        //Stringgrid = new Grid<StringObject>(20,20,10f,Vector3.zero,(Grid<StringObject> g,int x, int y) => new StringObject(g,x,y));
        // grid = new Grid<bool>(10,10,10.0f,transform.position,() => new bool());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Position = UtilsClass.GetMouseWorldPositionWithZ();
        //if(Input.GetMouseButtonDown(0)) {
        //    grid.SetGridObject(UtilsClass.GetMouseWorldPositionWithZ(),());
        //}
        //if(Input.GetKeyDown(KeyCode.A)) {Stringgrid.GetGridObject(Position).AddLetter("A");}
        //if(Input.GetKeyDown(KeyCode.B)) {Stringgrid.GetGridObject(Position).AddLetter("B");}
        //if(Input.GetKeyDown(KeyCode.C)) {Stringgrid.GetGridObject(Position).AddLetter("C");}

        //if(Input.GetKeyDown(KeyCode.Alpha1)) {Stringgrid.GetGridObject(Position).AddNumber("1");}
    }

    
}
public class StringObject{
    private Grid<StringObject> grid;
    private int x;
    private int y;

    private string letters; 
    private string numbers;

    public StringObject(Grid<StringObject> grid, int x, int y) {
        this.grid = grid;
        this.x = x;
        this.y = y;
        letters = "";
        numbers = "";
    }

    public void AddLetter(string letter) {
        letters += letter;
        grid.TriggerGridObjectChange(x,y);
    }

    public void AddNumber(string number) {
        numbers += number;
        grid.TriggerGridObjectChange(x, y);

    }

    public override string ToString() {
        return letters + "\n" + numbers;
    }
}
