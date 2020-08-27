using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum STATES
    {
    WANDERING = 0,
    IDLE = 1,
    RUNNING = 2,
    FORAGING = 3,
    ATTACKING = 4,
    SLEEPING = 5,
}
public class WanderAI : MonoBehaviour
{
    private STATES _state;
    private bool _isWandering;
    private bool _isWalking;
    [SerializeField] private float wanderRange = 6, minWanderRange = 2;
    BimbuStats bimbus;
    public Rigidbody rb;
    public NavMeshAgent nav;
    BimbusMove move;

    public STATES state
    {
        get { return state; }
        set
        {
            StopAllCoroutines();

            _state = value;

            switch(_state)
            {
                case STATES.WANDERING:
                    StartCoroutine (Wander());
                    break;
            }
        }
    }

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
        if (_isWalking && !bimbus.isDead)
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
            _isWalking = false;
        }
    }

    IEnumerator Wander()
    {
        if(!bimbus.isDead)
        {



            float walkWait = Random.Range(0.1f, 4f);
            float walkTime = Random.Range(1f, 4f);

            _isWandering = true;

            yield return new WaitForSeconds(walkWait);
            _isWalking = true;
            yield return new WaitForSeconds(walkTime);
            _isWalking = false;

            _isWandering = false;
        }

    }
}
