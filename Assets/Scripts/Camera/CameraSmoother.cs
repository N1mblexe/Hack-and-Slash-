using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoother : MonoBehaviour
{
    #region Bool
    [SerializeField] bool isFollowing;
    [SerializeField] bool isLooking;
    #endregion

    #region Objects
    [SerializeField] GameObject objectToFollow;
    [SerializeField] GameObject objectToLook;

    [SerializeField] GameObject virtualCam;
    #endregion

    #region Smoothening
    [SerializeField] float followSmoothening; 
    [SerializeField] float lookSmoothening;

    double smoother = 0;
    #endregion


    private void Update()
    {
        smoother += smoother < 1 ? 0.00008: -smoother;
        if (isLooking) LookGameObject();
        if (isFollowing) FollowGameObject();
        virtualCam.transform.LookAt(objectToLook.transform); //Virtual camera is looking game obj real time
    }

    void FollowGameObject()
    {
        transform.position = Vector3.Lerp(transform.position, objectToFollow.transform.position, (float)smoother * lookSmoothening);
    }

    void LookGameObject()
    {
        Vector3 lTargetDir =objectToLook.transform.position - transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir),Quaternion.Angle(transform.rotation , virtualCam.transform.rotation) / followSmoothening); 
    }
}
