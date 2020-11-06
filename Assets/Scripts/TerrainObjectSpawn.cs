using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainObjectSpawn : MonoBehaviour
{
    public MeshRenderer terrain;
    private float meshW, meshL;
    private bool init;
    public int numObjectsToSpawn;
    public GameObject[] objects;

    void Start()
    {
        meshW = terrain.bounds.size.x;
        meshL = terrain.bounds.size.z;
        if (!init)
        {
            for (int i = 0; i < numObjectsToSpawn; i++)
            {
                int random = Random.Range(0, objects.Length);
                float randX = Random.Range(-meshW, meshW);
                float randZ = Random.Range(-meshL, meshL);
                Vector3 spawnPos = new Vector3(randX,0,randZ);
                Instantiate(objects[random], spawnPos,Quaternion.identity);
            }
        }
    }

}
