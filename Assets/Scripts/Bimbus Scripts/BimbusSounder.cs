using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BimbusSounder : MonoBehaviour
{
    public int randomSound;
    public float randomRange, randomRangeEnd;
    public AudioSource[] chirpy;
    public BimbuStats bimbus;
    void Start()
    {
        bimbus = this.GetComponent<BimbuStats>();
        StartCoroutine(Sounder());  
    }

    IEnumerator Sounder()
    {
        if (!bimbus.isDead)
        {
            this.randomSound = Random.Range(0, chirpy.Length);
            yield return new WaitForSeconds(Random.Range(randomRange, randomRangeEnd));
            chirpy[randomSound].Play();
            if(!bimbus.isDead) StartCoroutine(Sounder());
        }
    }
}
