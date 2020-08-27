using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrow : MonoBehaviour
{
    public float sizeGoal = .1f;
    public float startSize = .1f, timeToMax = 500, maxSize = 1,growthMultiplier = 1;
    public Vector3 currentSize;
    public GameObject[] growthStates;
    private bool growing;
    public bool fullyGrown;

    void Start()
    {
        sizeGoal = startSize;
        currentSize = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        sizeGoal = Mathf.Clamp(sizeGoal,0,maxSize);
        if(sizeGoal <= maxSize && !fullyGrown)
        {
            PlantGrower();
        }
        if(sizeGoal >= maxSize && !fullyGrown)
        {
            fullyGrown = true;
        }
    }
     void PlantGrower()
    {

        sizeGoal += ((Time.deltaTime * growthMultiplier * maxSize) + Random.Range(0,.0000005f)) / (timeToMax);
        if (growthStates.Length > 0)
        {
            GetComponent<MeshFilter>().mesh = growthStates[Mathf.CeilToInt(sizeGoal / maxSize) - 1].GetComponent<MeshFilter>().sharedMesh;
        }
        currentSize = new Vector3(sizeGoal, sizeGoal, sizeGoal) ;
        this.transform.localScale = currentSize;
    }
}
