using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    List<BimbuStats> bimbi = new List<BimbuStats>();
    public Animator anim;

    private void Start()
    {
        bimbi.Add(GameObject.FindObjectOfType<BimbuStats>());
        anim = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        foreach(var bimbus in bimbi)
        {
            if (Vector3.Distance(transform.position, bimbus.transform.position) < 10f)
            {
                anim.SetBool("isOpen", true);
            }
            else
            {
                anim.SetBool("isOpen", false);
            }
        }
    }
}
