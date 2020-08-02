using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerate : MonoBehaviour
{
    Mesh mesh;
    
    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize+1) * (zSize+1)];

        for (int z = 0; z < zSize; z++)
        {

        }
    }
    
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }
    
}
