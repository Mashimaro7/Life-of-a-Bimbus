using System.Collections;
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
    public float riseSpeed;
    public Transform transformo;
    private bool rising;
    public float speed = 0;

    public void Start()
    {
        transformo = this.transform;
        animator = GetComponent<Animator>();
        stats = GetComponent<BimbuStats>();
        navAgent = GetComponent<NavMeshAgent>();
    }
    public void MoveUnit(Vector3 dest)
    {
        if (!stats.isDead)
        {
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
=======
        
>>>>>>> parent of 99ad2c4... 0.13
        if (!stats.isDead)
        {
            speed = navAgent.velocity.magnitude / navAgent.speed;
            animator.SetFloat("speed", speed, animDelay, Time.deltaTime);
        }
    }
    IEnumerator GetUp()
    {
<<<<<<< HEAD
<<<<<<< HEAD
        step.Play();
=======
=======
>>>>>>> parent of 99ad2c4... 0.13

        var step = riseSpeed * Time.deltaTime;
        rising = true;
        yield return new WaitForSeconds(0.2f);
        Quaternion.RotateTowards(transformo.rotation, transformo.rotation,step);
        rising = false;
<<<<<<< HEAD
>>>>>>> 7a32e9a78d2c5431339cdcc3544d2ef483840b79
=======
>>>>>>> parent of 99ad2c4... 0.13
    }
}
