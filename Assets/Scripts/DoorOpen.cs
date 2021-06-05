using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    List<BimbuStats> bimbi = new List<BimbuStats>();
    Animator anim;
    bool doorOpen;

    private void Start()
    {
        bimbi.Add(GameObject.FindObjectOfType<BimbuStats>());
        anim = GetComponentInParent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bimbi" && !doorOpen)
        {
            DoorAnimation(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bimbi" && doorOpen)
        {
            DoorAnimation(false);
        }
    }

    void DoorAnimation(bool open)
    {
        doorOpen = open;
        if (open)
        {
            anim.SetBool("isOpen", true);
        }
        else
        {
            anim.SetBool("isOpen", false);
        }
        print("Door is doing someting");
    }
}
