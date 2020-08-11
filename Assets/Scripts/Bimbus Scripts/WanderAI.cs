﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderAI : MonoBehaviour
{

    private bool isWandering;
    private bool isWalking;
    [SerializeField] private float wanderRange = 6, minWanderRange = 2;
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
            Vector3 dest = new Vector3(Random.Range(-wanderRange, wanderRange),0,Random.Range(-wanderRange,wanderRange));
            if (Vector3.Distance(transform.position, dest) > minWanderRange)
            {
                move.MoveUnit(transform.position + dest);
            }
            else
            {
                dest = new Vector3(Random.Range(-wanderRange, wanderRange), 0, Random.Range(-wanderRange, wanderRange));
            }
            isWalking = false;
        }
    }

    IEnumerator Wander()
    {
        if(!bimbus.isDead)
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
