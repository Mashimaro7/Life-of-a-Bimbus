using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BimbusSpawner : MonoBehaviour
{
    public GameObject bimbus;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UtilityHelper.Create(bimbus);
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            UtilityHelper.RandomPosition(bimbus,new Vector3(0, 0, 0),new Vector3(0, 0, 0));
        }
    }
}
