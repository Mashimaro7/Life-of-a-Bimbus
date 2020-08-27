using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BimbusSelect : MonoBehaviour
{
    Ray camRay;
    RaycastHit hit;
    public List<BimbusMove> selectedBimbi = new List<BimbusMove>();
    bool isDragging = false;
    Vector3 mousePosition,mousePos1, mousePos2, currentDest;

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
                            
                            Vector3 dest = hit.point +new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f));
                            selectableObject.MoveUnit(dest);
                        }
                    }
                }

                else if (hit.transform.CompareTag("Enemy"))
                    {

                    }

                else if (hit.transform.CompareTag("Plant"))
                {
                    foreach(var selectable in selectedBimbi)
                    {
                        Vector3 dest = hit.point + new Vector3(0 + Random.Range(-1, 1), 0, 0 + Random.Range(-1, 1));
                        selectable.MoveUnit(dest);
                    }
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
