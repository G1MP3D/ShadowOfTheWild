﻿using UnityEngine;
using System.Collections;

public class PlayerMovement_Input : MonoBehaviour 
{
    public string horizontalKey = "Horizontal";
    public string verticalKey = "Vertical";

    float horizontalAxis;
    float verticalAxis;

    bool grounded;

    PlayerMovement_Motor _motor;
    private PlayerMovement_Motor Motor
    {
        get
        {
            if(_motor == null)
            {
                _motor = GetComponent<PlayerMovement_Motor>();
            }
            return _motor;
        }
    }
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationX = 0F;
    float rotationY = 0F;
    public GameObject camMain;
    public bool camRotateCheck;
    //public bool sitting;

    Quaternion originalRotation;
    Quaternion originalCamRotation;
	// Use this for initialization
	void Start () 
    {
        camMain = GameObject.Find("Main Camera");
        Debug.Log("Camera:" + camMain);
        originalRotation = transform.localRotation;
        originalCamRotation = camMain.transform.localRotation;
        camRotateCheck = false;
        grounded = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
        horizontalAxis = Input.GetAxis(horizontalKey);
        verticalAxis = Input.GetAxis(verticalKey);
        

        //Vector3 moveDirection = new Vector3(horizontalAxis, 0, verticalAxis);
        //moveDirection.y = 0f;
        
        if(horizontalAxis < 0 && grounded)
        {
            Motor.Move(transform.right *-1,Motor.playerWalkSpeed);
            Motor.anim.SetBool("isWalking", true);
        }
        if (horizontalAxis > 0 && grounded)
        {
            Motor.Move(transform.right, Motor.playerWalkSpeed);
            Motor.anim.SetBool("isWalking", true);
        }
        if (verticalAxis < 0 && grounded)
        {
            Motor.Move(transform.forward *-1, Motor.playerWalkSpeed);
            Motor.anim.SetBool("isWalking", true);
        }
        if (verticalAxis > 0 && grounded)
        {
            Motor.Move(transform.forward, Motor.playerWalkSpeed);
            Motor.anim.SetBool("isWalking", true);
        }
        else if(verticalAxis == 0 && horizontalAxis == 0)
        {
            Motor.anim.SetBool("isWalking", false);
        }
        if(Input.GetKey(KeyCode.Space) && grounded)
        {
            Motor.Jump(Vector3.up, Motor.jumpheight);
            grounded = false;
        }
        if(!Motor.jumped)
        {
            grounded = true;
        }
       

        

        

        #region Rotation
        if (axes == RotationAxes.MouseXAndY)
        {
            // Read the mouse input axis
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

            transform.localRotation = originalRotation * xQuaternion;
            camMain.transform.localRotation = originalCamRotation * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            camMain.transform.localRotation = originalCamRotation * yQuaternion;
        }
        #endregion
	}
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
        {
            angle += 360F;
        }
        if (angle > 360F)
        {
            angle -= 360F;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
