using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// A class that creates a que in so that multiple AI agents arn not on top of each other when finding thier paths
/// If two agents request the exact same path only one of them will be able to take it
/// </summary>
public class PathRequestManager : MonoBehaviour
{
    //creates a que of path requests
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    //creates an instance of the class to use
    static PathRequestManager instance;
    Pathfinding pathfinding;

    //boolean in order to keep the que in line/order
    bool isProcessingPath;

    private void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> Callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, Callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    /// <summary>
    /// method to make sure that a path is not currently being processed, before moving onto the next path
    /// and checking to make sure there is a next path TO process
    /// </summary>
    private void TryProcessNext()
    {
        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    /// <summary>
    /// takes the path that is beinb processsed and returns wether it was available(successful)
    /// </summary>
    /// <param name="path">The path in question</param>
    /// <param name="success">Wether or not the path is available</param>
    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.Callback(path, success);
        //returns to the state of not processing anything
        isProcessingPath = false;
        //moves on to next process
        TryProcessNext();
    }

    //data structure that contains the start of the path, the end of the path, and the returning of it's availability
    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> Callback;

        public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> callback)
        {
            pathStart = start;
            pathEnd = end;
            Callback = callback;
        }
    }


}
