using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BimbusMove : MonoBehaviour
{
    const float animDelay = .1f;
    public NavMeshAgent navAgent;
    private Rigidbody rb;
    private Animator anim;
    private BimbuStats stats;
    public float riseSpeed;
    public Transform transformo;
    private bool rising;
    public float speed = 0;

    public void Start()
    {
        transformo = this.transform;
        anim = GetComponent<Animator>();
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
            speed = navAgent.velocity.magnitude / navAgent.speed;
            anim.SetFloat("speed", speed, animDelay, Time.deltaTime);
        }
    }
    IEnumerator GetUp()
    {

        var step = riseSpeed * Time.deltaTime;
        rising = true;
        yield return new WaitForSeconds(0.2f);
        Quaternion.RotateTowards(transformo.rotation, transformo.rotation,step);
        rising = false;
    }
}
