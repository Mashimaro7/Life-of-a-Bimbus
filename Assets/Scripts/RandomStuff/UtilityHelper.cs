using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityHelper
{
    public static void Create(GameObject gameObject)
    {
        GameObject.Instantiate(gameObject);
    }

    public static void RandomPosition(GameObject thisObj, Vector3 min, Vector3 max)
    {
        thisObj.transform.position = new Vector3(Random.Range(min.x,max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
    }
}
