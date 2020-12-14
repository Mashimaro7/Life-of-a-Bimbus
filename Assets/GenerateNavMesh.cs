using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateNavMesh : MonoBehaviour
{
    NavMeshSurface surface;
    
    void Awake()
    {
        surface = GameObject.Find("Navmesh").GetComponent<NavMeshSurface>();
        StartCoroutine(GenerateNavMeshes());
    }


    IEnumerator GenerateNavMeshes()
    {
        yield return new WaitForSeconds(2);
        surface.BuildNavMesh();
    }
}
