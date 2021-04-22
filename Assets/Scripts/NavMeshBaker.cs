using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    public NavMeshSurface surface;
    private bool once = true;
    private float waitTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        surface = surface.GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        if(once & waitTime < Time.time)
        {
            surface.BuildNavMesh();
            once = false;
        }
    }
}
