using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerate : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        StartCoroutine(CreateShape());
        UpdateMesh();

        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    void Update()
    {
        UpdateMesh();
    }
    IEnumerator CreateShape()
    {
        vertices = new Vector3[(xSize+1) * (zSize+1)];
        print(vertices.Length);


        for (int z = 0, i = 0; z < zSize; z++)
        {
                for (int x = 0; x <= xSize; x++)
                {
                float y = Mathf.PerlinNoise(x* .3f, z* .3f) * 2f;
                vertices[i] = new Vector3(x, y, z);
                i++;
                }
            }
        triangles = new int[6 * (xSize * zSize)];
        for (int z = 0, vert = 0, tris = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
                yield return new WaitForSeconds(.01f);
            }
            vert++;
        }

    }
    
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
    

    private void OnDrawGizmoes()
    {
        if (vertices == null)
        {
            print("There no spheres");
            return;
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            print("thems the spheres");
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
}
