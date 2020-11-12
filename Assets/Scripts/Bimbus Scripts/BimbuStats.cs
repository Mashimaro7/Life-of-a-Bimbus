﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BimbuStats : MonoBehaviour
{
    [Range(0,100)]public float health = 100;
    [Range(0, 100)] public float thirst = 100;
    [Range(0, 100)] public float hunger = 100;
    [Range(0, 100)] public float happiness = 100;
    [Range(0, 15)] public int age, maxAge;
    public int birthDay;
    [Range(0, 100)] private int str = 5;
    [Range(0, 100)] private int def = 5;
    [Range(-30, 200)] private float temp;
    public bool isDead, deathRoutine;
    public string bimbusName;
    Rigidbody rb;
    
    private Animator animator;
    public Vector3 rotationDirection;
    public float rotationSpeed,deathTimer;
    private TimeOfDay day;


    private void Awake()
    {
        day = FindObjectOfType<TimeOfDay>();
        maxAge = 10 + Random.Range(0, 5);
        birthDay = day.calendarDay;
    }
    void Start()
    {
        bimbusName = RandomNameGenerator.GetRandomName();
        temp = 20f;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    IEnumerator Death()
    {
        deathRoutine = true;
        rb.constraints = RigidbodyConstraints.None;
        animator.SetBool("isDead", true);
        //Destroy(GetComponent<NavMeshAgent>());
        yield return new WaitForFixedUpdate();
        rb.AddTorque(Random.Range(-30,30), 0, Random.Range(-30, 30));
    }

    public void ReceiveDamage(float dmg)
    {
        dmg -= ((float)def / 3);
        if (dmg < 0)
        {
            dmg = 0;
            return;
        }
        if (health > dmg)
        {
            health -= dmg;
        }
        else
        {
            isDead = true;
            health = 0;
        }
    }

    void Update()
    {
        if (age >= maxAge) isDead = true;
        hunger -= (Time.deltaTime  + Random.Range(0f, 0.005f)) / 20;
        thirst -= (Time.deltaTime  + Random.Range(0f, 0.005f)) / 30;

        if(isDead && !deathRoutine)
            {
            StartCoroutine (Death());    
            }
        if(hunger <0.1f || thirst < 0.1f)
        {
            ReceiveDamage(Time.deltaTime);
        }
        if (temp < -30f)
        {
            ReceiveDamage(Time.deltaTime / 3f);
        }

    }
    
}

