using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private float speed = 0;
    public float normalSpeed = -15;
    private float fastSpeed;
    public bool isFast;
    public float cameraDistanceMax = 100;
    public float cameraDistanceMin = 15;
    public float cameraDistanceGoal;
    public float scrollSpeed = 5f;
    public Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
        cam.position = new Vector3(transform.position.x, cameraDistanceGoal, transform.position.z);

        fastSpeed = normalSpeed * 2;
    }

    void LateUpdate()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * speed;
        Vector3 moveGoal = velocity * Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, transform.position + moveGoal, 10 * Time.deltaTime);
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
        cameraDistanceGoal += Input.GetAxis("Mouse ScrollWheel") * (scrollSpeed * -1);
        cameraDistanceGoal = Mathf.Clamp(cameraDistanceGoal, hit.point.y + cameraDistanceMin, hit.point.y + cameraDistanceMax);
        cam.position = Vector3.Lerp(cam.position,new Vector3(cam.position.x,cameraDistanceGoal,transform.position.z),3 * Time.deltaTime);
    }
}
