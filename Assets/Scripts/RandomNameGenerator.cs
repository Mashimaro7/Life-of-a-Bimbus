using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNameGenerator
{
    [SerializeField] static List<string> names = new List<string>() {"Andy","Chris","James","Daniel","John","Yamcha","Gohan","Goku","Jeff","Jesse","George","Vegeta" };

    public static string GetRandomName()
    {
        int random = Random.Range(0, names.Count-1); ;
        string name = names[random];

        return name;
    }

}
