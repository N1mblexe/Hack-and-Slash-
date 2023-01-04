using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField] float maxDistance = 10;
    [SerializeField] float distanceToPlayer;
    [SerializeField] float sensitivity;
    [SerializeField] float speed = 10;

    [SerializeField] GameObject camPos;
    [SerializeField] GameObject virtualCam;

    private void FixedUpdate()
    {
       camPos.transform.LookAt(transform.position);
       if (distanceToPlayer - 0.02f > Vector3.Distance(camPos.transform.position, transform.position)) camPos.transform.position -= camPos.transform.forward * Time.deltaTime * speed * (distanceToPlayer - Vector3.Distance(camPos.transform.position , transform.position));
       if (distanceToPlayer + 0.02f < Vector3.Distance(camPos.transform.position, transform.position)) camPos.transform.position += camPos.transform.forward * Time.deltaTime * speed *-(distanceToPlayer - Vector3.Distance(camPos.transform.position, transform.position));
    }

    private void Update()
    {
        LookAround();
        RayToCamera();
    }

    void LookAround()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        transform.Rotate(Input.GetAxis("Mouse Y") * sensitivity, Input.GetAxis("Mouse X") * sensitivity, 0);
        virtualCam.transform.LookAt(new Vector3(camPos.transform.position.x , virtualCam.transform.position.y , camPos.transform.position.z));
        if (Quaternion.Angle(virtualCam.transform.rotation, transform.rotation) > 50)
            transform.Rotate(Mathf.Sign(transform.position.y - camPos.transform.position.y) * (50 - Quaternion.Angle(virtualCam.transform.rotation, transform.rotation)), 0, 0);
    }

    void RayToCamera()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            if (hit.transform.gameObject.tag != "camera" || hit.transform.gameObject.tag != "MainCamera")
                distanceToPlayer = Vector3.Distance(hit.point, transform.position) - 0.3f;
        }
        else
            distanceToPlayer = maxDistance;
    }
}
