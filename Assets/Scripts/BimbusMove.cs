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
    public bool rising;

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
        if (!stats.isDead)
        {
            float speed = 0;
            speed = navAgent.velocity.magnitude / navAgent.speed;
            animator.SetFloat("speed", speed, animDelay, Time.deltaTime);
            if (transform.rotation.x > 10 || transform.rotation.x < -10)
            {
                if(!rising) StartCoroutine(GetUp());
            }
        }
    }
    IEnumerator GetUp()
    {
        var step = riseSpeed * Time.deltaTime;
        rising = true;
        yield return new WaitForSeconds(0.2f);
        Quaternion.RotateTowards(transform.rotation, transformo.rotation,step);
    }
}
