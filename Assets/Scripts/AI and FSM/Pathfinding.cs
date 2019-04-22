using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Pathfinding : MonoBehaviour
{
    PathRequestManager requestManager;
    Grid grid;

    //gets the Grid and PathRequestManager components
    void Awake()
    {
        grid = GetComponent<Grid>();
        requestManager = GetComponent<PathRequestManager>();
    }

    //finds path with using a coroutine for use with multiple agents
    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }
    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        // makes nodes out of the start and target vector 3s
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        if (startNode.IsWalkable && targetNode.IsWalkable)
        {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            //HashSet used to avoid duplicates in the closed set
            HashSet<Node> closedSet = new HashSet<Node>();

            //adds the starting position to the open set
            openSet.Add(startNode);

            //while there are nodes in the open set
            while (openSet.Count > 0)
            {
                //removes the current node from open set
                Node currentNode = openSet.RemoveFirst();
                //adds current node to closed set
                closedSet.Add(currentNode);
                //checks if the agent has reached its target 
                if (currentNode == targetNode)
                {
                    //if target reached, the path was a success and breaks
                    pathSuccess = true;
                    break;
                }
                //gets the nodes around the current node on the grid
                foreach (Node neighbor in grid.GetNeighbors(currentNode))
                {
                    //checks if the neighbor node is walkable OR already closed
                    if (!neighbor.IsWalkable || closedSet.Contains(neighbor))
                    {
                        continue;
                    }
                    int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                    if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = newMovementCostToNeighbor;
                        neighbor.hCost = GetDistance(neighbor, targetNode);
                        neighbor.parent = currentNode;

                        if (!openSet.Contains(neighbor))
                            openSet.Add(neighbor);
                    }
                }
            }
        }
        yield return null;
        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    Vector3[] RetracePath(Node startingNode, Node endingNode)
    {
       List<Node> path = new List<Node>();
       Node currentNode = endingNode;

       while (currentNode != startingNode)
       {
           path.Add(currentNode);
           currentNode = currentNode.parent;
       }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].GridX - path[i].GridX, path[i - 1].GridY - path[i].GridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i].WorldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    //Finds distance between nodes (10 on NSEW axis and 14 on diagonal)
    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int distY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        if (distX > distY)
           return 14 * distY + 10 * (distX - distY);
        return 14 * distX + 10 * (distY - distX);
    }
        
}

