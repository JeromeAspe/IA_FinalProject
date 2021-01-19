using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class FP_Grid : FP_Singleton<FP_Grid>
{

    public static event Action OnGridReady = null;
    public static event Action OnGridObstacleCheck = null;
    [SerializeField, Range(1, 10)] int nodeSize = 1;
    [SerializeField, Range(1, 100)] int xSize = 2;
    [SerializeField, Range(1, 100)] int ySize = 2;

    List<FP_Node> allNodes = new List<FP_Node>();


    public List<FP_Node> AllNodes => allNodes;


    void GenerateGrid()
    {
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                FP_Node _node = new FP_Node(transform.position.x + i, transform.position.y, transform.position.z + j);
                allNodes.Add(_node);
            }
            OnGridObstacleCheck?.Invoke();
        }
        OnGridReady?.Invoke();

    }
    public override void Awake()
    {
        base.Awake();
        OnGridReady += GetSuccessors;
    }
    private void Start()
    {
        GenerateGrid();

    }
    void GetSuccessors()
    {
        for (int i = 0; i < xSize * ySize; i++)
        {
            //En bas a droite
            if (i % ySize < ySize - 1 && i < ySize * (xSize - 1))
            {
                //Debug.Log($"{i}, {ySize + i + 1}");
                allNodes[i].AddSuccessor(allNodes[ySize + i + 1]);
            }

            //En haut a droite
            if (i % ySize < ySize - 1 && i > ySize - 1)
            {
                //Debug.Log($"{i}, {i - ySize + 1}");
                allNodes[i].AddSuccessor(allNodes[i - ySize + 1]);
            }

            //En bas a gauche
            if (i % ySize < ySize && i % ySize > 0 && i < ySize * (xSize - 1) + 1)
            {
                //Debug.Log($"{i}, {i + ySize - 1}");
                allNodes[i].AddSuccessor(allNodes[i + ySize - 1]);
            }
            //En haut a gauche
            if (i % ySize < ySize && i % ySize > 0 && i > ySize)
            {
                //Debug.Log($"{i}, {i - ySize - 1}");
                allNodes[i].AddSuccessor(allNodes[i - ySize - 1]);

            }
            //A gauche
            if (i % ySize > 0)
            {
                //Debug.Log($"{i}, {i - 1}");
                allNodes[i].AddSuccessor(allNodes[i - 1]);

            }
            //A droite
            if (i % ySize < ySize - 1)
            {
                //Debug.Log($"{i}, {i + 1}");
                allNodes[i].AddSuccessor(allNodes[i + 1]);

            }
            //En haut 
            if (i > ySize - 1)
            {
                //Debug.Log($"{i}, {i - ySize}");
                allNodes[i].AddSuccessor(allNodes[i - ySize]);

            }
            //En bas 
            if (i < ySize * (xSize - 1))
            {
                //Debug.Log($"{i}, {ySize + i}");
                allNodes[i].AddSuccessor(allNodes[ySize + i]);

            }
            //Debug.Log($"{i}, {nodes[i].Successors.Count}");

        }
    }



    private void OnDrawGizmos()
    {
        for (int z = 0; z < allNodes.Count; z++)
        {
            Gizmos.color = Color.blue;
            for (int y = 0; y < allNodes[z].Successors.Count; y++)
            {
                Gizmos.DrawLine(allNodes[z].Position, allNodes[z].Successors[y].Position);
            }
            Gizmos.DrawSphere(allNodes[z].Position, 0.1f);


        }
    }
    public void ResetGridCost()
    {
        AllNodes.ForEach(n => n.ResetCost());
    }
    public FP_Node GetNearestNode(Vector3 _position) => AllNodes.Where(n => n.IsNaviguable).OrderBy(n => Vector3.Distance(n.Position, _position)).FirstOrDefault();
    private void OnDestroy()
    {
        OnGridReady = null;
    }
    /*public void CheckForObstacle(Obstacle _obstacle)
    {
        for (int i = 0; i < AllNodes.Count; i++)
        {
            if (AllNodes[i].IsNaviguable && _obstacle.ContainsNode(AllNodes[i]))
                AllNodes[i].SetNaviguable(false);
        }
    }*/
}