using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject virtualCam , bodyToLook;

    [SerializeField] int rotateSmoother = 8;

    const float speedMultipler = 5;
    const float jumpSpeed = 7;

    public bool isWalking;
    #region Jump
    public bool isGrounded;

    public bool canJump;
    public bool doubleJump = true;
    #endregion
    Rigidbody rb;
    #endregion
    private void Start()
    {   
        rb = GetComponent<Rigidbody>();
    }

    private void Rotate()
    {
        Vector3 lTargetDir = bodyToLook.transform.position - transform.position;
        lTargetDir.y = 0;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), rotateSmoother);
    }

    void SetBodyToLookObjectPos()
    {
        bodyToLook.transform.position = virtualCam.transform.position-(Input.GetAxis("Vertical") * virtualCam.transform.forward + Input.GetAxis("Horizontal") * virtualCam.transform.right);
    }

    void Move(float speed)
    {
        float walkSpeed = speed * (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Vertical")) ? Mathf.Sign(Input.GetAxis("Horizontal")) * Input.GetAxis("Horizontal") : Mathf.Sign(Input.GetAxis("Vertical")) * Input.GetAxis("Vertical"));
        rb.velocity = transform.forward * walkSpeed + new Vector3(0, rb.velocity.y, 0);
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpSpeed , rb.velocity.z);
    }
    void JumpManager()
    {
        if(isGrounded)
            canJump = doubleJump = true;
        if (Input.GetKeyDown(KeyCode.Space) && (canJump || doubleJump))
        {
            if (doubleJump)
                doubleJump = false;
            else
                canJump = false;
            Jump();
        }
    }
    private void Update()
    {
        JumpManager();
        SetBodyToLookObjectPos();
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Rotate();
            Move(speedMultipler);
        }
    }

}
