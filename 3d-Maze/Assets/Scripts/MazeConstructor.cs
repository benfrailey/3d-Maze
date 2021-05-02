using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MazeConstructor : MonoBehaviour
{
    //1
    public bool showDebug;
    private MazeDataGenerator dataGenerator;
    private MazeMeshGenerator meshGenerator;


    
    [SerializeField] private Material mazeMat1;
    [SerializeField] private Material mazeMat2;
    [SerializeField] private Material startMat;
    [SerializeField] private Material treasureMat;

    //2
    public int[,] data
    {
        get; private set;
    }
    public float hallWidth
    {
    get; private set;
    }
    public float hallHeight
    {
    get; private set;
    }
    public int startRow
    {
        get; private set;
    }
    public int startCol
    {
        get; private set;
    }
    public int goalRow
    {
        get; private set;
    }
    public int goalCol
    {
        get; private set;
    }
    
    public int numGoals = 500;


    //3
    void Awake()
    {
        dataGenerator = new MazeDataGenerator();
        meshGenerator = new MazeMeshGenerator();
    }
    
    public void GenerateNewMaze(int sizeRows, int sizeCols,TriggerEventHandler startCallback=null, TriggerEventHandler goalCallback=null)
    {
        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
        {
            Debug.LogError("Odd numbers work better for dungeon size.");
        }

        DisposeOldMaze();

        data = dataGenerator.FromDimensions(sizeRows, sizeCols);
        

        // store values used to generate this mesh
        hallWidth = meshGenerator.width;
        hallHeight = meshGenerator.height;

        DisplayMaze();

        FindStartPosition();
        PlaceStartTrigger(startCallback);
        
        int goalsPlaced = 0;
        
        while(goalsPlaced < numGoals){
          FindGoalPosition();
          PlaceGoalTrigger(goalCallback);
          goalsPlaced++;
        }
    }


    private void DisplayMaze()
    {
        GameObject go = new GameObject();
        go.transform.position = Vector3.zero;
        go.name = "Procedural Maze";
        go.tag = "Generated";

        MeshFilter mf = go.AddComponent<MeshFilter>();
        mf.mesh = meshGenerator.FromData(data);
        
        MeshCollider mc = go.AddComponent<MeshCollider>();
        mc.sharedMesh = mf.mesh;

        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        mr.materials = new Material[2] {mazeMat1, mazeMat2};
    }

    public void DisposeOldMaze()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Generated");
        foreach (GameObject go in objects) {
            Destroy(go);
        }
    }

    private void FindStartPosition()
    {
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    startRow = i;
                    startCol = j;
                    return;
                }
            }
        }
    }

    private void FindGoalPosition()
    {
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);
        
        List<Vector2> openList = new List<Vector2>();

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                  openList.Add(new Vector2(i,j));
                }
            }
        }
        
        if(openList.Count == 0){
          return;
        }
      int randomIndex = (int) (openList.Count * Random.Range(0f,1f));

      goalRow = (int)openList[randomIndex].x;
      goalCol = (int)openList[randomIndex].y;
      
      maze[(int)openList[randomIndex].x, (int)openList[randomIndex].y] = 1;

      return;
        
    }

    private void PlaceStartTrigger(TriggerEventHandler callback)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.position = new Vector3(startCol * hallWidth, .5f, startRow * hallWidth);
        go.name = "Start Trigger";
        go.tag = "Generated";

        go.GetComponent<BoxCollider>().isTrigger = true;
        go.GetComponent<MeshRenderer>().sharedMaterial = startMat;

        TriggerEventRouter tc = go.AddComponent<TriggerEventRouter>();
        tc.callback = callback;
    }

    private void PlaceGoalTrigger(TriggerEventHandler callback)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.position = new Vector3(goalCol * hallWidth, .5f, goalRow * hallWidth);
        go.name = "Treasure";
        go.tag = "Generated";

        go.GetComponent<BoxCollider>().isTrigger = true;
        go.GetComponent<MeshRenderer>().sharedMaterial = treasureMat;

        TriggerEventRouter tc = go.AddComponent<TriggerEventRouter>();
        tc.callback = callback;
    }

}