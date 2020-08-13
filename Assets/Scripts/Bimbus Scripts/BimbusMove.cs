﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BimbusMove : MonoBehaviour
{
    const float animDelay = .1f;
    public NavMeshAgent navAgent;
    private Rigidbody rb;
    private Animator animator;
    private BimbuStats stats;
    public AudioSource step;
    public Transform transformo;
    private bool moving;
    public float speed = 0;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        transformo = this.transform;
        animator = GetComponent<Animator>();
        stats = GetComponent<BimbuStats>();
        navAgent = GetComponent<NavMeshAgent>();
    }
    public void MoveUnit(Vector3 dest)
    {
        if (!stats.isDead)
        {
            moving = true;
            navAgent.destination = dest;
        }
    }

    public void SetSelected(bool isSelected)
    {
        transform.Find("Highlight").gameObject.SetActive(isSelected);
    }
    private void Update()
    {
<<<<<<< HEAD
        if (speed > 0.2f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
=======
        
>>>>>>> 7a32e9a78d2c5431339cdcc3544d2ef483840b79
        if (!stats.isDead)
        {
            speed = navAgent.velocity.magnitude / navAgent.speed;
            animator.SetFloat("speed", speed, animDelay, Time.deltaTime);
        }

    }
    public void Step()
    {
<<<<<<< HEAD
        step.Play();
=======

        var step = riseSpeed * Time.deltaTime;
        rising = true;
        yield return new WaitForSeconds(0.2f);
        Quaternion.RotateTowards(transformo.rotation, transformo.rotation,step);
        rising = false;
>>>>>>> 7a32e9a78d2c5431339cdcc3544d2ef483840b79
    }
}
