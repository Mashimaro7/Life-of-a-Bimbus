using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeStuff : MonoBehaviour
{
    const float animDelay = .1f;
    NavMeshAgent slimeNav;
    private bool attacking, wandering;
    [SerializeField]private float wanderRange;
    public float timeToWait,speed;
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
        slimeNav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!attacking)
        Wander();
        speed = slimeNav.velocity.magnitude / slimeNav.speed;
        anim.SetFloat("Speed", speed, animDelay, Time.deltaTime);
    }

    void Wander()
    {
        if (!wandering)
        {
            wandering = true;
            timeToWait = Random.Range(20, 30);
        }
        timeToWait -= Time.deltaTime;
        if(timeToWait<= 0)
        {
            slimeNav.destination = new Vector3(Random.Range(-wanderRange, wanderRange), 0, Random.Range(-wanderRange, wanderRange));
            wandering = false;
        }

    }
}
