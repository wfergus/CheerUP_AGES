using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public float elapsedTime;


    public Transform playerTransform;

    public Vector3 destPos;

    public GameObject[] pointList;

    public virtual void Initialize() { }
    public virtual void FSMUpdate() { }
    public virtual void FSMFixedUpdate() { }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        FSMUpdate();
    }

    void FixedUpdate()
    {
        FSMFixedUpdate();
    }
}
