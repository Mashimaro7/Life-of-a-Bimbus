using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBox : MonoBehaviour
{
    [SerializeField]
    private RectTransform selectSquareImg;

    Vector3 startPos;
    Vector3 endPos;
    void Awake()
    {
        selectSquareImg.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                startPos = hit.point;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            selectSquareImg.gameObject.SetActive(false);

        }
        if (Input.GetMouseButton(0))
        {
            if (!selectSquareImg.gameObject.activeInHierarchy)
            {
                selectSquareImg.gameObject.SetActive(true);
            }

            endPos = Input.mousePosition;

            Vector3 squareStart = Camera.main.WorldToScreenPoint(startPos);
            squareStart.z = 0f;

            Vector3 centre = (squareStart + endPos) / 2f;

            selectSquareImg.position = centre;

            float sizeX = Mathf.Abs(squareStart.x - endPos.x);
            float sizeY = Mathf.Abs(squareStart.y - endPos.y);

            selectSquareImg.sizeDelta = new Vector2(sizeX, sizeY);
        }
    }
}
