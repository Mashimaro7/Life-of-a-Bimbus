using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BimbusSelect : MonoBehaviour
{
    Ray camRay;
    RaycastHit hit;
    List<BimbusMove> selectedBimbi = new List<BimbusMove>();
    bool isDragging = false;
    Vector3 mousePosition;
    Vector3 mousePos1;
    Vector3 mousePos2;
    Vector3 currentDest;

    public void Update()
    {
        foreach (var selectableBimbus in selectedBimbi)
        {
            if (selectableBimbus.GetComponent<BimbuStats>().isDead)
            {
                selectableBimbus.SetSelected(false);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(camRay, out hit))
            {
                if (hit.transform.CompareTag("Bimbi"))
                {
                    SelectBimbus(hit.transform.GetComponent<BimbusMove>(), Input.GetKey(KeyCode.LeftShift));
                }
                else 
                {
                    isDragging = true;
                }

            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                DeselectBimbus();

                foreach (var selectableObject in FindObjectsOfType<BimbusMove>())
                {
                    if (IsInSelectBox(selectableObject.transform))
                    {
                        SelectBimbus(selectableObject.gameObject.GetComponent<BimbusMove>(), true);
                    }

                }
                isDragging = false;
            }

            
        }

        if (Input.GetMouseButtonDown(1) && selectedBimbi.Count > 0)
        {

                mousePosition = Input.mousePosition;
                var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(camRay, out hit))
                {
                if (hit.transform.CompareTag("Terrain"))
                {
                    foreach (var selectableObject in selectedBimbi)
                    {
                        if (!selectableObject.GetComponent <BimbuStats>().isDead)
                        {
                            Vector3 dest = hit.point;
                            selectableObject.MoveUnit(dest + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)));
                        }
                    }
                }
                else if (hit.transform.CompareTag("Enemy"))
                    {

                    }
                }
            
        }

    }


    private void SelectBimbus(BimbusMove unit, bool isMultiBimbi = false)
    {
        if (!isMultiBimbi)
        {
            DeselectBimbus();
        }
            selectedBimbi.Add(unit);
            unit.SetSelected(true);
    }
    private void DeselectBimbus()
    {
       
        for(int i = 0; i < selectedBimbi.Count; i++)
        {
            selectedBimbi[i].SetSelected(false);
        }
        selectedBimbi.Clear();
    }
    private bool IsInSelectBox(Transform transform)
    {
        if(!isDragging)
        {
            return false;
        }
        var camera = Camera.main;
        var viewportBounds = ScreenHelper.GetViewportBounds(camera, mousePosition, Input.mousePosition);
        return viewportBounds.Contains(camera.WorldToViewportPoint(transform.position));
        
    }

}
