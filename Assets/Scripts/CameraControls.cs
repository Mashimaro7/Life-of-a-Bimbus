using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float speed, normalSpeed,fastSpeed;
    public bool isFast;
    public float cameraDistanceMax;
    public float cameraDistanceMin;
    public float cameraDistance;
    public float scrollSpeed = 1f;
    void Start()
    {
        fastSpeed = normalSpeed * 2;
    }

    void LateUpdate()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down * 100, Color.blue);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * speed;
        Vector3 moveAmount = velocity * Time.deltaTime;

        transform.position += moveAmount;
        if (Input.GetKey(KeyCode.LeftShift)) {
            isFast = true;
        }
        
        else
        {
            isFast = false;
        }
        if(isFast == true)
        {
            speed = fastSpeed;
        }
        else
        {
            speed = normalSpeed;
        }
        cameraDistance += Input.GetAxis("Mouse ScrollWheel") * (scrollSpeed * -1);
        cameraDistance = Mathf.Clamp(cameraDistance, hit.point.y + cameraDistanceMin, hit.point.y + cameraDistanceMax);
        transform.position = new Vector3(transform.position.x, cameraDistance, transform.position.z);
    }
}
