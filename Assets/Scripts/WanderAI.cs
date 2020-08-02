using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderAI : MonoBehaviour
{

    private bool isWandering;
    private bool isWalking;
    BimbuStats bimbus;
    public Rigidbody rb;
    public NavMeshAgent nav;
    BimbusMove move;

    private void Start()
    {
        move = GetComponent<BimbusMove>();
        nav = this.GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        bimbus = FindObjectOfType<BimbuStats>();
    }
    void Update()
    {
        if (bimbus.isDead)
        {
            StopAllCoroutines();
        }
        if (!isWandering)
        {
            StartCoroutine(Wander());
        }
        if (isWalking && !bimbus.isDead)
        {
            move.MoveUnit(transform.position + new Vector3(Random.Range(-6, 6), 0, Random.Range(-6,6)));
            isWalking = false;
        }
    }

    IEnumerator Wander()
    {
        if(bimbus.isDead == false)
        {



            float walkWait = Random.Range(0.1f, 4f);
            float walkTime = Random.Range(1f, 4f);

            isWandering = true;

            yield return new WaitForSeconds(walkWait);
            isWalking = true;
            yield return new WaitForSeconds(walkTime);
            isWalking = false;

            isWandering = false;
        }
        else
        {
            rb.angularDrag = 1;
        }

    }
}
