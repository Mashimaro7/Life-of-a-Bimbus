using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeStuff : MonoBehaviour
{
    const float animDelay = .1f;
    NavMeshAgent slimeNav;
    private bool attacking, wandering;
    [SerializeField]private float wanderRange = 3;
    public float timeToWait,speed;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.speed = 1;
        slimeNav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!attacking)
        {
            Wander();
        }
        speed = slimeNav.velocity.magnitude / slimeNav.speed;
        anim.SetFloat("Speed", speed, animDelay, Time.deltaTime);
    }

    void Wander()
    {
        if (!wandering)
        {
            wandering = true;
            timeToWait = Random.Range(5f, 10f);
        }
        timeToWait -= Time.deltaTime;
        if(timeToWait<= 0)
        {
            Vector3 dest;
            dest = new Vector3(Random.Range(-wanderRange + 3, wanderRange + 3), 0, Random.Range(-wanderRange + 3, wanderRange + 3));
            slimeNav.SetDestination(transform.position + dest);
            wandering = false;
        }

    }
}
