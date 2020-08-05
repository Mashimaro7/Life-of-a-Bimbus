using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrow : MonoBehaviour
{
    public float sizeGoal = .1f;
    public float startSize, timeToMax = 500, maxSize,growthMultiplier = 1;
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
        if(sizeGoal <= maxSize && !growing)
        {
            StartCoroutine(PlantGrower());
        }
        if(sizeGoal >= maxSize && !fullyGrown)
        {
            fullyGrown = true;
        }
    }
    IEnumerator PlantGrower()
    {
        growing = true;
        yield return new WaitForFixedUpdate();
        sizeGoal += ((Time.deltaTime * growthMultiplier) + Random.Range(0,.000002f)) / timeToMax;
        if (growthStates.Length > 0)
        {
            GetComponent<MeshFilter>().mesh = growthStates[Mathf.CeilToInt(sizeGoal / maxSize) - 1].GetComponent<MeshFilter>().sharedMesh;
        }
        currentSize = new Vector3(sizeGoal, sizeGoal, sizeGoal) ;
        this.transform.localScale = currentSize;
        growing = false;
    }
}
