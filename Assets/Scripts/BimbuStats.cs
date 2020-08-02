using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BimbuStats : MonoBehaviour
{
    [Range(0,100)]public float health = 100;
    [Range(0, 100)] public float thirst = 100;
    [Range(0, 100)] public float hunger = 100;
    [Range(0, 100)] public float happiness = 100;
    [Range(0, 100)] private int str = 5;
    [Range(0, 100)] private int def = 5;
    [Range(-30, 200)] private float temp;
    public bool isDead, deathRoutine;
    Rigidbody rb;
    
    private Animator animator;
    public Vector3 rotationDirection;
    public float rotationSpeed,deathTimer;



    void Start()
    {
        temp = 20f;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    IEnumerator Death()
    {
      
        deathRoutine = true;
        rb.constraints = RigidbodyConstraints.None;
        animator.SetBool("isDead", true);
        Destroy(GetComponent<NavMeshAgent>());
        yield return new WaitForFixedUpdate();
        rb.AddTorque(Random.Range(-30,30), 0, Random.Range(-30, 30));
    }
    void Update()
    {
        hunger -= (Time.deltaTime /3);
        thirst -= (Time.deltaTime /2);
        if(health <= 0)
            {
            isDead = true;
            health = 0;
            }
        if(isDead && !deathRoutine)
            {
            StartCoroutine (Death());    
            }
        if(hunger <0.1f || thirst < 0.1f)
        {
            health -= Time.deltaTime;
        }
    }
}

